using LRC.Business.Entidades.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRC.Business.Entidades
{
    public abstract class Pessoa : Entidade
    {
        public string? RazaoSocial { get; set; }
        public string? NomeFantasia { get; set; }
        public DateTime? Nascimento { get; set; }
        public Int64 Documento { get; set; }
        public string? Documento2 { get; set; }
        public string? Telefone { get; set; }
        public string? Celular { get; set; }
        public string? Celular2 { get; set; }
        public string? Email { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
        public IList<Endereco>? Enderecos { get; set; }

        public string? DocumentoFormatado
        {
            get
            {
                if (TipoPessoa == TipoPessoa.Física)
                    return Documento.ToString(@"000\.000\.000\-00");
                else
                    return Documento.ToString(@"00\.000\.000\/0000\-00");
            }
        }
    }
}
