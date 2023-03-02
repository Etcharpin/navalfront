using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DTO
{
    [Serializable]
    public class Ship
	{
		public int Durability { get; set; }
		public int ShipType { get; set; }

		public Ship(int durability)
		{
			ShipType = durability;
            Durability = durability;
		}
	}
}
