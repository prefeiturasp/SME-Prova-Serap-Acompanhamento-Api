using System.ComponentModel;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Enums
{
    public enum Turno
    {
        [Description("Manhã")]
        Manha = 1,
        [Description("Intermediário")]
        Intermediario = 2,
        [Description("Tarde")]
        Tarde = 3,
        [Description("Vespertino")]
        Vespertino = 4,
        [Description("Noite")]
        Noite = 5,
        [Description("Integral")]
        Integral = 6,
    }
}
