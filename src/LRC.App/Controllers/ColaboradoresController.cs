using AutoMapper;
using LRC.App.ViewModels;
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Entidades;
using LRC.Business.Interfaces.Servicos;
using LRC.Business.Interfaces;
using LRC.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LRC.App.Controllers
{
    public class ColaboradoresController : BaseController
    {
        private readonly IColaboradorService _colaboradorService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MeuDbContext _context;
        private readonly ILogAlteracaoService _logAlteracaoService;

        public ColaboradoresController(IMapper mapper,
                                  IColaboradorService colaboradorService,
                                  UserManager<IdentityUser> userManager,
                                  MeuDbContext context,
                                  ILogAlteracaoService logAlteracaoService,
                                  INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _colaboradorService = colaboradorService;
            _userManager = userManager;
            _context = context;
            _logAlteracaoService = logAlteracaoService;
        }


        [Route("lista-de-colaboradores")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ColaboradorVM>>(await _colaboradorService.ObterTodos()));
        }

        [Route("editar-colaborador/{id}")]
        public async Task<IActionResult> Editar(Guid Id)
        {
            var colaboradorVM = new ColaboradorVM();
            if (Id != Guid.Empty)
            {
                var colaborador = await _colaboradorService.ObterPorId(Id);
                if (colaborador == null) return NotFound();

                colaboradorVM = _mapper.Map<ColaboradorVM>(colaborador);
                colaboradorVM.UsuarioCadastro = await _userManager.FindByIdAsync(colaboradorVM.UsuarioCadastroId.ToString());
                colaboradorVM.UsuarioAlteracao = await _userManager.FindByIdAsync(colaboradorVM.UsuarioAlteracaoId.ToString());
            }

            return View(colaboradorVM);
        }

        [Route("editar-colaborador/{id:guid}")]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Editar(Guid Id, ColaboradorVM colaboradorVM)
        {
            if (Id != colaboradorVM.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors, isModelState = true });
            }

            IdentityUser? user = await _userManager.GetUserAsync(User);
            Colaborador colaborador;

            if (user != null)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (Id != Guid.Empty)
                    {
                        var colaboradorClone = await _colaboradorService.ObterPorId(Id);
                        colaboradorVM.DataAlteracao = DateTime.Now;
                        colaborador = _mapper.Map<Colaborador>(colaboradorVM);
                        colaborador.UsuarioAlteracaoId = Guid.Parse(user.Id);
                        await _logAlteracaoService.CompararAlteracoes(colaboradorClone, colaborador, Guid.Parse(user.Id), $"Colaborador[{colaborador.Id}]");
                        await _colaboradorService.Atualizar(colaborador);
                    }
                    else
                    {
                        colaborador = _mapper.Map<Colaborador>(colaboradorVM);
                        colaborador.UsuarioCadastroId = Guid.Parse(user.Id);
                        await _colaboradorService.Adicionar(colaborador);
                    }

                    if (!OperacaoValida())
                    {
                        await transaction.RollbackAsync();
                        List<string> errors = new List<string>();
                        errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem).ToList();
                        errors.Add(ObterNotificacoes.ExecutarValidacao(new ColaboradorValidation(), colaborador));
                        return Json(new { success = false, errors });
                    }
                    await transaction.CommitAsync();
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Json(new { success = false, errors = ex.Message });
                }
            }
            return View(colaboradorVM);
        }

        [HttpPost]
        [Route("excluir-colaborador/{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var colaborador = await _colaboradorService.ObterPorId(id);
            if (colaborador == null) return NotFound();
            IdentityUser? user = await _userManager.GetUserAsync(User);

            if (user == null) return NotFound();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _logAlteracaoService.RegistrarLogDiretamente($"Registro: {colaborador.RazaoSocial} excluído.", Guid.Parse(user.Id), $"Colaborador[{colaborador.Id}]");
                await _colaboradorService.Remover(id);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }

            if (!OperacaoValida())
            {
                await transaction.RollbackAsync();
                var errors = ObterNotificacoes.ExecutarValidacao(new ColaboradorValidation(), colaborador);
                return Json(new { success = false, errors });
            }

            return RedirectToAction("Index");
        }
    }
}

