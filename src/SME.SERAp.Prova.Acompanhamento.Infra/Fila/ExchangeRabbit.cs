namespace SME.SERAp.Prova.Acompanhamento.Infra.Fila
{
    public class ExchangeRabbit
    {
        public static string Log => "EnterpriseApplicationLog";
        public static string SerapEstudanteAcompanhamento => "serap.estudante.acomp.workers";
        public static string SerapEstudanteAcompanhamentoDeadLetter => "serap.estudante.acomp.workers.deadletter";
    }
}
