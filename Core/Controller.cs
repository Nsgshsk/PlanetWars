using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetWars.Core
{
    internal class Controller : IController
    {
        private PlanetRepository planets = new PlanetRepository();

        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            IMilitaryUnit unit = null;

            switch (unitTypeName)
            {
                case "StormTroopers": unit = new StormTroopers(); break;
                case "SpaceForces": unit = new SpaceForces(); break;
                case "AnonymousImpactUnit": unit = new AnonymousImpactUnit(); break;
            }

            if (planet == null)
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            else if (unit == null)
                throw new InvalidOperationException($"{unitTypeName} still not available!");
            else if (planet.Army.Where(e => e.Cost == unit.Cost).Count() != 0)
                throw new InvalidOperationException($"{unitTypeName} already added to the Army of {planetName}!");
            else
            {
                planet.Spend(unit.Cost);
                planet.AddUnit(unit);
                return $"{unitTypeName} added successfully to the Army of {planetName}!";
            }
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = planets.FindByName(planetName);
            IWeapon weapon = null;

            switch (weaponTypeName)
            {
                case "BioChemicalWeapon": weapon = new BioChemicalWeapon(destructionLevel); break;
                case "NuclearWeapon": weapon = new NuclearWeapon(destructionLevel); break;
                case "SpaceMissiles": weapon = new SpaceMissiles(destructionLevel); break;
            }

            if (planet == null)
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            else if (weapon == null)
                throw new InvalidOperationException($"{weaponTypeName} still not available!");
            else if (planet.Weapons.Where(e => e.Price == weapon.Price).Count() != 0)
                throw new InvalidOperationException($"{weaponTypeName} already added to the Weapons of {planetName}!");
            else
            {
                planet.Spend(weapon.Price);
                planet.AddWeapon(weapon);
                return $"{planetName} purchased {weaponTypeName}!";
            }
        }

        public string CreatePlanet(string name, double budget)
        {
            if (planets.FindByName(name) == null)
            {
                planets.AddItem(new Planet(name, budget));
                return $"Successfully added Planet: {name}";
            }
            return $"Planet {name} is already added!";
        }

        public string ForcesReport()
        {
            StringBuilder tmp = new StringBuilder("***UNIVERSE PLANET MILITARY REPORT***\n");
            foreach (var item in planets.Models)
            {
                tmp.AppendLine(item.PlanetInfo());
            }
            int length = tmp.ToString().Length;
            return tmp.ToString().Substring(0, length - 2);
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet p1 = planets.FindByName(planetOne);
            IPlanet p2 = planets.FindByName(planetTwo);
            IPlanet winner;
            IPlanet loser;

            if (p1.MilitaryPower == p2.MilitaryPower)
            {
                int n1 = p1.Weapons.Where(e => e.Price == 15).Count();
                int n2 = p2.Weapons.Where(e => e.Price == 15).Count();
                if ((n1 != 0 && n2 != 0) || (n1 == 0 && n2 == 0))
                {
                    p1.Spend(p1.Budget / 2);
                    p2.Spend(p2.Budget / 2);
                    return "The only winners from the war are the ones who supply the bullets and the bandages!";
                }
                else
                {
                    if (n1 > n2)
                    {
                        winner = p1;
                        loser = p2;
                    }
                    else
                    {
                        loser = p1;
                        winner = p2;
                    }
                }
            }
            else
            {
                if (p1.MilitaryPower > p2.MilitaryPower)
                {
                    winner = p1;
                    loser = p2;
                }
                else
                {
                    loser = p1;
                    winner = p2;
                }
            }
            string tmp = loser.Name;
            winner.Spend(p1.Budget / 2);
            double expenses = 0;
            foreach (var item in loser.Weapons)
            {
                expenses += item.Price;
            }
            foreach (var item in loser.Army)
            {
                expenses += item.Cost;
            }
            p1.Profit(p2.Budget / 2 + expenses);
            planets.RemoveItem(tmp);
            return $"{winner.Name} destructed {tmp}!";
        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
                throw new InvalidOperationException($"Planet {planetName} does not exist!");
            else if (!planet.Army.Any())
                throw new InvalidOperationException("No units available for upgrade!");
            else
            {
                planet.Spend(1.25);
                planet.TrainArmy();
                return $"{planetName} has upgraded its forces!";
            }
        }
    }
}
