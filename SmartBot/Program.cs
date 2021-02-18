using System.Net;
using System;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Newtonsoft.Json;
using System.Web.Helpers;
using System.Net.WebSockets;
using Telegram.Bot.Types;

namespace Awesome
{
    class Program
    {
        static ITelegramBotClient botClientOker;
        static ITelegramBotClient botClientAsker;
        static ITelegramBotClient botClientSender;
        static ITelegramBotClient botClientTalker;
        static ITelegramBotClient botClientExit;

        static void Main()
        {
            botClientOker = new TelegramBotClient("-------");

            botClientAsker = new TelegramBotClient("-------");

            botClientSender = new TelegramBotClient("-------");

            botClientTalker = new TelegramBotClient("-------");

            botClientExit = new TelegramBotClient("-------");


            //receive any initial response
            botClientOker.OnMessage += Bot_OnMessageOker;
            //botClientOker.StopReceiving();
            botClientOker.StartReceiving();

            //console output
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(
                 $"Wussup! I'm {botClientOker}."
                );
                System.Threading.Thread.Sleep(100);
                Console.Clear();
            }
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

        static  void Bot_OnMessageOker(object sender, MessageEventArgs e)
        {

            botClientOker.SendTextMessageAsync(
                     chatId: e.Message.Chat,
                        text: $"ok");
            //botClientOker.StopReceiving();

            botClientSender.OnMessage += Bot_OnMessageHello;
            botClientOker.StopReceiving();
            botClientSender.StartReceiving();
        }

        static void Bot_OnMessageExit(object sender, MessageEventArgs e)
        {
            botClientSender.StopReceiving();
            botClientAsker.StopReceiving();
            botClientOker.StopReceiving();
            botClientTalker.StopReceiving();
            Console.WriteLine("ok");
            return;
        }
        static async void Bot_OnMessageHello(object sender, MessageEventArgs e)
        {
            await botClientSender.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: $"Hey, wussup! I'm Robby~");



            botClientTalker.OnMessage += Bot_OnMessageTalker;
            botClientSender.StopReceiving();
            botClientTalker.StartReceiving();
        }


        static async void Bot_OnMessageTalker(object sender, MessageEventArgs e)
        {

            //e.Message.Text = default;

          // await botClient.SendStickerAsync(
            //chatId: e.Message.Chat,
           //sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-dali.webp");
           //sticker: "https://s.tcdn.co/ec5/c1b/ec5c1b75-12ea-45bd-aa7b-33491089b8e5/192/2.png");

            // System.Threading.Thread.Sleep(2000);

            // photo
            //    await botClient.SendPhotoAsync(
            //     chatId: e.Message.Chat,
            // photo: "https://www.capoliticalreview.com/wp-content/uploads/2017/08/Robot.jpeg",
            // caption: "how ya like my selfie?!",
            // parseMode: ParseMode.Html

            //);
            //System.Threading.Thread.Sleep(5000);

            await botClientTalker.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: $"~Well, nevermind. Better guess where I am...");
            // System.Threading.Thread.Sleep(2000);

            // video
            //  using (var stream = System.IO.File.OpenRead("C:/AAA/Sender/SmartBot/video-waves.mp4"))
            //  {
            //      await botClient.SendVideoNoteAsync(
            //       chatId: e.Message.Chat,
            //       videoNote: stream,
            //      duration: 47,
            //       length: 360 // value of width/height
            //     );
            //  }
            //  System.Threading.Thread.Sleep(5000);

