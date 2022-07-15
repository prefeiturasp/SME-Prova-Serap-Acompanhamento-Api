namespace SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables
{
    public class RabbitLogOptions
    {
        public static string Secao => "RabbitLog";
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public ushort LimiteDeMensagensPorExecucao { get; set; }
    }
}
