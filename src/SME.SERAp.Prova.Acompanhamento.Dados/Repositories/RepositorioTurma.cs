﻿using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Repositories
{
    public class RepositorioTurma : RepositorioBase<Turma>, IRepositorioTurma
    {
        protected override string IndexName => "turma";
        public RepositorioTurma(IElasticClient elasticClient) : base(elasticClient)
        {
        }

        public async Task<IEnumerable<Turma>> ObterPorUeIdAnoAsync(int anoLetivo, long ueId, Modalidade modalidade, string ano)
        {
            QueryContainer query = new QueryContainerDescriptor<Turma>();

            var search = new SearchDescriptor<Turma>(IndexName).Query(q =>
                q.Term(p => p.Field(p => p.AnoLetivo).Value(anoLetivo)) &&
                q.Term(p => p.Field(p => p.UeId).Value(ueId)) &&
                q.Term(p => p.Field(p => p.Ano).Value(ano)) &&
                q.Term(p => p.Field(p => p.Modalidade).Value(modalidade))
            ).From(0).Size(10000);

            var response = await elasticClient.SearchAsync<Turma>(search);

            if (!response.IsValid) return default;

            return response.Hits.Select(hit => hit.Source).ToList();
        }
    }
}