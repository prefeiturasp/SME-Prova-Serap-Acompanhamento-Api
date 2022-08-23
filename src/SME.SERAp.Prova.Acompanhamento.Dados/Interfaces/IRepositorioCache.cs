using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Dados.Interfaces
{
    public interface IRepositorioCache
    {
        Task<T> ObterRedisAsync<T>(string nomeChave);
        Task RemoverRedisAsync(string nomeChave);
        Task SalvarRedisAsync(string nomeChave, object valor, int minutosParaExpirar = 720);
    }
}
