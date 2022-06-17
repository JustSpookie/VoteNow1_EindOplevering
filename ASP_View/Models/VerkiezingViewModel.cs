using BusinessLogic;
using System.ComponentModel.DataAnnotations;

namespace ASP_View.Models
{
    public class VerkiezingViewModel
    {
        [Key]
        public int VerkiezingID { get; set; }
        [Required]
        public string VerkiezingNaam { get; set; }

        public List<Kandidaat> kandidaats { get; set; }

        public VerkiezingViewModel(int id, string naam)
        {
            VerkiezingID = id;
            VerkiezingNaam = naam;
        }

        public VerkiezingViewModel() { }
    }
}
