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
using LRC.Business.Notificacoes;

namespace LRC.App.Controllers
{
    [Authorize]
    public class ClientesController : BaseController
    {
        private readonly IClienteService _clienteService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MeuDbContext _context;
        private readonly ILogAlteracaoService _logAlteracaoService;

        public ClientesController(IMapper mapper,
                                  IClienteService clienteService,
                                  UserManager<IdentityUser> userManager,
                                  MeuDbContext context,
                                  ILogAlteracaoService logAlteracaoService,
                                  INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _clienteService = clienteService;
            _userManager = userManager;
            _context = context;
            _logAlteracaoService = logAlteracaoService;
        }


        [Route("lista-de-clientes")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ClienteVM>>(await _clienteService.ObterTodos()));
        }

        [Route("editar-cliente/{id}")]
        public async Task<IActionResult> Editar(Guid Id)
        {
            var clienteVM = new ClienteVM();
            if (Id != Guid.Empty)
            {
                var cliente = await _clienteService.ObterPorId(Id);
                if (cliente == null) return NotFound();

                clienteVM = _mapper.Map<ClienteVM>(cliente);
                clienteVM.UsuarioCadastro = await _userManager.FindByIdAsync(clienteVM.UsuarioCadastroId.ToString());
                clienteVM.UsuarioAlteracao = await _userManager.FindByIdAsync(clienteVM.UsuarioAlteracaoId.ToString());
            }

            return View(clienteVM);
        }

        [Route("editar-cliente/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Editar(Guid Id, ClienteVM clienteVM)
        {
            if (Id != clienteVM.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors, isModelState = true });
            }

            IdentityUser? user = await _userManager.GetUserAsync(User);
            Cliente cliente;

            if (user != null)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (Id != Guid.Empty)
                    {
                        var cLienteClone = await _clienteService.ObterPorId(Id);
                        clienteVM.DataAlteracao = DateTime.Now;
                        cliente = _mapper.Map<Cliente>(clienteVM);
                        cliente.UsuarioAlteracaoId = Guid.Parse(user.Id);
                        await _logAlteracaoService.CompararAlteracoes(cLienteClone, cliente, Guid.Parse(user.Id), $"Cliente[{cliente.Id}]");
                        await _clienteService.Atualizar(cliente);
                    }
                    else
                    {
                        cliente = _mapper.Map<Cliente>(clienteVM);
                        cliente.UsuarioCadastroId = Guid.Parse(user.Id);
                        await _clienteService.Adicionar(cliente);
                    }

                    if (!OperacaoValida())
                    {
                        await transaction.RollbackAsync();
                        List<string> errors = new List<string>();
                        errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem).ToList();
                        errors.Add(ObterNotificacoes.ExecutarValidacao(new ClienteValidation(), cliente));
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
            return View(clienteVM);
        }

        [HttpPost]
        [Route("excluir-cliente/{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var cliente = await _clienteService.ObterPorId(id);
            if (cliente == null) return NotFound();
            IdentityUser? user = await _userManager.GetUserAsync(User);

            if (user == null) return NotFound();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _logAlteracaoService.RegistrarLogDiretamente($"Registro: {cliente.RazaoSocial} excluído.", Guid.Parse(user.Id), $"Cliente[{cliente.Id}]");
                await _clienteService.Remover(id);
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
                var errors = ObterNotificacoes.ExecutarValidacao(new ClienteValidation(), cliente);
                return Json(new { success = false, errors });
            }

            return RedirectToAction("Index");
        }
    }
}
