using NavalWar.DAL.Models;
using NavalWar.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.Business
{
	public interface ISessionService
	{
		public GameArea GetPlayersSessionService(string gameId);
		public int GetNbrOfPlayers(string gameId);
		public void AddSession(string gameId);
		public void UpdateSession(string gameId, int nbr, int state);
		public GameDAL GetSession(string gameId);
		public void RemoveSession(GameDAL gdal);

	}
}
