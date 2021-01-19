using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SodaMachine
{
    class SodaMachine
    {
        //Member Variables (Has A)
        private List<Coin> _register;
        private List<Can> _inventory;

        //Constructor (Spawner)
        public SodaMachine()
        {
            _register = new List<Coin>();
            _inventory = new List<Can>();
            FillInventory();
            FillRegister();
        }

        //Member Methods (Can Do)

        //A method to fill the sodamachines register with coin objects.
        public void FillRegister()
        {
            Coin q = new Quarter();
            Coin d = new Dime();
            Coin n = new Nickel();
            Coin p = new Penny(); //coins for the register were instantiated here
            for(int i = 0; i < 20; i++)
            {
                _register.Add(q);
            }
            for (int i = 0; i < 10; i++)
            {
                _register.Add(d);
            }
            for (int i = 0; i < 20; i++)
            {
                _register.Add(n);
            }
            for (int i = 0; i < 50; i++) //coins were added to the register
            {
                _register.Add(p);
            }
        }
        //A method to fill the sodamachines inventory with soda can objects.
        public void FillInventory()
        {
            Can rb = new RootBeer();
            Can c = new Cola();
            Can os = new OrangeSoda();//instantiated the soda cans

            for (int i = 0; i < 10; i++)
            {
                _inventory.Add(rb);
            }
            for (int i = 0; i < 10; i++)
            {
                _inventory.Add(c);
            }
            for (int i = 0; i < 10; i++) //added 10 cans of each soda to the soda machine's inventory
            {
                _inventory.Add(os);
            }
        }
        //Method to be called to start a transaction.
        //Takes in a customer which can be passed freely to which ever method needs it.
        public void BeginTransaction(Customer customer)
        {
            bool willProceed = UserInterface.DisplayWelcomeInstructions(_inventory);
            if (willProceed)
            {
                Transaction(customer);
            }
        }
        
        //This is the main transaction logic think of it like "runGame".  This is where the user will be prompted for the desired soda.
        //grab the desired soda from the inventory.
        //get payment from the user.
        //pass payment to the calculate transaction method to finish up the transaction based on the results.
        private void Transaction(Customer customer)
        {
            Can chosenSoda = GetSodaFromInventory(UserInterface.SodaSelection(_inventory));
            List<Coin> customerCoins = customer.GatherCoinsFromWallet(chosenSoda);
            CalculateTransaction(customerCoins, chosenSoda, customer);
            UserInterface.EndMessage(chosenSoda.Name, TotalCoinValue(customerCoins));
        }
        //Gets a soda from the inventory based on the name of the soda.
        private Can GetSodaFromInventory(string nameOfSoda) // may need to remove the soda from the inventory
        {
            return _inventory.Find(x => x.Name == nameOfSoda);
        }

        //This is the main method for calculating the result of the transaction.
        //It takes in the payment from the customer, the soda object they selected, and the customer who is purchasing the soda.
        //This is the method that will determine the following:
        //If the payment is greater than the price of the soda, and if the sodamachine has enough change to return: Dispense soda, and change to the customer.
        //If the payment is greater than the cost of the soda, but the machine does not have ample change: Dispense payment back to the customer.
        //If the payment is exact to the cost of the soda:  Dispense soda.
        //If the payment does not meet the cost of the soda: dispense payment back to the customer.
        private void CalculateTransaction(List<Coin> payment, Can chosenSoda, Customer customer)
        {
           if(TotalCoinValue(payment) > chosenSoda.Price && TotalCoinValue(_register) >= TotalCoinValue(payment))
            {
                customer.AddCanToBackpack(chosenSoda);
                //may need to add coins going into register here
                customer.AddCoinsIntoWallet(GatherChange(DetermineChange(TotalCoinValue(payment), chosenSoda.Price)));
            }
           else if(TotalCoinValue(payment) > chosenSoda.Price && TotalCoinValue(_register) < TotalCoinValue(payment))
            {
                customer.AddCoinsIntoWallet(GatherChange(TotalCoinValue(payment)));
            }
           else if(TotalCoinValue(payment) == chosenSoda.Price)
            {
                //may need to add coins going into register here
                customer.AddCanToBackpack(chosenSoda);
            }
            else //payment is less than soda price
            {
                //may need to add coins into register
                customer.AddCoinsIntoWallet(GatherChange(TotalCoinValue(payment)));
            }
        }
        //Takes in the value of the amount of change needed.
        //Attempts to gather all the required coins from the sodamachine's register to make change.
        //Returns the list of coins as change to despense.
        //If the change cannot be made, return null.
        private List<Coin> GatherChange(double changeValue)
        {
            List<Coin> coins = new List<Coin>();
            Coin q = new Quarter();
            Coin d = new Dime();
            Coin n = new Nickel();
            Coin p = new Penny();

           while(changeValue > 0)
            {
                while(changeValue % .25 > 0 && changeValue >= .25)
                {
                    if(_register.Remove(q)) // might change this remove to RegisterHasCoin and implement remove in the if statement
                    {
                        coins.Add(q);
                        changeValue -= .25;
                    }
                    else
                    {
                        DepositCoinsIntoRegister(coins);
                        return null;
                    }
                }
                while (changeValue % .10 > 0 && changeValue >= .10)
                {
                    if(_register.Remove(d))
                    {
                        coins.Add(d);
                        changeValue -= .10;
                    }
                     else
                    {
                        DepositCoinsIntoRegister(coins);
                        return null;
                    }
                }
                while (changeValue % .05 > 0 && changeValue >= .05)
                {
                    if(_register.Remove(n))
                    {
                        coins.Add(n);
                        changeValue -= .05;
                    }
                    else
                    {
                        DepositCoinsIntoRegister(coins);
                        return null;
                    }
                }
                while (changeValue % .01 > 0 && changeValue >= .01)
                {
                    if(_register.Remove(p))
                    {
                        coins.Add(p);
                        changeValue -= .01;
                    }
                    else
                    {
                        DepositCoinsIntoRegister(coins);
                        return null;
                    }
                }
            }
            return coins;
        }
        //Reusable method to check if the register has a coin of that name.
        //If it does have one, return true.  Else, false.
        private bool RegisterHasCoin(string name)
        {
            return (_register.Exists(x => x.Name == name));
        }//
        //Reusable method to return a coin from the register.
        //Returns null if no coin can be found of that name.
        private Coin GetCoinFromRegister(string name) //might need to remove coin from the register
        {
            return _register.Find(x => x.Name == name);
        }
        //Takes in the total payment amount and the price of can to return the change amount.
        private double DetermineChange(double totalPayment, double canPrice)
        {
            return totalPayment -= canPrice;
        }
        //Takes in a list of coins to returnt he total value of the coins as a double.
        private double TotalCoinValue(List<Coin> payment)
        {
            double total = 0;
            for(int i = 0; i < payment.Count(); i++)
            {
                total += payment[i].Value;
            }
            return total;
        }
        //Puts a list of coins into the soda machines register.
        private void DepositCoinsIntoRegister(List<Coin> coins)
        {
           for(int i = 0; i < coins.Count(); i++)
            {
                _register.Add(coins[i]);
            }
        }
    }
}
