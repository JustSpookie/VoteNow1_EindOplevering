using DataTransferObjects;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestenVoteNow.DummyDal
{
    public class DummyVerkiezingDAL : IVerkiezing, IVerkiezingContainer
    {
        public List<VerkiezingDTO> verkiezings = new List<VerkiezingDTO>();
        public List<List<KandidaatDTO>> KV = new List<List<KandidaatDTO>>();

        public void ADDKV(int verkiezingID, List<KandidaatDTO> kandidaatDTOs)
        {
            KV.Add(kandidaatDTOs);
        }

        public bool AddVerkiezing(VerkiezingDTO verkiezingDTO, List<KandidaatDTO> kandidaatDTOs)
        {
            int lengte = verkiezings.Count;
            verkiezings.Add(verkiezingDTO);
            ADDKV(verkiezingDTO.VerkiezingID,kandidaatDTOs);

            if(lengte < verkiezings.Count)
            {
                return true;
            }
            else { return false; }
        }

        public void DeleteKV(int verkiezingID, List<KandidaatDTO> kandidaatDTOs)
        {
            KV.RemoveAt(verkiezingID);
        }

        public List<VerkiezingDTO> GetAllVerkiezingen(int id)
        {
            return verkiezings;
        }

        public List<VerkiezingDTO> GetAllVerkiezingenVote()
        {
            throw new NotImplementedException();
        }

        public VerkiezingDTO GetVerkiezing(int verkiezingID)
        {
            int index = 0;

            for (int i = 0; i < verkiezings.Count; i++)
            {
                if(verkiezings[i].VerkiezingID == verkiezingID)
                {
                    index = i;
                }
            }
            return verkiezings[index];
        }

        public bool RemoveVerkiezing(VerkiezingDTO verkiezingDTO)
        {
            int index = 0;
            int lengte = verkiezings.Count;

            for (int i = 0; i < verkiezings.Count; i++)
            {
                if (verkiezings[i].VerkiezingID == verkiezingDTO.VerkiezingID)
                {
                    index = i;
                }
            }
            verkiezings.RemoveAt(index);
            if(lengte > verkiezings.Count)
            {
                return true;
            }
            else { return false; }
        }

        public bool UpdateVerkiezing(VerkiezingDTO verkiezingDTO)
        {
            int index = 0;

            for (int i = 0; i < verkiezings.Count; i++)
            {
                if (verkiezings[i].VerkiezingID == verkiezingDTO.VerkiezingID)
                {
                    index = i;
                }
            }
            verkiezings[index].VerkiezingNaam = verkiezingDTO.VerkiezingNaam;
            if(verkiezings[index] == verkiezingDTO) { return true; }
            else { return false; }
        }

        public bool UpdateVerkiezingKandidaat(VerkiezingDTO verkiezingDTO, List<KandidaatDTO> kandidaatListADD, List<KandidaatDTO> kandidaatListDELETE)
        {
            if((verkiezingDTO != null) && (kandidaatListADD != null) && (kandidaatListDELETE != null))
            {
                return true;
            }
            else { return false;}
        }


    }
}
