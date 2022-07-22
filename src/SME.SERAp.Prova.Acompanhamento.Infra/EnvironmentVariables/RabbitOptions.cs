namespace SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables
{
    public class RabbitOptions
    {
        public static string Secao => "Rabbit";
        public string HostName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string VirtualHost { get; set; }
        public ushort LimiteDeMensagensPorExecucao { get; set; }
    }
}
