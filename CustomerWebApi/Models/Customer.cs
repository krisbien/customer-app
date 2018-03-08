using CustomerWebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerWebApi.Models
{
    public class Customer: IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Telephone { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
