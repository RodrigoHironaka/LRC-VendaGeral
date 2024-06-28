﻿using LRC.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LRC.App.Controllers
{
    public class BaseController : Controller
    {
        public readonly INotificador _notificador;

        protected BaseController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }
    }
}
