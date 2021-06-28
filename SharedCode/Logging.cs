using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;

namespace SharedCode {
    public static class Logging {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger => (hostContext, loggerConf) => {
            loggerConf.MinimumLevel.Information()
             .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
             .MinimumLevel.Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Warning);

            if (hostContext.HostingEnvironment.IsDevelopment()) {
                //something
            }
            var elasticUrl = hostContext.Configuration["ElasticUrl"];
            if (!string.IsNullOrEmpty(elasticUrl)) {
                loggerConf.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUrl)) {
                    AutoRegisterTemplate = true,
                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7,
                    IndexFormat = "logs-{0:yyyy.MM.dd}",
                    MinimumLogEventLevel = Serilog.Events.LogEventLevel.Information

                });
            }

        };
    }
}
