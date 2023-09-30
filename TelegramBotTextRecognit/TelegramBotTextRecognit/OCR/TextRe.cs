using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace TelegramBotTextRecognit.OCR
{
    internal class TextRe
    {
        public static string RecognizRussianText(long chatId)
        {
            string textInimage = "Error";
            string imagePath = $"{chatId}.jpg";
            using (var engine = new TesseractEngine(@"C:\Users\asd\Documents\GitHub\TelegramCSharpTextRecognitionBot\TelegramBotTextRecognit\TelegramBotTextRecognit\Testdata", "rus", EngineMode.Default))
            {
                using (var img = Pix.LoadFromFile(imagePath))
                {
                    using (var page = engine.Process(img))
                    {
                        textInimage = page.GetText();

                        return textInimage;
                    }
                }
            }
        }
    }
}
