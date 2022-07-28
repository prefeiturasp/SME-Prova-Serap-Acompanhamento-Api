using System;
using System.Collections.Generic;

namespace SME.SERAp.Prova.Acompanhamento.Dominio.Entities
{
    public class Autenticacao : EntidadeBase
    {
        public Autenticacao(Guid codigo, IEnumerable<Abrangencia> abrangencias)
        {
            Codigo = codigo;
            Abrangencias = abrangencias;
            ExpiraEm = DateTime.Now.AddMinutes(1);
        }

        public Guid Codigo { get; set; }
        public IEnumerable<Abrangencia> Abrangencias { get; set; }
        public DateTime ExpiraEm { get; set; }
    }
}
