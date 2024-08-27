using AutoMapper;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using LRC.Business.Interfaces;
using LRC.Data.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LRC.App.ViewModels;
using LRC.Data.Repository;
using LRC.Business.Servicos;
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Entidades;
using Microsoft.AspNetCore.Authorization;

namespace LRC.App.Controllers
{
    [Authorize]
    public class FluxosCaixaController : BaseController
    {
        private readonly IFluxoCaixaRepository _fluxoCaixaRepository;
        private readonly IFormaPagamentoService _formaPagamentoService;
        private readonly ICaixaRepository _caixaRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MeuDbContext _context;
        private readonly ILogAlteracaoService _logAlteracaoService;

        public FluxosCaixaController(IMapper mapper,
                                  IFluxoCaixaRepository fluxoCaixaRepository,
                                  IFormaPagamentoService formaPagamentoService,
                                  ICaixaRepository caixaRepository,
                                  UserManager<IdentityUser> userManager,
                                  MeuDbContext context,
                                  ILogAlteracaoService logAlteracaoService,
                                  INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _fluxoCaixaRepository = fluxoCaixaRepository;
            _formaPagamentoService = formaPagamentoService;
            _caixaRepository = caixaRepository;
            _userManager = userManager;
            _context = context;
            _logAlteracaoService = logAlteracaoService;
        }

        [Route("lista-de-fluxos")]
        public async Task<IActionResult> Index()
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            if(user != null)
            {
                var caixa = _caixaRepository.ObterCaixaAberto(user.Id);
                if(caixa != null)
                    return View(_mapper.Map<IEnumerable<FluxoCaixaVM>>(await _fluxoCaixaRepository.ObterTodosComEntidades(Guid.Parse(caixa.Id.ToString()))));
                else
                    return View(_mapper.Map<IEnumerable<FluxoCaixaVM>>(await _fluxoCaixaRepository.ObterTodosComEntidades(Guid.NewGuid())));
            }
            else
                return Json(new { success = false, errors = "Nenhum usuário encontrado!" });

        }

        [Route("editar-fluxo-caixa/{id}")]
        public async Task<IActionResult> Editar(Guid Id)
        {
            IdentityUser? user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                var caixa = await _caixaRepository.ObterCaixaAberto(user.Id);
                if (caixa != null)
                {
                    var fluxoCaixaVM = new FluxoCaixaVM();
                    if (Id != Guid.Empty)
                    {
                        var fluxoCaixa = await _fluxoCaixaRepository.ObterPorIdComEntidade(Id, Guid.Parse(caixa.Id.ToString()));
                        if (fluxoCaixa == null) return NotFound();

                        fluxoCaixaVM = _mapper.Map<FluxoCaixaVM>(fluxoCaixa);
                        fluxoCaixaVM.UsuarioCadastro = await _userManager.FindByIdAsync(fluxoCaixaVM.UsuarioCadastroId.ToString());
                        fluxoCaixaVM.UsuarioAlteracao = await _userManager.FindByIdAsync(fluxoCaixaVM.UsuarioAlteracaoId.ToString());
                        fluxoCaixaVM = await PopularFormaspagamento(fluxoCaixaVM);
                    }
                    else
                        fluxoCaixaVM = await PopularFormaspagamento(new FluxoCaixaVM());

                    return View(fluxoCaixaVM);
                }
                else
                    return Json(new { success = false, errors = "O Caixa está fechado!" });
            }
            else
                return Json(new { success = false, errors = "Nenhum usuário encontrado!" });
        }


        [Route("editar-fluxo-caixa/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Editar(Guid Id, FluxoCaixaVM fluxoCaixaVM)
        {
            if (Id != fluxoCaixaVM.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToList());
                return Json(new { success = false, errors, isModelState = true });
            }

            IdentityUser? user = await _userManager.GetUserAsync(User);
            FluxoCaixa fluxoCaixa;

            if (user != null)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    var caixa = await _caixaRepository.ObterCaixaAberto(user.Id);
                    if (caixa != null)
                    {
                        if (Id != Guid.Empty)
                        {
                            var fluxoCaixaClone = await _fluxoCaixaRepository.ObterPorIdComEntidade(Id, Guid.Parse(caixa.Id.ToString()));
                            fluxoCaixaVM.DataAlteracao = DateTime.Now;
                            fluxoCaixa = _mapper.Map<FluxoCaixa>(fluxoCaixaVM);
                            fluxoCaixa.UsuarioAlteracaoId = Guid.Parse(user.Id);
                            fluxoCaixa.FormaPagamento = await _formaPagamentoService.ObterPorId(fluxoCaixaVM.FormaPagamentoId);
                            if (fluxoCaixa.DebitoCredito == Business.Entidades.Enums.DebitoCredito.Debito)
                                fluxoCaixa.Valor = fluxoCaixa.Valor * -1;
                            await _logAlteracaoService.CompararAlteracoes(fluxoCaixaClone, fluxoCaixa, Guid.Parse(user.Id), $"FluxoCaixa[{fluxoCaixa.Id}]");
                            await _fluxoCaixaRepository.Atualizar(fluxoCaixa);
                        }
                        else
                        {
                            fluxoCaixa = _mapper.Map<FluxoCaixa>(fluxoCaixaVM);
                            fluxoCaixa.UsuarioCadastroId = Guid.Parse(user.Id);
                            fluxoCaixa.CaixaId = caixa.Id;
                            if (fluxoCaixa.DebitoCredito == Business.Entidades.Enums.DebitoCredito.Debito)
                                fluxoCaixa.Valor = fluxoCaixa.Valor * -1;
                            await _fluxoCaixaRepository.Adicionar(fluxoCaixa);
                        }
                        if (!OperacaoValida())
                        {
                            await transaction.RollbackAsync();
                            List<string> errors = new List<string>();
                            errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem).ToList();
                            errors.Add(ObterNotificacoes.ExecutarValidacao(new FluxoCaixaValidation(), fluxoCaixa));
                            return Json(new { success = false, errors });
                        }
                        await transaction.CommitAsync();
                    }
                    else
                        return Json(new { success = false, errors = "O Caixa está fechado!" });

                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return Json(new { success = false, errors = ex.Message });
                }

                return Json(new { success = true });
            }
            return View(fluxoCaixaVM);
        }

        [HttpPost]
        [Route("excluir-fluxo-caixa/{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var fluxoCaixa = await _fluxoCaixaRepository.ObterPorId(id);
            if (fluxoCaixa == null) return NotFound();
            IdentityUser? user = await _userManager.GetUserAsync(User);

            if (user == null) return NotFound();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _logAlteracaoService.RegistrarLogDiretamente($"Registro: {fluxoCaixa.Descricao} excluído.", Guid.Parse(user.Id), $"FluxoCaixa[{fluxoCaixa.Id}]");
                await _fluxoCaixaRepository.Remover(id);
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
                return View(fluxoCaixa);
            }

            return RedirectToAction("Index","Caixas");
        }

        private async Task<FluxoCaixaVM> PopularFormaspagamento(FluxoCaixaVM fluxoCaixa)
        {
            var formasPagamento = await _formaPagamentoService.Buscar(x => x.Situacao == Business.Entidades.Enums.Situacao.Ativo);
            var formasPagamentoVM = _mapper.Map<IEnumerable<FormaPagamentoVM>>(formasPagamento);
            formasPagamentoVM = formasPagamentoVM.OrderBy(x => x.Nome);
            fluxoCaixa.FormasPagamento = formasPagamentoVM;
            return fluxoCaixa;
        }
    }
}
