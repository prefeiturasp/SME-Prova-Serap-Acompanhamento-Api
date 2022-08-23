using System.ComponentModel;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Enums
{
    public enum Modalidade
    {

        NaoCadastrado = 0,

        [Description("Educação de Jovens e Adultos")]
        EJA = 3,

        [Description("Ensino Fundamental")]
        EF = 5,

        [Description("Ensino Médio")]
        EM = 6,
    }
}
