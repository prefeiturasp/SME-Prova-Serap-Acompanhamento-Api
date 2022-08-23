using MediatR;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterAnosPorModalidadeEscolasQuery : IRequest<IEnumerable<Ano>>
    {
        public ObterAnosPorModalidadeEscolasQuery(Modalidade? modalidade, string[] ueId)
        {
            Modalidade = modalidade;
            UeId = ueId;
        }

        public Modalidade? Modalidade { get; set; }
        public string[] UeId { get; set; }
    }
}