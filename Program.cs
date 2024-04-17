using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using FollowerBot.DataBase;
using Telegram.Bot.Types.ReplyMarkups;
using System.Security.Permissions;
using FollowerBot;
using System;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
//7190916687:AAG4L9eYwyj8bLJtXajo6uTP-k-MuIkRdIs
class Program
{
    private static ITelegramBotClient _botClient;
    private static ReceiverOptions _receiverOptions;
    private static BotMenu _botMenu;
    private static DbContextOptionsBuilder<DrinkTTKDbContext> builder;
   
    static async Task Main()
    {
        builder = new DbContextOptionsBuilder<DrinkTTKDbContext>();
        
        builder.UseNpgsql("DefaultConnection");

        var options = builder.Options;
        using (var context = new DrinkTTKDbContext(options))
        {
        }
        _botMenu = new BotMenu();
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
        if (update.Type == UpdateType.Message && update.Message.Type == MessageType.Text)
        {
            var message = update.Message;

            if (message.From.IsBot == false)
            {
                await HandleNewUser(message);
            }
        }
        try
        {
           
            switch (update.Type)
            {
                case UpdateType.Message:
                    {
                        var message = update.Message;
                        var user = message.From;
                        var chatInfo = message.Chat;

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
                                await botClient.SendTextMessageAsync(chatInfo.Id, "Тут все ттк, все то что ты должен знать, как Отче наш", replyMarkup: _botMenu.TTKMenu());
                                Console.WriteLine($"{user.Username} Send: {message.Text}");
                                break;
                            case "shippers":
                                await botClient.SendTextMessageAsync(chatInfo.Id, "Тут все твои поставщики", replyMarkup: _botMenu.ShippersMenu(5));
                                Console.WriteLine($"{user.Username} Send: {message.Text}");
                                break;
                            default:
                                break;
                        }
                        
                        
                        return;
                    }
                case UpdateType.CallbackQuery:
                    {
                        return;
                    }
                case UpdateType.InlineQuery:
                    {
                        return;
                    }
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
    private static async Task HandleNewUser(Message message)
    {
        var chatId = message.Chat.Id;
        await _botClient.SendTextMessageAsync(chatId, "/start");
    }
}