using MediatR;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries;
using SME.SERAp.Prova.Acompanhamento.Dominio.Enums;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases
{
    public class ReabrirProvaAlunoUseCase : AbstractUseCase, IReabrirProvaAlunoUseCase
    {
        public ReabrirProvaAlunoUseCase(IMediator mediator) : base(mediator)
        {
        }

        public async Task<bool> Executar(ReabrirProvaAlunoDto reabrirProvaDto)
        {
            var claims = await mediator.Send(new ObterAbrangenciaUsuarioLogadoPorClaimsQuery("GRUPOID", "LOGIN", "USUARIOID"));
            var perfil = claims.FirstOrDefault(a => a.Chave == "GRUPOID")?.Valor;
            var login = claims.FirstOrDefault(a => a.Chave == "LOGIN")?.Valor;
            var usuarioId = claims.FirstOrDefault(a => a.Chave == "USUARIOID")?.Valor;

            var provaAlunoResultados = await mediator.Send(new ObterProvaAlunoResultadoPorProvaIdAlunoRaQuery(reabrirProvaDto.ProvaId, reabrirProvaDto.AlunoRa));
            if (provaAlunoResultados == null || !provaAlunoResultados.Any()) throw new Exception($"Não foi encontrado resultado para a prova: {reabrirProvaDto.ProvaId} e aluno: {reabrirProvaDto.AlunoRa}. ");

            foreach (var resultado in provaAlunoResultados)
            {
                resultado.SituacaoProvaAluno = SituacaoProvaAluno.Reabrindo;
                await mediator.Send(new AlterarProvaAlunoResultadoCommand(resultado));
            }

            var provaAlunoReabertura = new ProvaAlunoReaberturaDto
            {
                AlunoRA = reabrirProvaDto.AlunoRa,
                ProvaId = reabrirProvaDto.ProvaId,
                GrupoCoresso = Guid.Parse(perfil),
                LoginCoresso = login.ToString(),
                UsuarioCoresso = Guid.Parse(usuarioId)
            };
            await mediator.Send(new PublicaFilaRabbitCommand(RotaRabbit.TratarReaberturaProvaAluno, provaAlunoReabertura));
            return true;
        }
    }
}