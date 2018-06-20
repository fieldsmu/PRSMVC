using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRSMVC.ViewModels {
	public class ProductsForVendor {

		public Vendor Vendor { get; set; }
		public IEnumerable<Product> Products { get; set; }

		public ProductsForVendor() {

		}
	}
}