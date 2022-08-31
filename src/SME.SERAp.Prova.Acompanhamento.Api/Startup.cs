using Elastic.Apm.AspNetCore;
using Elastic.Apm.DiagnosticSource;
using Elastic.Apm.SqlClient;
using Elastic.Apm.StackExchange.Redis;
using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Nest;
using RabbitMQ.Client;
using SME.SERAp.Prova.Acompanhamento.Api.Configurations;
using SME.SERAp.Prova.Acompanhamento.Infra.EnvironmentVariables;
using SME.SERAp.Prova.Acompanhamento.IoC;
using StackExchange.Redis;
using System;

namespace SME.SERAp.Prova.Acompanhamento.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddHttpContextAccessor();

            ConfigEnvoiromentVariables(services);

            RegistraDependencias.Registrar(services);
            RegistraAutenticacao.Registrar(services, Configuration);
            RegistraDocumentacaoSwagger.Registrar(services);
            RegistraMvc.Registrar(services);
        }

        private void ConfigEnvoiromentVariables(IServiceCollection services)
        {
            var rabbitOptions = new RabbitOptions();
            Configuration.GetSection(RabbitOptions.Secao).Bind(rabbitOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(rabbitOptions);

            var factory = new ConnectionFactory
            {
                HostName = rabbitOptions.HostName,
                UserName = rabbitOptions.UserName,
                Password = rabbitOptions.Password,
                VirtualHost = rabbitOptions.VirtualHost
            };

            services.AddSingleton(factory);

            var conexaoRabbit = factory.CreateConnection();
            IModel channel = conexaoRabbit.CreateModel();

            var elasticOptions = new ElasticOptions();
            Configuration.GetSection(ElasticOptions.Secao).Bind(elasticOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(elasticOptions);

            var nodes = new Uri[]
            {
                new Uri(elasticOptions.Url),
            };

            var connectionPool = new StaticConnectionPool(nodes);
            var connectionSettings = new ConnectionSettings(connectionPool);
            connectionSettings.DefaultIndex(elasticOptions.DefaultIndex);

            if (!string.IsNullOrEmpty(elasticOptions.CertificateFingerprint))
                connectionSettings.CertificateFingerprint(elasticOptions.CertificateFingerprint);

            if (!string.IsNullOrEmpty(elasticOptions.Username) && !string.IsNullOrEmpty(elasticOptions.Password))
                connectionSettings.BasicAuthentication(elasticOptions.Username, elasticOptions.Password);

            var elasticClient = new ElasticClient(connectionSettings);
            services.AddSingleton<IElasticClient>(elasticClient);

            var rabbitLogOptions = new RabbitLogOptions();
            Configuration.GetSection(RabbitLogOptions.Secao).Bind(rabbitLogOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(rabbitLogOptions);

            var factoryLog = new ConnectionFactory
            {
                HostName = rabbitOptions.HostName,
                UserName = rabbitOptions.UserName,
                Password = rabbitOptions.Password,
                VirtualHost = rabbitOptions.VirtualHost
            };

            var conexaoRabbitLog = factoryLog.CreateConnection();
            IModel channelLog = conexaoRabbitLog.CreateModel();

            var redisOptions = new RedisOptions();
            Configuration.GetSection(RedisOptions.Secao).Bind(redisOptions, c => c.BindNonPublicProperties = true);

            var redisConfigurationOptions = new ConfigurationOptions()
            {
                Proxy = redisOptions.Proxy,
                SyncTimeout = redisOptions.SyncTimeout,
                EndPoints = { redisOptions.Endpoint }
            };
            var muxer = ConnectionMultiplexer.Connect(redisConfigurationOptions);
            services.AddSingleton<IConnectionMultiplexer>(muxer);

            var telemetriaOptions = new TelemetriaOptions();
            Configuration.GetSection(TelemetriaOptions.Secao).Bind(telemetriaOptions, c => c.BindNonPublicProperties = true);
            services.AddSingleton(telemetriaOptions);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseElasticApm(Configuration,
              new SqlClientDiagnosticSubscriber(),
              new HttpDiagnosticsSubscriber());

            var muxer = app.ApplicationServices.GetService<IConnectionMultiplexer>();
            muxer.UseElasticApm();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SME.SERAp.Prova.Acompanhamento.Api v1"));

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCors(builder => builder
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
