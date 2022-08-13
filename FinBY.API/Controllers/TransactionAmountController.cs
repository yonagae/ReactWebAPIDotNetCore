using AutoMapper;
using FinBY.Domain.Commands;
using FinBY.Domain.Contracts;
using FinBY.Domain.Data.DTO;
using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FinBY.API.Controllers
{
    [Route("api/TransactionAmounts")]
    [ApiController]
    public class TransactionAmountController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private ITransactionAmountRepository _transactionAmountRepository;
        private ITransactionRepository _transactionRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public TransactionAmountController(
            IMapper mapper,
            IMediator mediator,
            ILoggerManager logger,
            ITransactionAmountRepository TransactionAmountRepository,
            ITransactionRepository TransactionRepository)
        {
            _mapper = mapper;
            _mediator = mediator;
            _logger = logger;
            _transactionRepository = TransactionRepository;
            _transactionAmountRepository = TransactionAmountRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionAmountsById(int id)
        {
            try
            {
                var transactionAmount = await _transactionAmountRepository.SelectByIdAsync(id);
                var transactionAmountDTO = _mapper.Map<TransactionAmount>(transactionAmount);
                return Ok(transactionAmountDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the TransactionAmount get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]  
        public async Task<IActionResult> CreateTransactionAmount([FromBody] NewTransactionAmountDTO TransactionAmount)
        {
            try
            {
                if (TransactionAmount == null)
                {
                    return BadRequest("Owner object is null");
                }

                var ta = _mapper.Map<TransactionAmount>(TransactionAmount);
                var response = await _mediator.Send(new CreateTransactionAmountCommand(ta));

                if(!response.Success)
                    return StatusCode(500, "Internal server error");

                var transactionAmountDTO = _mapper.Map<TransactionAmount>(response.Data);
                return Ok(transactionAmountDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the CreateOwner action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
            

        [HttpPut]
        public async Task<IActionResult> UpdateTransactionAmount([FromBody] TransactionAmountDTO transactionAmount)
        {
            try
            {     
                var ta = _mapper.Map<TransactionAmount>(transactionAmount);
                var response = await _mediator.Send(new UpdateTransactionAmountCommand(ta));         

                if (response.Success)
                {
                    var transactionAmountDTO = _mapper.Map<TransactionAmount>(response.Data);
                    return Ok(transactionAmountDTO);
                }
                else
                {
                    return BadRequest($"Transaction type with id {ta.Id} was not updated");
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionAmount(int id)
        {
            try
            {
                 var response = await _mediator.Send(new DeleteTransactionAmountCommand(id));

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
