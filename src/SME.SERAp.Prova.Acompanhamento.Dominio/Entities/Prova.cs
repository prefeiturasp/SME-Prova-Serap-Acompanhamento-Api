using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class Prova : EntidadeBase
    {
        public Prova(long id, long codigo, string descricao, Modalidade modalidade, int ano, DateTime inicio, DateTime fim)
        {
            Id = id.ToString();
            Codigo = codigo;
            Descricao = descricao;
            Modalidade = modalidade;
            Ano = ano;
            Inicio = inicio;
            Fim = fim;
        }

        public long Codigo { get; set; }
        public string Descricao { get; set; }
        public Modalidade Modalidade { get; set; }
        public int Ano { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
    }
}
