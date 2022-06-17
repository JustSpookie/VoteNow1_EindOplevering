using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IStemContainer
    {
        public bool AddStem(int verkiezingID, int kandidaatID, int userID);
        public int GetStemCount(int kandidaatID,int verkiezingID);
        public bool CheckStem(int verkiezingID, int userID);
    }
}
