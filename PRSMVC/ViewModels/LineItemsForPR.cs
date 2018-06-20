using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRSMVC.ViewModels {
	public class LineItemsForPR {

		public PurchaseRequest PurchaseRequest { get; set; }
		public IEnumerable<PRLineItem> PRLineItems { get; set; }

		public LineItemsForPR() {

		}
	}
}