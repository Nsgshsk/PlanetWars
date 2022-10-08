using PlanetWars.Models.Weapons.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.Weapons
{
    internal abstract class Weapon : IWeapon
    {
        private double price;
        private int destructionLevel;
        public double Price
        {
            get { return price; }
            private set { price = value; }
        }

        public int DestructionLevel
        {
            get { return destructionLevel; }
            private set
            {
                if (value < 1)
                    throw new ArgumentException("Destruction level cannot be zero or negative.");
                else if (value > 10)
                    throw new ArgumentException("Destruction level cannot exceed 10 power points.");
                else
                    destructionLevel = value;
            }
        }

        public Weapon(int destructionLevel, double price)
        {
            DestructionLevel = destructionLevel;
            Price = price;
        }
    }
}
