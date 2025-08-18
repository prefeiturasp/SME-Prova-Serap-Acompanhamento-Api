using Moq;
using SME.SERAp.Prova.Acompanhamento.Dados;
using SME.SERAp.Prova.Acompanhamento.Dominio;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Teste.Queries
{
    public class ObterAlunosProvaTurmaQueryHandlerTeste
    {
        private readonly Mock<IRepositorioProvaAlunoResultado> repositorio;
        private readonly ObterAlunosProvaTurmaQueryHandler handler;
        public ObterAlunosProvaTurmaQueryHandlerTeste()
        {
            repositorio = new Mock<IRepositorioProvaAlunoResultado>();
            handler = new ObterAlunosProvaTurmaQueryHandler(repositorio.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Repositorio_Retornar_Null()
        {
            repositorio
                .Setup(r => r.ObterPorProvaTurmaAsync(1, 1))
                .ReturnsAsync((IEnumerable<ProvaAlunoResultado>)null);

            var query = new ObterAlunosProvaTurmaQuery(1, 1);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deve_Retornar_Null_Quando_Repositorio_Retornar_Lista_Vazia()
        {
            repositorio
                .Setup(r => r.ObterPorProvaTurmaAsync(1, 1))
                .ReturnsAsync(new List<ProvaAlunoResultado>());

            var query = new ObterAlunosProvaTurmaQuery(1, 1);

            var resultado = await handler.Handle(query, CancellationToken.None);

            Assert.Null(resultado);
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Mapeada_Quando_Existirem_Registros()
        {
            var provaAluno = new ProvaAlunoResultado(
                provaId: 1,
                dreId: 10,
                ueId: 20,
                turmaId: 30,
                ano: "5A",
                modalidade: Modalidade.EF,
                anoLetivo: 2025,
                inicio: DateTime.Now.AddMonths(-1),
                fim: DateTime.Now.AddMonths(1),
                alunoId: 100,
                alunoRa: 123456,
                alunoNome: "João",
                alunoNomeSocial: null,
                situacao: 1,
                alunoDownload: true,
                alunoInicio: DateTime.Now.AddMinutes(-30),
                alunoFim: DateTime.Now,
                alunoTempo: 30,
                alunoQuestaoRespondida: 10,
                usuarioIdReabertura: "USR01",
                dataHoraReabertura: DateTime.Now,
                situacaoProvaAluno: Dominio.Enums.SituacaoProvaAluno.Finalizada
            );

            var dados = new List<ProvaAlunoResultado> { provaAluno };

            repositorio
                .Setup(r => r.ObterPorProvaTurmaAsync(1, 1))
                .ReturnsAsync(dados);

            var query = new ObterAlunosProvaTurmaQuery(1, 1);

            var resultado = (await handler.Handle(query, CancellationToken.None)).ToList();

            Assert.Single(resultado);
            Assert.Equal(provaAluno.AlunoNome, resultado[0].NomeEstudante);
            Assert.True(resultado[0].FezDownload);
            Assert.Equal(provaAluno.AlunoRa, resultado[0].Ra);
            Assert.Equal(provaAluno.AlunoQuestaoRespondida, resultado[0].QuestoesRespondidas);
            Assert.Equal(Dominio.Enums.SituacaoProvaAluno.Finalizada, resultado[0].SituacaoProvaAluno);
        }
    }
}
