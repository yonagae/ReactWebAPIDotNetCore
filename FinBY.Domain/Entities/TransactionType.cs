using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Entities
{
    public class TransactionType : Entity
    {
        [StringLength(100)]
        [Required(ErrorMessage = "TransactionId must be specified")]
        public string Name { get; private set; }
        public Int32 ArgbColor { get; set; }

        public TransactionType(string name, int argbColor)
        {
            Name = name;
            ArgbColor = argbColor;
        }

        public void SetArgbColorFromColor(Color color) => ArgbColor = color.ToArgb();
        public Color GetArgbColor() => Color.FromArgb(ArgbColor);
    }
}
