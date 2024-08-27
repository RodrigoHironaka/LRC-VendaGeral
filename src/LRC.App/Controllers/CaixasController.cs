using AutoMapper;
using LRC.App.ViewModels;
using LRC.Business.Entidades;
using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using LRC.Business.Servicos;
using LRC.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LRC.App.Controllers
{
    [Authorize]
    public class CaixasController : BaseController
    {
        private readonly ICaixaRepository _caixaRepository;
        private readonly IFormaPagamentoService _formaPagamentoService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MeuDbContext _context;
        private readonly ILogAlteracaoService _logAlteracaoService;

        public CaixasController(IMapper mapper,
                                  ICaixaRepository caixaRepository,
                                  IFormaPagamentoService formaPagamentoService,
                                  UserManager<IdentityUser> userManager,
                                  MeuDbContext context,
                                  ILogAlteracaoService logAlteracaoService,
                                  INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _caixaRepository = caixaRepository;
            _formaPagamentoService = formaPagamentoService;
            _userManager = userManager;
            _context = context;
            _logAlteracaoService = logAlteracaoService;
        }

        [Route("lista-de-caixas")]
        public async Task<IActionResult> Index(Guid Id)
        {
            Caixa? caixa = new Caixa();
            CaixaVM caixaVM = new CaixaVM();

            var usuLogado = await _userManager.GetUserAsync(User);

            if (usuLogado != null)
            {
                if(Id != Guid.Empty)
                    caixa = await _caixaRepository.ObterPorIdComFluxosDeCaixa(Id);
                else
                    caixa = await _caixaRepository.ObterCaixaAberto(usuLogado != null ? usuLogado.Id : string.Empty);

                if (caixa != null)
                {
                    foreach (var item in caixa.FluxosCaixa)
                    {
                        if (item.FormaPagamento == null)
                            item.FormaPagamento = await _formaPagamentoService.ObterPorId(item.FormaPagamentoId);
                    }
                    caixaVM = _mapper.Map<CaixaVM>(caixa);
                }  
            }
            else
                return NotFound();

            return View(caixaVM);
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> PesquisaCaixas()
        {
            try
            {
                var caixasVM = _mapper.Map<List<CaixaVM>>(await _caixaRepository.ObterTodos());
                return PartialView("_Pesquisa", caixasVM);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = ex.Message });
            }

        }

        [HttpPost]
        [Route("abrir-caixa")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> AbrirCaixa()
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            Caixa caixa = new Caixa();
            if (user != null)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var caixaVM = new CaixaVM();
                    caixa = _mapper.Map<Caixa>(caixaVM);
                    caixa.Numero = await _caixaRepository.ObteNumeroUltimoCaixa(user.Id.ToString()) + 1;
                    caixa.UsuarioCadastroId = Guid.Parse(user.Id);
                    await _caixaRepository.Adicionar(caixa);
                    await _logAlteracaoService.RegistrarLogDiretamente($"Caixa Aberto por: {user.UserName} - Data/Hora: {DateTime.Now}", Guid.Parse(user.Id), $"Caixa[{caixa.Id}]");
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Json(new { success = false, errors = ex.Message });
                }
            }

            return Json(new { success = true });
        }

        [HttpPost]
        [Route("fechar-caixa")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> FecharCaixa()
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToList());
                return Json(new { success = false, errors, isModelState = true });
            }

            IdentityUser? user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var caixa = await _caixaRepository.ObterCaixaAberto(user.Id);

                    if (caixa != null)
                    {
                        caixa.Situacao = Business.Entidades.Enums.SituacaoCaixa.Fechado;
                        caixa.UsuarioAlteracaoId = Guid.Parse(user.Id);
                        await _caixaRepository.Atualizar(caixa);
                        await _logAlteracaoService.RegistrarLogDiretamente($"Caixa Fechado por: {user.UserName} - Data/Hora: {DateTime.Now}", Guid.Parse(user.Id), $"Caixa[{caixa.Id}]");
                        await transaction.CommitAsync();
                    }
                    else
                    {
                        await transaction.RollbackAsync();
                        return Json(new { success = false, errors = "Caixa não encontrado!" });
                    }

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Json(new { success = false, errors = ex.Message });
                }
            }

            return Json(new { success = true });
        }
    }
}
