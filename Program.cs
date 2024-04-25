using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using System.Security.Permissions;
using FollowerBot;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using FollowerBot.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;
//7190916687:AAG4L9eYwyj8bLJtXajo6uTP-k-MuIkRdIs
class Program
{
    private static ITelegramBotClient _botClient;
    private static ReceiverOptions _receiverOptions;
    private static BotMenu _botMenu;

    private static TTK_Context _ttkContext;
    private static ShippersContext _shippersContext;
    public static List<User> inviteUsers = null!;
    static async Task Main()
    {
        _ttkContext = new TTK_Context();
        _shippersContext = new ShippersContext();

        _ttkContext.Database.EnsureCreated();
        _shippersContext.Database.EnsureCreated();

        try
        {
        _ttkContext.DrinkTTKs.Load();

        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex.ToString());
        }

        _ttkContext.DimCategories.Load();
        _ttkContext.DimContainers.Load();
        _ttkContext.DimVolumes.Load();

        _shippersContext.Shippers.Load();

        /*_context.Database.EnsureCreated();
        _context.Items.Load();
        _context.Shippers.Load();
        //_context.DrinkTTKs.Load();
        try
        {
            _context.InviteUsers.Load();
        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex.ToString());
        }*/


        _botMenu = new BotMenu(_shippersContext);
        _botClient = new TelegramBotClient("7190916687:AAG4L9eYwyj8bLJtXajo6uTP-k-MuIkRdIs");

        _receiverOptions = new ReceiverOptions 
        {
            AllowedUpdates = new[]
            {
                UpdateType.Message,
                UpdateType.CallbackQuery
            },

            ThrowPendingUpdates = true,
        };


        using var cts = new CancellationTokenSource();
        _botClient.StartReceiving(UpdateHandler, ErrorHandler, _receiverOptions, cts.Token); 
        var me = await _botClient.GetMeAsync(); 
        Console.WriteLine($"{me.FirstName} запущен!");
        await Task.Delay(-1);
    }
    private static async Task UpdateHandler(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        try
        {
            switch (update.Type)
            {
                case UpdateType.Message:
                    {
                        var message = update.Message;
                        var user = message.From;
                        var chatInfo = message.Chat;
                        Console.WriteLine($"{user.Username} Пишет хуйню следующего характера: {message.Text}\n");


                        switch (message.Text)
                        {
                            case "/start":
                                await botClient.SendTextMessageAsync(chatInfo.Id, "Твоя главная клавиатура.", replyMarkup: _botMenu.StartMenu());
                                Console.WriteLine($"{user.Username} Send: {message.Text}");
                                break;
                            case "base products":
                                await botClient.SendTextMessageAsync(chatInfo.Id, "Тут все продукты батончики, бутылочные напитки и выпечка", replyMarkup: _botMenu.BaseListMenu());
                                Console.WriteLine($"{user.Username} Send: {message.Text}");
                                break;
                            case "TTK":
                                await botClient.SendTextMessageAsync(chatInfo.Id, "Тут все ттк, все то что ты должен знать, как Отче наш", replyMarkup: _botMenu.CategoryMenu(_ttkContext.DimCategories.ToList()));
                                Console.WriteLine($"{user.Username} Send: {message.Text}");
                                break;
                            case "shippers":
                                await botClient.SendTextMessageAsync(chatInfo.Id, "Тут все твои поставщики", replyMarkup: _botMenu.ShippersMenu(_shippersContext.Shippers.ToList()));
                                Console.WriteLine($"{user.Username} Send: {message.Text}");
                                break;
                            default:
                                break;
                        }
                        return;
                    }
                case UpdateType.CallbackQuery:
                    {
                        var message = update.Message;
                        var callbackQuery = update.CallbackQuery;
                        var user = callbackQuery.From;
                        Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");
                        var chat = callbackQuery.Message.Chat;

                        if (callbackQuery.Data.Contains("category"))
                        {
                            var choseCategoryId = callbackQuery.Data.Split("||");
                            await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                            Console.WriteLine($"{choseCategoryId[0]} || {choseCategoryId[1]}");
                            await botClient.SendTextMessageAsync(chat.Id, "список :", replyMarkup: _botMenu.TTKDrinkMenu(_ttkContext, int.Parse(choseCategoryId[1])));
                        }
                        if (callbackQuery.Data.Contains("drink"))
                        {
                            var choseCategoryId = callbackQuery.Data.Split("||");
                            await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                            Console.WriteLine($"{choseCategoryId[0]} || {choseCategoryId[1]}");
                            await botClient.SendTextMessageAsync(chat.Id, "список :", replyMarkup: _botMenu.TTKDrinkMenu(_ttkContext, int.Parse(choseCategoryId[1])));
                        }
                        if (callbackQuery.Data.Contains("shipper"))
                        {
                            var choseShipperId = callbackQuery.Data.Split("||");
                            await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                            Console.WriteLine($"{choseShipperId[0]} || {choseShipperId[1]}");
                            await botClient.SendTextMessageAsync(chat.Id, "список :", replyMarkup: _botMenu.ItemsShipperMenu(_shippersContext.Items.ToList(), int.Parse(choseShipperId[1])));
                        }
                        if (callbackQuery.Data.Contains("item"))
                        {
                            await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                            var choseShipperId = callbackQuery.Data.Split("||");
                            Console.WriteLine($"{choseShipperId[0]} || {choseShipperId[1]}");
                            var matchedItem = _botMenu.itemsList[int.Parse(choseShipperId[1]) - 1];
                            await botClient.SendTextMessageAsync(chat.Id, $"{matchedItem.Name}\n" +
                                $"\n========================================\n" +
                                $"Описание: {matchedItem.Description}" +
                                $"\n========================================\n" +
                                $"Состав: {matchedItem.Composition}" +
                                $"\n========================================\n" +
                                $"Вес 1 порции: {matchedItem.Weight}" +
                                $"\n========================================\n" +
                                $"Белки, гр: {matchedItem.Proteins}" +
                                $"\n========================================\n" +
                                $"Жиры, гр: {matchedItem.Fats}" +
                                $"\n========================================\n" +
                                $"Углеводы, гр: {matchedItem.Carbohydrates}" +
                                $"\n========================================\n" +
                                $"Калорийность, ккал: {matchedItem.Calorie}" +
                                $"\n========================================\n" +
                                $"КлДж: {matchedItem.EnergyValue}" +
                                $"\n========================================\n" +
                                $"Сроки хранения: {matchedItem.StorageConditions}" +
                                $"\n========================================\n" +
                                $"Условия хранения: {matchedItem.ExpirationsDate}" +
                                $"\n========================================\n");
                        }
                    }
                    return;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
       
    
    private static Task ErrorHandler(ITelegramBotClient botClient, Exception error, CancellationToken cancellationToken)
    {
        var ErrorMessage = error switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }
    /*
    private static async Task HandleNewUser(Message message)
    {
        //long chatId = message.Chat.Id; // Идентификатор чата
        //long userId = message.From.Id; // Идентификатор пользователя
        //string firstName = message.From.FirstName; // Имя пользователя
        //string lastName = message.From.LastName; // Фамилия пользователя
        //string username = message.From.Username; // Имя пользователя (username)
        _context.InviteUsers.Add(new InviteUser(message.From.Username, 
            message.From.FirstName, 
            message.From.LastName,
            message.Chat.Id,
            message.From.Id));
    }*/
}