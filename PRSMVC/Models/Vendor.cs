using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PRSMVC {
	public class Vendor {

		public int Id { get; set; }

		[Index(IsUnique = true)]
		[Required]
		[StringLength(30)]
		public string Code { get; set; }

		[Required]
		[StringLength(30)]
		public string Name { get; set; }

		[Required]
		[StringLength(30)]
		public string Address { get; set; }

		[Required]
		[StringLength(30)]
		public string City { get; set; }

		[Required]
		[StringLength(2)]
		public string State { get; set; }

		[Required]
		[StringLength(5)]
		public string Zip { get; set; }

		[Required]
		[StringLength(12)]
		public string Phone { get; set; }

		[Required]
		[StringLength(255)]
		public string Email { get; set; }

		public bool IsPreapproved { get; set; }
		public bool Active { get; set; }

		public Vendor() {

		}
	}
}
