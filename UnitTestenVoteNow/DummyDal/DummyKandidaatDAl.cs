using DataTransferObjects;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestenVoteNow.DummyDal 
{
    public class DummyKandidaatDAl : IKandidaatContainer, IKandidaat
    {
        List<KandidaatDTO> kandidaatDTOs = new List<KandidaatDTO>();

        public KandidaatDTO GetKandidaat(int kandidaatID)
        {
            if(kandidaatDTOs.Count - 1 >= kandidaatID)
            {
                return kandidaatDTOs[kandidaatID];
            }
            else { return new KandidaatDTO(" ", 0); }

        }

        public List<KandidaatDTO> GetKandidaten(int id)
        {
            return kandidaatDTOs;
        }
        public List<KandidaatDTO> GetKandidatenFromVerkiezing(int verkiezingID)
        {
            List<KandidaatDTO> list = new List<KandidaatDTO>();
            list.Add(new KandidaatDTO("bert", 1));
            list.Add(new KandidaatDTO("tio", 2));
            list.Add(new KandidaatDTO("kees", 3));
            list.Add(new KandidaatDTO("nick", 4));
            return list;

        }
        public bool AddKandidaat(KandidaatDTO kandidaatDTO)
        {
            int lengte = kandidaatDTOs.Count;
            kandidaatDTOs.Add(kandidaatDTO);
            kandidaatDTOs[kandidaatDTOs.Count - 1].KandidaatID = kandidaatDTOs.Count;

            if (lengte+1 == kandidaatDTOs.Count)
            {
                return true;
            }
            else { return false; }
        }
        public bool DeleteKandidaat(KandidaatDTO kandidaatDTO)
        {
            int lengte = kandidaatDTOs.Count;
            int id = 0;

            for (int i = 0; i < kandidaatDTOs.Count; i++)
            {
                if(kandidaatDTOs[i].KandidaatID == kandidaatDTO.KandidaatID)
                {
                    id = i;
                }

            }

            kandidaatDTOs.RemoveAt(id);
            if (lengte - 1 == kandidaatDTOs.Count)
            {
                return true;
            }
            else { return false; }
        }
        public bool UpdateKandidaat(KandidaatDTO kandidaatDTO)
        {
            int index = -1;
            for (int i = 0; i < kandidaatDTOs.Count; i++)
            {
                if(kandidaatDTOs[i].KandidaatID == kandidaatDTO.KandidaatID)
                {
                    index = i;
                }
            }
            if(index > -1)
            {
                kandidaatDTOs[index] = kandidaatDTO;
                return true;
            }
            else { return false; }
               
        }

        public bool CheckKSV(KandidaatDTO kandidaatDTO)
        {
            throw new NotImplementedException();
        }
    }
}
