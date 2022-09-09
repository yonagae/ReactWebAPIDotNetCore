using AutoMapper;
using FinBY.Domain.Commands;
using FinBY.Domain.Contracts;
using FinBY.API.Data.DTO;
using FinBY.Domain.Entities;
using FinBY.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using FinBY.Domain.Data.PagedResult;
using FinBY.API.Helper;
using FinBY.Domain.Enum;

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
        public async Task<IActionResult> GetAllTransactions(DateTime start, DateTime end)
        {
            try
            { 
                var transactions = await _unitOfWork.TransactionRepository.GetAllDetailedWithoutAmountsAsync(start, end);
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
        [ProducesResponseType(typeof(NewTransactionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(typeof(NewTransactionDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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


        [HttpGet("{id}/transactionAmounts")]
        [ProducesResponseType(typeof(List<TransactionAmountDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllTransactionAmountsByTransactionID(int id)
        {
            try
            {
                var transactionAmounts = await _unitOfWork.TransactionAmountRepository.GetTransactionAmountsByTransactionIdAsync(id);

                if (transactionAmounts == null || transactionAmounts.Count <= 0)
                {
                    return NotFound($"Transaction Amounts  with id {id} not found");
                }

                var transDTOList = _mapper.Map<List<TransactionAmountDTO>>(transactionAmounts);
                return Ok(transDTOList);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Transaction get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("sumExpenseByType")]
        [ProducesResponseType(typeof(List<TransactionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetDashboardInfo(DateTime start, DateTime end)
        {
            try
            {
                var transactions = await _unitOfWork.TransactionRepository.GetSumOfTransactionsByTypeByPeriod(start, end);

                var expenseSums = new List<ExpenseSumByTransactionTypeDTO>();
                foreach (var transaction in transactions)
                {
                    expenseSums.Add(new ExpenseSumByTransactionTypeDTO()
                    {
                        Sum = transaction.Item2,
                        TransactionType = _mapper.Map<TransactionTypeDTO>(transaction.Item1)
                    });
                }                

                return Ok(expenseSums);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Transaction get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("userbalance")]
        [ProducesResponseType(typeof(List<TransactionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetUserBalanceByPeriod(DateTime start, DateTime end)
        {
            try
            {
                var userSumTransactionDictionary = new Dictionary<string, UserSumTransactionDTO>();
                var userSum = await _unitOfWork.TransactionRepository.GetSumOfTransactionsByUserByPeriod(start, end);

                if (userSum.Count != 0)
                {
                    foreach(var t in userSum)
                    {
                        (User user, eTransactionFlow eFlow, decimal sum) = t;
                        if (!userSumTransactionDictionary.ContainsKey(user.Name))
                            userSumTransactionDictionary.Add(user.Name, new UserSumTransactionDTO() { UserName = user.Name });

                        userSumTransactionDictionary[user.Name].SumByFlowType.Add(eFlow, sum);
                    }
                }

                return Ok(userSumTransactionDictionary.Values.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Transaction get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("yearlyExpense")]
        [ProducesResponseType(typeof(List<TransactionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMonthlyExpenseByPeriod(DateTime start, DateTime end, [FromQuery(Name = "listOfTransactionsTypeIds[]")] List<int> listOfTransactionsTypeIds)
        {
            try
            {
                var result = new List<Dictionary<string, object>>();
                var monthlyExpense = await _unitOfWork.TransactionRepository.GetMonthlyExpenseByPeriod(start, end, listOfTransactionsTypeIds);
                if (monthlyExpense.Count != 0)
                {
                    var monthlyExpenseByMonthDic = monthlyExpense.GroupBy(x => new { x.Item1.Year, x.Item1.Month })
                        .ToDictionary(
                                     g => $"{g.Key.Month}/{g.Key.Year}", 
                                     g => g.Select(x => new { x.Item2, x.Item3}).ToList() //create a list of just the name of type and sum value
                                 );

                    foreach (var t in monthlyExpenseByMonthDic)
                    {
                        var userSumTransactionDictionary = new Dictionary<string, object>();
                        userSumTransactionDictionary.Add("name", t.Key);
                        foreach(var typesAndSum in t.Value)
                        {
                            userSumTransactionDictionary.Add(typesAndSum.Item2.Name, typesAndSum.Item3);
                        }
                        result.Add(userSumTransactionDictionary);
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Transaction get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("monthlyExpenseByPeriodAndUser")]
        [ProducesResponseType(typeof(List<TransactionDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetMonthlyExpenseByPeriodAndUser(int userId, DateTime start, DateTime end, [FromQuery(Name = "listOfTransactionsTypeIds[]")] List<int> listOfTransactionsTypeIds)
        {
            try
            {
                var result = new List<Dictionary<string, object>>();
                var monthlyExpense = await _unitOfWork.TransactionAmountRepository.GetMonthlyExpenseByPeriod(userId, start, end, listOfTransactionsTypeIds);
                if (monthlyExpense.Count != 0)
                {
                    var monthlyExpenseByMonthDic = monthlyExpense.GroupBy(x => new { x.Item1.Year, x.Item1.Month })
                        .ToDictionary(
                                     g => $"{g.Key.Month}/{g.Key.Year}",
                                     g => g.Select(x => new { x.Item2, x.Item3 }).ToList() //create a list of just the name of type and sum value
                                 );

                    foreach (var t in monthlyExpenseByMonthDic)
                    {
                        var userSumTransactionDictionary = new Dictionary<string, object>();
                        userSumTransactionDictionary.Add("name", t.Key);
                        foreach (var typesAndSum in t.Value)
                        {
                            userSumTransactionDictionary.Add(typesAndSum.Item2, typesAndSum.Item3);
                        }
                        result.Add(userSumTransactionDictionary);
                    }
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside the Transaction get action: {ex}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
