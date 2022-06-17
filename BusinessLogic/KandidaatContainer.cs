using DataAccessLayer;
using DataTransferObjects;
using Interfaces;

namespace BusinessLogic
{
    public class KandidaatContainer
    {
        IKandidaatContainer context;

        public KandidaatContainer(IKandidaatContainer dal)
        {
            this.context = dal;
        }

        /// <summary>
        /// Haalt alle kandidaten van de database
        /// </summary>
        /// <returns>Alle kandidaten die in de database zitten</returns>
        public List<Kandidaat> GetKandidaten(int userid)
        {
            List<Kandidaat> kandidaten = new List<Kandidaat>();
            List<KandidaatDTO> kandidatenDTO = context.GetKandidaten(userid);

            foreach(KandidaatDTO kandidaatDTO in kandidatenDTO)
            {
                kandidaten.Add(new Kandidaat(kandidaatDTO)); 
            }

            return kandidaten;
        }
        
        /// <summary>
        /// Haalt alle kandidaten van de database die gekoppeld zijn aan een bepaalde verkiezing
        /// </summary>
        /// <param name="vekiezingID">Het ID van de verkiezing waarvan je de kandidaten wil zien</param>
        /// <returns>De lijst met kandidaten van de verkiezing</returns>
        public List<Kandidaat> GetKandidatenFromVerkiezing(int vekiezingID)
        {
            List<KandidaatDTO> kandidaatDTOs = context.GetKandidatenFromVerkiezing(vekiezingID);
            List<Kandidaat> Kandidaten= new List<Kandidaat>();
            foreach(KandidaatDTO kandidaatDTO in kandidaatDTOs)
            {
                Kandidaten.Add(new Kandidaat(kandidaatDTO.KandidaatNaam, kandidaatDTO.KandidaatID));
            }
            return Kandidaten;
        }

        /// <summary>
        /// Voegt een kandidaat toe aan de database
        /// </summary>
        /// <param name="kandidaat">De kandidaat die je toe wilt voegen aan de database</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool AddKandidaat(Kandidaat kandidaat)
        {
            kandidaat.KandidaatNaam = kandidaat.KandidaatNaam;
            return context.AddKandidaat(kandidaat.TODTO());
        }

        /// <summary>
        /// Verwijdert een kandidaat uit de database
        /// </summary>
        /// <param name="kandidaat">De kandidaat die je wilt verwijderen</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet </returns>
        public bool DeleteKandidaat(Kandidaat kandidaat)
        {
            return context.DeleteKandidaat(kandidaat.TODTO());
        }

        /// <summary>
        /// Haalt een speciefieke kandidaat op uit de database
        /// </summary>
        /// <param name="kandiaatID">Het ID van de kandidaat die je wilt ophalen</param>
        /// <returns>De kandidaat die je uit de database heb opgehaald</returns>
        public Kandidaat GetKandidaat(int kandiaatID)
        {
            return new Kandidaat(context.GetKandidaat(kandiaatID));
        }
    }
}
