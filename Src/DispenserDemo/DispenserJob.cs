using System;
using System.Collections.Generic;

namespace DispenserDemo
{
    public class DispenserJob
    {
        public List<Canister> Canisters { get; private set; }

        public int AmountToDispense { get; private set; }   

        public DispenserJob(List<Canister> canisters, int amountToDispense)
        {
            Canisters = canisters;
            AmountToDispense = amountToDispense;
        }

        public Dictionary<string, int> GetCanisters()
        {
            Dictionary<string, int> _canisters = new Dictionary<string, int>();

            foreach (var can in Canisters)
            {
                _canisters.Add(can.CanisterId, AmountToDispense);
            }

            return _canisters;
        }
        

    }
}
