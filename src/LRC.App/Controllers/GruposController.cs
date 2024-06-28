using AutoMapper;
using LRC.App.ViewModels;
using LRC.Business.Entidades;
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using LRC.Business.Notificacoes;
using LRC.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LRC.App.Controllers
{
    [Authorize]
    public class GruposController : BaseController
    {
        private readonly IGrupoService _grupoService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MeuDbContext _context;
        private readonly ILogAlteracaoService _logAlteracaoService;

        public GruposController(IMapper mapper,
                                  IGrupoService grupoService,
                                  UserManager<IdentityUser> userManager,
                                  MeuDbContext context,
                                  ILogAlteracaoService logAlteracaoService,
                                  INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _grupoService = grupoService;
            _userManager = userManager;
            _context = context;
            _logAlteracaoService = logAlteracaoService;
        }

        [Route("lista-de-grupos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<GrupoVM>>(await _grupoService.ObterTodos()));
        }

        [Route("editar-grupo/{id}")]
        public async Task<IActionResult> Editar(Guid Id)
        {
            var grupoVM = new GrupoVM();
            if (Id != Guid.Empty)
            {
                var grupo = await _grupoService.ObterPorId(Id);
                if (grupo == null) return NotFound();

                grupoVM = _mapper.Map<GrupoVM>(grupo);
                grupoVM.UsuarioCadastro = await _userManager.FindByIdAsync(grupoVM.UsuarioCadastroId.ToString());
                grupoVM.UsuarioAlteracao = await _userManager.FindByIdAsync(grupoVM.UsuarioAlteracaoId.ToString());
            }

            return View(grupoVM);
        }

        [Route("editar-grupo/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Editar(Guid Id, GrupoVM grupoVM)
        {
            if (Id != grupoVM.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList());
                return Json(new { success = false, errors, isModelState = true });
            }

            IdentityUser? user = await _userManager.GetUserAsync(User);
            Grupo grupo;

            if (user != null)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (Id != Guid.Empty)
                    {
                        var grupoClone = await _grupoService.ObterPorId(grupoVM.Id);
                        grupoVM.DataAlteracao = DateTime.Now;
                        grupo = _mapper.Map<Grupo>(grupoVM);
                        grupo.UsuarioAlteracaoId = Guid.Parse(user.Id);

                        await _logAlteracaoService.CompararAlteracoes(grupoClone, grupo, Guid.Parse(user.Id), $"Grupo[{grupo.Id}]");
                        await _grupoService.Atualizar(grupo);
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        grupo = _mapper.Map<Grupo>(grupoVM);
                        grupo.UsuarioCadastroId = Guid.Parse(user.Id);
                        await _grupoService.Adicionar(grupo);
                    }

                    if (!OperacaoValida())
                    {
                        await transaction.RollbackAsync();
                        List<string> errors = new List<string>();
                        errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem).ToList();
                        errors.Add(ObterNotificacoes.ExecutarValidacao(new GrupoValidation(), grupo));
                        return Json(new { success = false, errors });
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Json(new { success = false, errors = ex.Message });
                }
                
                return Json(new { success = true });
            }
            return View(grupoVM);
        }

        [HttpPost]
        [Route("excluir-grupo/{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var grupo = await _grupoService.ObterPorId(id);
            IdentityUser? user = await _userManager.GetUserAsync(User);
            if (grupo == null) return NotFound();
            if (user == null) return NotFound();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _logAlteracaoService.RegistrarLogDiretamente($"Registro: {grupo.Nome} excluído.", Guid.Parse(user.Id), $"Grupo[{grupo.Id}]");
                await _grupoService.Remover(id);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }

            if (!OperacaoValida()) return View(grupo);

            return RedirectToAction("Index");
        }
    }
}
