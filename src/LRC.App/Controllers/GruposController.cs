using AutoMapper;
using LRC.App.ViewModels;
using LRC.Business.Entidades;
using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LRC.App.Controllers
{
    [Authorize]
    public class GruposController : BaseController
    {
        private readonly IGrupoRepository _grupoRepository;
        private readonly IGrupoService _grupoService;
        private readonly IMapper _mapper;

        public GruposController(IGrupoRepository grupoRepository,
                                  IMapper mapper,
                                  IGrupoService grupoService,
                                  INotificador notificador) : base(notificador)
        {
            _grupoRepository = grupoRepository;
            _mapper = mapper;
            _grupoService = grupoService;
        }

        //[AllowAnonymous]
        [Route("lista-de-grupos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<GrupoVM>>(await _grupoRepository.ObterTodos()));
        }

        [Route("novo-grupo")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [Route("novo-grupo")]
        [HttpPost]
        public async Task<IActionResult> Create(GrupoVM grupoVM)
        {
            if (!ModelState.IsValid) return View(grupoVM);

            var grupo = _mapper.Map<Grupo>(grupoVM);
            await _grupoService.Adicionar(grupo);

            if (!OperacaoValida()) return View(grupoVM);

            return RedirectToAction("Index");
        }

        [Route("editar-grupo/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var grupo = await _grupoService.ObterPorId(id);

            if (grupo == null)
            {
                return NotFound();
            }
            var grupoVM = _mapper.Map<GrupoVM>(grupo);
            return View(grupoVM);
        }

        [Route("editar-grupo/{id:guid}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, GrupoVM grupoVM)
        {
            if (id != grupoVM.Id) return NotFound();

            if (!ModelState.IsValid) return View(grupoVM);

            grupoVM.DataAlteracao = DateTime.Now;

            var grupo = _mapper.Map<Grupo>(grupoVM);
            await _grupoService.Atualizar(grupo);

            if (!OperacaoValida()) return View(await _grupoService.ObterPorId(id));

            return RedirectToAction("Index");
        }

        [Route("excluir-grupo/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var grupo = await _grupoService.ObterPorId(id);

            if (grupo == null)
            {
                return NotFound();
            }
            var grupoVM = _mapper.Map<GrupoVM>(grupo);
            return View(grupoVM);
        }

        [Route("excluir-grupo/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var grupo = await _grupoService.ObterPorId(id);

            if (grupo == null) return NotFound();

            await _grupoService.Remover(id);

            if (!OperacaoValida()) return View(grupo);

            return RedirectToAction("Index");
        }
    }
}
