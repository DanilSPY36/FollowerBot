using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowerBot.Models
{
    public class DrinkTTK
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty; // описание 
        public string HowToCook { get; set; } = string.Empty;// как готовить 
        public double Volume { get; set; } = 0.2; // объем 
        public string Ingridients { get; set; } = string.Empty; // ингридиенты 
        public double Weight { get; set; } = 0;// выход 
        public string Container { get; set; } = string.Empty;// тара
        public string Addivitives { get; set; } // добавки 
        public string Blank {  get; set; } = string.Empty;// заготовка
        public byte[] Photos { get; set; } // скрин ттк
    }
}
