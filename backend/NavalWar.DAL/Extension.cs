using NavalWar.DAL.Models;
using NavalWar.DTO;
using NavalWar.DTO.WebDTO;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NavalWar.DAL
{
	public static class Extension
	{
		public static PlayerDTO PlayerDalToDto(PlayerDAL pdal)
		{
			PlayerDTO pDTO = new PlayerDTO();
			pDTO.Id = pdal.Id;
			pDTO.Name = pdal.Name;
			pDTO.GameMap = Extension.DeserializeMapToMap(pdal.Map);
			pDTO.shipslist = Extension.DeserializeStringTolistship(pdal.shiplist);
			pDTO.isready = pdal.isready;
			return pDTO;
		}

		public static PlayerDAL PlayerDtoToPlayerDal(PlayerDTO pdto)
		{
			PlayerDAL pDAL = new PlayerDAL();
			pDAL.Id = pdto.Id;
			pDAL.Name = pdto.Name;
			pDAL.Map = Extension.SerializeMap(pdto.GameMap);
			pDAL.shiplist = Extension.SerializeListship(pdto.shipslist);
			pDAL.GameId = pdto.GameId;
			pDAL.isready = pdto.isready;
			return pDAL;
		}

		public static Map DeserializeMapToMap (string map)
		{
			Map mapDto = JsonConvert.DeserializeObject<Map>(map);
            return mapDto;
		}

		public static string SerializeMap(Map map)
		{
			string seri_map = JsonConvert.SerializeObject(map);
            return seri_map;
		}

        public static List<Ship> DeserializeStringTolistship(string shiplist)
        {
            List<Ship> shilist = JsonConvert.DeserializeObject<List<Ship>>(shiplist);
            return shilist;
        }

        public static string SerializeListship(List<Ship> lis)
        {
            string seri_li = JsonConvert.SerializeObject(lis);
            return seri_li;
        }
    }
}
