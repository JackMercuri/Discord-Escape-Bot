using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;


using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace EscapeProject
{
    class Program
    {
        public static DiscordSocketClient Client;
        private CommandService Commands;
        public static SocketCommandContext Context;

        public string token;


        static void Main(string[] args)
        {
            new Program().MainAsync().GetAwaiter().GetResult();
        }




        private async Task MainAsync()
        {
            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            Client.MessageReceived += Client_MessageReceived;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);

            Client.Ready += Client_Ready;
            Console.WriteLine("Please enter your bot token here:");
            token = Console.ReadLine();
            Client.Log += Client_Log;

            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();

            await Task.Delay(-1);
            
            
        }

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");
            
        }

        private async Task Client_Ready()
        {
            await Client.SetGameAsync("Escape Room", "https://i.imgur.com/5CpLgdP.jpg", ActivityType.Playing);
            

            States.canPlace = false;
            States.isPlaced = false;
            States.bladesOn = true;
            States.users.Clear();
            States.state = States.StatesList[0];
            
        }


        //Command Handler
        public async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            Context = new SocketCommandContext(Client, Message);

            //message conditions for bot ignore
            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            //prefix conditions
            int ArgPos = 0;
            if (!(Message.HasStringPrefix("!", ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser,ref ArgPos))) return;

            var Result = await Commands.ExecuteAsync(Context, ArgPos, null);
            if (!Result.IsSuccess)
            {
                Console.WriteLine($"{DateTime.Now} at Commands] Something went wrong with entering a command. Text: {Context.Message.Content} | Error: {Result.ErrorReason}");
            }
        }



    }
}
