using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.MODEL
{
    public class Route
    {
        [Key]
        public int RouteId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int StopsNum { get; set; }
    }
}
