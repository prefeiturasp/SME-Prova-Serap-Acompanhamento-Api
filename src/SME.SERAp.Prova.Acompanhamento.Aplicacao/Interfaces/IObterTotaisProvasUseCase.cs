using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Interfaces
{
 public interface IObterTotaisProvasUseCase
    {
        Task<List<TotalDto>> Executar(FiltroDto filtro);
    }
}
