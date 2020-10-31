using System;
using System.IO;
using System.Threading.Tasks;
using com.debtcalculator.Domain.Contracts.Infra;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace com.debtcalculator.API.Infra
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogAPI _log;

        public GlobalExceptionHandlerMiddleware(ILogAPI log)
        {
            _log = log;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _log.Error($"{ex}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var resp = string.Empty;
            if (exception.InnerException != null)
            {
                if (exception.InnerException.InnerException != null)
                {
                    resp = exception.InnerException.InnerException.Message;
                }
                resp += exception.InnerException.Message;
            }
            else
                resp = exception.Message;

            var erro = new
            {
                msg = resp,
                exception = exception.GetType()
            };

            var verb = context.Request.Method;
            var msgLog = $"Requisição: {verb} {context.Request.Path}" +
                           $"{Environment.NewLine}Erro: {JsonConvert.SerializeObject(erro)}";

            if (verb.ToLower() == "post" || verb.ToLower() == "put")
            {
                using (var reader = new StreamReader(context.Request.Body))
                {
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                    _log.BodyContent = reader.ReadToEnd();
                }
            }

            _log.Error(msgLog);
            context.Response.StatusCode = 500;
            return context.Response.WriteAsync(msgLog);
        }
    }
}