using AutoMapper;
using FinBY.Domain.Commands;
using FinBY.Domain.Contracts;
using FinBY.API.Data.DTO;
using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinBY.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/transactionTypes")]
    [ApiController]
    public class TransactionTypeController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private ITransactionTypeRepository _transactionTypeRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TransactionTypeController(
            IMapper mapper,
            IMediator mediator,
            ILoggerManager logger,
            ITransactionTypeRepository transactionTypeRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _transactionTypeRepository = transactionTypeRepository;
        }

        [HttpGet]
        [HttpGet]
        [ProducesResponseType(typeof(List<TransactionTypeDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTransactionTypes()
        {
            try
            {
                var transactionTypes = await _transactionTypeRepository.GetAllAsync();
                var tsDTO = _mapper.Map<List<TransactionTypeDTO>>(transactionTypes);
                return Ok(tsDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the TransactionType get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        [ProducesResponseType(typeof(TransactionTypeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTransactionType([FromBody] NewTransactionTypeDTO transactionType)
        {
            try
            {
                if (transactionType == null)
                {
                    return BadRequest("Owner object is null");
                }

                var tt = _mapper.Map<TransactionType>(transactionType);
                var response = await _mediator.Send(new CreateTransactionTypeCommand(tt));

                if(!response.Success)
                    return StatusCode(500, "Internal server error");

                var typeDTO = _mapper.Map<TransactionTypeDTO>(response.Data);
                return Ok(typeDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the CreateOwner action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TransactionTypeDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTransactionType(long id, [FromBody] TransactionTypeDTO transactionType)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid model object");
                }

                var tt = _mapper.Map<TransactionType>(transactionType);
                var response = await _mediator.Send(new UpdateTransactionTypeCommand(tt));         

                if (response.Success)
                {
                    var typeDTO = _mapper.Map<TransactionTypeDTO>(response.Data);
                    return Ok(typeDTO);
                }
                else
                {
                    return BadRequest($"Transaction type with id {transactionType.Id} was not updated");
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTransactionType(int id)
        {
            try
            {
                 var response = await _mediator.Send(new DeleteTransactionTypeCommand(id));

                if (response.DataNotFound)
                {
                    return NotFound($"Transaction type with id {id} not found");
                }

                if (response.Success)
                {
                    // Return a response message with status code 204 (No Content)
                    // To indicate that the operation was successful
                    return NoContent();
                }
                else
                {
                    // Otherwise return a 400 (Bad Request) error response
                    return BadRequest($"Transaction type with id {id} not deleted");
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
