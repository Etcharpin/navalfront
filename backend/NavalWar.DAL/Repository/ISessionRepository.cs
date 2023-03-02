using NavalWar.DAL.Models;
using NavalWar.DTO;
using NavalWar.DTO.WebDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DAL.Repository
{
	public interface ISessionRepository
	{
		public int GetNumberOfPlayersFromSession(string gameId);
		public GameArea GetPlayersFromSession(string gameId);
		public void AddSession(GameDAL game);
		public void RemoveSession(GameDAL game);
		public void UpdateSession(GameDAL game);
		public GameDAL GetSession(string gameId);
	}
}
