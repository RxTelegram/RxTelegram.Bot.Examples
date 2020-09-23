using System;
using System.Threading.Tasks;
using RxTelegram.Bot.Interface.BaseTypes;
using RxTelegram.Bot.Interface.BaseTypes.Requests.Callbacks;
using RxTelegram.Bot.Interface.BaseTypes.Requests.Messages;

namespace RxTelegram.Bot.Examples.InlineKeyboard
{
    internal static class InlineKeyboard
    {
        private static TelegramBot Bot { get; set; }
        private const string BotToken = "<PASTE YOUR BOT TOKEN HERE>";

        public static async Task Main()
        {
            Bot = new TelegramBot($"{BotToken}");
            var me = await Bot.GetMe();
            Console.WriteLine($"Bot name: @{me.Username}");

            var subscriptionMessage = Bot.Updates.Message.Subscribe(HandleReceivedMessage,
                exception => Console.WriteLine($"An error has occured: {exception.Message}"));
            var subscriptionCallbackQuery = Bot.Updates.CallbackQuery.Subscribe(HandleCallbackQuery,
                exception => Console.WriteLine($"An error has occured: {exception.Message}"));
            Console.ReadLine();
            subscriptionMessage.Dispose();
            subscriptionCallbackQuery.Dispose();
        }

        private static void HandleCallbackQuery(CallbackQuery callbackQuery)
        {
            Bot.AnswerCallbackQuery(new AnswerCallbackQuery
            {
                Text = callbackQuery.Data,
                CallbackQueryId = callbackQuery.Id
            });
        }
        
        private static void HandleReceivedMessage(Message message)
        {
            if (message.Text.Length != 0)
            {
                Bot.SendMessage(new SendMessage
                {
                    ChatId = message.Chat.Id,
                    ReplyMarkup = new InlineKeyboardMarkup
                    {
                        InlineKeyboard = new[]
                        {
                            new[]
                            {
                                new InlineKeyboardButton
                                {
                                    CallbackData = "You clicked on Abc",
                                    Text = "Abc"
                                },
                            },
                            new[]
                            {
                                new InlineKeyboardButton
                                {
                                    CallbackData = "You clicked on 1",
                                    Text = "1"
                                },
                                new InlineKeyboardButton
                                {
                                    CallbackData = "You clicked on 2",
                                    Text = "2"
                                }
                            }
                        }
                    },
                    Text = "Here is a inline keyboard!"
                });
            }
        }
    }
}