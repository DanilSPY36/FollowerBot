using FollowerBot.Models;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;


public class BotMenu
{
    private ReplyKeyboardMarkup? startMenu;
    private InlineKeyboardMarkup? baseListMenu;
    private InlineKeyboardMarkup? shippersMenu;
    private InlineKeyboardMarkup? categoryMenu;
    private InlineKeyboardMarkup? itemsShipperMenu;
    private InlineKeyboardMarkup? ttkDrinkMenu;



    public List<Items> itemsList = null!;
    public List<Shipper> shipperList = null!;
    public List<DrinkTTK> drinkList = null!;

    public BotMenu(ShippersContext _context) 
    {
        this.itemsList = _context.Items.ToList();
        this.shipperList = _context.Shippers.ToList();

    }
    public ReplyKeyboardMarkup StartMenu()
    {
        var buttonRows = new List<KeyboardButton[]>()
    {
        new KeyboardButton[]
        {
            new KeyboardButton("base products"),
            new KeyboardButton("shippers"),
            new KeyboardButton("TTK"),
        }
    };

        startMenu = new ReplyKeyboardMarkup(buttonRows)
        {
            ResizeKeyboard = true
        };

        return startMenu;
    }
    public InlineKeyboardMarkup BaseListMenu()
    {
        baseListMenu = new InlineKeyboardMarkup
        (
            new List<InlineKeyboardButton[]>()
            {
                new InlineKeyboardButton[]
                {
                    InlineKeyboardButton.WithCallbackData("bottled drinks"),
                    InlineKeyboardButton.WithCallbackData("snacks"),
                    InlineKeyboardButton.WithCallbackData("bakery"),
                },

            });
        return baseListMenu;
    }
    public InlineKeyboardMarkup ShippersMenu(List<Shipper> shippers)
    {
        List<List<InlineKeyboardButton>> buttonRows = new List<List<InlineKeyboardButton>>();


        foreach (var ship in shippers)
        {
            var button = new InlineKeyboardButton("shippers menu")
            {
                Text = $"{ship.ID} || {ship.Name}",
                CallbackData = $"shipper||{ship.ID}"
            };
            buttonRows.Add(new List<InlineKeyboardButton> { button });
        }

        shippersMenu = new InlineKeyboardMarkup(buttonRows);

        return shippersMenu;
    }
    public InlineKeyboardMarkup ItemsShipperMenu(List<Items> items, int shipperId)
    {
        List<List<InlineKeyboardButton>> buttonRows = new List<List<InlineKeyboardButton>>();

        foreach (var item in items)
        {
            //Console.WriteLine(item.ShipperId + "||" + shipperId);
            if(item.ShipperId == shipperId)
            {
                var button = new InlineKeyboardButton("List items menu")
                {
                    Text = $"{item.Name}",
                    CallbackData = $"item||{item.Id}"
                };
                buttonRows.Add(new List<InlineKeyboardButton> { button });
            }
        }

        itemsShipperMenu = new InlineKeyboardMarkup(buttonRows);

        return itemsShipperMenu;
    }
    public InlineKeyboardMarkup CategoryMenu(List<DimCategory> categories)
    {
        var buttonRows = new List<List<InlineKeyboardButton>>();

        foreach (var item in categories)
        {
            var button = new InlineKeyboardButton($"{item.Category}") 
            {
                Text = item.Category,
                CallbackData = $"category||{item.Id}"
            };
            buttonRows.Add(new List<InlineKeyboardButton> { button });
        }

        categoryMenu = new InlineKeyboardMarkup(buttonRows);

        return categoryMenu;
    }

    public InlineKeyboardMarkup TTKDrinkMenu(List<DrinkTTK> drinkTTKs, int categoryId)
    {
        InlineKeyboardButton[][] buttonRows = new InlineKeyboardButton[drinkTTKs.Count][];

        // Остальной код остается без изменений

        foreach (var drink in drinkTTKs)
        {
            Console.WriteLine(drink.CategoryId + "||" + categoryId);
            if (drink.CategoryId == categoryId)
            {
                var button = new InlineKeyboardButton("list ttk menu")
                {
                    Text = $"{drink.Name}",
                    CallbackData = $"drink||{drink.ID}"
                };
                // Используйте массив для добавления кнопки в соответствующий ряд
                buttonRows = buttonRows.Append(new InlineKeyboardButton[] { button }).ToArray();
            }
        }
        ttkDrinkMenu = new InlineKeyboardMarkup(buttonRows);

        return ttkDrinkMenu;
    }

    public Items returnItemInfo()
    {


        return null;
    }
}