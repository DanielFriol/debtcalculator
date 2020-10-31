using com.debtcalculator.Domain.Contracts.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace com.debtcalculator.API.Controllers
{
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class TestController
    {
        private readonly ILogAPI _log;

        public TestController(ILogAPI log)
        {
            _log = log;
        }

        /// <summary>
        /// Testa a conectividade com a api.
        /// </summary>
        /// <returns>string com a data do server</returns>
        /// <response code="200">Conectividade OK</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("ping")]
        public string Ping()
        {
            _log.Info($"API tested at {System.DateTime.Now}");
            return $"Pong - {System.DateTime.Now}";
        }
    }
}