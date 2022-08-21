﻿using MediatR;
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
            var coressoid = httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(a => a.Type == "CORESSOID");
            if (coressoid == null) return default;

            if (Perfis.PerfilEhAdministrador(coressoid.Value))
                return Task.FromResult(Array.Empty<long>());

            var dresUesTurmas = httpContextAccessor.HttpContext?.User?.Claims?.Where(a => a.Type == "DRE-UE-TURMA").ToList();

            if (dresUesTurmas != null && dresUesTurmas.Any())
                return Task.FromResult(dresUesTurmas
                    .Select(t => long.Parse(t.Value.Split("-")[0]))
                    .ToArray());

            return default;
        }
    }
}
