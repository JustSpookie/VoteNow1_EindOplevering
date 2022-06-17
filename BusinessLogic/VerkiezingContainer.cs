using DataTransferObjects;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class VerkiezingContainer
    {
        IVerkiezingContainer context;

        public VerkiezingContainer(IVerkiezingContainer dal)
        {
            this.context = dal;
        }

        /// <summary>
        /// Voegt een verkiezing toe aan de database
        /// </summary>
        /// <param name="verkiezing">De verkiezing die je wilt toevoegen</param>
        /// <param name="kandidaten">De kandidaten die je wilt koppelen aan de verkiezing</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool AddVerkiezing(Verkiezing verkiezing, List<Kandidaat> kandidaten)
        {
            List<KandidaatDTO> kandidaatDTOs = new List<KandidaatDTO>();

            foreach (Kandidaat kandidaat in kandidaten)
            {
                kandidaatDTOs.Add(kandidaat.TODTO());

            }
            verkiezing.VerkiezingNaam = verkiezing.VerkiezingNaam.TrimEnd();

            return this.context.AddVerkiezing(verkiezing.TODTO(), kandidaatDTOs);
        }

        /// <summary>
        /// Pakt alle verkiezingen van de database en stopt ze in een lijst
        /// </summary>
        /// <returns>De lijst met alle verkiezingen</returns>
        public List<Verkiezing> GetAllVerkiezingen(int userid)
        {
            List<Verkiezing> verkiezingList = new List<Verkiezing>();
            List<VerkiezingDTO> verkiezingDTOs = this.context.GetAllVerkiezingen(userid);
            foreach (VerkiezingDTO verkiezingDTO in verkiezingDTOs)
            {
                verkiezingList.Add(new Verkiezing(verkiezingDTO));
            }
            return verkiezingList;
        }

        public List<Verkiezing> GetAllVerkiezingenVote()
        {
            List<Verkiezing> verkiezingList = new List<Verkiezing>();
            List<VerkiezingDTO> verkiezingDTOs = this.context.GetAllVerkiezingenVote();
            foreach (VerkiezingDTO verkiezingDTO in verkiezingDTOs)
            {
                verkiezingList.Add(new Verkiezing(verkiezingDTO));
            }
            return verkiezingList;
        }

        /// <summary>
        /// Verwijdert een verkiezing uit de database
        /// </summary>
        /// <param name="verkiezing">De verkiezing die je weg wilt hebben</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool DeleteVerkiezing(Verkiezing verkiezing)
        {
            return this.context.RemoveVerkiezing(new VerkiezingDTO(verkiezing.VerkiezingID, verkiezing.VerkiezingNaam));
        }

        /// <summary>
        /// Pakt een specifieke verkiezing op basis van zijn ID
        /// </summary>
        /// <param name="verkiezingID">Het ID van de verkiezing die je wilt hebben</param>
        /// <returns>Geeft de verkiezing die je wilt hebben terug</returns>
        public Verkiezing GetVerkiezing(int verkiezingID)
        {
            return new Verkiezing(context.GetVerkiezing(verkiezingID));
        }
    }
}
