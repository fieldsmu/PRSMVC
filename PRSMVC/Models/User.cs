using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSMVC {
	public class User {

		public int Id { get; set; }

		[Required]
		[Index(IsUnique = true)]
		[StringLength(30)]
		public string Username { get; set; }

		[Required]
		[MinLength(6)]
		[StringLength(30)]
		public string Password { get; set; }

		[Required]
		[StringLength(30)]
		public string Firstname { get; set; }

		[Required]
		[StringLength(30)]
		public string Lastname { get; set; }

		[Required]
		[StringLength(12)]
		public string Phone { get; set; }

		[Required]
		[StringLength(255)]
		public string Email { get; set; }

		public bool IsReviewer { get; set; }
		public bool IsAdmin { get; set; }
		public bool Active { get; set; }

		public User() {

		}
	}
}
