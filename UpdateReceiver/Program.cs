using System.Text.Json;
using RxTelegram.Bot;
using RxTelegram.Bot.Interface.BaseTypes;
using RxTelegram.Bot.Interface.BaseTypes.Enums;
using RxTelegram.Bot.Interface.BaseTypes.Requests.Attachments;
using RxTelegram.Bot.Interface.BaseTypes.Requests.Chats;
using RxTelegram.Bot.Interface.BaseTypes.Requests.Messages;
using RxTelegram.Bot.Utils.Keyboard;

public class Programm
{
    public static void Main()
    {
        var bot = new TelegramBot("<PASTE YOUR BOT TOKEN HERE>");
        const string chatId = "<CHATID>";

        // Send some messages so we can trigger updates
        bot.SendMessage(new SendMessage { Text = "Hello!", ChatId = chatId, });
        bot.SendChatAction(new SendChatAction { ChatId = chatId, Action = ChatActions.Typing });
        bot.SendPoll(new SendPoll
        {
            AllowsMultipleAnswers = true, ChatId = chatId, Question = "What's your favorite color?",
            Options = new[] { "Red", "Blue", "Green" },
        });
        bot.SendChatAction(new SendChatAction { ChatId = chatId, Action = ChatActions.Typing });
        bot.SendMessage(new SendMessage
        {
            Text = "Please chose a place!", ChatId = chatId,
            ReplyMarkup = new InlineKeyboardMarkup
            {
                InlineKeyboard = KeyboardBuilder.CreateInlineKeyboard()
                    .BeginRow()
                    .AddCallbackData("Berlin", "Berlin")
                    .AddCallbackData("London", "London")
                    .EndRow()
                    .BeginRow()
                    .AddCallbackData("Paris", "Paris")
                    .EndRow()
                    .Build()
            }
        });
        
        Console.WriteLine("All Messages send. Receiving updates now (press enter to stop):");
        // Wait a bit 
        Thread.Sleep(4000);

        // Subscribe to all possible updates
        using var updateSubscription = bot.Updates.Update.Subscribe(Serialize, OnError);
        using var messageSubscription = bot.Updates.Message.Subscribe(Serialize, OnError);
        using var editedMessageSubscription = bot.Updates.EditedMessage.Subscribe(Serialize, OnError);
        using var inlineQuerySubscription = bot.Updates.InlineQuery.Subscribe(Serialize, OnError);
        using var chosenInlineResultSubscription = bot.Updates.ChosenInlineResult.Subscribe(Serialize, OnError);
        using var callbackQuerySubscription = bot.Updates.CallbackQuery.Subscribe(Serialize, OnError);
        using var channelPostSubscription = bot.Updates.ChannelPost.Subscribe(Serialize, OnError);
        using var editedChannelPostSubscription = bot.Updates.EditedChannelPost.Subscribe(Serialize, OnError);
        using var shippingQuerySubscription = bot.Updates.ShippingQuery.Subscribe(Serialize, OnError);
        using var preCheckoutQuerySubscription = bot.Updates.PreCheckoutQuery.Subscribe(Serialize, OnError);
        using var pollSubscription = bot.Updates.Poll.Subscribe(Serialize, OnError);
        using var pollAnswerSubscription = bot.Updates.PollAnswer.Subscribe(Serialize, OnError);
        Console.ReadLine();
    }


    private static void Serialize<T>(T update) =>
        Console.WriteLine(JsonSerializer.Serialize(update, new JsonSerializerOptions { WriteIndented = true }));

    private static void OnError(Exception obj) => Console.WriteLine("Error: " + obj.Message);
}