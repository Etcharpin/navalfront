using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NavalWar.DTO
{
	[Serializable]
	public class Map
	{
		public List<Box> listBox = new List<Box>();

		public int[] shipnb = new int[] { 0, 0, 0, 0 };
		public int Size { get; set; }
		
		public Map() { }

		public Map(int size) 
		{
			int y = 0;

			for (int i = 0; i < size; i++)
			{
				listBox.Add(new Box((i % 10) * 50, y * 50, false, false));

				if ((i + 1) % 10 == 0)
				{
					y++;
				}
			}

			Size = size;
		}

	}
}
