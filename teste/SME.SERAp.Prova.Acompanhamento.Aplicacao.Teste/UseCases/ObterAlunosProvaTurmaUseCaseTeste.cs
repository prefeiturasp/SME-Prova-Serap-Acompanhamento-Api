using MediatR;
using Moq;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Teste.UseCases
{
    public class ObterAlunosProvaTurmaUseCaseTeste
    {
        private readonly Mock<IMediator> mediator;
        private readonly ObterAlunosProvaTurmaUseCase useCase;
        public ObterAlunosProvaTurmaUseCaseTeste()
        {
            mediator = new Mock<IMediator>();
            useCase = new ObterAlunosProvaTurmaUseCase(mediator.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Padrao_Quando_Nao_Existir_Alunos()
        {
            var prova = new Dominio.Entities.Prova(1, 123, "teste", Dominio.Enums.Modalidade.EF, 2025, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), false);

            mediator
                .Setup(m => m.Send(It.IsAny<ObterProvaPorIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(prova);

            mediator
                .Setup(m => m.Send(It.IsAny<ObterAlunosProvaTurmaQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IEnumerable<AlunoTurmaDto>)null);

            var result = await useCase.Executar(1, 1);

            Assert.Null(result);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Ordenada_Quando_Alunos_Existirem()
        {
            var prova = new Dominio.Entities.Prova(1, 123, "teste", Dominio.Enums.Modalidade.EF, 2025, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), false);

            var alunos = new List<AlunoTurmaDto>
            {
                new AlunoTurmaDto { NomeEstudante = "Carlos", FimProva = DateTime.Now, SituacaoProvaAluno = Dominio.Enums.SituacaoProvaAluno.Finalizada },
                new AlunoTurmaDto { NomeEstudante = "Ana", FimProva = DateTime.Now, SituacaoProvaAluno = Dominio.Enums.SituacaoProvaAluno.Finalizada }
            };

            mediator
                .Setup(m => m.Send(It.IsAny<ObterProvaPorIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(prova);

            mediator
                .Setup(m => m.Send(It.IsAny<ObterAbrangenciaUsuarioLogadoPorClaimsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ParametroDto>
                {
                    new ParametroDto { Chave = "PERMITEALTERAR", Valor = "true" }
                });

            mediator
                .Setup(m => m.Send(It.IsAny<ObterAlunosProvaTurmaQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(alunos);

            mediator
                .Setup(m => m.Send(It.IsAny<ObterNomeUsuarioPorIdCoressoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((Abrangencia)null);

            var result = (await useCase.Executar(1, 1)).ToList();

            Assert.Equal(2, result.Count);
            Assert.Equal("Ana", result.First().NomeEstudante);
            Assert.All(result, r => Assert.True(r.PodeReabrirProva));
        }

        [Fact]
        public async Task Deve_Definir_Ultima_Reabertura_Quando_Usuario_Encontrado()
        {
            var prova = new Dominio.Entities.Prova(1, 123, "teste", Dominio.Enums.Modalidade.EF, 2025, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), false);
            var alunos = new List<AlunoTurmaDto>
            {
                new AlunoTurmaDto
                {
                    NomeEstudante = "Carlos",
                    UsurioCoressoUltimaReabertura = "123",
                    DataUltimaReabertura = DateTime.Now,
                    FimProva = DateTime.Now,
                    SituacaoProvaAluno = Dominio.Enums.SituacaoProvaAluno.Finalizada
                }
            };

            mediator
                .Setup(m => m.Send(It.IsAny<ObterProvaPorIdQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(prova);

            mediator
                .Setup(m => m.Send(It.IsAny<ObterAbrangenciaUsuarioLogadoPorClaimsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<ParametroDto>
                {
                new ParametroDto { Chave = "PERMITEALTERAR", Valor = "true" }
                });

            mediator
                .Setup(m => m.Send(It.IsAny<ObterAlunosProvaTurmaQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(alunos);

            mediator
                .Setup(m => m.Send(It.IsAny<ObterNomeUsuarioPorIdCoressoQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Abrangencia { Usuario = "Prof. João" });

            var result = (await useCase.Executar(1, 1)).First();

            Assert.Contains("Prof. João", result.UltimaReabertura);
            Assert.True(result.PodeReabrirProva);
        }
    }
}
