using Moq;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Teste.Queries
{
    public class ObterNomeUsuarioPorIdCoressoQueryHandlerTeste
    {
        private readonly Mock<IRepositorioAbrangencia> repositorio;
        private readonly ObterNomeUsuarioPorIdCoressoQueryHandler handler;
        public ObterNomeUsuarioPorIdCoressoQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioAbrangencia>();
            handler = new ObterNomeUsuarioPorIdCoressoQueryHandler(repositorio.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Abrangencia_Quando_Encontrada()
        {
            var usuarioId = "usuario123";
            var abrangenciaEsperada = new Abrangencia();

            repositorio
                .Setup(r => r.ObterPorUsuarioCoressoAsync(usuarioId))
                .ReturnsAsync(abrangenciaEsperada);

            var query = new ObterNomeUsuarioPorIdCoressoQuery(usuarioId);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Equal(abrangenciaEsperada, resultado);
            repositorio.Verify(r => r.ObterPorUsuarioCoressoAsync(usuarioId), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Nao_Encontrada()
        {
            var usuarioId = "usuarioInexistente";

            repositorio
                .Setup(r => r.ObterPorUsuarioCoressoAsync(usuarioId))
                .ReturnsAsync((Abrangencia)null);

            var query = new ObterNomeUsuarioPorIdCoressoQuery(usuarioId);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
            repositorio.Verify(r => r.ObterPorUsuarioCoressoAsync(usuarioId), Times.Once);
        }
    }
}
