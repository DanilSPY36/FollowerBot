using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using FollowerBot.DataBase;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;


public class BotMenu
{
    private ReplyKeyboardMarkup? startMenu;
    private InlineKeyboardMarkup? baseListMenu;
    private InlineKeyboardMarkup? shippersMenu;
    private InlineKeyboardMarkup? ttkMenu;

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
    public InlineKeyboardMarkup ShippersMenu(int shippersAmount)
    {
        List<List<InlineKeyboardButton>> buttonRows = new List<List<InlineKeyboardButton>>();

        for (int i = 0; i < shippersAmount; i++)
        {
            var button = new InlineKeyboardButton("shippers menu")
            {
                Text = $"Button {i + 1}",
                CallbackData = $"button_{i + 1}"
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
}