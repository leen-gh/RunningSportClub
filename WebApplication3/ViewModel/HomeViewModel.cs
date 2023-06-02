using WebApplication3.Models;

namespace WebApplication3.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Club> clubs { get; set; }
        public string City { get; set; }

        public string State { get; set; }
        public string Phone { get; set; }

    }
}
