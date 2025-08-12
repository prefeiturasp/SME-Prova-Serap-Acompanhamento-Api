using Moq;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using System;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Teste.Queries
{
    public class ObterProvaPorIdQueryHandlerTeste
    {
        private readonly Mock<IRepositorioProva> repositorio;
        private readonly ObterProvaPorIdQueryHandler handler;

        public ObterProvaPorIdQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioProva>();
            handler = new ObterProvaPorIdQueryHandler(repositorio.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Prova_Quando_Encontrada()
        {
            var provaEsperada = new Dominio.Entities.Prova(1, 123, "teste", Dominio.Enums.Modalidade.EF, 2025, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), false);
            repositorio
                .Setup(r => r.ObterPorIdAsync(1))
                .ReturnsAsync(provaEsperada);

            var request = new ObterProvaPorIdQuery(1);

            var resultado = await handler.Handle(request, CancellationToken.None);

            Assert.Equal(provaEsperada, resultado);
            repositorio.Verify(r => r.ObterPorIdAsync(1), Times.Once);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Nao_Encontrar_Prova()
        {
            repositorio
                .Setup(r => r.ObterPorIdAsync(2))
                .ReturnsAsync((Dominio.Entities.Prova)null);

            var request = new ObterProvaPorIdQuery(2);

            var resultado = await handler.Handle(request, CancellationToken.None);

            Assert.Null(resultado);
            repositorio.Verify(r => r.ObterPorIdAsync(2), Times.Once);
        }
    }
}
