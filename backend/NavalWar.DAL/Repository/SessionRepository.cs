using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavalWar.DAL.Models;
using NavalWar.DTO.WebDTO;
using NavalWar.DTO;

namespace NavalWar.DAL.Repository
{
	public class SessionRepository : ISessionRepository
	{
		private readonly NavalContext _context;

		public SessionRepository(NavalContext context)
		{
			_context = context;
		}

		public int GetNumberOfPlayersFromSession(string gameId)
		{
			try
			{
				var game = _context.Games.Find(gameId);

				if (game == null)
				{
					Console.WriteLine("This game doesn't exist.");
					return 0;
				}

				return game.NbrPlayer;
			}
			catch
			{
				throw;
			}
		}

		public GameArea GetPlayersFromSession(string gameId)
		{
			try
			{
				var listOfPlayerDAL= _context.Players.Where(p => p.GameId == gameId);
				GameArea gameArea = new GameArea(gameId);

				foreach (PlayerDAL player in listOfPlayerDAL.ToList())
				{
					PlayerDTO playerDto = Extension.PlayerDalToDto(player);
					Map map = Extension.DeserializeMapToMap(player.Map);
					Player playerFromDto = new Player(player.Id, player.Name, map);
					gameArea.ListPlayers.Add(playerFromDto);
				}

				return gameArea;
			}
			catch (Exception)
			{
				throw;
			}			
		}

		public void AddSession(GameDAL game)
		{
			_context.Games.Add(game);
			_context.SaveChanges();
		}

		public void RemoveSession(GameDAL game)
		{
			_context.Games.Remove(game);
			_context.SaveChanges();
		}

		public void UpdateSession(GameDAL game)
		{
			var newGame = _context.Games.First(g => g.GameId == game.GameId);
			newGame.NbrPlayer = game.NbrPlayer;
			_context.SaveChanges();
		}

		public GameDAL GetSession(string gameId)
		{
			try
			{
				var game = _context.Games.Find(gameId);

				if (game == null)
				{
					return null;
				}

				return game;
			}
			catch
			{
				throw;
			}
		}
	}
}
