using FollowerBot.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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

    private InlineKeyboardMarkup? registrationMenu;


    public List<Items> itemsList = null!;
    public List<Shipper> shipperList = null!;

    public List<Spot> spotList = null!;
    public List<User> userList = null!;
    public List<Position> positionList = null!;
 
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
    public InlineKeyboardMarkup TTKDrinkMenu(TTK_Context _ttkContext, int categoryId)
    {
        var drinkTTKs = _ttkContext.DrinkTTKs.ToList();
        var volumesDrinks = _ttkContext.DimVolumes.ToList();
        List<List<InlineKeyboardButton>> buttonRows = new List<List<InlineKeyboardButton>>();

        var adderDrinkNames = new List<string>();
        foreach (var drink in drinkTTKs)
        {
            if (drink.CategoryId == categoryId)
            {
                if (!adderDrinkNames.Contains(drink.Name))
                {
                    adderDrinkNames.Add(drink.Name);

                    List<InlineKeyboardButton> row = new List<InlineKeyboardButton>();
                    for (int i = 0; i < drinkTTKs.Count; i++)
                    {
                        if (drink.Name == drinkTTKs[i].Name)
                        {
                            var button = new InlineKeyboardButton("list ttk menu")
                            {
                                Text = $"{drinkTTKs[i].Name} {volumesDrinks[drinkTTKs[i].VolumeId - 1].Volume}",
                                CallbackData = $"drink||{drinkTTKs[i].Id}"
                            };
                            row.Add(button);
                        }
                    }
                    buttonRows.Add(row);
                }
            }
        }
        ttkDrinkMenu = new InlineKeyboardMarkup(buttonRows);
        return ttkDrinkMenu;
    }

    public InlineKeyboardMarkup RegistrationMenu()
    {
        List<List<InlineKeyboardButton>> buttonRows = new List<List<InlineKeyboardButton>>()
        {
            new List<InlineKeyboardButton>() 
            {
                new InlineKeyboardButton("reg Menu")
                {
                    Text = "Информация о боте",
                    CallbackData = "BotInfobtn"
                },
                new InlineKeyboardButton("reg Menu")
                {
                    Text = "Зарегистрироваться",
                    CallbackData = "regbtn"
                }
            }
        };

        registrationMenu = new InlineKeyboardMarkup(buttonRows);
        return registrationMenu;
    }
}