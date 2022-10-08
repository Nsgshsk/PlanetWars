using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PlanetWars.Repositories
{
    internal class WeaponRepository : IRepository<IWeapon>
    {
        private Dictionary<string, IWeapon> weapons;

        public WeaponRepository()
        {
            weapons = new Dictionary<string, IWeapon>();
        }

        public IReadOnlyCollection<IWeapon> Models
        {
            get { return weapons.Values; }
        }

        public void AddItem(IWeapon model)
        {
            string tmp = String.Empty;
            switch (model.Price)
            {
                case 3.2: tmp = "BioChemicalWeapon"; break;
                case 15: tmp = "NuclearWeapon"; break;
                case 8.75: tmp = "SpaceMissiles"; break;
            }

            if (!weapons.ContainsKey(tmp) && tmp != String.Empty)
                weapons.Add(tmp, model);
        }

        public IWeapon FindByName(string name)
        {
            if (weapons.ContainsKey(name))
            {
                return weapons[name];
            }
            else
                return null;
        }

        public bool RemoveItem(string name)
        {
            if (weapons.ContainsKey(name))
            {
                weapons.Remove(name);
                return true;
            }
            else
                return false;
        }
    }
}
