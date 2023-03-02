using Microsoft.EntityFrameworkCore;
using NavalWar.DAL;
using NavalWar.DAL.Models;
using NavalWar.DTO.WebDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DAL.Repository
{
	public class PlayerRepository : IPlayerRepository
	{
		private readonly NavalContext _context;

		public PlayerRepository(NavalContext context)
		{
			_context = context;
		}

		public PlayerDTO GetPlayerDto(int playerId)
		{
			try
			{
				var player = _context.Players.AsNoTracking().FirstOrDefault(p => p.Id == playerId);
				if (player == null)
				{
					return null;
				}
				return Extension.PlayerDalToDto(player);
			}
			catch (Exception)
			{
				throw;
			}
		}

		public void AddPlayer(PlayerDAL pdal)
		{
			_context.Players.Add(pdal);
			_context.SaveChanges();
		}

		public void RemovePlayer(PlayerDAL pdal)
		{
			_context.Players.Remove(pdal);
			_context.SaveChanges();
		}

        public void UpdateMap(PlayerDAL pdal)
		{
            var player = _context.Players.First(p => p.Id == pdal.Id);
			player.Map = pdal.Map;
			player.shiplist = pdal.shiplist;
			player.isready = pdal.isready;
            _context.SaveChanges();
        }
    }
}
