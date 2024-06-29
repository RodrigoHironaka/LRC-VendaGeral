﻿using LRC.Business.Entidades;
using LRC.Business.Interfaces.Repositorios;
using LRC.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Data.Repository
{
    public class EntregadorRepository : Repository<Entregador>, IEntregadorRepository
    {
        public EntregadorRepository(MeuDbContext db) : base(db) { }
    }
}
