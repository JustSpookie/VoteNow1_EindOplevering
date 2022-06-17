using System.ComponentModel.DataAnnotations;

namespace ASP_View.Models
{
    public class KandidaatViewModel
    {
        [Key]
        public int KandidaatId { get; set; }

        public bool Selected { get; set; }

        [Required]
        public string KandidaatNaam { get; set; }


        public KandidaatViewModel(int ID,string kandidaatnaam, bool selected)
        {
            KandidaatId = ID;
            KandidaatNaam = kandidaatnaam;
            Selected = selected;
        }
        public KandidaatViewModel(int ID, string kandidaatnaam)
        {
            KandidaatId = ID;
            KandidaatNaam = kandidaatnaam;
        }
        public KandidaatViewModel() { }
    }
}
