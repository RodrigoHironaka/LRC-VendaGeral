using AutoMapper;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using LRC.Business.Interfaces;
using LRC.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using LRC.App.ViewModels;
using LRC.Business.Entidades;
using LRC.Business.Servicos;
using LRC.Data.Repository;
using Microsoft.AspNetCore.Authorization;
using System.Text.RegularExpressions;
using LRC.Business.Notificacoes;
using LRC.Business.Entidades.Validacoes;

namespace LRC.App.Controllers
{
    [Authorize]
    public class SubGruposController : BaseController
    {
        private readonly ISubGrupoRepository _subGrupoRepository;
        private readonly ISubGrupoService _subGrupoService;
        private readonly IGrupoService _grupoService;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly MeuDbContext _context;
        private readonly ILogAlteracaoService _logAlteracaoService;

        public SubGruposController(IMapper mapper,
                                  ISubGrupoRepository subGrupoRepository,
                                  ISubGrupoService subGrupoService,
                                  IGrupoService grupoService,
                                  UserManager<IdentityUser> userManager,
                                  MeuDbContext context,
                                  ILogAlteracaoService logAlteracaoService,
                                  INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _subGrupoRepository = subGrupoRepository;
            _subGrupoService = subGrupoService;
            _grupoService = grupoService;
            _userManager = userManager;
            _context = context;
            _logAlteracaoService = logAlteracaoService;
        }

        [Route("lista-de-subgrupos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<SubGrupoVM>>(await _subGrupoRepository.ObterTodosComGrupo()));
        }

        [Route("editar-subgrupo/{id}")]
        public async Task<IActionResult> Editar(Guid Id)
        {
            var subGrupoVM = new SubGrupoVM();
            if (Id != Guid.Empty)
            {

                var subGrupo = await _subGrupoRepository.ObterPorIdComGrupo(Id);
                if (subGrupo == null) return NotFound();

                subGrupoVM = _mapper.Map<SubGrupoVM>(subGrupo);
                subGrupoVM.UsuarioCadastro = await _userManager.FindByIdAsync(subGrupoVM.UsuarioCadastroId.ToString());
                subGrupoVM.UsuarioAlteracao = await _userManager.FindByIdAsync(subGrupoVM.UsuarioAlteracaoId.ToString());
                subGrupoVM = await PopularGrupos(subGrupoVM);
            }
            else
                subGrupoVM = await PopularGrupos(new SubGrupoVM());

            return View(subGrupoVM);
        }

        [Route("editar-subgrupo/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Editar(Guid Id, SubGrupoVM subGrupoVM)
        {

            if (Id != subGrupoVM.Id) return NotFound();
            if (!ModelState.IsValid)
            {
                var errors = ModelState.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToList());
                return Json(new { success = false, errors, isModelState = true });
            }

            IdentityUser? user = await _userManager.GetUserAsync(User);
            Subgrupo subGrupo;
            if (user != null)
            {
                if (Id != Guid.Empty)
                {
                    using var transaction = await _context.Database.BeginTransactionAsync();
                    try
                    {
                        var subGrupoClone = await _subGrupoRepository.ObterPorIdComGrupo(Id);
                        subGrupoVM.DataAlteracao = DateTime.Now;
                        subGrupo = _mapper.Map<Subgrupo>(subGrupoVM);
                        subGrupo.UsuarioAlteracaoId = Guid.Parse(user.Id);
                        subGrupo.Grupo = await _grupoService.ObterPorId(subGrupoVM.GrupoId);
                        await _logAlteracaoService.CompararAlteracoes(subGrupoClone, subGrupo, Guid.Parse(user.Id), $"SubGrupo[{subGrupo.Id}]");
                        await _subGrupoService.Atualizar(subGrupo);
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
                        var errors = ObterNotificacoes.ExecutarValidacao(new SubGrupoValidation(), subGrupo);
                        return Json(new { success = false, errors });
                    }
                }
                else
                {
                    subGrupoVM = await PopularGrupos(subGrupoVM);
                    subGrupo = _mapper.Map<Subgrupo>(subGrupoVM);
                    subGrupo.UsuarioCadastroId = Guid.Parse(user.Id);
                    await _subGrupoService.Adicionar(subGrupo);
                    if (!OperacaoValida())
                    {
                        var errors = ObterNotificacoes.ExecutarValidacao(new SubGrupoValidation(), subGrupo);
                        return Json(new { success = false, errors });
                    }
                }
                return Json(new { success = true });
            }
            return View(subGrupoVM);
        }

        [HttpPost]
        [Route("excluir-subgrupo/{id:guid}")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Deletar(Guid id)
        {
            var subGrupo = await _subGrupoService.ObterPorId(id);
            if (subGrupo == null) return NotFound();
            IdentityUser? user = await _userManager.GetUserAsync(User);

            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await _logAlteracaoService.RegistrarLogDiretamente($"Registro: {subGrupo.Nome} excluído.", Guid.Parse(user.Id), $"SubGrupo[{subGrupo.Id}]");
                await _subGrupoService.Remover(id);
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception(ex.Message);
            }

            if (!OperacaoValida()) 
            { 
                //await transaction.RollbackAsync(); 
                return View(subGrupo); 
            }

            return RedirectToAction("Index");
        }

        private async Task<SubGrupoVM> PopularGrupos(SubGrupoVM subGrupo)
        {
            var grupos = await _grupoService.Buscar(x => x.Situacao == Business.Entidades.Enums.Situacao.Ativo);
            var gruposVM = _mapper.Map<IEnumerable<GrupoVM>>(grupos);
            gruposVM = gruposVM.OrderBy(x => x.Nome);
            subGrupo.Grupos = gruposVM;
            
            //subGrupo.Grupos = _mapper.Map<IEnumerable<GrupoVM>>(await _grupoService.Buscar(x => x.Situacao == Business.Entidades.Enums.Situacao.Ativo));
            return subGrupo;
        }
    }
}
