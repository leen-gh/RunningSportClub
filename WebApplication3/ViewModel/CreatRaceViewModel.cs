using System.ComponentModel.DataAnnotations.Schema;
using WebApplication3.Data.Enum;
using WebApplication3.Models;

namespace WebApplication3.ViewModel
{
    public class CreatRaceViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("Address")]
        public int? AddressId { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public RaceCategory RaceCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
