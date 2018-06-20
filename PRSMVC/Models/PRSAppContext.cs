using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PRSMVC.Models
{
    public class PRSAppContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public PRSAppContext() : base("name=PRSAppContext")
        {
        }

		public System.Data.Entity.DbSet<PRSMVC.User> Users { get; set; }

		public System.Data.Entity.DbSet<PRSMVC.Vendor> Vendors { get; set; }

		public System.Data.Entity.DbSet<PRSMVC.Product> Products { get; set; }

		public System.Data.Entity.DbSet<PRSMVC.PurchaseRequest> PurchaseRequests { get; set; }

		public System.Data.Entity.DbSet<PRSMVC.PRLineItem> PRLineItems { get; set; }
	}
}
