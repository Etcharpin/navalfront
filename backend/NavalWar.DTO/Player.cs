using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DTO
{
	public class Player
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public Map map { get; set; }

		public List<Ship> shiplist { get; set; }

		public bool ready { get; set; }
		
		public Player() { }

		public Player(int id, string name, Map map) 
		{
			Id = id;
			Name = name; 
			this.map = map; 
			shiplist= new List<Ship>();
			ready = false;
		}

	}
}