            await botClientTalker.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: $"~OK. Let's go. What quote you want?");
            // System.Threading.Thread.Sleep(2000);



            //quotes start

            botClientAsker.OnMessage += Bot_OnMessageQuotesSender;
            botClientTalker.StopReceiving();

            botClientAsker.StartReceiving();
            // Console.WriteLine("pass 2");
            // Console.ReadKey();            //if (e.Message.Text != "btc")
            // {
            //     await botClient.SendTextMessageAsync(
            //            chatId: e.Message.Chat,
            //              text: $"xo xo");

            //  }
        }

        static void Bot_OnMessageQuotesSender(object sender, MessageEventArgs e)
        {

            string stringFlow3;
            if (e.Message.Text == "stop")
            {
               
                botClientSender.StopReceiving();
                botClientAsker.StopReceiving();
                botClientTalker.StopReceiving();
                botClientOker.StopReceiving();
                botClientExit.StartReceiving();
                return;
            }

            if ((e.Message.Text == "btc") || (e.Message.Text == "Btc") || (e.Message.Text == "BTC"))
            {
                 botClientSender.SendTextMessageAsync(
                      chatId: e.Message.Chat,
                      text: $"here your quotes:");
                System.Threading.Thread.Sleep(2000);

                if (e.Message.Text == "stop")
                {
                    botClientExit.OnMessage += Bot_OnMessageExit;
                    botClientSender.StopReceiving();
                    botClientAsker.StopReceiving();
                    botClientTalker.StopReceiving();
                    botClientOker.StopReceiving();
                    botClientExit.StartReceiving();
                }               

                var client = new WebClient();
                stringFlow3 = client.DownloadString("https://api.latoken.com/v2/ticker/92151d82-df98-4d88-9a4d-284fa9eca49f/0c3a106d-bde3-4c13-a26e-3fd2394529e5");
                
                if (stringFlow3 == "")
                {

                        botClientSender.SendTextMessageAsync(
                        chatId: e.Message.Chat,
                        text: $"b****, not received");

                }
                if (stringFlow3 != "")
                {
                    string stringFlow2 = stringFlow3.Substring(Math.Max(0, stringFlow3.Length - 10));
                    string stringFlow = stringFlow2.Remove(stringFlow2.Length - 2);

                    var quotes = stringFlow;
                    var charsToRemove = new string[] { "\"" };
                    foreach (var c in charsToRemove)
                    {
                        quotes = quotes.Replace(c, string.Empty);
                    }

                    var charsToRemove2 = new string[] { ":" };
                    foreach (var c in charsToRemove)
                    {
                        quotes = quotes.Replace(c, string.Empty);
                    }

                    //  string stringFlow4 = stringFlow.Remove(stringFlow.Length - 2);
                    // int x = Int32.Parse(stringFlow4);
                    //c
                    //int alarmPrice = 19145;
                    // str = str.Remove(str.Length - 3);
                    // int x = Convert.ToInt32(str);

                    // if (stringFlow4.Equals(alarmPrice))
                    //{
                    //    await botClient.SendTextMessageAsync(
                    //         chatId: e.Message.Chat,
                    //         text: $"btc: {stringFlow}");
                    //    System.Threading.Thread.Sleep(2000);
                    // }

                    bool b;

                    if (e.Message.Text == "stop")
                    {
                        botClientExit.OnMessage += Bot_OnMessageExit;
                        botClientSender.StopReceiving();
                        botClientAsker.StopReceiving();
                        botClientTalker.StopReceiving();
                        botClientOker.StopReceiving();
                        botClientExit.StartReceiving();
                        b = false;

                        stringFlow3 = null;
                        
                    }
                    else b = true;

                        
                    while (b)
                    {
                        // for (int i = 0; i < 5; i++)
                        // {

                        //    Console.ForegroundColor = ConsoleColor.DarkCyan;
                        //     Console.WriteLine($"Received a text message in chat ");
                        //    await botClientSender.SendTextMessageAsync(
                        //  chatId: e.Message.Chat,
                        //   text: $"btc: {quotes}");
                        //    System.Threading.Thread.Sleep(2500);
                        // }
                        //  for (int i = 0; i < 5; i++)
                        //  {
                        // ConsoleColor fgcolor = Console.ForegroundColor;
                       
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            Console.WriteLine($"Received a text message in chat ");
                            botClientSender.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: $"btc: {quotes}");
                            System.Threading.Thread.Sleep(2500);
                        //  }
                    }

                    // if (x <= alarmPrice)
                    //  {
                    //     await botClient.SendTextMessageAsync(
                    //         chatId: e.Message.Chat,
                    //          text: $"here we go");
                    //     System.Threading.Thread.Sleep(50000);
                    // }


                    //System.Threading.Thread.Sleep(500);
                    //{ DateTimeOffset.Now}

                    //botClientSender.StopReceiving();
                }

               // if (e.Message.Text == "stop")
               // {

               //     await botClientSender.SendTextMessageAsync(
               //               chatId: e.Message.Chat,
                //              text: $"xo xo~");

                //    botClientSender.StopReceiving();
                //    botClientAsker.StopReceiving();

               // }
                
            }

            else if (e.Message.Text == "stop")
            {
               botClientSender.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: $"xo xo~");

                botClientExit.OnMessage += Bot_OnMessageExit;
                botClientSender.StopReceiving();
                botClientAsker.StopReceiving();
                botClientTalker.StopReceiving();
                botClientOker.StopReceiving();
                botClientExit.StartReceiving();

                stringFlow3 = null;
            }

            else  //(e.Message.Text != "btc")
            {
                botClientSender.SendTextMessageAsync(
                          chatId: e.Message.Chat,
                          text: $"try another one");
            }            

        }// Sender Method

        
    } //program


}//namespace 
