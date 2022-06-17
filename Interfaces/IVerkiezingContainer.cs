using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IVerkiezingContainer
    {
        public VerkiezingDTO GetVerkiezing(int verkiezingID);
        public List<VerkiezingDTO> GetAllVerkiezingen(int userid);
        public bool AddVerkiezing(VerkiezingDTO verkiezingDTO, List<KandidaatDTO> kandidaatDTOs);
        public bool RemoveVerkiezing(VerkiezingDTO verkiezingDTO);
        public List<VerkiezingDTO> GetAllVerkiezingenVote();
        public void DeleteKV(int verkiezingID, List<KandidaatDTO> kandidaatDTOs);

        public void ADDKV(int verkiezingID, List<KandidaatDTO> kandidaatDTOs);



    }
}
