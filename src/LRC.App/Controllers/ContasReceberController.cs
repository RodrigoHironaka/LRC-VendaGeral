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
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Servicos;
using LRC.Business.Entidades;

namespace LRC.App.Controllers
{
    public class ContasReceberController : BaseController
    {
        private readonly IContaReceberRepository _contaReceberRepository;
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MeuDbContext _context;
        private readonly ILogAlteracaoService _logAlteracaoService;

        public ContasReceberController(IMapper mapper,
                                  IContaReceberRepository contaReceberRepository,
                                  IClienteService clienteService,
                                  UserManager<IdentityUser> userManager,
                                  MeuDbContext context,
                                  ILogAlteracaoService logAlteracaoService,
                                  INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _contaReceberRepository = contaReceberRepository;
            _clienteService = clienteService;
            _userManager = userManager;
            _context = context;
            _logAlteracaoService = logAlteracaoService;
        }

        [Route("lista-de-contasreceber")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ContaReceberVM>>(await _contaReceberRepository.ObterTodosComCliente()));
        }

        [Route("editar-contareceber/{id}")]
        public async Task<IActionResult> Editar(Guid Id)
        {
            var contaReceberVM = new ContaReceberVM();
            if (Id != Guid.Empty)
            {

                var contaReceber = await _contaReceberRepository.ObterPorIdComCliente(Id);
                if (contaReceber == null) return NotFound();

                contaReceberVM = _mapper.Map<ContaReceberVM>(contaReceber);
                contaReceberVM.UsuarioCadastro = await _userManager.FindByIdAsync(contaReceberVM.UsuarioCadastroId.ToString());
                contaReceberVM.UsuarioAlteracao = await _userManager.FindByIdAsync(contaReceberVM.UsuarioAlteracaoId.ToString());
            }


            return View(contaReceberVM);
        }

        [Route("editar-contareceber/{id:guid}")]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Editar(Guid Id, ContaReceberVM contaReceberVM)
        {
            if (Id != contaReceberVM.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors, isModelState = true });
            }

            IdentityUser? user = await _userManager.GetUserAsync(User);
            ContaReceber contaReceber;

            if (user != null)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (Id != Guid.Empty)
                    {
                        var contaReceberClone = await _contaReceberRepository.ObterPorIdComCliente(Id);
                        contaReceberVM.DataAlteracao = DateTime.Now;
                        contaReceber = _mapper.Map<ContaReceber>(contaReceberVM);
                        contaReceber.UsuarioAlteracaoId = Guid.Parse(user.Id);
                        contaReceber.Cliente = await _clienteService.ObterPorId(contaReceberVM.ClienteId);
                        await _logAlteracaoService.CompararAlteracoes(contaReceberClone, contaReceber, Guid.Parse(user.Id), $"ContaReceber[{contaReceber.Id}]");
                        await _contaReceberRepository.Atualizar(contaReceber);
                    }
                    else
                    {
                        contaReceber = _mapper.Map<ContaReceber>(contaReceberVM);
                        contaReceber.UsuarioCadastroId = Guid.Parse(user.Id);
                        await _contaReceberRepository.Adicionar(contaReceber);
                    }
                    if (!OperacaoValida())
                    {
                        await transaction.RollbackAsync();
                        List<string> errors = new List<string>();
                        errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem).ToList();
                        errors.Add(ObterNotificacoes.ExecutarValidacao(new ContaReceberValidation(), contaReceber));
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
            return View(contaReceberVM);
        }

        [HttpPost]
        [Route("excluir-contareceber/{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var contaReceber = await _contaReceberRepository.ObterPorId(id);
            if (contaReceber == null) return NotFound();
            IdentityUser? user = await _userManager.GetUserAsync(User);

            if (user == null) return NotFound();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _logAlteracaoService.RegistrarLogDiretamente($"Registro: {contaReceber.Descricao} excluído.", Guid.Parse(user.Id), $"ContaReceber[{contaReceber.Id}]");
                await _contaReceberRepository.Remover(id);
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
                return View(contaReceber);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> PesquisaClientes()
        {
            try
            {
                var clientes = _mapper.Map<IEnumerable<ClienteVM>>(await _clienteService.Buscar(x => x.Situacao == Business.Entidades.Enums.Situacao.Ativo));
                return PartialView("_Pesquisa", clientes);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = ex.Message });
            }

        }
    }
}
