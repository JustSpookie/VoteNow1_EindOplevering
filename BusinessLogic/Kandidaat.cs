using DataAccessLayer;
using DataTransferObjects;
using Interfaces;

namespace BusinessLogic
{
    public class Kandidaat
    {
        public string KandidaatNaam { get; set; }
        public int KandidaatID { get; set; }
        public bool Selected { get; set; }
        public int UserID { get; set; }
        
        /// <summary>
        /// Maakt een kandidaat aan zonder data
        /// </summary>
        public Kandidaat()
        {

        }
        
        /// <summary>
        /// Maakt een kandidaat aan met ID en Naam
        /// </summary>
        /// <param name="naam">De naam van de Kandidaat</param>
        /// <param name="id">Het ID van de Kandidaat</param>
        public Kandidaat(string naam, int id)
        {
            KandidaatNaam = naam;
            KandidaatID = id;
        }

        public Kandidaat(string naam, int id, int userID)
        {
            KandidaatNaam = naam;
            KandidaatID = id;
            UserID = userID;
        }

        /// <summary>
        /// Maakt een kandidaat aan met ID, Naam en of die geselecteerd is of niet
        /// </summary>
        /// <param name="naam">De naam van de Kandidaat</param>
        /// <param name="id">Het ID van de Kandidaat</param>
        /// <param name="selected">Of de kandidaat geselecteerd is of niet</param>
        public Kandidaat(string naam, int id, bool selected)
        {

            KandidaatNaam = naam;
            KandidaatID= id;
            Selected = selected;
        }

        /// <summary>
        /// Maakt een Kandidaat met alleen een naam aan(voor bijvoorbeeld een nieuwe kandidaat aan te maken)
        /// </summary>
        /// <param name="naam"></param>
        public Kandidaat(string naam)
        {
            KandidaatNaam = naam;
            KandidaatID = 0;
        }

        /// <summary>
        /// Pakt een kandidaatDTO en verandert het in een normale kandidaat
        /// </summary>
        /// <param name="dto">De kandidaatDTO die je krijgt van de datalaag</param>
        public Kandidaat(KandidaatDTO dto)
        {
            KandidaatNaam = dto.KandidaatNaam;
            KandidaatID = dto.KandidaatID; 
            UserID = dto.UserID;
        }

        /// <summary>
        /// Pakt een kandidaat en verandert hem in een KandidaatDTO
        /// </summary>
        /// <returns>Hij geeft een KandidaatDTO terug</returns>
        public KandidaatDTO TODTO()
        {
            return new KandidaatDTO(KandidaatNaam, KandidaatID, UserID);    
        }

        /// <summary>
        /// Stuurt een een kandidaatDTO met nieuwe waardes naar de datalaag
        /// In de datalaag verandert die de database met de nieuwe waardes
        /// Dit doet die bij het meegegeven ID
        /// </summary>
        /// <param name="context">De interface die met de dal is gekoppelt</param>
        /// <returns>Een bool waarmee je kan controlleren of het wel goed is gegaan</returns>
        public bool UpdateKandidaat(IKandidaat context)
        {
            KandidaatNaam.TrimEnd();
            return context.UpdateKandidaat(new KandidaatDTO(KandidaatNaam,KandidaatID));
        }

        public bool CheckKSV(IKandidaat context)
        {
            return context.CheckKSV(TODTO());
        }
    }
}