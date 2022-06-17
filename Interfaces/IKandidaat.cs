using DataTransferObjects;

namespace Interfaces
{
    public interface IKandidaat
    {
        public bool UpdateKandidaat(KandidaatDTO kandidaatDTO);
        public bool CheckKSV(KandidaatDTO kandidaatDTO);
    }
}