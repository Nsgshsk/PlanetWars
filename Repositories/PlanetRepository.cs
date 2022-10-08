using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PlanetWars.Repositories
{
    internal class PlanetRepository : IRepository<IPlanet>
    {
        private Dictionary<string, IPlanet> planets;

        public PlanetRepository()
        {
            planets = new Dictionary<string, IPlanet>();
        }

        public IReadOnlyCollection<IPlanet> Models
        {
            get { return planets.Values; }
        }

        public void AddItem(IPlanet model)
        {
            if (!planets.ContainsKey(model.Name))
                planets.Add($"{model.Name}", model);
        }

        public IPlanet FindByName(string name)
        {
            if (planets.ContainsKey(name))
            {
                return planets[name];
            }
            else
                return null;
        }

        public bool RemoveItem(string name)
        {
            if (planets.ContainsKey(name))
            {
                planets.Remove(name);
                return true;
            }
            else
                return false;
        }
    }
}
