using Elastic.Apm;
using Microsoft.AspNetCore.Mvc.Filters;
using SME.SERAp.Prova.Acompanhamento.Api.ViewModels;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.Exceptions;
using SME.SERAp.Prova.Acompanhamento.Infra.Interfaces;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using static SME.SERAp.Prova.Acompanhamento.Infra.Services.ServicoLog;

namespace SME.SERAp.Prova.Acompanhamento.Api.Filters
{
    public class FiltroExcecoesAttribute : ExceptionFilterAttribute
    {
        private readonly IServicoLog servicoLog;

        public FiltroExcecoesAttribute(IServicoLog servicoLog)
        {
            this.servicoLog = servicoLog;
        }

        public override void OnException(ExceptionContext context)
        {
            var internalIP = string.Join(", ", Dns.GetHostEntry(Dns.GetHostName()).AddressList?.Where(c => c.AddressFamily == AddressFamily.InterNetwork));

            switch (context.Exception)
            {
                case NegocioException negocioException:
                    servicoLog.Registrar(LogNivel.Negocio, context.Exception.Message, internalIP, context.Exception.StackTrace);
                    context.Result = new ResultadoBaseResult(context.Exception.Message, negocioException.StatusCode);
                    break;
                case ValidacaoException validacaoException:
                    var observacao = $"IPs: {internalIP}, Erros: {string.Join(", ", validacaoException.Mensagens())}";
                    servicoLog.Registrar(LogNivel.Negocio, context.Exception.Message, observacao, context.Exception.StackTrace);
                    context.Result = new ResultadoBaseResult(new RetornoBaseDto(validacaoException.Erros));
                    break;
                default:
                    servicoLog.Registrar(LogNivel.Critico, context.Exception.Message, internalIP, context.Exception.StackTrace);
                    context.Result = new ResultadoBaseResult("Ocorreu um erro interno. Favor contatar o suporte.", 500);
                    break;
            }

            Agent.Tracer.CurrentTransaction?.CaptureException(context.Exception);

            base.OnException(context);
        }
    }
}
