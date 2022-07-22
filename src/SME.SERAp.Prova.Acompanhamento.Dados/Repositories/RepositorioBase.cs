using Nest;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados
{
    public abstract class RepositorioBase<TEntidade> : IRepositorioBase<TEntidade> where TEntidade : EntidadeBase
    {
        private readonly IElasticClient elasticClient;

        public abstract string IndexName { get; }

        public RepositorioBase(IElasticClient elasticClient)
        {
            this.elasticClient = elasticClient ?? throw new ArgumentException(nameof(elasticClient));
        }

        public async Task<bool> CriarIndexAsync()
        {
            if (!(await elasticClient.Indices.ExistsAsync(IndexName)).Exists)
            {
                await elasticClient.Indices.CreateAsync(IndexName, c =>
                {
                    c.Map<TEntidade>(p => p.AutoMap());
                    return c;
                });
            }
            return true;
        }

        public virtual async Task<bool> InserirAsync(TEntidade entidade)
        {
            var response = await elasticClient.IndexAsync(entidade, descriptor => descriptor.Index(IndexName));

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return true;
        }

        public virtual async Task<bool> AlterarAsync(TEntidade entidade)
        {
            var response = await elasticClient.UpdateAsync(DocumentPath<TEntidade>.Id(entidade.Id).Index(IndexName), p => p.Doc(entidade));

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return true;
        }

        public virtual async Task<bool> DeletarAsync(string id)
        {
            var response = await elasticClient.DeleteAsync(DocumentPath<TEntidade>.Id(id).Index(IndexName));

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return true;
        }

        public async Task<TEntidade> ObterPorIdAsync(string id)
        {
            var response = await elasticClient.GetAsync(DocumentPath<TEntidade>.Id(id).Index(IndexName));

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return response.Source;
        }

        public async Task<IEnumerable<TEntidade>> ObterTodosAsync()
        {
            var search = new SearchDescriptor<TEntidade>(IndexName).MatchAll();
            var response = await elasticClient.SearchAsync<TEntidade>(search);

            if (!response.IsValid)
                throw new Exception(response.ServerError?.ToString(), response.OriginalException);

            return response.Hits.Select(hit => hit.Source).ToList();
        }
    }
}
