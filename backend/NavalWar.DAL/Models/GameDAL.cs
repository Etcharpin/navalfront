using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DAL.Models
{
	public class GameDAL
	{
		[Key]
		public string GameId { get; set; }
		public int NbrPlayer { get; set; }

		public int State { get; set; }
	}
}
