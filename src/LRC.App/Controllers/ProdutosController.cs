using AutoMapper;
using LRC.App.ViewModels;
using LRC.Business.Entidades;
using LRC.Business.Entidades.Validacoes;
using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using LRC.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LRC.App.Controllers
{
    public class ProdutosController : BaseController
    {

        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly ISubGrupoService _subgrupoService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MeuDbContext _context;
        private readonly ILogAlteracaoService _logAlteracaoService;

        public ProdutosController(IMapper mapper,
                                  IProdutoRepository subGrupoRepository,
                                  IProdutoService produtoService,
                                  ISubGrupoService subgrupoService,
                                  UserManager<IdentityUser> userManager,
                                  MeuDbContext context,
                                  ILogAlteracaoService logAlteracaoService,
                                  INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _produtoRepository = subGrupoRepository;
            _produtoService = produtoService;
            _subgrupoService = subgrupoService;
            _userManager = userManager;
            _context = context;
            _logAlteracaoService = logAlteracaoService;
        }


        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoVM>>(await _produtoRepository.ObterTodosComSubGrupo()));
        }

        [Route("editar-produto/{id}")]
        public async Task<IActionResult> Editar(Guid Id)
        {
            var ProdutoVM = new ProdutoVM();
            if (Id != Guid.Empty)
            {

                var produto = await _produtoRepository.ObterPorIdComSubGrupo(Id);
                if (produto == null) return NotFound();

                ProdutoVM = _mapper.Map<ProdutoVM>(produto);
                ProdutoVM.UsuarioCadastro = await _userManager.FindByIdAsync(ProdutoVM.UsuarioCadastroId.ToString());
                ProdutoVM.UsuarioAlteracao = await _userManager.FindByIdAsync(ProdutoVM.UsuarioAlteracaoId.ToString());
                ProdutoVM = await PopularSubGrupos(ProdutoVM);
            }
            else
                ProdutoVM = await PopularSubGrupos(new ProdutoVM());

            return View(ProdutoVM);
        }


        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Editar(Guid Id, ProdutoVM produtoVM)
        {
            if (Id != produtoVM.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value?.Errors.Select(e => e.ErrorMessage).ToList());
                return Json(new { success = false, errors, isModelState = true });
            }

            IdentityUser? user = await _userManager.GetUserAsync(User);
            Produto produto;

            if (user != null)
            {
                using var transaction = await _context.Database.BeginTransactionAsync();
                try
                {
                    if (Id != Guid.Empty)
                    {
                        var produtoClone = await _produtoRepository.ObterPorIdComSubGrupo(Id);
                        produtoVM.DataAlteracao = DateTime.Now;
                        produto = _mapper.Map<Produto>(produtoVM);
                        produto.UsuarioAlteracaoId = Guid.Parse(user.Id);
                        produto.Subgrupo = await _subgrupoService.ObterPorId(produto.SubgrupoId);
                        await _logAlteracaoService.CompararAlteracoes(produtoClone, produto, Guid.Parse(user.Id), $"Produto[{produto.Id}]");
                        await _produtoService.Atualizar(produto);
                    }
                    else
                    {
                        produtoVM = await PopularSubGrupos(produtoVM);
                        produto = _mapper.Map<Produto>(produtoVM);
                        produto.UsuarioCadastroId = Guid.Parse(user.Id);
                        await _produtoService.Adicionar(produto);
                    }

                    if (!OperacaoValida())
                    {
                        await transaction.RollbackAsync();
                        List<string> errors = new List<string>();
                        errors = _notificador.ObterNotificacoes().Select(x => x.Mensagem).ToList();
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
            return View(produtoVM);
        }

        [HttpPost]
        [Route("excluir-produto/{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var produto = await _produtoService.ObterPorId(id);
            if (produto == null) return NotFound();
            IdentityUser? user = await _userManager.GetUserAsync(User);

            if (user == null) return NotFound();

            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                await _logAlteracaoService.RegistrarLogDiretamente($"Registro: {produto.Nome} excluído.", Guid.Parse(user.Id), $"Produto[{produto.Id}]");
                await _produtoService.Remover(id);
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
                var errors = ObterNotificacoes.ExecutarValidacao(new ProdutoValidation(), produto);
                return Json(new { success = false, errors });
            }

            return RedirectToAction("Index");
        }

        private async Task<ProdutoVM> PopularSubGrupos(ProdutoVM produto)
        {
            var subgrupos = await _subgrupoService.Buscar(x => x.Situacao == Business.Entidades.Enums.Situacao.Ativo);
            var subgruposVM = _mapper.Map<IEnumerable<SubGrupoVM>>(subgrupos);
            subgruposVM = subgruposVM.OrderBy(x => x.Nome);
            produto.SubGrupos = subgruposVM;
            return produto;
        }
    }
}
