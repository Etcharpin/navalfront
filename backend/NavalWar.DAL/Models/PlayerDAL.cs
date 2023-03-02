using NavalWar.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NavalWar.DAL.Models
{
	public class PlayerDAL
	{
		[Key]
		public int Id { get; set; }
		public string Name { get; set; }
		public string Map { get; set; }

		public bool isready { get; set; }

		public string shiplist { get; set; }
		public string GameId { get; set; }
		public GameDAL GameDAL { get; set; }
	}
}
