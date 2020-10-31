using com.debtcalculator.Domain.Contracts.Infra;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace com.debtcalculator.API.Infra
{
    public class SerilogFileLog : ILogAPI
    {
        private readonly Logger _logger;
        private string bodyContent;

        public string BodyContent
        {
            get
            {
                if (!string.IsNullOrEmpty(bodyContent))
                    return $"{System.Environment.NewLine}Body: {bodyContent}";

                return string.Empty;
            }
            set => bodyContent = value;
        }

        public SerilogFileLog(IConfiguration config)
        {
            _logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();
        }

        public void Error(string msg)
        {
            _logger.Error(msg + BodyContent);
        }

        public void Warnning(string msg)
        {
            _logger.Warning(msg + BodyContent);
        }

        public void Info(string msg)
        {
            _logger.Information(msg + BodyContent);
        }

        public void Debug(string msg)
        {
            _logger.Debug(msg + BodyContent);
        }
    }
}