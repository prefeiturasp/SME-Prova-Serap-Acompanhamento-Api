using Microsoft.AspNetCore.Http;
using Moq;
using SME.SERAp.Prova.Acompanhamento.Aplicacao.Queries;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace SME.SERAp.Prova.Acompanhamento.Aplicacao.Teste.Queries
{
    public class ObterAbrangenciaUsuarioLogadoPorClaimsQueryHandlerTeste
    {
        private readonly Mock<IHttpContextAccessor> httpContextAccessor;
        private readonly ObterAbrangenciaUsuarioLogadoPorClaimsQueryHandler handler;

        public ObterAbrangenciaUsuarioLogadoPorClaimsQueryHandlerTeste()
        {
            httpContextAccessor = new Mock<IHttpContextAccessor>();
            handler = new ObterAbrangenciaUsuarioLogadoPorClaimsQueryHandler(httpContextAccessor.Object);
        }

        [Fact]
        public async Task Deve_Retornar_Claims_Quando_Existirem()
        {
            var claims = new List<Claim>
            {
                new Claim("Tipo1", "Valor1"),
                new Claim("Tipo2", "Valor2"),
                new Claim("OutroTipo", "Valor3")
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var httpContext = new DefaultHttpContext { User = claimsPrincipal };

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var handler = new ObterAbrangenciaUsuarioLogadoPorClaimsQueryHandler(httpContextAccessorMock.Object);
            var request = new ObterAbrangenciaUsuarioLogadoPorClaimsQuery
            {
                Claims = new[] { "Tipo1", "Tipo2" }
            };

            var resultado = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(resultado);
            var lista = resultado.ToList();
            Assert.Equal(2, lista.Count);
            Assert.Contains(lista, p => p.Chave == "Tipo1" && p.Valor == "Valor1");
            Assert.Contains(lista, p => p.Chave == "Tipo2" && p.Valor == "Valor2");
        }

        [Fact]
        public async Task Deve_Retornar_Lista_Vazia_Quando_Nao_Houver_Claims_Correspondentes()
        {
            var claims = new List<Claim>
            {
                new Claim("OutroTipo", "Valor3")
            };

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims));
            var httpContext = new DefaultHttpContext { User = claimsPrincipal };

            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var handler = new ObterAbrangenciaUsuarioLogadoPorClaimsQueryHandler(httpContextAccessorMock.Object);
            var request = new ObterAbrangenciaUsuarioLogadoPorClaimsQuery
            {
                Claims = new[] { "Tipo1", "Tipo2" }
            };

            var resultado = await handler.Handle(request, CancellationToken.None);

            Assert.NotNull(resultado);
            Assert.Empty(resultado);
        }

        [Fact]
        public async Task Deve_Retornar_Nulo_Quando_HttpContext_For_Nulo()
        {
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext)null);

            var handler = new ObterAbrangenciaUsuarioLogadoPorClaimsQueryHandler(httpContextAccessorMock.Object);
            var request = new ObterAbrangenciaUsuarioLogadoPorClaimsQuery
            {
                Claims = new[] { "Tipo1", "Tipo2" }
            };

            var resultado = await handler.Handle(request, CancellationToken.None);

            Assert.Null(resultado);
        }
    }
}
