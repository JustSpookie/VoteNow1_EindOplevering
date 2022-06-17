using System.ComponentModel.DataAnnotations;

namespace ASP_View.Models
{
    public class StemmenViewModel
    {
        [Key]
        public int StemmenId { get; set; }

        public List<KandidaatViewModel> kandidaten { get; set; }

        public VerkiezingViewModel verkiezing { get; set; }


    }
}
