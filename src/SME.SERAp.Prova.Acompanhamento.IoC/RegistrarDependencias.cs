using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.UseCases;
using SME.SERAp.Prova.Acompanhamento.Dados;
using SME.SERAp.Prova.Acompanhamento.Dados.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Dados.Repositories;
using SME.SERAp.Prova.Acompanhamento.Infra.Interfaces;
using SME.SERAp.Prova.Acompanhamento.Infra.Services;
using SME.SERAp.Prova.Acompanhamento.IoC.Extensions;

namespace SME.SERAp.Prova.Acompanhamento.IoC
{
    public static class RegistraDependencias
    {
        public static void Registrar(IServiceCollection services)
        {
            services.AdicionarMediatr();
            services.AdicionarValidadoresFluentValidation();
            RegistrarServicos(services);
            RegistrarRepositorios(services);
            RegistrarCasosDeUso(services);
        }

        private static void RegistrarServicos(IServiceCollection services)
        {
            services.TryAddScoped<IServicoTelemetria, ServicoTelemetria>();
            services.TryAddScoped<IServicoLog, ServicoLog>();
        }

        private static void RegistrarRepositorios(IServiceCollection services)
        {
            services.AddScoped<IRepositorioAutenticacao, RepositorioAutenticacao>();
            services.AddScoped<IRepositorioAbrangencia, RepositorioAbrangencia>();
            services.AddScoped<IRepositorioDre, RepositorioDre>();
            services.AddScoped<IRepositorioUe, RepositorioUe>();
            services.AddScoped<IRepositorioProva, RepositorioProva>();
            services.AddScoped<IRepositorioAno, RepositorioAno>();
            services.AddScoped<IRepositorioTurma, RepositorioTurma>();
            services.AddScoped<IRepositorioProvaAlunoResultado, RepositorioProvaAlunoResultado>();
        }

        private static void RegistrarCasosDeUso(IServiceCollection services)
        {
            services.AddScoped<IAutenticacaoUseCase, AutenticacaoUseCase>();
            services.AddScoped<IAutenticacaoValidarUseCase, AutenticacaoValidarUseCase>();
            services.AddScoped<IAutenticacaoRevalidarUseCase, AutenticacaoRevalidarUseCase>();
            services.AddScoped<IObterAnoLetivoUseCase, ObterAnoLetivoUseCase>();
            services.AddScoped<IObterSituacaoProvaUseCase, ObterSituacaoProvaUseCase>();
            services.AddScoped<IObterProvaUseCase, ObterProvaUseCase>();
            services.AddScoped<IObterModalidadeUseCase, ObterModalidadeUseCase>();
            services.AddScoped<IObterDreUsuarioLogadoUseCase, ObterDreUsuarioLogadoUseCase>();
            services.AddScoped<IObterUeUsuarioLogadoUseCase, ObterUeUsuarioLogadoUseCase>();
            services.AddScoped<IObterAnosUseCase, ObterAnosUseCase>();
            services.AddScoped<IObterTurmasUseCase, ObterTurmasUseCase>();
            services.AddScoped<IObterAlunosProvaTurmaUseCase, ObterAlunosProvaTurmaUseCase>();
            services.AddScoped<IObterTotaisProvasUseCase, ObterTotaisProvasUseCase>();

        }
    }
}
