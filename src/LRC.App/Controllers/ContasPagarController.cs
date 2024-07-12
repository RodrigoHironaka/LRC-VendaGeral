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
using LRC.Business.Entidades;
using LRC.Business.Servicos;
using Microsoft.AspNetCore.Authorization;
using LRC.Data.Migrations;

namespace LRC.App.Controllers
{
    [Authorize]
    public class ContasPagarController : BaseController
    {
        private readonly IContaPagarRepository _contaPagarRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MeuDbContext _context;
        private readonly ILogAlteracaoService _logAlteracaoService;

        public ContasPagarController(IMapper mapper,
                                  IContaPagarRepository contaPagarRepository,
                                  IFornecedorService fornecedorService,
                                  UserManager<IdentityUser> userManager,
                                  MeuDbContext context,
                                  ILogAlteracaoService logAlteracaoService,
                                  INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _contaPagarRepository = contaPagarRepository;
            _fornecedorService = fornecedorService;
            _userManager = userManager;
            _context = context;
            _logAlteracaoService = logAlteracaoService;
        }

        [Route("lista-de-contaspagar")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ContaPagarVM>>(await _contaPagarRepository.ObterTodosComFornecedor()));
        }

        [Route("editar-contapagar/{id}")]
        public async Task<IActionResult> Editar(Guid Id)
        {
            var contaPagarVM = new ContaPagarVM();
            if (Id != Guid.Empty)
            {

                var contaPagar = await _contaPagarRepository.ObterPorIdComFornecedor(Id);
                if (contaPagar == null) return NotFound();

                contaPagarVM = _mapper.Map<ContaPagarVM>(contaPagar);
                contaPagarVM.UsuarioCadastro = await _userManager.FindByIdAsync(contaPagarVM.UsuarioCadastroId.ToString());
                contaPagarVM.UsuarioAlteracao = await _userManager.FindByIdAsync(contaPagarVM.UsuarioAlteracaoId.ToString());
            }
          

            return View(contaPagarVM);
        }

        [Route("editar-contapagar/{id:guid}")]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Editar(Guid Id, ContaPagarVM contaPagarVM)
        {
            if (Id != contaPagarVM.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, errors, isModelState = true });
            }

            IdentityUser? user = await _userManager.GetUserAsync(User);
            ContaPagar contaPagar;

            if (user != null)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (Id != Guid.Empty)
                    {
                        var contaPagarClone = await _contaPagarRepository.ObterPorIdComFornecedor(Id);
                        contaPagarVM.DataAlteracao = DateTime.Now;
                        contaPagar = _mapper.Map<ContaPagar>(contaPagarVM);
                        contaPagar.UsuarioAlteracaoId = Guid.Parse(user.Id);
                        contaPagar.Fornecedor = await _fornecedorService.ObterPorId(contaPagarVM.FornecedorId);
                        await _logAlteracaoService.CompararAlteracoes(contaPagarClone, contaPagar, Guid.Parse(user.Id), $"ContaPagar[{contaPagar.Id}]");
                        await _contaPagarRepository.Atualizar(contaPagar);
                    }
                    else
                    {
                        contaPagar = _mapper.Map<ContaPagar>(contaPagarVM);
                        contaPagar.UsuarioCadastroId = Guid.Parse(user.Id);
                        await _contaPagarRepository.Adicionar(contaPagar);
                    }
                    if (!OperacaoValida())
                    {
                        await transaction.RollbackAsync();
                        List<string> errors = new List<string>();
                        errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem).ToList();
                        errors.Add(ObterNotificacoes.ExecutarValidacao(new ContaPagarValidation(), contaPagar));
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
            return View(contaPagarVM);
        }

        [HttpPost]
        [Route("excluir-contapagar/{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var contaPagar = await _contaPagarRepository.ObterPorId(id);
            if (contaPagar == null) return NotFound();
            IdentityUser? user = await _userManager.GetUserAsync(User);

            if (user == null) return NotFound();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _logAlteracaoService.RegistrarLogDiretamente($"Registro: {contaPagar.Descricao} excluído.", Guid.Parse(user.Id), $"ContaPagar[{contaPagar.Id}]");
                await _contaPagarRepository.Remover(id);
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
                return View(contaPagar);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> PesquisaFornecedores()
        {
            try
            {
                var fornecedores = _mapper.Map<IEnumerable<FornecedorVM>>(await _fornecedorService.Buscar(x => x.Situacao == Business.Entidades.Enums.Situacao.Ativo));
                return PartialView("_Pesquisa", fornecedores);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, errors = ex.Message });
            }
            
        }

        //[Route("baixa-contapagar/{id:guid}")]
        //[IgnoreAntiforgeryToken]
        //public async Task<IActionResult> BaixaRegistro(Guid id, ContaPagarVM contaPagarVM)
        //{
        //    try
        //    {
                
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, errors = ex.Message });
        //    }

        //}
    }
}
