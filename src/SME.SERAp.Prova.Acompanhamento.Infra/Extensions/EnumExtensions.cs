using System;
using System.ComponentModel;
using System.Linq;

namespace SME.SERAp.Prova.Acompanhamento.Infra.Extensions
{
    public static class EnumExtensions
    {
        public static string Descricao(this Enum enumerador)
        {
            if (enumerador == null) return default;

            var fieldInfo = enumerador.GetType().GetField(enumerador.ToString());
            if (fieldInfo != null && (fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any()))
                return attributes.FirstOrDefault().Description;

            return enumerador.ToString();
        }
    }
}
