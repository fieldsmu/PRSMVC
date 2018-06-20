using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRSMVC.ViewModels {
	public class ShowAllPRsWithLineItemTotal {
		public IEnumerable<PurchaseRequest> PurchaseRequests { get; set; }
		public IEnumerable<PRLineItem> PRLineItems { get; set; }

		public ShowAllPRsWithLineItemTotal() {

		}
	}
}