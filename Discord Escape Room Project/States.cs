using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Reflection;
using System.IO;

using Discord;
using Discord.Commands;
using Discord.API;
using Discord.WebSocket;
using Discord.Rest;

namespace EscapeProject
{
    public class States 
    {
        public static bool canPlace;
        public static bool isPlaced;
        public static bool bladesOn;
        public static bool isDead;
        public static string state;
        public static System.Timers.Timer bleedTimer;
        public static System.Timers.Timer bleedReminder;
        public static System.Timers.Timer LaptopTimer;
        public static System.Timers.Timer GasTimer;
        public static bool timerStarted;


        public static string KidRoomState;
        public static string StudyState;


        public static IRole gasRole;
        public static SocketGuildUser gasUser;

        public static void setTimer()
        {
            bleedTimer = new System.Timers.Timer(1000 * 60 * 10);
            bleedReminder = new System.Timers.Timer(1000 * 60 * 3);
            bleedReminder.AutoReset = true;
            timerStarted = true;
            
            bleedTimer.Elapsed += TimerOnElapsed;
            bleedTimer.Enabled = true;

            bleedReminder.Elapsed += ReminderOnElapsed;
            bleedReminder.Enabled = true;
        }
        public static async void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Timer raised at time {e.SignalTime}");
            isDead = true;
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Florence Carter");
            Embed.WithDescription("you collapse to the ground, losing consciousness");
            Embed.AddField("> SYSTEM FAILURE", "> PROCESS END, RESTART REQUIRED");
            Embed.WithFooter("Type !Ready to restart");
            await Program.Context.Channel.SendMessageAsync("", false, Embed.Build());
            await Task.Delay(3000).ContinueWith(t => isDead = false);

            canPlace = false;
            isPlaced = false;
            bladesOn = true;
            timerStarted = false;
            users.Clear();
            state = StatesList[1];
            bleedTimer.Close();
            bleedTimer.Stop();
            bleedTimer.AutoReset = false;
            bleedReminder.AutoReset = false;
            bleedReminder.Close();
            bleedReminder.Stop();
            Inventory.Clear();
        }

        public static async void ReminderOnElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Timer raised at time {e.SignalTime}");
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Florence Carter");
            Embed.AddField("I can't stop the bleeding", "I need to get out of here before I pass out");
            await Program.Context.Channel.SendMessageAsync("", false, Embed.Build());

        }


        public static void setLaptopTimer()
        {
            LaptopTimer = new System.Timers.Timer(1000 * 60);

            LaptopTimer.Elapsed += LaptopTimerOnElapsed;
            LaptopTimer.Enabled = true;
        }

        public static async void LaptopTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Timer raised at time {e.SignalTime}");
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithDescription("The timer ran out");
            Embed.AddField("The laptop returns to the screen with the 'click' button", "I think I can give it another try");
            await Program.Context.Channel.SendMessageAsync("", false, Embed.Build());
            state = StatesList[14];
            LaptopTimer.Close();
            LaptopTimer.AutoReset = false;
        }


        public static void setGasTimer(SocketGuildUser user, IRole role)
        {
            GasTimer = new System.Timers.Timer(1000 * 60);;
            user.RemoveRoleAsync(role);

            GasTimer.Elapsed += GasTimerOnElapsed;

            gasRole = role;
            gasUser = user;

            GasTimer.Enabled = true;
        }

        public static async void GasTimerOnElapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine($"Timer raised at time {e.SignalTime}");
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithDescription("I wake up from unconsciousness");
            await Program.Context.Channel.SendMessageAsync("", false, Embed.Build());
            await gasUser.AddRoleAsync(gasRole);

            GasTimer.Close();
            GasTimer.AutoReset = false;
            GasTimer.Stop();
        }




        public static IList<string> StatesList = new List<string>()
        {
            "Init 1",
            "Ready 2",
            "RoomView 3",
            "Kitchen 4",
            "Cupboards 5",
            "Garbage Disposal 6",
            "Electric Pannel 7",
            "Cups 8",
            "Office Light 9",
            "Whiteboard 9",
            "Desk 10",
            "DeskDrawer 11",
            "Paperweight 12",
            "Picture 13",
            "Laptop 14",
            "Laptop Puzzle 15",
            "Laptop Solved 16",
            "Escaped 17"


        };

        public static IList<string> StatesListKids = new List<string>()
        {
            "Bedroom 0",
            "lock 1",
            "Beds 2",
            "Bedside Table 3",
            "Toybox 4",
            "Posters 5",
            "Escape 6",
        };

        public static IList<string> StatesListMaster = new List<string>()
        {
            "Bedroom 0",
            "Shelf 1",
            "Right Bedside Table 2",
            "Left Bedside Table 3",
            "Ensuite 4",
            "Sink 5",
            "Toilet 6",
            "Lounge 7",
            "TV 8"
        };

        //player 1 inventory
        public static IList<string> BedInventory = new List<string>()
        {

        };

        public static IList<string> StatesListStudy = new List<string>()
        {
            "Study 0",
            "Lock 1",
            "Desk 2",
            "Computer 3",
            "Bookcases 4",
            "Escape 5",
        };

        public static IList<string> StatesListKitchen = new List<string>()
        {
            "Kitchen 0",
            "Pantry 1",
            "Drawers 2",
            "Dishwasher 3",
            "Sink 4",
            "Dining Table 5",
            "Photos 6",
            "Clock 7",
            "Living Room 8",
            "EntertainmentUNit 9"
        };

        //player 2 inventory
        public static IList<string> StudyInventory = new List<string>()
        {

        };

        public static IList<string> plateslist = new List<string>()
        {

        };

        public static IList<string> dvdlist = new List<string>()
        {

        };

        public static IList<string> Inventory = new List<string>()
        {

        };


        public static IList<SocketGuildUser> users = new List<SocketGuildUser>()
        {

        };







    }
}
