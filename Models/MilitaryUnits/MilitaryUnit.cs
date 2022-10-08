using PlanetWars.Models.MilitaryUnits.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace PlanetWars.Models.MilitaryUnits
{
    internal abstract class MilitaryUnit : IMilitaryUnit
    {
        private double cost;
        private int enduranceLevel;

        public double Cost
        {
            get { return cost; }
            private set { cost = value; }
        }

        public int EnduranceLevel
        {
            get { return enduranceLevel; }
            private set
            {
                if (value > 20)
                {
                    enduranceLevel = 20;
                    throw new ArgumentException("Endurance level cannot exceed 20 power points.");
                }
                else
                    enduranceLevel = value;
            }
        }

        public MilitaryUnit(double cost)
        {
            Cost = cost;
            EnduranceLevel = 1;
        }

        public void IncreaseEndurance()
        {
            EnduranceLevel++;
        }
    }
}
