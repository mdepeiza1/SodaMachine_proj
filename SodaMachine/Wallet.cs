using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class Wallet
    {
        //Member Variables (Has A)
        public List<Coin> Coins;
        public Card creditCard; //may need to change
        //Constructor (Spawner)
        public Wallet()
        {
            Coins = new List<Coin>();
            creditCard = new Card(); //added later
            FillRegister();
        }
        //Member Methods (Can Do)
        //Fills wallet with starting money
        private void FillRegister()
        {
            Coin q = new Quarter();
            Coin d = new Dime();
            Coin n = new Nickel();
            Coin p = new Penny(); //coins for the register were instantiated here

            Coin c = new Card(); //modified to add card
            Coins.Add(c); //modified to add card

            for (int i = 0; i < 12; i++)
            {
                Coins.Add(q);
            }
            for (int i = 0; i < 10; i++)
            {
                Coins.Add(d);
            }
            for (int i = 0; i < 14; i++)
            {
                Coins.Add(n);
            }
            for (int i = 0; i < 30; i++) //coins were added to the register
            {
                Coins.Add(p);
            }
        }
    }
}
