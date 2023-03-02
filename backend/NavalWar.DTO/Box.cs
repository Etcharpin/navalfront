using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DTO
{
	[Serializable]
	public class Box
	{
		public int PositionX { get; set; }
		public int PositionY { get; set; }
		public bool IsOccupied { get; set; }
		public bool WasHit { get; set; }

		public int occupant { get; set; }
		
		public Box() { }

		public Box(int x, int y, bool isOccupied, bool wasHit) 
		{
			PositionX = x;
			PositionY = y;
			IsOccupied = isOccupied;
			WasHit = wasHit;
			occupant= 0;

		}
	}
}
