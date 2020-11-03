using System.Linq;
using System.Threading.Tasks;
using com.debtcalculator.API.Models;
using com.debtcalculator.Domain.Contracts.Repositories;
using com.debtcalculator.Domain.DTOs;
using com.debtcalculator.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace com.debtcalculator.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class DebtController : ControllerBase
    {
        private readonly IUserReadRepository _userReadRepository;
        private readonly IMediator _mediator;
        private readonly IDebtReadRepository _debtReadRepository;
        private readonly IDebtWriteRepository _debtWriteRepository;
        private readonly DadosSessaoDTO _dadosSessao;

        public DebtController(IUserReadRepository userReadRepository, IMediator mediator, IDebtReadRepository debtReadRepository, IDebtWriteRepository debtWriteRepository, DadosSessaoDTO dadosSessao)
        {
            _userReadRepository = userReadRepository;
            _mediator = mediator;
            _debtReadRepository = debtReadRepository;
            _debtWriteRepository = debtWriteRepository;
            _dadosSessao = dadosSessao;
        }

        /// <summary>
        /// Lista todos as dividas paginadas.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>Uma lista de dividas</returns>
        /// <response code="200">Dividas retornardas com sucesso</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{skip}/{take}")]
        public async Task<IActionResult> GetAllPaginated(int skip, int take)
        {
            if (_dadosSessao.DadosDoUsuario.IdProfile == (int)UserProfile.User)
                return Unauthorized("User must administrator to see all debts");

            var data = await _debtReadRepository.GetAllPaginated(skip, take);
            return Ok(
                new
                {
                    Data = data.Select(d => d.ToVMSimple()),
                    Total = _debtReadRepository.Total
                }
                );
        }


        /// <summary>
        /// Retorna informações da divida. Retorna as informações calculada da divida.
        /// </summary>
        /// <param name="debtId"></param>
        /// <returns>Uma lista de dividas</returns>
        /// <response code="200">Divida retornarda com sucesso</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetDebtInfo/{debtId}")]
        public async Task<IActionResult> GetAllPaginated(long debtId)
        {

            var request = new Domain.Mediator.Debt.ResfreshData.Request();
            request.Id = debtId;
            var response = await _mediator.Send(request).ConfigureAwait(false);
            if (response.Errors.Any())
                return BadRequest(response.Errors);

            return Ok(
                new
                {
                    response.Result
                }
                );
        }


        /// <summary>
        /// Retorna informações da divida.
        /// </summary>
        /// <param name="debtId"></param>
        /// <returns>Informações da divida</returns>
        /// <response code="200">Informações retornardas com sucesso</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{debtId}")]
        public async Task<IActionResult> GetInformation(long debtId)
        {
            if (_dadosSessao.DadosDoUsuario.IdProfile == (int)UserProfile.User)
                return Unauthorized("User must administrator to see all debts");

            var debt = await _debtReadRepository.GetAsync(debtId);
            if (debt == null)
                return BadRequest("Debt does not exists");

            return Ok(debt);
        }

        /// <summary>
        /// Lista todos as dividas paginadas por cliente.
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <returns>Uma lista de dividas</returns>
        /// <response code="200">Dividas retornardas com sucesso</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("GetAllByClient/{skip}/{take}")]
        public async Task<IActionResult> GetAllByClient(int skip, int take)
        {
            var user = await _userReadRepository.GetAsync(long.Parse(_dadosSessao.DadosDoUsuario.UsuarioId));
            if (user == null)
                return BadRequest();

            var data = await _debtReadRepository.GetAllPaginatedByCPF(user.CPF, skip, take);
            return Ok(
                new
                {
                    Data = data.Select(d => d.ToVMSimple()),
                    Total = _debtReadRepository.Total
                }
                );
        }

        /// <summary>
        /// Adiciona uma novo divida.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     POST
        ///     {
        ///         "cLientCPF": "string"
        ///         "value": 0,
        ///         "dueDate": "2020-03-01",
        ///         "contactPhone": "string"
        ///         "maxSplit": 0,
        ///         "interestType": 0,
        ///         "interest": 0,
        ///         "paschoalottoPercentage": 0
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Nova divida</returns>
        /// <response code="200">Retorna a divida cadastrada</response>
        /// <response code="400">Caso o request seja inválido ou seja nulo</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Domain.Mediator.Debt.Add.Request request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            var result = response.Result as Domain.Entities.Debt;

            return Ok(result.ToVMSimple());
        }


        /// <summary>
        /// Edita configuração de uma divida.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     POST
        ///     {
        ///         "maxSplit": 0,
        ///         "interestType": 0,
        ///         "interest": 0,
        ///         "paschoalottoPercentage": 0
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Divida configurada</returns>
        /// <response code="200">Retorna a divida configurada</response>
        /// <response code="400">Caso o request seja inválido ou seja nulo</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Domain.Mediator.Debt.UpdateConfig.Request request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            var result = response.Result as Domain.Entities.Debt;

            return Ok(result.ToVMSimple());
        }

        /// <summary>
        /// Finaliza uma dívida
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     POST
        ///     {
        ///         "id": "string"
        ///     }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>Nova divida</returns>
        /// <response code="200"></response>
        /// <response code="400">Caso o request seja inválido ou seja nulo</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var request = new Domain.Mediator.Debt.Delete.Request();
            request.Id = id;
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return Ok();
        }





    }
}