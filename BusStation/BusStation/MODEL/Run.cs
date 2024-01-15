using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusStation.MODEL
{
    public class Run
    {
        [Key]
        public int RunId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int BusId { get; set; }
        [Required]
        public int RouteId { get; set; }
        [Required]
        public DateTime DepartureDate { get; set; }
        [Required]
        public DateTime ArrivalDate { get; set; }

        public List<Passenger> Passengers { get; set; } = new List<Passenger>();
    }
}
