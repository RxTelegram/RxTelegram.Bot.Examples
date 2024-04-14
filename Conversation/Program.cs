using RxTelegram.Bot.Examples.Conversation.StateMachine;
using RxTelegram.Bot.Interface.BaseTypes.Requests.Messages;
using RxTelegram.Bot.Interface.Setup;

namespace RxTelegram.Bot.Examples.Conversation;

public static class Echo
{
    private const string BotToken = "<PASTE YOUR BOT TOKEN HERE>";
    private static FsmExecutor _fsm = new();
    private static TelegramBot? _bot;

    public static async Task Main(string[] args)
    {
        _bot = new TelegramBot($"{BotToken}");
        var me = await _bot.GetMe();
        Console.WriteLine($"Bot name: @{me.Username}");

        var subscription = _bot.Updates
            .Update
            .Subscribe(HandleReceivedMessage,
                exception => Console.WriteLine($"An error has occured: {exception.Message}"));
        Console.ReadLine();
        subscription.Dispose();
    }

    private static void HandleReceivedMessage(Update update)
    {
        if (update.Message == null)
            return;
        var message = update.Message;
        Console.WriteLine($"{message.From.Username}: {message.Text}");
        if (!_fsm.IsCompleted())
        {
            _bot?.SendMessage(new SendMessage
            {
                ChatId = message.Chat.Id,
                Text =  _fsm.HandleInput(message.Text)
            });
            if (!string.IsNullOrEmpty(_fsm.GetCurrentPrompt()))
            {
                _bot?.SendMessage(new SendMessage
                {
                    ChatId = message.Chat.Id,
                    Text = _fsm.GetCurrentPrompt()
                });
            }

            return;
        }
        
        _bot?.SendMessage(new SendMessage
        {
            ChatId = message.Chat.Id,
            Text = "I'm done!"
        });
            
        // Reset the state machine if the conversation is completed
        _fsm = new FsmExecutor();
    }
}