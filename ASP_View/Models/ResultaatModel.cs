namespace ASP_View.Models
{
    public class ResultaatModel
    {
        public KandidaatViewModel KandidaatViewModel { get; set; }
        public int Stemmen { get; set; }

        public ResultaatModel(KandidaatViewModel kandidaatViewModel, int stemmen)
        {
            KandidaatViewModel = kandidaatViewModel;
            Stemmen = stemmen;
        }
    }
}
