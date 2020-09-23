using System;
using System.Threading.Tasks;
using RxTelegram.Bot.Interface.BaseTypes.Requests.Messages;

namespace RxTelegram.Bot.Examples.Echo
{
    public static class Echo
    {
        private const string BotToken = "<PASTE YOUR BOT TOKEN HERE>";
        public static async Task Main()
        {
            var bot = new TelegramBot($"{BotToken}");
            var me = await bot.GetMe();
            Console.WriteLine($"Bot name: @{me.Username}");

            var subscription = bot.Updates.Message.Subscribe(x =>
                {
                    bot.SendMessage(new SendMessage
                    {
                        ChatId = x.Chat.Id,
                        Text = x.Text
                    });
                },
                exception => Console.WriteLine($"An error has occured: {exception.Message}"));
            Console.ReadLine();
            subscription.Dispose();
        }
    }
}