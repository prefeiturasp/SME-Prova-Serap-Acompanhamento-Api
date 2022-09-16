using MediatR;
using RabbitMQ.Client;
using SME.SERAp.Prova.Acompanhamento.Infra.Fila;
using SME.SERAp.Prova.Acompanhamento.Infra.Interfaces;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class PublicaFilaRabbitCommandHandler : IRequestHandler<PublicaFilaRabbitCommand, bool>
    {
        private readonly IModel modelRabbit;
        private readonly IServicoLog servicoLog;
        public PublicaFilaRabbitCommandHandler(IModel modelRabbit, IServicoLog servicoLog)
        {
            this.modelRabbit = modelRabbit ?? throw new ArgumentNullException(nameof(modelRabbit));
            this.servicoLog = servicoLog ?? throw new ArgumentNullException(nameof(servicoLog));
        }

        public Task<bool> Handle(PublicaFilaRabbitCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var mensagem = new MensagemRabbit(request.Mensagem, Guid.NewGuid());

                var mensagemJson = JsonSerializer.Serialize(mensagem);
                var body = Encoding.UTF8.GetBytes(mensagemJson);

                var props = modelRabbit.CreateBasicProperties();
                props.Persistent = true;
                modelRabbit.BasicPublish(ExchangeRabbit.SerapEstudante, request.Fila, props, body);

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                servicoLog.Registrar(ex);
                return Task.FromResult(false);
            }

        }
    }
}