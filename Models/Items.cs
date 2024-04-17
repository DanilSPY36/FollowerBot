using FollowerBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Items
{
    public Guid Id { get; set; } 
    public Guid ShipperId { get; set; } // id поставщика
    public Shipper? ShipperLink { get; set; } // ссылка на объект
    public string Name { get; set; } // название продукта
    public string Description { get; set; } // описание
    public string Composition {  get; set; } // состав
    public double Weight { get; set; } // вес одной порции
    public double Proteins { get; set; } // белки
    public double Fats { get; set; } // жиры 
    public double Carbohydrates { get; set; } // углеводы
    public double Calorie {  get; set; } // кКал
    public double EnergyValue { get; set; } // клДж
    public string StorageConditions { get; set; } // условия хранения
    public string ExpirationsDate { get; set; } // Сроки годности
}