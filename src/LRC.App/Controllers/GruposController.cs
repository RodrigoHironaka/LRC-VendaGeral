﻿using AutoMapper;
using LRC.App.ViewModels;
using LRC.Business.Entidades;
using LRC.Business.Interfaces;
using LRC.Business.Interfaces.Repositorios;
using LRC.Business.Interfaces.Servicos;
using LRC.Data.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

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
            if (!ModelState.IsValid) return View(grupoVM);

            IdentityUser? user = await _userManager.GetUserAsync(User);
            if (user != null)
            {
                if (Id != Guid.Empty)
                {
                    using var transaction = await _context.Database.BeginTransactionAsync();
                    try
                    {
                        var grupoClone = await _grupoService.ObterPorId(grupoVM.Id);
                        grupoVM.DataAlteracao = DateTime.Now;
                        var grupo = _mapper.Map<Grupo>(grupoVM);
                        grupo.UsuarioAlteracaoId = Guid.Parse(user.Id);

                        await _logAlteracaoService.CompararAlteracoes(grupoClone, grupo, Guid.Parse(user.Id), $"Grupo[{grupo.Id}]");
                        await _grupoService.Atualizar(grupo);
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
                        return View(await _grupoService.ObterPorId(Id));
                    }
                }
                else
                {
                    var grupo = _mapper.Map<Grupo>(grupoVM);
                    grupo.UsuarioCadastroId = Guid.Parse(user.Id);
                    await _grupoService.Adicionar(grupo);
                    if (!OperacaoValida()) return View(grupoVM);
                }
                return RedirectToAction("Index");
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