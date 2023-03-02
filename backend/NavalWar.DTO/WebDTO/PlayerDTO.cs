using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DTO.WebDTO
{
	public class PlayerDTO
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string GameId { get; set; }

		public List<Ship> shipslist { get; set; }

		public bool isready { get; set; }

		public Map GameMap { get; set; }
	}
}
