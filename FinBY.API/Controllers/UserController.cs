using AutoMapper;
using FinBY.Domain.Contracts;
using FinBY.Domain.Data.DTO;
using FinBY.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinBY.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private ILoggerManager _logger;
        private IUnitOfWork _unitOfWork;
        private IMediator _mediator;
        private IMapper _mapper;
        private ILoginService _loginService;

        public UserController(
            IMapper mapper,
            IMediator mediator,
            ILoggerManager logger,
            IUnitOfWork unitOfWork,
            ILoginService loginService)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _unitOfWork = unitOfWork;
            _loginService = loginService; ;
        }

        [HttpGet]
        [Authorize("Bearer")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var transactionTypes = await _unitOfWork.UserRepository.GetAllAsync();
                var tsDTO = _mapper.Map<List<UserDTO>>(transactionTypes);
                return Ok(tsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the User get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [Route("signin")]
        public IActionResult SignIn([FromBody] UserLoginDTO user)
        {
            if (user == null) return BadRequest("Ivalid client request");

            var token = _loginService.ValidateCredentials(user);

            if (token == null) return Unauthorized();
            return Ok(token);
        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh([FromBody] TokenDTO tokenVo)
        {
            if (tokenVo is null) return BadRequest("Ivalid client request");

            var token = _loginService.ValidateCredentials(tokenVo);

            if (token == null) return BadRequest("Ivalid client request");
            return Ok(token);
        }


        [HttpGet]
        [Route("revoke")]
        //[Authorize("Bearer")]
        public IActionResult Revoke()
        {
            var username = User.Identity.Name;
            var result = _loginService.RevokeToken(username);

            if (!result) return BadRequest("Ivalid client request");
            return NoContent();
        }
    }
}
