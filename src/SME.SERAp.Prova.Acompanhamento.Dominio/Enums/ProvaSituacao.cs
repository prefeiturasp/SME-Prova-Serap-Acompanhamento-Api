using System.ComponentModel;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Enums
{
    public enum ProvaSituacao
    {
        NaoCadastrado = 0,

        [Description("Provas em andamento")]
        EmAndamento = 1,
        [Description("Provas concluídas")]
        Concluida = 2
    }
}
