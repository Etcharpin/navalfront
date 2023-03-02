using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using NavalWar.DAL;
using NavalWar.DAL.Models;
using NavalWar.DAL.Repository;
using NavalWar.DTO;
using NavalWar.DTO.WebDTO;

namespace NavalWar.Business
{
	public class PlayerService : IPlayerService
	{
		private readonly IPlayerRepository playerRepo;

		public PlayerService(IPlayerRepository playerRepo)
		{
			this.playerRepo = playerRepo;
		}

		public void PlaceShip(PlayerDTO player, int x, int y, int size, bool orientation)
		{
			if (x > 10 || y > 10 || x < 0 || y < 0 || (orientation == false && x + size > 10) || (orientation == true && y + size > 10))
			{
				throw new Exception("Hors grille");
			}
			else if ((size == 2 && orientation == false && (player.GameMap.listBox[y * 10 + x].IsOccupied == true
				|| player.GameMap.listBox[y * 10 + x + 1].IsOccupied == true))
				|| (size == 2 && orientation == true && (player.GameMap.listBox[y * 10 + x].IsOccupied == true
				&& player.GameMap.listBox[y * 10 + x + 10].IsOccupied == true))
				|| (size == 3 && orientation == false && (player.GameMap.listBox[y * 10 + x].IsOccupied == true
				&& player.GameMap.listBox[y * 10 + x + 1].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 1].IsOccupied == true))
				|| (size == 3 && orientation == true && (player.GameMap.listBox[y * 10 + x].IsOccupied == true
				&& player.GameMap.listBox[y * 10 + x + 10].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 20].IsOccupied == true))
				|| (size == 4 && orientation == false && (player.GameMap.listBox[y * 10 + x].IsOccupied == true
				&& player.GameMap.listBox[y * 10 + x + 1].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 2].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 3].IsOccupied == true))
				|| (size == 4 && orientation == true && (player.GameMap.listBox[y * 10 + x].IsOccupied == true
				&& player.GameMap.listBox[y * 10 + x + 10].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 20].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 30].IsOccupied == true))
				|| (size == 5 && orientation == false && (player.GameMap.listBox[y * 10 + x].IsOccupied == true
				&& player.GameMap.listBox[y * 10 + x + 1].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 2].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 3].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 4].IsOccupied == true))
				|| (size == 5 && orientation == true && (player.GameMap.listBox[y * 10 + x].IsOccupied == true
				&& player.GameMap.listBox[y * 10 + x + 10].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 20].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 30].IsOccupied == true && player.GameMap.listBox[y * 10 + x + 40].IsOccupied == true)))
			{
				throw new Exception("Case occupée");
			}
			else
			{
				player.GameMap.shipnb[size - 2]++;

				if (orientation == false)
				{
					for (int i = 0; i < size; i++)
					{
						player.GameMap.listBox[y * 10 + x + i].IsOccupied = true;
                        player.GameMap.listBox[y * 10 + x + i].occupant = size;
                    }
				}
				else
				{
					for (int i = 0; i < size; i++)
					{
						player.GameMap.listBox[(y + i) * 10 + x].IsOccupied = true;
                        player.GameMap.listBox[(y + i) * 10 + x].occupant = size;
                    }
				}
			}
			
			PlayerDAL pdal = Extension.PlayerDtoToPlayerDal(player);
			playerRepo.UpdateMap(pdal);
		}

		public PlayerDTO DeleteShip(PlayerDTO player, int x, int y, int size, bool orientation)
		{
			if (orientation == false)
			{
				for (int i = 0; i < size; ++i)
				{
					player.GameMap.listBox[y * 10 + x + i].IsOccupied = false;
				}
			}
			else
			{
				for (int i = 0; i < size; ++i)
				{
					player.GameMap.listBox[(y + i) * 10 + x].IsOccupied = false;
				}
			}

			player.GameMap.shipnb[size - 2]--;
			return player;
		}

		public void AddPlayerService(PlayerDAL player)
		{
			playerRepo.AddPlayer(player);
		}

		public PlayerDTO GetPlayerService(int playerId)
		{
			var ret = playerRepo.GetPlayerDto(playerId);
			if (ret.shipslist == null)
			{
				ret.shipslist = new List<Ship>();
			}
            return ret;
		}

		public void RemovePlayerService(PlayerDAL player)
		{
			playerRepo.RemovePlayer(player);
		}

		public void Shoot(PlayerDTO player, int x, int y)
		{
			player.GameMap.listBox[x + y * 10].WasHit = true;
				foreach(Ship shi in player.shipslist)
				{
					if(shi.ShipType == player.GameMap.listBox[x + y * 10].occupant)
					{
						if(shi.Durability > 1)
						{
                            shi.Durability--;
                        }
						else
						{
                            player.shipslist.Remove(shi);
						break;

                        }
					}
				}
            PlayerDAL pdal = Extension.PlayerDtoToPlayerDal(player);
			playerRepo.UpdateMap(pdal);
		}


		public void updatePlayer(PlayerDTO player)
		{
            PlayerDAL pdal = Extension.PlayerDtoToPlayerDal(player);
            playerRepo.UpdateMap(pdal);
        }
	}
}
