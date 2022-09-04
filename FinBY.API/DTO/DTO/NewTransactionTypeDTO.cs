using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.API.Data.DTO
{
    public  class NewTransactionTypeDTO
    { 
        [Required]
        [StringLength(10)]
        public string Name { get;  set; }

        public string ArgbColor { get; set; }
    }
}
