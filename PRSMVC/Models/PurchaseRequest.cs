using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSMVC {
	public class PurchaseRequest {

		public int Id { get; set; }

		[StringLength(80)]
		[Required]
		public string Description { get; set; }

		[StringLength(80)]
		[Required]
		public string Justification { get; set; }

		[StringLength(80)]
		[Required]
		public string RejectionReason { get; set; }

		[StringLength(20)]
		[Required]
		public string DeliveryMode { get; set; }

		[StringLength(15)]
		[Required]
		public string Status { get; set; } = "NEW";

		public int UserId { get; set; }
		public virtual User User { get; set; }

		public PurchaseRequest() {
			
		}

	}
}
