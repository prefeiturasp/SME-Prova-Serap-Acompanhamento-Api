using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTurmasQuery : IRequest<IEnumerable<Turma>>
    {
        public ObterTurmasQuery(int anoLetivo, long ueId, Modalidade modalidade, string ano)
        {
            AnoLetivo = anoLetivo;
            UeId = ueId;
            Modalidade = modalidade;
            Ano = ano;
        }

        public int AnoLetivo { get; set; }
        public long UeId { get; set; }
        public Modalidade Modalidade { get; set; }
        public string Ano { get; set; }
    }
}
