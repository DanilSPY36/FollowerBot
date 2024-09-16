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
    private InlineKeyboardMarkup? ttkMenu;
    private InlineKeyboardMarkup? itemsShipperMenu;

    public List<Items> itemsList = null!;
    public List<Shipper> shipperList = null!;
    public List<DrinkTTK> drinkList = null!;

    public BotMenu(Context _context) 
    {
        this.itemsList = new List<Items>(_context.Items.ToList());
        shipperList = _context.Shippers.ToList();

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
    public InlineKeyboardMarkup TTKMenu()
    {
        var buttonRows = new List<List<InlineKeyboardButton>>();

        var categories = new string[]
        {
        "season", "classic", "Draft", "Coffee mixes", "Tea", "Fruits", "Kids",
        "Какао Beam-to-bar", "FM Каши", "FM Выпечка", "FM Йогурты", "FM Чиа-пудинги", "FM Хот-доги", "Back"
        };

        for (int i = 0; i < categories.Length; i++)
        {
            var button = new InlineKeyboardButton($"category_{i + 1}")
            {
                Text = categories[i],
                CallbackData = $"category_{i + 1}"
            };
            buttonRows.Add(new List<InlineKeyboardButton> { button });
        }

        ttkMenu = new InlineKeyboardMarkup(buttonRows);

        return ttkMenu;
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

    public Items returnItemInfo()
    {


        return null;
    }
}