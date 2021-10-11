using System;
using System.Collections.Generic;

#nullable disable

namespace ThinkBridgeAPI.Models
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Quantity { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
    }
}
