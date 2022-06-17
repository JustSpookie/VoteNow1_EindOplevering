using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IVerkiezing
    {
        public bool UpdateVerkiezing(VerkiezingDTO verkiezingDTO);

        public bool UpdateVerkiezingKandidaat(VerkiezingDTO verkiezingDTO, List<KandidaatDTO> kandidaatListADD, List<KandidaatDTO> kandidaatListDELETE);
    }
}
