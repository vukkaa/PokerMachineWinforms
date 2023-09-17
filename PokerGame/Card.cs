using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerGame
{
    public class Card
    {

        private const string ImagePath = @"images\";

        public int Value { get; set; }

        public int Suit { get; set; }

        public string ImageName
        {
            get
            {
                return ImagePath + "card" + Suit + "_" + Value + ".png";
            }
        }


        //public override string ToString()
        //{
        //    if (Number > 10)
        //    {
        //        return (Number + 1).ToString();
        //    } else
        //    {
        //        return Number.ToString();
        //    }
        //}


    }
}
