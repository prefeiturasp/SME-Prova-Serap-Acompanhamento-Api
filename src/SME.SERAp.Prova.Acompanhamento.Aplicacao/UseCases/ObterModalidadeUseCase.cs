using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterModalidadeUseCase : AbstractUseCase, IObterModalidadeUseCase
    {
        public ObterModalidadeUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<IEnumerable<SelecioneDto>> Executar()
        {
            var lista = Enum.GetValues(typeof(Modalidade));
            var retorno = new List<SelecioneDto>();

            var uesId = await mediator.Send(new ObterUesUsuarioLogadoQuery());

            Modalidade modalidade;
            foreach (var item in lista)
            {
                modalidade = (Modalidade)item;
                if (modalidade != Modalidade.NaoCadastrado)
                {
                    if (uesId != null && uesId.Any())
                    {
                        var idsUesPorModalidade = await mediator.Send(new ObterUesIdPorAnoLetivoModalidadeQuery(DateTime.Now.Year, modalidade, uesId.Select(t => t.ToString()).ToArray()));

                        if (idsUesPorModalidade.Any())
                            retorno.Add(new SelecioneDto((int)modalidade, modalidade.Descricao()));

                    }
                    else
                        retorno.Add(new SelecioneDto((int)modalidade, modalidade.Descricao()));
                }
            }

            return await Task.FromResult(retorno);
        }
    }
}
