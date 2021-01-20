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
        //Constructor (Spawner)
        public Wallet()
        {
            Coins = new List<Coin>();
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
