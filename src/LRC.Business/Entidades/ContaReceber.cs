﻿using LRC.Business.Entidades.Componentes;
using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public class ContaReceber : Entidade
    {
        public string? Descricao { get; set; }
        public DateTime? DataEmissao { get; set; }
        public DateTime? DataFechamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public string? NumeroDocumento { get; set; }
        public string? Observacao { get; set; }
        public decimal Valor { get; set; }
        public SituacaoConta Situacao { get; set; }

        public Guid ClienteId { get; set; }
        public Cliente? Cliente { get; set; }

        public ICollection<Parcela> Parcelas { get; set; } = new List<Parcela>();
    }
}
