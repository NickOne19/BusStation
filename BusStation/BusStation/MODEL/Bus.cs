using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.MODEL
{
    public class Bus
    {
        [Key]
        public int BusId { get; set; }
        [Required]
        public string? Model { get; set; }
        [Required]
        public string? RegNum { get; set; }
    }
}
