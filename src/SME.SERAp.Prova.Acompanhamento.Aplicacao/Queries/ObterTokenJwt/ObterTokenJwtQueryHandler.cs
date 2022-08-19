using MediatR;
using Microsoft.IdentityModel.Tokens;
using SME.SERAp.Prova.Acompanhamento.Infra.Dtos;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao
{
    public class ObterTokenJwtQueryHandler : IRequestHandler<ObterTokenJwtQuery, AutenticacaoRetornoDto>
    {
        private readonly JwtOptions jwtOptions;

        public ObterTokenJwtQueryHandler(JwtOptions jwtOptions)
        {
            this.jwtOptions = jwtOptions ?? throw new ArgumentNullException(nameof(jwtOptions));
        }

        public Task<AutenticacaoRetornoDto> Handle(ObterTokenJwtQuery request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now;
            var claims = new List<Claim>
            {
                new Claim("LOGIN", request.Abrangencias.FirstOrDefault().Login),
                new Claim("USUARIO", request.Abrangencias.FirstOrDefault().Usuario),
                new Claim("CORESSOID", request.Abrangencias.FirstOrDefault().CoressoId),
                new Claim("GRUPO", request.Abrangencias.FirstOrDefault().Grupo)
            };

            foreach (var abrangencia in request.Abrangencias)
                claims.Add(new Claim("DRE-UE-TURMA", $"{abrangencia.DreId}-{abrangencia.UeId}-{abrangencia.TurmaId}"));

            var dataHoraExpiracao = now.AddMinutes(double.Parse(jwtOptions.ExpiresInMinutes));

            var token = new JwtSecurityToken(
                issuer: jwtOptions.Issuer,
                audience: jwtOptions.Audience,
                notBefore: now,
                claims: claims,
                expires: dataHoraExpiracao,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                        System.Text.Encoding.UTF8.GetBytes(jwtOptions.IssuerSigningKey)),
                        SecurityAlgorithms.HmacSha256)
                );

            var tokenGerado = new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult(new AutenticacaoRetornoDto(tokenGerado, dataHoraExpiracao));
        }
    }
}
