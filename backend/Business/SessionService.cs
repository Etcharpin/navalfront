using NavalWar.DAL.Models;
using NavalWar.DAL.Repository;
using NavalWar.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.Business
{
	public class SessionService : ISessionService
	{
		private readonly ISessionRepository sessionRepo;

		public SessionService(ISessionRepository sessionRepo)
		{
			this.sessionRepo = sessionRepo;
		}

		public GameArea GetPlayersSessionService(string gameId)
		{
			return sessionRepo.GetPlayersFromSession(gameId);
		}

		public int GetNbrOfPlayers(string gameId)
		{
			return sessionRepo.GetNumberOfPlayersFromSession(gameId);
		}

		public void AddSession(string gameId)
		{
			GameDAL gdal = new GameDAL();
			gdal.State = 0;
			gdal.GameId = gameId;
			gdal.NbrPlayer = 0;
			sessionRepo.AddSession(gdal);
		}

		public void UpdateSession(string gameId, int nbr,int state)
		{
			GameDAL gdal = new GameDAL();
			gdal.State = state;
			gdal.GameId = gameId;
			gdal.NbrPlayer = nbr;
			sessionRepo.UpdateSession(gdal);
		}

		public GameDAL GetSession(string gameId)
		{
			return sessionRepo.GetSession(gameId);
		}

		public void RemoveSession(GameDAL gdal)
		{
			sessionRepo.RemoveSession(gdal);
		}
	}
}
