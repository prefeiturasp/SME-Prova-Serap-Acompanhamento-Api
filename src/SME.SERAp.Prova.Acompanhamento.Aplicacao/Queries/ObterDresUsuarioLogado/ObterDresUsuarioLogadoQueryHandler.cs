using MediatR;
using Microsoft.AspNetCore.Http;
using SME.SERAp.Prova.Acompanhamento.Dominio.Constraints;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterDresUsuarioLogadoQueryHandler : IRequestHandler<ObterDresUsuarioLogadoQuery, long[]>
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public ObterDresUsuarioLogadoQueryHandler(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Task<long[]> Handle(ObterDresUsuarioLogadoQuery request, CancellationToken cancellationToken)
        {
            var grupoId = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(a => a.Type == "GRUPOID");
            if (grupoId == null) return default;

            if (Perfis.PerfilEhAdministrador(grupoId.Value))
                return Task.FromResult(Array.Empty<long>());

            var dresUesTurmas = httpContextAccessor.HttpContext?.User?.Claims?.Where(a => a.Type == "DRE-UE-TURMA").ToList();

            if (dresUesTurmas != null && dresUesTurmas.Any())
                return Task.FromResult(dresUesTurmas
                    .Select(t => long.Parse(t.Value.Split("-")[0]))
                    .Where(a => a > 0)
                    .Distinct()
                    .ToArray());

            return default;
        }
    }
}
