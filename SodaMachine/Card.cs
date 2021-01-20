using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Card : Coin
    {
        public Card()
        {
            this.Name = "Card";
            this.value = 100;
        }
    }
}
