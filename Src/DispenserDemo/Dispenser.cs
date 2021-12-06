using System;
using System.Collections.Generic;
using System.Linq;

namespace DispenserDemo
{
    public class Dispenser
    {
        public int NumberOfCanisters { get; private set; }

        public List<Canister> Canisters { get; private set; }

        public Dispenser(int numberOfCanisters)
        {
            if (numberOfCanisters <= 0 || numberOfCanisters > 32)
                throw new ArgumentOutOfRangeException(nameof(numberOfCanisters), "Canisters must be set between 1 and 32");

            NumberOfCanisters = numberOfCanisters;
            InitCanisters();
        }

        public void Dispense(DispenserJob dispenserJob)
        {
            var loadedCanisters = dispenserJob.GetCanisters();           

            foreach (var canister in loadedCanisters)
            {
                var index = Canisters.FindIndex(a => a.CanisterId.Equals(canister.Key));
                var item = Canisters.ElementAt(index);
                item.CurrentLevel = item.CurrentLevel - canister.Value;
                if(item.CurrentLevel < item.MinimumLevel)
                {
                    throw new InvalidOperationException($"Dispense colud not proceed. {item.CanisterId} current level falls below minimum level. Adjust dispense amount");
                }
            }
        }

        public void InitCanisters()
        {
            Canisters = new List<Canister>();

            for (int i = 0; i < NumberOfCanisters; i++)
            {
                Canisters.Add(new Canister($"Canister {i + 1}"));
            }
        }

        public override string ToString()
        {
            return $"Dispenser with {NumberOfCanisters} canisters";
        }
    }
}
