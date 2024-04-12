using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using FollowerBot.DataBase;
using Telegram.Bot.Types.ReplyMarkups;

class Program
{
    private static ITelegramBotClient botClient;
    private static ReceiverOptions receiverOptions;
    private static SQLiteScript sql;
    static async Task Main()
    {
        sql = new SQLiteScript();
        sql.createDB();
        botClient = new TelegramBotClient("7190916687:AAG4L9eYwyj8bLJtXajo6uTP-k-MuIkRdIs");
        receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = new[]
            {
                UpdateType.Message,
                UpdateType.CallbackQuery
            },
            ThrowPendingUpdates = true,
        };
        using var cts = new CancellationTokenSource();

        botClient.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions, cts.Token);
        Console.ReadKey();
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
                        var chat = message.Chat;

                        switch (message.Type)
                        {   
                            case MessageType.Unknown:
                                break;
                            case MessageType.Text:
                                if (message.Text == "/start")
                                {
                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        "Выбери клавиатуру:\n" +
                                        "/inline\n" +
                                        "/reply\n");
                                    return;
                                }
                                if (message.Text == "/inline")
                                {
                                    // Тут создаем нашу клавиатуру
                                    var inlineKeyboard = new InlineKeyboardMarkup(
                                        new List<InlineKeyboardButton[]>() // здесь создаем лист (массив), который содрежит в себе массив из класса кнопок
                                        {
                                        // Каждый новый массив - это дополнительные строки,
                                        // а каждая дополнительная строка (кнопка) в массиве - это добавление ряда

                                        new InlineKeyboardButton[] // тут создаем массив кнопок
                                        {
                                            InlineKeyboardButton.WithCallbackData("список поставщиков:", "ShippersBtn"),
                                        },
                                        new InlineKeyboardButton[]
                                        {
                                            InlineKeyboardButton.WithCallbackData("ТТК", "ttkBtn")
                                        },
                                        });
                                    await botClient.SendTextMessageAsync(
                                    chat.Id,
                                    "Это inline клавиатура!",
                                    replyMarkup: inlineKeyboard); // Все клавиатуры передаются в параметр replyMarkup

                                    return;
                                }
                                if (message.Text == "/reply")
                                {
                                    // Тут все аналогично Inline клавиатуре, только меняются классы
                                    // НО! Тут потребуется дополнительно указать один параметр, чтобы
                                    // клавиатура выглядела нормально, а не как абы что

                                    var replyKeyboard = new ReplyKeyboardMarkup(
                                        new List<KeyboardButton[]>()
                                        {
                                        new KeyboardButton[]
                                        {
                                            new KeyboardButton("Привет!"),
                                            new KeyboardButton("Пока!"),
                                        },
                                        new KeyboardButton[]
                                        {
                                            new KeyboardButton("Позвони мне!")
                                        },
                                        new KeyboardButton[]
                                        {
                                            new KeyboardButton("Напиши моему соседу!")
                                        }
                                        })
                                    {
                                        // автоматическое изменение размера клавиатуры, если не стоит true,
                                        // тогда клавиатура растягивается чуть ли не до луны,
                                        // проверить можете сами
                                        ResizeKeyboard = true,
                                    };

                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        "Это reply клавиатура!",
                                        replyMarkup: replyKeyboard); // опять передаем клавиатуру в параметр replyMarkup

                                    return;
                                }
                                    break;
                            default:
                                break;
                        }
                        return;
                    }
                case UpdateType.CallbackQuery: 
                    {
                        var callbackQuery = update.CallbackQuery;
                        var user = callbackQuery.From;
                        Console.WriteLine($"{user.FirstName} ({user.Id}) нажал на кнопку: {callbackQuery.Data}");
                        var chat = callbackQuery.Message.Chat;
                        switch (callbackQuery.Data)
                        {
                            // Data - это придуманный нами id кнопки, мы его указывали в параметре
                            // callbackData при создании кнопок. У меня это button1, button2 и button3

                            case "ShippersBtn":
                                {
                                    // В этом типе клавиатуры обязательно нужно использовать следующий метод
                                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                    // Для того, чтобы отправить телеграмму запрос, что мы нажали на кнопку
                                    var chatId = callbackQuery.Message.Chat.Id;
                                    var newInlineKeyboard = new InlineKeyboardMarkup
                                        (
                                             new List<InlineKeyboardButton[]>()
                                                {
                                                    new InlineKeyboardButton[]
                                                    {
                                                        InlineKeyboardButton.WithCallbackData("ИП Судин", "Shipper1"),
                                                    },
                                                    new InlineKeyboardButton[]
                                                    {
                                                        InlineKeyboardButton.WithCallbackData("...", "newButton2"),
                                                    }
                                                }
                                        );
                                    //await botClient.SendTextMessageAsync(chat.Id, $"Вы нажали на {callbackQuery.Data}");
                                    await botClient.SendTextMessageAsync(chatId, "Поставщики", replyMarkup: newInlineKeyboard);
                                    return;
                                }
                            case "Shipper1":
                                {
                                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id);
                                    var chatId = callbackQuery.Message.Chat.Id;
                                    for(int i = 0; i < 3; i++)
                                    {
                                        await botClient.SendTextMessageAsync(chat.Id, $"{sql.getFood(1)[i]}");
                                    }
                                    return;
                                }

                            case "ttkBtn":
                                {
                                    // А здесь мы добавляем наш сообственный текст, который заменит слово "загрузка", когда мы нажмем на кнопку
                                    await botClient.AnswerCallbackQueryAsync(callbackQuery.Id, "Тут может быть ваш текст!");

                                    await botClient.SendTextMessageAsync(
                                        chat.Id,
                                        $"Вы нажали на {callbackQuery.Data}");
                                    return;
                                }
                        }

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
        // Тут создадим переменную, в которую поместим код ошибки и её сообщение 
        var ErrorMessage = error switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => error.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }


}