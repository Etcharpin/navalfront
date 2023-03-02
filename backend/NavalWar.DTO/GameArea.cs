using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DTO
{
	public class GameArea
	{
		public List<Player> ListPlayers { get; private set; }

		public string Id { get; set; }

		public int state { get; set; }

		public GameArea(string id)
		{
			Id = id;
			ListPlayers = new List<Player>();
		}
	}
}
