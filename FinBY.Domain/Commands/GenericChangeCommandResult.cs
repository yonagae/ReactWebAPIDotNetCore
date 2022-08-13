using FinBY.Domain.Commands.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Commands
{
    public class GenericChangeCommandResult :  ICommandResult
    {
        public GenericChangeCommandResult(bool success, string message, object data, bool dataNotFound = false)
        {
            Success = success;
            Message = message;
            Data = data;
            DataNotFound = dataNotFound;
        }

        public bool Success { get; set; }
        public bool DataNotFound { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}
