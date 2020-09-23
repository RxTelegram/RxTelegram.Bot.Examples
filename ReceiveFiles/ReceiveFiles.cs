using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using RxTelegram.Bot.Api;
using RxTelegram.Bot.Interface.BaseTypes;
using RxTelegram.Bot.Interface.BaseTypes.Requests.Messages;
using File = System.IO.File;

namespace RxTelegram.Bot.Examples.ReceiveFiles
{
    internal static class ReceiveFiles
    {
        private static TelegramBot Bot { get; set; }
        private const string BotToken = "<PASTE YOUR BOT TOKEN HERE>";

        public static async Task Main()
        {
            Bot = new TelegramBot($"{BotToken}");
            var me = await Bot.GetMe();
            Console.WriteLine($"Bot name: @{me.Username}");

            var subscription = Bot.Updates.Message.Subscribe(HandleReceivedMessage,
                exception => Console.WriteLine($"An error has occured: {exception.Message}"));
            Console.ReadLine();
            subscription.Dispose();
        }

        private static void HandleReceivedMessage(Message message)
        {
            RxTelegram.Bot.Interface.BaseTypes.File file = null;
            if (message.Photo != null && message.Photo.Any())
            {
                var photo = message.Photo.OrderBy(x => x.Height).FirstOrDefault();
                if (photo != null) file = Bot.GetFile(photo.FileId).Result;
            }
            else if (message.Document != null)
            {
                file = Bot.GetFile(message.Document.FileId).Result;
            }
            else if (message.Video != null)
            {
                file = Bot.GetFile(message.Video.FileId).Result;
            }
            else if (message.Audio != null)
            {
                file = Bot.GetFile(message.Audio.FileId).Result;
            }

            if (file == null)
            {
                Bot.SendMessage(new SendMessage {ChatId = message.Chat.Id, Text = "Please send me a file!"});
                return;
            }
            var photoData = Bot.DownloadFileByteArray(file).Result;
            var filename = file.FilePath.Split("/").LastOrDefault();
            File.WriteAllBytes(
                Path.Combine(Environment.CurrentDirectory, $"Output/{filename}"), photoData);
        }
    }
}