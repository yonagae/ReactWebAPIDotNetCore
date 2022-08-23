using AutoMapper;
using FinBY.Domain.Contracts;
using FinBY.Domain.Data.DTO;
using FinBY.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinBY.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(
            IMapper mapper,
            IMediator mediator,
            ILoggerManager logger,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
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
    }
}
