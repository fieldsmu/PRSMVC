using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSMVC {
	public class Product {

		public int Id { get; set; }

		[StringLength(30)]
		[Required]
		public string PartNumber { get; set; }

		[StringLength(30)]
		[Required]
		public string Name { get; set; }

		[Required]
		public double Price { get; set; }

		[StringLength(30)]
		[Required]
		public string Unit { get; set; }

		[StringLength(130)]
		public string PhotoPath { get; set; }

		public bool Active { get; set; }

		public int VendorId { get; set; }
		public virtual Vendor Vendor { get; set; }

		public Product() {

		}
	}
}
