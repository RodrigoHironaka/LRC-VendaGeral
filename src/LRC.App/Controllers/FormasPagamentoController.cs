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
using Microsoft.AspNetCore.Authorization;

namespace LRC.App.Controllers
{
    [Authorize]
    public class FormasPagamentoController : BaseController
    {
        private readonly IFormaPagamentoService _formaPagamentoService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MeuDbContext _context;
        private readonly ILogAlteracaoService _logAlteracaoService;

        public FormasPagamentoController(IMapper mapper,
                                  IFormaPagamentoService formaPagamentoService,
                                  UserManager<IdentityUser> userManager,
                                  MeuDbContext context,
                                  ILogAlteracaoService logAlteracaoService,
                                  INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _formaPagamentoService = formaPagamentoService;
            _userManager = userManager;
            _context = context;
            _logAlteracaoService = logAlteracaoService;
        }

        [Route("lista-de-formaspagamento")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<FormaPagamentoVM>>(await _formaPagamentoService.ObterTodos()));
        }

        [Route("editar-formapagamento/{id}")]
        public async Task<IActionResult> Editar(Guid Id)
        {
            var formaPagamentoVM = new FormaPagamentoVM();
            if (Id != Guid.Empty)
            {
                var formaPagamento = await _formaPagamentoService.ObterPorId(Id);
                if (formaPagamento == null) return NotFound();

                formaPagamentoVM = _mapper.Map<FormaPagamentoVM>(formaPagamento);
                formaPagamentoVM.UsuarioCadastro = await _userManager.FindByIdAsync(formaPagamentoVM.UsuarioCadastroId.ToString());
                formaPagamentoVM.UsuarioAlteracao = await _userManager.FindByIdAsync(formaPagamentoVM.UsuarioAlteracaoId.ToString());
            }

            return View(formaPagamentoVM);
        }

        [Route("editar-formapagamento/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Editar(Guid Id, FormaPagamentoVM formaPagamentoVM)
        {
            if (Id != formaPagamentoVM.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToList());
                return Json(new { success = false, errors, isModelState = true });
            }

            IdentityUser? user = await _userManager.GetUserAsync(User);
            FormaPagamento formaPagamento;

            if (user != null)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (Id != Guid.Empty)
                    {
                        var formaPagamentoClone = await _formaPagamentoService.ObterPorId(formaPagamentoVM.Id);
                        formaPagamentoVM.DataAlteracao = DateTime.Now;
                        formaPagamento = _mapper.Map<FormaPagamento>(formaPagamentoVM);
                        formaPagamento.UsuarioAlteracaoId = Guid.Parse(user.Id);

                        await _logAlteracaoService.CompararAlteracoes(formaPagamentoClone, formaPagamento, Guid.Parse(user.Id), $"FormaPagamento[{formaPagamento.Id}]");
                        await _formaPagamentoService.Atualizar(formaPagamento);

                    }
                    else
                    {
                        formaPagamento = _mapper.Map<FormaPagamento>(formaPagamentoVM);
                        formaPagamento.UsuarioCadastroId = Guid.Parse(user.Id);
                        await _formaPagamentoService.Adicionar(formaPagamento);
                    }

                    if (!OperacaoValida())
                    {
                        await transaction.RollbackAsync();
                        List<string> errors = new List<string>();
                        errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem).ToList();
                        errors.Add(ObterNotificacoes.ExecutarValidacao(new FormaPagamentoValidation(), formaPagamento));
                        return Json(new { success = false, errors });
                    }
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Json(new { success = false, errors = ex.Message });
                }

                return Json(new { success = true });
            }
            return View(formaPagamentoVM);
        }

        [HttpPost]
        [Route("excluir-formapagamento/{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var formaPagamento = await _formaPagamentoService.ObterPorId(id);
            IdentityUser? user = await _userManager.GetUserAsync(User);
            if (formaPagamento == null) return NotFound();
            if (user == null) return NotFound();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _logAlteracaoService.RegistrarLogDiretamente($"Registro: {formaPagamento.Nome} excluído.", Guid.Parse(user.Id), $"FormaPagamento[{formaPagamento.Id}]");
                await _formaPagamentoService.Remover(id);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }

            if (!OperacaoValida()) return View(formaPagamento);

            return RedirectToAction("Index");
        }
    }
}
