using AutoMapper;
using FinBY.Domain.Commands;
using FinBY.Domain.Contracts;
using FinBY.Domain.Data.DTO;
using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FinBY.Domain.Data.PagedResult;
using FinBY.API.Helper;

namespace FinBY.API.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/transactions")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private IUnitOfWork _unitOfWork;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public TransactionController(
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
        [ProducesResponseType(typeof(List<TransactionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTransactions()
        {
            try
            { 
                var transactions = await _unitOfWork.TransactionRepository.GetAllDetailedWithouAmountsAsync();
                var tsDTOs = _mapper.Map<List<TransactionDTO>>(transactions);
                return Ok(tsDTOs);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Transaction get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("paged")]
        [ProducesResponseType(typeof(List<TransactionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDetailedTransactionsByParams([FromQuery] PagedTransactionParams pageParams)
        {
            try
            {
                var pagedResult = await _unitOfWork.TransactionRepository.GetAllWithDetailsAsPagedResultAsync(pageParams);

                var alunosResult = _mapper.Map<List<TransactionDTO>>(pagedResult.Results);

                if (Response != null)
                    Response.AddPagination(pagedResult.CurrentPage, pagedResult.PageSize, pagedResult.RowCount, pagedResult.PageCount);

                return Ok(alunosResult);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Transaction get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]  
        public async Task<IActionResult> CreateTransaction([FromBody] NewTransactionDTO transaction)
        {
            try
            {
                if (transaction == null)
                {
                    return BadRequest("Owner object is null");
                }

                var trans = _mapper.Map<Transaction>(transaction);
                var response = await _mediator.Send(new CreateTransactionCommand(trans));

                if(!response.Success)
                    return StatusCode(500, "Internal server error");

                return Ok(response.Data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the CreateOwner action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(long id, [FromBody] TransactionDTO transaction)
        {
            try
            {
                if (id != transaction.Id) return BadRequest();

                var trans = _mapper.Map<Transaction>(transaction);
                var response = await _mediator.Send(new UpdateTransactionCommand(trans));         

                if (response.Success)
                {
                    var transDTO = _mapper.Map<TransactionDTO>(response.Data);
                    return Ok(transDTO);
                }
                else
                {
                    return BadRequest($"Transaction  with id {transaction.Id} was not updated");
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            try
            {
                 var response = await _mediator.Send(new DeleteTransactionCommand(id));

                if (response.DataNotFound)
                {
                    return NotFound($"Transaction  with id {id} not found");
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
                    return BadRequest($"Transaction  with id {id} not deleted");
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpGet("{id}/TransactionAmounts")]
        public async Task<IActionResult> GetAllTransactionAmountsByTransactionID(int id)
        {
            try
            {
                var transactionAmounts = await _unitOfWork.TransactionAmountRepository.GetTransactionAmountsByTransactionIdAsync(id);
                var transDTOList = _mapper.Map<List<TransactionAmountDTO>>(transactionAmounts);
                return Ok(transDTOList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Transaction get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}
