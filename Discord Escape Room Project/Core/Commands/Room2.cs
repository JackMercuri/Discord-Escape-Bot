using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Rest;

namespace EscapeProject.Core.Commands
{

    public class Room2 : ModuleBase<SocketCommandContext>
    {


        //STUDY

        public static bool BookshelvesOpen;
        public static bool ComputerOpen;

        public static bool password1;
        public static bool password2;
        public static bool password3;
        public static bool password4;

        public static bool studyDoorOpen;


        [Command("Study"), Alias("study")]
        public async Task StudyOverview()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListStudy[0];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("Looks like I'm stuck in a study");
            embed.AddField("It's locked, but at least there's a door out of here", "It looks like theres an electronic !lock on the door");
            embed.AddField("There's a few things around me. There's a large !map of the earth in the middle of the room", "There's also a !workdesk with a few things on it like a computer, and some !bookshelves");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587889807145828352/image0.png?width=978&height=676");
            await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Lock"), Alias("lock")]
        public async Task ElectronicLock()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListStudy[1];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I inspect the electronic lock");

            embed.AddField("It's got four seperate password screens, each with four spaces for a password", "I'm pretty sure i'm going to have to find those passwords and input them one at a time.");
            await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("FREE"), Alias("free")]
        public async Task ElectronicLockAnswerPart1()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;
            if (States.StudyState == States.StatesListStudy[1] && studyDoorOpen == false)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I input the code");
                embed.AddField("The lock flashes a green light", "I think I got this password right");
                password1 = true;
                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());

                if (password1 == true && password2 == true && password3 == true && password4 == true)
                {
                    studyDoorOpen = false;
                    EmbedBuilder embed1 = new EmbedBuilder();
                    embed1.WithDescription("The door cracks open ");
                    embed1.AddField("Yes, I got all of the passwords right", "Time to move out of the study, looks like the next room is a !kitchen");
                    await Room1.StudyChannel.SendMessageAsync("", false, embed1.Build());

                    Room3.Kitchen = await Context.Guild.CreateRoleAsync("Kitchen", null, Color.Green, false, null);
                    var user2 = Context.User as SocketGuildUser;
                    await user2.AddRoleAsync(Room3.Kitchen);
                    Room3.KitchenChannel = await Context.Guild.CreateTextChannelAsync("Kitchen", null, null);

                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Context.Guild.EveryoneRole, OverwritePermissions.DenyAll(Room3.KitchenChannel));
                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Room3.Master_Bedroom, OverwritePermissions.DenyAll(Room3.KitchenChannel));
                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Room3.Kitchen, OverwritePermissions.AllowAll(Room3.KitchenChannel));

                    Room3.PlatesWrong = false;
                    Room3.LightonCans = false;
                    Room3.PlaqueGoop = true;
                    Room3.ClockOpen = false;
                    Room3.needlesIn = false;
                    Room3.nooseDown = false;

                }
            }

        }

        [Command("LIVE"), Alias("live")]
        public async Task ElectronicLockAnswerPart2()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;
            if (States.StudyState == States.StatesListStudy[1] && studyDoorOpen == false)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I input the code");
                embed.AddField("The lock flashes a green light", "I think I got this password right");
                password2 = true;
                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());

                if (password1 == true && password2 == true && password3 == true && password4 == true)
                {
                    studyDoorOpen = false;
                    EmbedBuilder embed1 = new EmbedBuilder();
                    embed1.WithDescription("The door cracks open ");
                    embed1.AddField("Yes, I got all of the passwords right", "Time to move out of the study, looks like the next room is a !kitchen");
                    await Room1.StudyChannel.SendMessageAsync("", false, embed1.Build());

                    Room3.Kitchen = await Context.Guild.CreateRoleAsync("Kitchen", null, Color.Green, false, null);
                    var user2 = Context.User as SocketGuildUser;
                    await user2.AddRoleAsync(Room3.Kitchen);
                    Room3.KitchenChannel = await Context.Guild.CreateTextChannelAsync("Kitchen", null, null);

                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Context.Guild.EveryoneRole, OverwritePermissions.DenyAll(Room3.KitchenChannel));
                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Room3.Master_Bedroom, OverwritePermissions.DenyAll(Room3.KitchenChannel));
                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Room3.Kitchen, OverwritePermissions.AllowAll(Room3.KitchenChannel));

                    Room3.PlatesWrong = false;
                    Room3.LightonCans = false;
                    Room3.PlaqueGoop = true;
                    Room3.ClockOpen = false;
                    Room3.needlesIn = false;
                    Room3.nooseDown = false;

                }
            }

        }

        [Command("MIND"), Alias("mind")]
        public async Task ElectronicLockAnswerPart3()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;
            if (States.StudyState == States.StatesListStudy[1] && studyDoorOpen == false)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I input the code");
                embed.AddField("The lock flashes a green light", "I think I got this password right");
                password3 = true;
                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());

                if (password1 == true && password2 == true && password3 == true && password4 == true)
                {
                    studyDoorOpen = false;
                    EmbedBuilder embed1 = new EmbedBuilder();
                    embed1.WithDescription("The door cracks open ");
                    embed1.AddField("Yes, I got all of the passwords right", "Time to move out of the study, looks like the next room is a !kitchen");
                    await Room1.StudyChannel.SendMessageAsync("", false, embed1.Build());

                    Room3.Kitchen = await Context.Guild.CreateRoleAsync("Kitchen", null, Color.Green, false, null);
                    var user2 = Context.User as SocketGuildUser;
                    await user2.AddRoleAsync(Room3.Kitchen);
                    Room3.KitchenChannel = await Context.Guild.CreateTextChannelAsync("Kitchen", null, null);

                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Context.Guild.EveryoneRole, OverwritePermissions.DenyAll(Room3.KitchenChannel));
                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Room3.Master_Bedroom, OverwritePermissions.DenyAll(Room3.KitchenChannel));
                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Room3.Kitchen, OverwritePermissions.AllowAll(Room3.KitchenChannel));

                    Room3.PlatesWrong = false;
                    Room3.LightonCans = false;
                    Room3.PlaqueGoop = true;
                    Room3.ClockOpen = false;
                    Room3.needlesIn = false;
                    Room3.nooseDown = false;
                }
            }

        }

        [Command("WIRE"), Alias("wire")]
        public async Task ElectronicLockAnswerPart4()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;
            if (States.StudyState == States.StatesListStudy[1] && studyDoorOpen == false)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I input the code");
                embed.AddField("The lock flashes a green light", "I think I got this password right");
                password4 = true;
                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());

                if (password1 == true && password2 == true && password3 == true && password4 == true)
                {
                    studyDoorOpen = false;
                    EmbedBuilder embed1 = new EmbedBuilder();
                    embed1.WithDescription("The door cracks open ");
                    embed1.AddField("Yes, I got all of the passwords right", "Time to move out of the study, looks like the next room is a !kitchen");
                    await Room1.StudyChannel.SendMessageAsync("", false, embed1.Build());

                    Room3.Kitchen = await Context.Guild.CreateRoleAsync("Kitchen", null, Color.Green, false, null);
                    var user2 = Context.User as SocketGuildUser;
                    await user2.AddRoleAsync(Room3.Kitchen);
                    Room3.KitchenChannel = await Context.Guild.CreateTextChannelAsync("Kitchen", null, null);

                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Context.Guild.EveryoneRole, OverwritePermissions.DenyAll(Room3.KitchenChannel));
                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Room3.Master_Bedroom, OverwritePermissions.DenyAll(Room3.KitchenChannel));
                    await Room3.KitchenChannel.AddPermissionOverwriteAsync((IRole)Room3.Kitchen, OverwritePermissions.AllowAll(Room3.KitchenChannel));

                    Room3.PlatesWrong = false;
                    Room3.LightonCans = false;
                    Room3.PlaqueGoop = true;
                    Room3.ClockOpen = false;
                    Room3.needlesIn = false;
                    Room3.nooseDown = false;
                }
            }

        }

        [Command("Map"), Alias("map")]
        public async Task Globe()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I inspect the map of the globe");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586728114961252352/image0.png?width=820&height=614");
            embed.AddField("It looks pretty much like a regular map", "Except printed on the top is 11° 20' 59.99'' N, and printed on the bottom is 142° 11' 60.00'' E");
            await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("WorkDesk"), Alias("workdesk")]
        public async Task StudyDesk()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListStudy[2];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I inspect the desk");
            embed.AddField("On closer inspection, it looks like next to the computer, there's a few framed photos and also a small marble !bust", "The computer itself is on, but it's stuck on a password screen");
            
            await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Computer"), Alias("computer")]
        public async Task Computer()
        {

            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.StudyState == States.StatesListStudy[2])
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I move to the computer");
                if (ComputerOpen == false)
                {
                    embed.AddField("This doesn't seem to running on Windows, in the corner is says RomeOS. At least the username is already filled in. The user's name is Helen", "Maybe that'll help me find out what the password is!");
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586804671448875018/image0.png?width=1015&height=677");
                } else
                {
                    embed.AddField("The screen continues to flash and glitch out", "The only thing I can make out on the screen is the number 7");
                }

                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
                States.StudyState = States.StatesListStudy[3];
            }

        }

        [Command("48454c454e")]
        public async Task ComputerPassword()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.StudyState == States.StatesListStudy[3] && ComputerOpen == false)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I put in the password");
                embed.AddField("Looks like I got it right! The screen begins to flash and stutter", "The only visible thing on the screen is the number 7");
                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
                ComputerOpen = true;
            }

        }

        [Command("Photos"), Alias("Photos")]
        public async Task Photos()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;


            if (States.StudyState == States.StatesListStudy[2])
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I inspect the photos");
                embed.AddField("What a suprise! There's nothing out of the ordinary here.", "guess I should check something else out.");
                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
            }
        }

        [Command("Bust"), Alias("bust")]
        public async Task Bust()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.StudyState == States.StatesListStudy[2])
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I inspect the bust");
                embed.WithImageUrl("https://cdn.discordapp.com/attachments/575130189281886218/586735015379992576/image0.png");
                embed.AddField("The bust seems to be of some famous old guy", "It has the inscription 'Ludwig von Köchel' on it");
                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
            }
        }

        [Command("BookShelves"), Alias("bookshelves")]
        public async Task BookShelves()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListStudy[4];
            EmbedBuilder embed = new EmbedBuilder();
            EmbedBuilder embed1 = new EmbedBuilder();
            embed.WithDescription("I move towards the two bookshelves");
            if (BookshelvesOpen == false)
            {
                embed.AddField("The bookshelf on the left has a bunch of nearly identical books, all of them have an atlas printed on the front, and each has a letter on the spine", "But it also has an empty shelf that looks bit different from the other ones, like there's cameras behind where some books should go.");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586787028709146625/image0.png?width=1005&height=677");
                embed.AddField("The books on the left have some different letters on them: three with A, one with R, one with N, one with M and one with I", "Maybe they place them out in a certain order I can make a word?");
                embed1.AddField("The shelf on the right has some unique books on it, but there behind a locked glass screen.", "I can't make out what the books are yet. Maybe there's a way to open it!");
                embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586821423507701763/image0.png?width=1005&height=677");
            } else
            {
                embed.AddField("The bookshelf on the left has now has its books ordered in the specific order", "I feel a little safer not having as many camera things looking at me now!");
                embed1.AddField("The shelf on the right has some unique books on it, and with the door open, I can check out the books", "There's three books inside. First, 'Functions of the Brain'. Second, 'Encryption Techniques Through History'. And third, 'Unsolved: Case Files'");
                embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586821484715048970/image0.png?width=1005&height=677");
            }


            await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
            await Room1.StudyChannel.SendMessageAsync("", false, embed1.Build());
        }

        [Command("Mariana"), Alias("mariana","MARIANA")]
        public async Task BookShelvesPuzzzleAnswer()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.StudyState == States.StatesListStudy[4] && BookshelvesOpen == false)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I order some books on the shelf to spell out 'Mariana'");
                embed.AddField("The glass cover on the other shelf opens up", "I should check out the bookshelves again!");
                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
                BookshelvesOpen = true;
            }
        }

        [Command("FunctionsOftheBrain"), Alias("Functions of the Brain")]
        public async Task InteractableBook1()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.StudyState == States.StatesListStudy[4] && BookshelvesOpen == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I open up the book");
                embed.AddField("What a suprise! It's a book about the human brain, how fun.", "Theres a large diagram of the different sections of the brain. The way the make it look, it like a childs stuffed toy.");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586804236948340736/image2.png?width=677&height=677");
                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
            }
        }

        [Command("Encryption Techniques Through History"), Alias("EncryptionTechniquesThroughoutHistory")]
        public async Task InteractableBook2()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if(States.StudyState == States.StatesListStudy[4] && BookshelvesOpen == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I try to open up the book");
                embed.AddField("Huh, it's glued shut!", "Well, looks like the red cover has an image of Julius Caeser on the front");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586804236449349648/image1.png?width=677&height=677");
                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
            }
        }

        [Command("Unsolved: Case Files"), Alias("Unsolved:CaseFiles")]
        public async Task InteractableBook3()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Study.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.StudyState == States.StatesListStudy[4] && BookshelvesOpen == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I open up the book");
                embed.AddField("It's a black covered book with white writing, how mysterious.", "inside theres an image of a police tape");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586804235837112331/image0.png?width=677&height=677");
                await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());
            }
        }

        //KIDS BEDROOM
        [Command("Bag"), Alias("bag")]
        public async Task BedInventory()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(NavID)) return;


            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithDescription("Here is everything I have:");
            foreach (string item in States.BedInventory)
            {
                Embed.AddField(item, "-----------------");
            }
            await Context.Channel.SendMessageAsync("", false, Embed.Build());
            //await Context.Channel.SendMessageAsync("", false, Embed.Build());

        }


        [Command("KidsRoom"), Alias("kidsroom")]
        public async Task KidRoomOverview()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListKids[0];
            EmbedBuilder embed = new EmbedBuilder();
            EmbedBuilder embed1 = new EmbedBuilder();
            embed.WithDescription("Looks like I'm stuck in a kids bedroom");
            embed.AddField("The door's locked with a !codelock, but at least there's a way out", "In the room are two !beds, a !bedsidetable with a bunch of stuff on it, some !posters on the wall, and a !toybox in the corner ");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587063987645054987/Bedroom.jpg?width=1015&height=677");
            embed1.AddField("On top of the door, there's a wooden letter K", "Maybe it's the initial of whoever this room belongs to.");
            embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586735277406420992/image0.png?width=1015&height=677");
            embed1.WithFooter("I can check what I've got in my !bag at any time if I need.");
            await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
            await Room1.BedroomChannel.SendMessageAsync("", false, embed1.Build());
        }

        [Command("codelock"), Alias("CodeLock")]
        public async Task ElectronicLock1()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListKids[1];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I move to the lock on the door");
            embed.AddField("Looks like the lock needs a 3 digit passcode", "Well, I'd better find it soon, don't want to dawdle around here!");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586728818287443976/image0.png?width=1015&height=677");
            await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("331"), Alias("331")]
        public async Task ElectronicLock1Answer()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListKids[1])
            {
                
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I input the code");
                embed.AddField("YES, it's the right code.", "The door cracks open as the codelock flashes its green light. Looks like the next room is the !masterbedroom");
                await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());

                Room3.Master_Bedroom = await Context.Guild.CreateRoleAsync("Master-Bedroom", null, Color.Red, false, null);

                Room3.MasterBedChannel = await Context.Guild.CreateTextChannelAsync("Master-Bedroom", null, null);
                var user2 = Context.User as SocketGuildUser;
                await user2.AddRoleAsync(Room3.Master_Bedroom);

                await Room3.MasterBedChannel.AddPermissionOverwriteAsync((IRole)Room3.Master_Bedroom, OverwritePermissions.AllowAll(Room3.MasterBedChannel));
                await Room3.MasterBedChannel.AddPermissionOverwriteAsync((IRole)Context.Guild.EveryoneRole, OverwritePermissions.DenyAll(Room3.MasterBedChannel));
                await Room3.MasterBedChannel.AddPermissionOverwriteAsync((IRole)Room3.Kitchen, OverwritePermissions.DenyAll(Room3.MasterBedChannel));

                Room3.ensuiteLocked = true;
                Room3.writingRevealed = false;
                Room3.mirrorClean = true;
                Room3.bathDrained = false;




            }

        }

        [Command("Beds"), Alias("beds", "bed", "Bed")]
        public async Task Beds()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListKids[2];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I move to the beds");
            if (States.BedInventory.Contains("Key"))
            {
                embed.AddField("The pillows are in a bit of a mess now, there's fluff everywhere", "I hope whoever owns this room doesn't mind.");
            } else
            {
                embed.AddField("Looks like there's bunch of different coloured pillows stacked in a very precarious fashion", "Why are they organised like that? Is there something to these pillows? Maybe I should choose a colour.");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586774858008887317/image0.png?width=1015&height=677");
            }

            await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("TemporalLobe"), Alias("pink", "temporallobe", "Pink")]
        public async Task BedsAnswer()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;
            if (States.KidRoomState == States.StatesListKids[2] && !States.BedInventory.Contains("Key"))
            {
                if (States.BedInventory.Contains("Scalpel"))
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    EmbedBuilder embed1 = new EmbedBuilder();
                    embed.WithDescription("I cut into the pink pillow");
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586774858785095690/image1.png?width=1015&height=677");
                    embed1.AddField("There's a key inside!", "better take this for later.");
                    embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586742184339046400/image0.png?width=1015&height=677");
                    await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
                    await Room1.BedroomChannel.SendMessageAsync("", false, embed1.Build());
                    States.BedInventory.Add("Key");
                }
                else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithDescription("I grab the pillow");
                    embed.AddField("I can't be sure but I think there might be something inside.", "Maybe I can find a way to cut it open.");
                    await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
                }
            }

        }

        [Command("Blue"), Alias("blue", "Yellow", "yellow", "Red", "red", "Green", "green")]
        public async Task BedsWrongAnswer()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;
            if (States.KidRoomState == States.StatesListKids[2])
            {
                if (States.BedInventory.Contains("Scalpel"))
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithDescription("I cut into the pillow");
                    embed.AddField("There's nothing inside.", "Maybe I should try something else.");
                    await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
                } else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithDescription("I grab the pillow");
                    embed.AddField("I can't tell if theres anything interesting to it.", "Maybe I can find a way to cut it open.");
                    await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
                }

            }

        }

        [Command("BedsideTable"), Alias("bedsidetable")]
        public async Task BedsideTable()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListKids[3];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I move to the bedside table");
            embed.AddField("On the bedside table there is a night light shaped like a fish, a set of !crayons, and a music box", "I can always return to the centre of the !kidsroom");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587066814341578752/Bedside_Table.jpg?width=1015&height=677");
            await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
        }


        [Command("Crayons"), Alias("crayons", "GrabCrayons", "grabcrayons")]
        public async Task Crayons()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListKids[3] && !States.BedInventory.Contains("Crayons"))
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I grab the crayons");
                embed.AddField("Theres a bunch of different colours", "Maybe if I get bored of trying to escape, I can draw a picture");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586737166651228180/image0.png?width=1015&height=677");
                await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
                States.BedInventory.Add("Crayons");

            }

        }


        [Command("MusicBox"), Alias("musicbox")]
        public async Task MusicBox()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListKids[3])
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I inspect the music box");
                if (States.BedInventory.Contains("Key"))
                {
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586742273836843041/image0.png?width=1015&height=677");
                    embed.AddField("I turn the key on the music box", "It starts to play a classical tune, sounds familliar");
                    using (var client = new System.Net.Http.HttpClient())
                    using (var testStream = await client.GetStreamAsync("https://cdn.discordapp.com/attachments/572605573821104128/584952213869166646/music-box.mp3"))
                        await Context.Channel.SendFileAsync(testStream, "music-box.mp3");
                }
                else
                {
                    embed.AddField("Looks like there's a key lock on the music box", "Maybe I should come back here if I find one");
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586742225719918613/image0.png?width=1015&height=677");
                }
                await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
            }

        }


        [Command("Toybox"), Alias("toybox")]
        public async Task Toybox()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListKids[4];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I open the toybox");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586745952098844686/image0.png?width=1015&height=677");
            if (States.BedInventory.Contains("Scalpel"))
            {
                embed.AddField("There's nothing left in the toybox.", "Maybe I should check around the room some more.");
                await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
            } else
            {
                EmbedBuilder embed1 = new EmbedBuilder();
                EmbedBuilder embed2 = new EmbedBuilder();
                embed.AddField("Inside the box there's a scalpel and a saw.", "I'll pick these up incase I need it for later.");
                embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586746010425098251/image0.png?width=1015&height=677");
                embed2.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586761915854749718/image0.png?width=1015&height=677");
                States.BedInventory.Add("Scalpel");
                States.BedInventory.Add("Saw");
                await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
                await Room1.BedroomChannel.SendMessageAsync("", false, embed1.Build());
                await Room1.BedroomChannel.SendMessageAsync("", false, embed2.Build());
            }


        }


        [Command("Posters"), Alias("posters")]
        public async Task Posters()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Room1.Kids_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListKids[5];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I move to the posters");
            embed.AddField("There's two posters on the wall.", "The one on the left is a of a table of some kind.");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586812736415989781/image0.png?width=444&height=444");
            EmbedBuilder embed1 = new EmbedBuilder();
            embed1.AddField("The poster on the right is a sequence of letters.", "There's no words in the sequence though, maybe its a code of some sort.");
            embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/586813001164914728/image0.jpg?width=478&height=676");
            await Room1.BedroomChannel.SendMessageAsync("", false, embed.Build());
            await Room1.BedroomChannel.SendMessageAsync("", false, embed1.Build());
        }



        //DEBUG
        [Command("Exit")]
        public async Task Exit()
        {
            await Room1.StudyChannel.DeleteAsync();
            await Room1.BedroomChannel.DeleteAsync();
            await Room1.Kids_Bedroom.DeleteAsync();
            await Room1.Study.DeleteAsync();
            await Room3.KitchenChannel.DeleteAsync();
            await Room3.MasterBedChannel.DeleteAsync();
            await Room3.Master_Bedroom.DeleteAsync();
            await Room3.Kitchen.DeleteAsync();
            await Room1.GameChannel.DeleteAsync();
            await Room1.Navigator.DeleteAsync();
            await Room4.LoungeChannel.DeleteAsync();
            await Room4.LivingRoomChannel.DeleteAsync();
            await Room4.LoungeRoom.DeleteAsync();
            await Room4.LivingRoom.DeleteAsync();
        }


    }
}
