using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterSituacaoProvaUseCase : IObterSituacaoProvaUseCase
    {
        public async Task<IEnumerable<SelecioneDto>> Executar()
        {
            var lista = Enum.GetValues(typeof(ProvaSituacao));

            var retorno = new List<SelecioneDto>();

            ProvaSituacao situacao;
            foreach (var item in lista)
            {
                situacao = (ProvaSituacao)item;
                if (situacao != ProvaSituacao.NaoCadastrado)
                    retorno.Add(new SelecioneDto((int)situacao, situacao.Descricao()));
            }

            return await Task.FromResult(retorno);
        }
    }
}
