using MediatR;
using Microsoft.IdentityModel.Tokens;
using SME.SERAp.Prova.Acompanhamento.Dominio.Entities;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Acompanhamento.Infra.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterInformacoesPorTokenJwtQueryHandler : IRequestHandler<ObterInformacoesPorTokenJwtQuery, IEnumerable<Abrangencia>>
    {
        private readonly JwtOptions jwtOptions;

        public ObterInformacoesPorTokenJwtQueryHandler(JwtOptions jwtOptions)
        {
            this.jwtOptions = jwtOptions ?? throw new System.ArgumentNullException(nameof(jwtOptions));
        }

        public Task<IEnumerable<Abrangencia>> Handle(ObterInformacoesPorTokenJwtQuery request, CancellationToken cancellationToken)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.IssuerSigningKey));
            var validator = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = key,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateLifetime = false
            };
            try
            {
                if (validator.CanReadToken(request.Token))
                {
                    ClaimsPrincipal principal;
                    principal = validator.ValidateToken(request.Token, validationParameters, out SecurityToken validatedToken);

                    if (principal.HasClaim(c => c.Type == "LOGIN") &&
                        principal.HasClaim(c => c.Type == "USUARIO") &&
                        principal.HasClaim(c => c.Type == "CORESSOID") &&
                        principal.HasClaim(c => c.Type == "GRUPO") &&
                        principal.HasClaim(c => c.Type == "DRE-UE-TURMA"))
                    {
                        var login = principal.Claims.FirstOrDefault(c => c.Type == "LOGIN").Value;
                        var usuario = principal.Claims.FirstOrDefault(c => c.Type == "USUARIO").Value;
                        var coressoId = principal.Claims.FirstOrDefault(c => c.Type == "CORESSOID").Value;
                        var grupo = principal.Claims.FirstOrDefault(c => c.Type == "GRUPO").Value;

                        var abrangencias = new List<Abrangencia>();
                        foreach (var dreUeTurma in principal.Claims.Where(t => t.Type == "DRE-UE-TURMA"))
                        {
                            var codigos = dreUeTurma.Value.Split("-");
                            abrangencias.Add(new Abrangencia(0, login, usuario, coressoId, grupo, long.Parse(codigos[0]), long.Parse(codigos[1]), long.Parse(codigos[2])));
                        }

                        return Task.FromResult(abrangencias.AsEnumerable());
                    }
                }

                throw new NaoAutorizadoException("Token inválido");
            }
            catch (Exception)
            {
                throw new NaoAutorizadoException("Token inválido");
            }
        }
    }
}