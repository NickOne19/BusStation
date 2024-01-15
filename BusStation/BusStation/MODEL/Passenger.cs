using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.MODEL
{
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? IdType { get; set; }
        [Required]
        public string? IdNum { get; set; }
        [Required]
        public List<Run> Runs { get; set; } = new List<Run>();
    }
}
