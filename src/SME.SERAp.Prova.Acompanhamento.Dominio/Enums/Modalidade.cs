using System.ComponentModel;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Enums
{
    public enum Modalidade
    {

        NaoCadastrado = 0,

        [Description("Educação Infantil")]
        EI = 1,

        [Description("Educação de Jovens e Adultos")]
        EJA = 3,

        [Description("CIEJA")]
        CIEJA = 4,

        [Description("Ensino Fundamental")]
        EF = 5,

        [Description("Ensino Médio")]
        EM = 6,

        [Description("CMCT")]
        CMCT = 7,

        [Description("MOVA")]
        MOVA = 8,

        [Description("ETEC")]
        ETEC = 9
    }
}
