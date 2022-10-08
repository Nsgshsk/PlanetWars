using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.Weapons
{
    internal class NuclearWeapon : Weapon
    {
        public NuclearWeapon(int destructionLevel) : base(destructionLevel, 15)
        {
        }
    }
}
