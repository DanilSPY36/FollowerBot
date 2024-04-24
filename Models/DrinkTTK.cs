using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FollowerBot.Models
{
    public class DrinkTTK
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public int CategoryId { get; set; } // Id категории
        public int VolumeId { get; set; } // Id объема напитка
        public int SpotId { get; set; } = 1;  // Id спотов - для дальнейшего добавления локалок
        public string Description { get; set; } = string.Empty; // описание 
        public string Ingridients { get; set; } = string.Empty; // ингридиенты 
        public string HowToCook { get; set; } = string.Empty;// как готовить 
        public string ?Weight {  get; set; } = string.Empty; // выход
        public string ContainerId { get; set; } = string.Empty;// тара
        public string Addivitives { get; set; } = string.Empty; // добавки 
        public string Blank {  get; set; } = string.Empty;// заготовка
        public byte[]? Photos { get; set; } = null; // скрин ттк
    }
}
