using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowerBot.Models
{
    public class Shipper
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public List<Items> Items { get; set; } = new List<Items>();

    }
}
