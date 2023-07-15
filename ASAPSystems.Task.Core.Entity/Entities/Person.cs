using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ASAPSystems.Task.Core.Entity.Entities
{
    public class Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PersonId { get; set; }
        public int AddressId { get; set; }
        public string PersonName { get; set; }
        public int Age { get; set; }
        #region Navigation PROPS 
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }
        #endregion

    }
}
