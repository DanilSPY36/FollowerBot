using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowerBot.Models
{
    public class Shipper
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string FIO { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string ContactMail { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Region { get; set; } = string.Empty;
        public List<Items> Items { get; set; } = new List<Items>();

    }
}
