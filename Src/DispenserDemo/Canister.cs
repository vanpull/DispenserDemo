using System;

namespace DispenserDemo
{
    public class Canister
    {
        public int CurrentLevel { get; set; } = 500;

        public int MinimumLevel { get; set; } = 200;

        public int MaximumLevel { get; set; } = 1000;

        public string CanisterId { get; set; }        

        public int Amount { get; set; }

        public Canister()
        {
        }

        public Canister(string canisterId)
        {
            CanisterId = canisterId;
        }

        public Canister(string canisterId, int amount) : this(canisterId)
        {
            Amount = amount;
        }

        public void Fill(Canister item, int currentLevel)
        {   
            item.CurrentLevel = currentLevel;            
        }        

    }
}
