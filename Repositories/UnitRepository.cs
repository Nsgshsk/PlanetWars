using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Repositories
{
    internal class UnitRepository : IRepository<IMilitaryUnit>
    {
        private Dictionary<string, IMilitaryUnit> army;

        public UnitRepository()
        {
            army = new Dictionary<string, IMilitaryUnit>();
        }

        public IReadOnlyCollection<IMilitaryUnit> Models
        {
            get { return army.Values; }
        }

        public void AddItem(IMilitaryUnit model)
        {
            string tmp = String.Empty;
            switch (model.Cost)
            {
                case 2.5: tmp = "StormTroopers"; break;
                case 11: tmp = "SpaceForces"; break;
                case 30: tmp = "AnonymousImpactUnit"; break;
            }

            if (!army.ContainsKey(tmp) && tmp != String.Empty)
                army.Add(tmp, model);
        }

        public IMilitaryUnit FindByName(string name)
        {
            if (army.ContainsKey(name))
            {
                return army[name];
            }
            else
                return null;
        }

        public bool RemoveItem(string name)
        {
            if (army.ContainsKey(name))
            {
                army.Remove(name);
                return true;
            }
            else
                return false;
        }
    }
}
