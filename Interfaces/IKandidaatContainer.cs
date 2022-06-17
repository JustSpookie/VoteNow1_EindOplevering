using DataTransferObjects;

namespace Interfaces
{
    public interface IKandidaatContainer
    {
        public KandidaatDTO GetKandidaat(int kandidaatID);
        public List<KandidaatDTO> GetKandidaten(int userid);
        public List<KandidaatDTO> GetKandidatenFromVerkiezing(int verkiezingID);
        public bool AddKandidaat(KandidaatDTO kandidaatDTO);
        public bool DeleteKandidaat(KandidaatDTO kandidaatDTO);

    }
}
