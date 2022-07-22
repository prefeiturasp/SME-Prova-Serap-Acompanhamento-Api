using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SME.SERAp.Prova.Acompanhamento.Aplicacao;
using System;

namespace SME.SERAp.Prova.Acompanhamento.IoC.Extensions
{
    public static class RegistraMediatr
    {
        public static void AdicionarMediatr(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("SME.SERAp.Prova.Acompanhamento.Aplicacao");
            services.AddMediatR(assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidacoesPipeline<,>));
        }
    }
}
