using AutoMapper;
using LRC.Business.Interfaces.Servicos;
using LRC.Business.Interfaces;
using LRC.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LRC.App.ViewModels;
using LRC.Business.Servicos;
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Entidades;

namespace LRC.App.Controllers
{
    public class EntregadoresController : BaseController
    {
        private readonly IEntregadorService _entregadorService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MeuDbContext _context;
        private readonly ILogAlteracaoService _logAlteracaoService;

        public EntregadoresController(IMapper mapper,
                                  IEntregadorService entregadorService,
                                  UserManager<IdentityUser> userManager,
                                  MeuDbContext context,
                                  ILogAlteracaoService logAlteracaoService,
                                  INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _entregadorService = entregadorService;
            _userManager = userManager;
            _context = context;
            _logAlteracaoService = logAlteracaoService;
        }

        [Route("lista-de-entregadores")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<EntregadorVM>>(await _entregadorService.ObterTodos()));
        }

        [Route("editar-entregador/{id}")]
        public async Task<IActionResult> Editar(Guid Id)
        {
            var entregadorVM = new EntregadorVM();
            if (Id != Guid.Empty)
            {
                var entregador = await _entregadorService.ObterPorId(Id);
                if (entregador == null) return NotFound();

                entregadorVM = _mapper.Map<EntregadorVM>(entregador);
                entregadorVM.UsuarioCadastro = await _userManager.FindByIdAsync(entregadorVM.UsuarioCadastroId.ToString());
                entregadorVM.UsuarioAlteracao = await _userManager.FindByIdAsync(entregadorVM.UsuarioAlteracaoId.ToString());
            }

            return View(entregadorVM);
        }

        [Route("editar-entregador/{id:guid}")]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Editar(Guid Id, EntregadorVM entregadorVM)
        {
            if (Id != entregadorVM.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors, isModelState = true });
            }

            IdentityUser? user = await _userManager.GetUserAsync(User);
            Entregador entregador;

            if (user != null)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (Id != Guid.Empty)
                    {
                        var entregadorClone = await _entregadorService.ObterPorId(Id);
                        entregadorVM.DataAlteracao = DateTime.Now;
                        entregador = _mapper.Map<Entregador>(entregadorVM);
                        entregador.UsuarioAlteracaoId = Guid.Parse(user.Id);
                        await _logAlteracaoService.CompararAlteracoes(entregadorClone, entregador, Guid.Parse(user.Id), $"Entregador[{entregador.Id}]");
                        await _entregadorService.Atualizar(entregador);
                    }
                    else
                    {
                        entregador = _mapper.Map<Entregador>(entregadorVM);
                        entregador.UsuarioCadastroId = Guid.Parse(user.Id);
                        await _entregadorService.Adicionar(entregador);
                    }

                    if (!OperacaoValida())
                    {
                        await transaction.RollbackAsync();
                        List<string> errors = new List<string>();
                        errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem).ToList();
                        errors.Add(ObterNotificacoes.ExecutarValidacao(new EntregadorValidation(), entregador));
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
            return View(entregadorVM);
        }

        [HttpPost]
        [Route("excluir-entregador/{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var entregador = await _entregadorService.ObterPorId(id);
            if (entregador == null) return NotFound();
            IdentityUser? user = await _userManager.GetUserAsync(User);

            if (user == null) return NotFound();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _logAlteracaoService.RegistrarLogDiretamente($"Registro: {entregador.RazaoSocial} excluído.", Guid.Parse(user.Id), $"Entregador[{entregador.Id}]");
                await _entregadorService.Remover(id);
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
                var errors = ObterNotificacoes.ExecutarValidacao(new EntregadorValidation(), entregador);
                return Json(new { success = false, errors });
            }

            return RedirectToAction("Index");
        }
    }
}
