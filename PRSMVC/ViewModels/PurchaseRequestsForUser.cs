using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRSMVC.ViewModels {
	public class PurchaseRequestsForUser {

		public User User { get; set; }
		public IEnumerable<PurchaseRequest> PurchaseRequests { get; set; }

		public PurchaseRequestsForUser() {

		}
	}
}