using System.Linq;
using System.Threading.Tasks;
using com.debtcalculator.API.Models;
using com.debtcalculator.Domain.Contracts.Repositories;
using com.debtcalculator.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace com.debtcalculator.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [Produces("application/json")]
    public class UserController : ControllerBase
    {
        private readonly IUserReadRepository _userReadRepository;

        private readonly IMediator _mediator;

        private readonly DadosSessaoDTO _dadosSessao;

        public UserController(IUserReadRepository userReadRepository, IMediator mediator, DadosSessaoDTO dadosSessao)
        {
            _userReadRepository = userReadRepository;
            _mediator = mediator;
            _dadosSessao = dadosSessao;
        }


        /// <summary>
        /// Lista todos os usuários.
        /// </summary>
        /// <returns>Uma lista de usuarios</returns>
        /// <response code="200">Usuários retornardos com sucesso</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _userReadRepository.GetAsync();
            return Ok(data.Select(d => d.ToVM()));
        }

        /// <summary>
        /// Lista todos os usuários paginados.
        /// </summary>
        /// <returns>Uma lista de usuarios</returns>
        /// <response code="200">Usuários retornardos com sucesso</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{skip}/{take}")]
        public async Task<IActionResult> GetAllPaginated(int skip, int take)
        {
            var data = await _userReadRepository.GetAsyncPaginated(skip, take);
            return Ok(
                new
                {
                    Data = data.Select(d => d.ToVM()),
                    Total = _userReadRepository.Total
                }
                );
        }

        /// <summary>
        /// Adiciona um novo usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     POST /Usuarios
        ///     {
        ///         "name": "string"
        ///         "email": "string",
        ///         "password": "string"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Novo usuário</returns>
        /// <response code="200">Retorna o novo usuário cadastrado</response>
        /// <response code="400">Caso o request seja inválido ou seja nulo</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Domain.Mediator.User.Add.Request request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            var result = response.Result as Domain.Entities.User;

            return Ok(result.ToVM());
        }

        /// <summary>
        /// Transforma usuário em administrador.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     POST /Usuarios
        ///     {
        ///         "id": 0
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Novo usuário</returns>
        /// <response code="200">Retorna o novo usuário administrador</response>
        /// <response code="400">Caso o request seja inválido ou seja nulo</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("Admin")]
        public async Task<IActionResult> PostAdmin([FromBody] Domain.Mediator.User.AddAdmin.Request request)
        {
            if (int.Parse(_dadosSessao.DadosDoUsuario.UsuarioId) == 2)
                return BadRequest("User must administrator to add administrator");

            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            var result = response.Result as Domain.Entities.User;

            return Ok(result.ToVM());
        }


        /// <summary>
        /// Troca a senha do usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     POST /Users
        ///     {
        ///         "email": "string",
        ///         "verificationCode": "string",
        ///         "newPassword: "string",
        ///         "grantType": "forgot_password"
        ///     }
        ///     ou
        ///     {
        ///         "email": "string",
        ///         "oldPassword": "string",
        ///         "newPassword: "string",
        ///         "grantType": "new_password"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Sucesso ao enviar o código</returns>
        /// <response code="200">Email enviado com sucesso</response>
        /// <response code="400">Caso o request seja inválido ou seja nulo</response>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] Domain.Mediator.User.ChangePassword.Request request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return Ok();
        }

        /// <summary>
        /// Envia um código de verificação para o usuário.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     POST /Users
        ///     {
        ///         "email": "string"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Sucesso ao enviar o código</returns>
        /// <response code="200">Email enviado com sucesso</response>
        /// <response code="400">Caso o request seja inválido ou seja nulo</response>
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] Domain.Mediator.User.ForgotPassword.Request request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return Ok();
        }


        /// <summary>
        /// Edita um usuário existente.
        /// </summary>
        /// <remarks>
        /// Exemplo de requisição:
        ///     PUT /User
        ///     {
        ///         "id": 0,
        ///         "name": "string"
        ///         "email": "string"
        ///     }
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>Usuário editado</returns>
        /// <response code="200">Retorna o usuário editado</response>
        /// <response code="400">Caso o request seja nulo ou o email já existente</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Domain.Mediator.User.Update.Request request)
        {
            var response = await _mediator.Send(request).ConfigureAwait(false);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            var result = response.Result as Domain.Entities.User;

            return Ok(result.ToVM());
        }

        /// <summary>
        /// Exclui um usuário existente.
        /// </summary>
        /// <param name="id">id do usuário a ser excluido</param>
        /// <returns>Usuário excluido</returns>
        /// <response code="200">Usuário excluido</response>
        /// <response code="400">Caso o id seja nulo ou não inexistente</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> Delete(long id)
        {
            var request = new Domain.Mediator.User.Delete.Request();
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