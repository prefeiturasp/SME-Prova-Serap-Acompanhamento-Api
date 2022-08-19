using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ObterModalidadeUseCase : IObterModalidadeUseCase
    {
        public async Task<IEnumerable<SelecioneDto>> Executar()
        {
            var lista = Enum.GetValues(typeof(Modalidade));

            var retorno = new List<SelecioneDto>();

            Modalidade modalidade;
            foreach (var item in lista)
            {
                modalidade = (Modalidade)item;
                if (modalidade != Modalidade.NaoCadastrado)
                    retorno.Add(new SelecioneDto((int)modalidade, modalidade.Descricao()));
            }

            return await Task.FromResult(retorno);
        }
    }
}
