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
        public GenericChangeCommandResult(bool success, List<string> messages, object data, bool dataNotFound = false)
        {
            Success = success;
            Messages = messages;
            Data = data;
            DataNotFound = dataNotFound;
        }

        public bool Success { get; set; }
        public bool DataNotFound { get; set; }
        public List<string> Messages { get; set; }
        public object Data { get; set; }
    }
}
