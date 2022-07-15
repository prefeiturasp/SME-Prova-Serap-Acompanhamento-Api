using SME.SERAp.Prova.Acompanhamento.Infra.Services;
using System;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Interfaces
{
    public interface IServicoLog
    {
        void Registrar(ServicoLog.LogMensagem log);
        void Registrar(Exception ex);
        void Registrar(ServicoLog.LogNivel nivel, Exception ex);
        void Registrar(string mensagem, Exception ex);
        void Registrar(ServicoLog.LogNivel nivel, string erro, string observacoes, string stackTrace);
    }
}
