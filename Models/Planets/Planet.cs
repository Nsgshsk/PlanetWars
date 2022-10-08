using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PlanetWars.Models.Planets
{
    internal class Planet : IPlanet
    {
        private string name;
        private double budget;
        private double militaryPower;
        private WeaponRepository weapons;
        private UnitRepository units;

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            militaryPower = 0;
            weapons = new WeaponRepository();
            units = new UnitRepository();
        }

        public string Name
        {
            get { return name; }
            private set
            {
                if (value == null || value == String.Empty || value == " ")
                {
                    throw new ArgumentException("Planet name cannot be null or empty.");
                }
                else
                    name = value;
            }
        }

        public double Budget
        {
            get { return budget; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Budget's amount cannot be negative.");
                }
                else
                    budget = value;
            }
        }

        public double MilitaryPower
        {
            get { return Math.Round(militaryPower, 3); }
            private set { militaryPower = value; }
        }

        public IReadOnlyCollection<IMilitaryUnit> Army
        {
            get { return units.Models; }
        }

        public IReadOnlyCollection<IWeapon> Weapons
        {
            get { return weapons.Models; }
        }

        public void AddUnit(IMilitaryUnit unit)
        {
            units.AddItem(unit);
            MilitaryPower = CalculateMilitaryPower();
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
            MilitaryPower = CalculateMilitaryPower();
        }

        public string PlanetInfo()
        {
            var army = Army.Any() ? String.Join(", ", Army.Select(e => e.GetType().ToString().Split('.').Last())) : "No units";
            var weapons = Weapons.Any() ? String.Join(", ", Weapons.Select(e => e.GetType().ToString().Split('.').Last())) : "No weapons";

            return
                $"Planet: {Name}" + Environment.NewLine
                + "--Budget: " + (Math.Ceiling(Budget) == Budget ? $"{Budget}" : $"{Budget:F2}") +" billion QUID" + Environment.NewLine
                + $"--Forces: {army}" + Environment.NewLine
                + $"--Combat equipment: {weapons}" + Environment.NewLine
                + $"--Military Power: {MilitaryPower}";
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public void Spend(double amount)
        {
            if (Budget - amount < 0)
                throw new InvalidOperationException("Budget too low!");
            else
                Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach (var item in Army)
            {
                item.IncreaseEndurance();
            }
            MilitaryPower++;
        }

        private double CalculateMilitaryPower()
        {
            double totalAmount = Weapons.Sum(e => e.DestructionLevel) + Army.Sum(e => e.EnduranceLevel);
            foreach (var item in Army)
            {
                if (item.GetType().ToString().Split('.').Last() == "AnonymousImpactUnit")
                    totalAmount *= 1.3;
            }
            foreach (var item in Weapons)
            {
                if (item.GetType().ToString().Split('.').Last() == "NuclearWeapon")
                    totalAmount *= 1.45;
            }
            return totalAmount;
        }
    }
}
