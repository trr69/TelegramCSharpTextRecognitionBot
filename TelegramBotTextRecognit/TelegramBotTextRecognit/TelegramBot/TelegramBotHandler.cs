using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBotTextRecognit.OCR;

namespace TelegramBotTextRecognit.TelegramBot
{
    internal class TelegramBotHandler
    {
        private static long _chatId;
        private static Message _message;
        private static ITelegramBotClient _client;
        public TelegramBotHandler(ITelegramBotClient client, long chatId, Message message)
        {
            _chatId = chatId;
            _message = message;
            _client = client;
        }

        public async Task startTelegramBotHandling()
        {
            if (_message != null)
            {
                if(_message.Text == "/start")
                {
                    await _client.SendTextMessageAsync(_chatId, "Отправь мне картинку и я отправлю тебе текст с нее");
                }




                else if (_message.Type == Telegram.Bot.Types.Enums.MessageType.Photo)

                {

                    var photo = _message.Photo.LastOrDefault(); 

                    if (photo != null)

                    {
                        var file = await _client.GetFileAsync(photo.FileId); 
                        using (var stream = new MemoryStream())
                        {
                            await _client.DownloadFileAsync(file.FilePath, stream); 
                            var filePath = $"{_chatId}.jpg";
                            using (var fileStream = new FileStream(filePath, FileMode.Create))

                            {
                                stream.Seek(0, SeekOrigin.Begin);
                                stream.CopyTo(fileStream);
                                

                            }

                        }
                        string textInImage = TextRe.RecognizRussianText(_chatId);
                        await _client.SendTextMessageAsync(_chatId, textInImage);

                    }

                }
            }
            
        }
        

    }
}
