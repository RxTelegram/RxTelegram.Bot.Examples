using System;
using System.Threading.Tasks;
using RxTelegram.Bot.Interface.BaseTypes;
using RxTelegram.Bot.Interface.BaseTypes.Requests.Messages;

namespace RxTelegram.Bot.Examples.HelloWorld;

public static class HelloWorld
{
    private static TelegramBot Bot { get; set; }
    private const string BotToken = "<PASTE YOUR BOT TOKEN HERE>";

    public static async Task Main()
    {
        Bot = new TelegramBot($"{BotToken}");
        var me = await Bot.GetMe();
        Console.WriteLine($"Bot name: @{me.Username}");

        var subscription = Bot.Updates.Message.Subscribe(HandleReceivedMessage);
        Console.ReadLine();
        subscription.Dispose();
    }

    private static void HandleReceivedMessage(Message message)
    {
        Console.WriteLine($"{message.From.Username}: {message.Text}");
        Bot.SendMessage(new SendMessage
        {
            ChatId = message.Chat.Id,
            Text = $"Hello World, {message.From.FirstName ?? message.From.Username}"!
        });
    }
}