using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBY.Domain.Entities
{
    public class Entity : IEntity<int>
    {
        public Entity()
        {
        }

        public int Id { get;  set; }

        /// <summary>
        /// Validate the peroperties of the entity using its annotation
        /// </summary>
        /// <param name="entity">Entity object to validate</param>
        /// <returns>A tuple with isValid to indicate if the entity is valid using its annotations
        /// and tje errorMessages with the list of error messages.</returns>
        public (bool isValid, List<String> errorMessages) Validate()
        {
            ICollection<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(this, new ValidationContext(this), results, true);
            return (isValid, results.Select(x => x.ErrorMessage).ToList());
        }
    }
}
