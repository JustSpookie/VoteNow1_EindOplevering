using DataAccessLayer;
using DataTransferObjects;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Verkiezing
    {
        public int VerkiezingID;
        public string VerkiezingNaam;
        public int UserID;
        
        /// <summary>
        /// Maakt een verkiezing aan zonder data
        /// </summary>
        public Verkiezing() { }

        /// <summary>
        /// Maakt een verkiezing aan met een ID en een naam
        /// </summary>
        /// <param name="verkiezingID">Het ID van de verkiezing</param>
        /// <param name="verkiezingnaam">De naam van de verkiezing</param>
        public Verkiezing(int verkiezingID, string verkiezingnaam)
        {
            VerkiezingID = verkiezingID;
            VerkiezingNaam = verkiezingnaam;
        }

        public Verkiezing(int verkiezingID, string verkiezingnaam, int userid)
        {
            VerkiezingID = verkiezingID;
            VerkiezingNaam = verkiezingnaam;
            UserID = userid;
        }

        /// <summary>
        /// Maakt een verkiezing aan met alleen een naam(voor bijvoorbeeld bij het maken van een nieuwe verkiezing)
        /// </summary>
        /// <param name="verkiezingnaam"></param>
        public Verkiezing(string verkiezingnaam)
        {
            VerkiezingID = 0;
            VerkiezingNaam = verkiezingnaam;
        }

        /// <summary>
        /// Maakt een verkiezing aan met de informatie van een DTO
        /// </summary>
        /// <param name="verkiezingDTO">De DTO die die gebruikt om een verkiezing te maken</param>
        public Verkiezing(VerkiezingDTO verkiezingDTO)
        {
            VerkiezingID = verkiezingDTO.VerkiezingID;
            VerkiezingNaam= verkiezingDTO.VerkiezingNaam;
            UserID = verkiezingDTO.UserID;
        }

        /// <summary>
        /// Maakt een DTO aan van de verkiezing 
        /// </summary>
        /// <returns>Geeft een DTO met dezefde data als de verkiezing terug</returns>
        public VerkiezingDTO TODTO()
        {
            return new VerkiezingDTO(VerkiezingID, VerkiezingNaam, UserID);
        }

        /// <summary>
        /// Dit Update de Naam en andere delen van de verkiezing(Niet kandidaten)
        /// </summary>
        /// <param name="context">De interface die met de DAL gekoppeld is die je wil gebruiken</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool UpdateVerkiezing(IVerkiezing context)
        {
            return context.UpdateVerkiezing(TODTO());
        }

        /// <summary>
        /// Update de kandidaten die in een verkiezing zitten door een lijst te maken van kandidaten die er niet meer inzitten
        /// En daarvan elke koppeling met de verkiezing te verwijderen
        /// En daarna controlleert die welke kandidaten er wel in horen zitten maar er niet in zitten
        /// En daarvan maakt die de koppelingen met de verkiezing
        /// </summary>
        /// <param name="context">De interface die met de DAL gekoppeld is die je wil gebruiken</param>
        /// <param name="KandidatenInVerkiezing">De lijst met kandidaten die in de verkiezing horen te zitten</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool UpdateVerkiezingKandidaten(IVerkiezing context, List<Kandidaat> KandidatenInVerkiezing)
        {
            List<KandidaatDTO> kandidatenADD = new List<KandidaatDTO>();
            List<KandidaatDTO> kandidatenDELETE = new List<KandidaatDTO>();

            KandidaatContainer kandidaatContainer = new KandidaatContainer(new KandidaatDAL());

            List<Kandidaat> CurrentKandidaten = kandidaatContainer.GetKandidatenFromVerkiezing(VerkiezingID);

            foreach(Kandidaat kandidaat in CurrentKandidaten)
            {
                if(!KandidatenInVerkiezing.Contains(kandidaat))
                {
                    kandidatenDELETE.Add(kandidaat.TODTO());
                }
            }
            foreach(Kandidaat kandidaat in KandidatenInVerkiezing)
            {
                if (!CurrentKandidaten.Contains(kandidaat))
                {
                    kandidatenADD.Add(kandidaat.TODTO());
                }
            }

            return context.UpdateVerkiezingKandidaat(TODTO(), kandidatenADD, kandidatenDELETE);
        }

    }
}
