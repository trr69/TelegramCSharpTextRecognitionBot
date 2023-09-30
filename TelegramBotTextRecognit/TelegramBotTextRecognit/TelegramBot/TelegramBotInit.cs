using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;

namespace TelegramBotTextRecognit.TelegramBot
{
    class TelegramBotInit
    {
        private static TelegramBotClient client;
        private static string telegramBotTOKEN = "6543629856:AAFc3GgdxGo4to8mqsecUYm5BC_zOXBE0Xo";
        public static async void startTelegramBot()
        {
            client = new TelegramBotClient(telegramBotTOKEN);
            client.StartReceiving(Update, Error);

            while(true)
            {
                Task.Delay(1000);
            }
        }
        async static Task Update(ITelegramBotClient client, Update update, CancellationToken token)
        {
            var message = update.Message;
            var chatId = message.Chat.Id;
            TelegramBotHandler teleHandler = new TelegramBotHandler(client, chatId, message);
            await teleHandler.startTelegramBotHandling();
        }

        async static Task Error(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            throw new NotImplementedException();
        }

    }
}
