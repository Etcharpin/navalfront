using NavalWar.DAL.Models;
using NavalWar.DTO.WebDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DAL.Repository
{
	public interface IPlayerRepository
	{
		public PlayerDTO GetPlayerDto(int playerId);
		public void AddPlayer(PlayerDAL pdal);
		public void RemovePlayer(PlayerDAL pdal);
		public void UpdateMap(PlayerDAL pdal);
	}
}
