using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class StemContainer
    {
        IStemContainer context;

        public StemContainer(IStemContainer dal)
        {
            this.context = dal;
        }

        /// <summary>
        /// Voegt een stem op een kandidaat in een verkiezing toe aan de database
        /// </summary>
        /// <param name="verkiezingID">Het ID van de verkiezing waarin de opgestemte kandidaat in zit</param>
        /// <param name="kandidaatID">Het ID van de kandidaat waarop gestemt is</param>
        /// <param name="userID">Het ID van de user die gestemt heeft</param>
        /// <returns>Een bool waaraan je kunt zien of het gelukt is of niet</returns>
        public bool ADDStem(int verkiezingID, int kandidaatID, int userID)
        {
            return(context.AddStem(verkiezingID, kandidaatID, userID));
        }

        /// <summary>
        /// Kijkt hoeveel stemmen een specifieke kandidaat in een verkiezing heeft
        /// </summary>
        /// <param name="kandidaatID">Het ID van de kandidaat waarvan je het aantal stemmen wilt hebben</param>
        /// <param name="verkiezingID">De verkiezing waar de kandidaat in zit</param>
        /// <returns>Het aantal stemmen dat de kandidaat in de verkiezing heeft</returns>
        public int GetStemCount(int kandidaatID, int verkiezingID)
        {
            return(context.GetStemCount(kandidaatID, verkiezingID));
        }

        public bool CheckStem(int verkiezingid, int userid)
        {
            return context.CheckStem(verkiezingid, userid);
        }
    }
}
