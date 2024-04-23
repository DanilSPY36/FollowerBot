using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

public static class AccessUserLogic
{
    public static bool CheckUserAccess(Update update, _shippersContext usersFromDb, ITelegramBotClient botClient)
    {
        
        if(update.Type == UpdateType.Message)
        {
            var message = update.Message;
            var userName = message.From.Username;
            var userTgId = message.From.Id;

            InviteUser checkedUser = usersFromDb.InviteUsers.FirstOrDefault(u => u.tgUserId == userTgId);

            if (message.From.IsBot || (checkedUser.AccessUser == 0) && checkedUser != null) // скорее всего надо переписать и уточнить проверку
            {
                Console.WriteLine($"user data from tg:: Name = {userName} TelegramId = {userTgId}\n");
                Console.WriteLine($"user data from DB:: Name = {checkedUser.Name} TelegramId = {checkedUser.tgUserId} Access: {checkedUser.AccessUser}\n");
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

            InviteUser checkedUser = usersFromDb.InviteUsers.FirstOrDefault(u => u.tgUserId == userTgId);

            if (update.CallbackQuery.From.IsBot || checkedUser.AccessUser == 0)
            {
                Console.WriteLine($"user data from tg:: Name = {userName} TelegramId = {userTgId}\n");
                Console.WriteLine($"user data from DB:: Name = {checkedUser.Name} TelegramId = {checkedUser.tgUserId} Access: {checkedUser.AccessUser}\n");
                botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Отказ в доступе");

                return false;
            }
            else
            {
                return true;
            }
        }
    }
}