using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public static class AccessUserLogic
{
    private static User newUser = null!;
    private static RegistrationState userInfo = null!;
    public static bool CheckUserAccess(Update update, SpotsContext usersFromDb, ITelegramBotClient botClient, BotMenu _botMenu)
    {
        usersFromDb.Users.LoadAsync();
        if (update.Type == UpdateType.Message)
        {
            var message = update.Message;
            var userName = message.From.Username;
            var userTgId = message.From.Id;

            newUser = usersFromDb.Users.FirstOrDefault(u => u.tgUserId == userTgId);

            if (message.From.IsBot) // если сообщение от бота
            {
                botClient.SendTextMessageAsync(update.Message.Chat.Id, "Ботам тут не рады");
                return false;
            }
            else if (newUser == null) // если пользователя нет в бд
            {
                //botClient.SendTextMessageAsync(update.Message.Chat.Id, "Привет. я твой будущий путеводитель по продуктам, но чтобы мной начать пользоваться надо пройти быструю регистрацию!", replyMarkup: _botMenu.RegistrationMenu());
                Console.WriteLine($"{userName} Send: {message.Text}");
                newUser = new User()
                {
                    tgName = userName,
                    tgFirstName = message.From.FirstName,
                    tgLastName = message.From.LastName,
                    tgChatId = update.Message.Chat.Id,
                    tgUserId = userTgId
                };
                usersFromDb.Users.Add(newUser);
                usersFromDb.SaveChangesAsync();
                
                return false;
            }
            else if (newUser.AccessUser == 0) // если пользователь есть в бд но нет доступа 
            {
                Console.WriteLine($"user data from tg:: Name = {userName} TelegramId = {userTgId}\n");
                Console.WriteLine($"user data from DB:: Name = {newUser.FirstName} TelegramId = {newUser.tgUserId} Access: {newUser.AccessUser}\n");
                botClient.SendTextMessageAsync(update.Message.Chat.Id, "Отказ в доступе");
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            var userName = update.CallbackQuery.From.Username;
            var userTgId = update.CallbackQuery.From.Id;


            if (update.CallbackQuery.From.IsBot) // update от бота
            {

                botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Отказ в доступе");

                return false;
            }
            else if (newUser == null) // update от ненайденного пользователя
            {
                return false;
            }
            else if (update.CallbackQuery.Data == "regbtn")
            {
                botClient.EditMessageTextAsync(chatId: update.CallbackQuery.Message.Chat.Id, messageId: update.CallbackQuery.Message.MessageId, "Отказ в доступе");

                return false;

            }
            else if(newUser.AccessUser == 0) 
            {

                usersFromDb.Users.Update(newUser);
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}