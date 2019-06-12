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

    public class Room3 : ModuleBase<SocketCommandContext>
    {
        public static RestRole Kitchen;
        public static RestRole Master_Bedroom;
        public static RestTextChannel MasterBedChannel;
        public static RestTextChannel KitchenChannel;

        //KITCHEN

        public static bool LightonCans;
        public static bool PlaqueGoop;
        public static bool ClockOpen;
        public static bool needlesIn;
        public static bool nooseDown;


        [Command("Kitchen"), Alias("kitchen")]
        public async Task KitchenOverview()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListKitchen[0];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I walk into the kitchen area");
            embed.AddField("On the other side of the kitchen are are two big doors, maybe they lead into the living room" , "Looks like they don't feel like opening unfortunately.");
            embed.AddField("The kitchen has a long marble island in the middle, a !pantry to the right, and behind the island is an L-shaped benchtop with some !drawers", "There's also a !dishwasher next to the !sink, how quaint.");
            embed.AddField("Past the island is the !diningtable, Theres stuff over there but its too hard to see from here.", "Finally, there's an old clock on the wall, doesn't look like it's working.");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587825602707456029/image0.png?width=1015&height=677");
            embed.WithFooter("I can check out !myinventory at any time");
            await KitchenChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("noose"), Alias("Noose", "Hang" , "hang")]
        public async Task NOOSE()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (nooseDown == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I tie the rope around my neck ");
                embed.AddField("The room begins to darken as you loose consciousness.", "You hope this really will be a way out.");
                await KitchenChannel.SendMessageAsync("", false, embed.Build());

                await Task.Delay(3000).ContinueWith(t => SetupRoom4());
            }

        }

        public async Task SetupRoom4()
        {
            var user = Context.User as SocketGuildUser;
            Room4.LivingRoom = await Context.Guild.CreateRoleAsync("Living-Room", null, Color.LightGrey, false, null);
            await user.RemoveRoleAsync(Kitchen);
            await user.RemoveRoleAsync(Room1.Study);
            await user.AddRoleAsync(Room4.LivingRoom);
            Room4.LivingRoomChannel = await Context.Guild.CreateTextChannelAsync("Living-Room", null, null);

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I wake up in a living room.");
            embed.AddField("Did it work", "I think I'm still alive");
            embed.AddField("There's no way back to the previous rooms", "This room is clean and starkly white. Like the other rooms, there aren't any windows here");
            embed.AddField("There's two large couches, centred around a coffee table.", "There’s an entertainment unit across from the couches, it has a flat-screen tv, heaps of picture frames, shelfloads full of dvds, and other shelves decorated with various items. This is the kind of place I could relax, If it weren't for the whole death and trap thing.");
            embed.AddField("To my left, there's a sleek, gas-lit fireplace with a grate over it", "Maybe this will finally be a way out");
            await Room4.LivingRoomChannel.SendMessageAsync("", false, embed.Build());

            await Room4.LivingRoomChannel.AddPermissionOverwriteAsync((IRole)Context.Guild.EveryoneRole, OverwritePermissions.DenyAll(Room4.LivingRoomChannel));
            await Room4.LivingRoomChannel.AddPermissionOverwriteAsync((IRole)Room4.LoungeRoom, OverwritePermissions.DenyAll(Room4.LivingRoomChannel));
            await Room4.LivingRoomChannel.AddPermissionOverwriteAsync((IRole)Room4.LivingRoom, OverwritePermissions.AllowAll(Room4.LivingRoomChannel));



            Room4.Tape1Watched = false;
            Room4.Tape2Watched = false;
            Room4.Tape3Watched = false;
            Room4.Tape4Watched = false;
            Room4.Tape5Watched = false;
            Room4.TVon = false;
            Room4.GrateOpen = false;
            Room4.Q1Answered = false;
            Room4.Q2Answered = false;
            Room4.Q3Answered = false;
            Room4.Q4Answered = false;
            Room4.Q5Answered = false;
            Room4.dvdWrong = false;
            Room4.figureInRoom = false;

        }

        [Command("Pantry"), Alias("pantry")]
        public async Task Pantry()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListKitchen[1];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I open up the pantry");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587127774326685716/image0.png?width=1015&height=677");
            if (LightonCans == false)
            {
                embed.AddField("The shelves are full of generic cans", "Even though they're all closed, they smell putrid");
            }
            else
            {
                if (States.StudyInventory.Contains("Knife") && !States.Inventory.Contains("Plaque") )
                {
                    EmbedBuilder embed1 = new EmbedBuilder();
                    embed1.AddField("The laser from the clock shines onto one speciific can. maybe theres something to it", "I'll cut it open with the knife");
                    embed1.AddField("I plunge the knife into the can. It’s full of a goopy red substance.", "I found out a small plaque inside, but its covered in goop, I'll have to clean it ");
                    embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587144865989525515/image1.png?width=1015&height=677");
                    await KitchenChannel.SendMessageAsync("", false, embed1.Build());
                    States.StudyInventory.Add("Plaque");
                    PlaqueGoop = true;
                } else
                {
                    embed.AddField("The light from the clock shines onto one speciific can. maybe theres something to it", "But I don't have anything to open it with");
                }

            }

            await KitchenChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Drawers"), Alias("drawers")]
        public async Task Drawers()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListKitchen[2];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I open up the drawers");

            if (!States.StudyInventory.Contains("Plate-LXXIX") && !States.StudyInventory.Contains("Plate-LXXXII") && !States.StudyInventory.Contains("Plate-LXXX") && !States.StudyInventory.Contains("Plate-LXXII") && !States.StudyInventory.Contains("Plate-LXV") && !States.StudyInventory.Contains("Plate-LXXVIII"))
            {
                States.StudyInventory.Add("Plate-LXV");
                States.StudyInventory.Add("Plate-LXXVIII");
                States.StudyInventory.Add("Plate-LXXX");
                States.StudyInventory.Add("Plate-LXXIX");
                States.StudyInventory.Add("Plate-LXXII");
                States.StudyInventory.Add("Plate-LXXXII");

                embed.AddField("There is a variety of different plates in there", "Looks like they’re porcelain, they each have similar roman numerals on them, all beginning with L");
                embed.AddField("They do look a bit dirty, maybe they could go for a wash", "I'll pick these up incase I need them, maybe I can smash them over the head of whoever put me here!");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587451111535935508/image1.png?width=1015&height=677");
            } else
            {
                embed.AddField("There's nothing left in the drawers", "Maybe the plates need to go somewhere");
            }
            await KitchenChannel.SendMessageAsync("", false, embed.Build());
        }



        [Command("Dishwasher"), Alias("dishwasher")]
        public async Task Dishwasher()
        {

            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListKitchen[3];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I open up the dishwasher");
            embed.AddField("It's almost empty, Maybe it needs to be filled", "On the top of the dishwasher the letters ASCII engraved.");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587456957028499456/Dishwasher.jpg?width=1015&height=677");
            if (!States.StudyInventory.Contains("Needles") && needlesIn == false)
            {
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.AddField("On top of the dishwasher is a drying rack with a couple of silver needles on it", "I'll take these for later");
                embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587451001489850388/image0.png?width=1015&height=677");
                await KitchenChannel.SendMessageAsync("", false, embed1.Build());
                States.StudyInventory.Add("Needles");
            }
            await KitchenChannel.SendMessageAsync("", false, embed.Build());
        }

        public static bool PlatesWrong;

        [Command("Place"), Alias("place")]
        public async Task Place(string platenumber)
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Room3.Kitchen.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.StudyInventory.Contains(platenumber))
            {
                States.plateslist.Add($"{platenumber}");
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription($"I place {platenumber} in the dishwasher");
                await KitchenChannel.SendMessageAsync("", false, embed.Build());
                //await Context.Channel.SendMessageAsync("", false, embed.Build());
                States.StudyInventory.Remove(platenumber);
                
            }

            if (States.plateslist[0] == "Plate-LXXIX" && States.plateslist[1] == "Plate-LXXXII" && States.plateslist[2] == "Plate-LXXX" && States.plateslist[3] == "Plate-LXXII" && States.plateslist[4] == "Plate-LXV" && States.plateslist[5] == "Plate-LXXVIII")
            {
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.WithDescription("Once the last plate's in, I hear a sound from the clock");
                embed1.AddField("The glass plate on the clock has opened up.", "I now have access to the clock itself.");
                await KitchenChannel.SendMessageAsync("", false, embed1.Build());
                ClockOpen = true;
            }
            if (States.plateslist.Count == 6 && !(States.plateslist[5] == "Plate-LXXVIII") || !(States.plateslist[4] == "Plate-LXV") || !(States.plateslist[3] == "Plate-LXXII") || !(States.plateslist[2] == "Plate-LXXX") || !(States.plateslist[1] == "Plate-LXXXII") || !(States.plateslist[0] == "Plate-LXXIX"))
            {
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.WithDescription("Nothing seems to be happening");
                embed1.AddField("Hmmm ... something's not right.", "maybe I got the order wrong. I should !pickup all the plates and try again.");
                PlatesWrong = true;

                await KitchenChannel.SendMessageAsync("", false, embed1.Build());
            }
        }

        [Command("pickup")]
        public async Task grabPlates()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Room3.Kitchen.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (PlatesWrong == true)
            {
                foreach (var plate in States.plateslist)
                {
                    States.StudyInventory.Add(plate);
                    States.plateslist.Remove(plate);
                }
                PlatesWrong = false;
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.WithDescription("I grab all the plates out of the dishwasher.");
                await KitchenChannel.SendMessageAsync("", false, embed1.Build());
            }


        }

        [Command("MyInventory"), Alias("myinventory")]
        public async Task Inventory()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Room3.Kitchen.Id);
            if (!user.Roles.Contains(NavID)) return;


            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithDescription("Here is everything I have:");
            foreach (string item in States.StudyInventory)
            {
                Embed.AddField(item, "-----------------");
            }
            await KitchenChannel.SendMessageAsync("", false, Embed.Build());
            //await Context.Channel.SendMessageAsync("", false, Embed.Build());

        }

        [Command("DISHES")]
        public async Task DISHES()
        {


            foreach (var thingo in States.plateslist)
            {
                Console.WriteLine(thingo);
            }
            Console.WriteLine(States.plateslist.Count);
        }


        [Command("Sink"), Alias("sink")]
        public async Task Sink()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListKitchen[4];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I walk up to the sink");
            if (States.StudyInventory.Contains("Hook"))
            {
                embed.AddField("There seems to be something in there, but it's too far down to reach by hand", "I'll use the hook!");
                embed.AddField("I managed to pull up a piece of paper", "The paper reads 'the water goes at the same rate as there are months of 30 days, then again on the day i receive five golden rings, and lastly on the day my woeful child was born'");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587451002597277723/image2.png?width=1015&height=677");
            } else
            {
                embed.AddField("There seems to be something in there, but it's too far down to reach", "Maybe if I had something to help me grab it.");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587451110634291201/image0.png?width=1015&height=677");
            }

            if (States.StudyInventory.Contains("Plaque") && PlaqueGoop == true)
            {
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.AddField("I wash the dirty plaque under the sink", "It reveals an engraving that reads 'Lamassu'.");
                embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587144865138212885/image0.png?width=1015&height=677");
                await KitchenChannel.SendMessageAsync("", false, embed1.Build());
                PlaqueGoop = false;
            }
            await KitchenChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("DiningRoom"), Alias("diningroom", "DiningTable", "Dining Table", "dining table", "diningtable")]
        public async Task DiningRoom()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListKitchen[5];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I walk forward to the dining table");
            embed.AddField("There are dishes on the table, but all the food looks rotten. I don't really want to eat it.", "Each plate has the rotting head of a different animal. There's one from a horse, a goat, a pig, and a sheep.");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/588180802223996948/image0.png?width=1015&height=677");
            embed.AddField("Next to the dining table, there's a few wierd !photos on the wall.", "Maybe I should investigate.");
            await KitchenChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Pig"), Alias("pig")]
        public async Task PigsHead()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;
            
            if (States.StudyState == States.StatesListKitchen[5] && !States.StudyInventory.Contains("Hook"))
            {
                States.StudyState = States.StatesListKitchen[5];
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I open up the pigs head");
                embed.AddField("Inside, there's a hook and some fishing line.", "This could be really usefull for hard to reach places, I'll take it!");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587854961254203412/image0.png?width=677&height=677");
                States.StudyInventory.Add("Hook");
                await KitchenChannel.SendMessageAsync("", false, embed.Build());
            }

        }

        [Command("Photos"), Alias("photos")]
        public async Task photos()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;


            States.StudyState = States.StatesListKitchen[6];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("There are two photos on the wall");
            embed.AddField("They're really strange and innapropriate, but considering everything else here, that's nothing new.", "The !first photo is of a woman with no arms or legs");
            embed.WithImageUrl("");
            EmbedBuilder embed1 = new EmbedBuilder();
            embed1.AddField("The !second is of a headless man in a suit.", "Maybe there's more to these photos");
            embed1.WithImageUrl("");
            await KitchenChannel.SendMessageAsync("", false, embed.Build());
            await KitchenChannel.SendMessageAsync("", false, embed1.Build());
        }

        [Command("first"), Alias("First")]
        public async Task photosWoman()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;


            if (States.StudyState == States.StatesListKitchen[6] && !States.StudyInventory.Contains("Knife"))
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I take the photo off the wall");
                embed.AddField("Upon inspection, It looks like there's a small knife on the back of the picture.", "This could come in handy later");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587868585465085963/image0.png?width=478&height=676");
                States.StudyInventory.Add("Knife");
                await KitchenChannel.SendMessageAsync("", false, embed.Build());

            }

        }

        [Command("Second"), Alias("second")]
        public async Task photosMan()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;


            if (States.StudyState == States.StatesListKitchen[6])
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I take the photo off the wall");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587868534378463253/image0.png?width=445&height=549");
                embed.AddField("Upon inspection, there's a folder with the number 64 written on it.", "Inside the folder is a file");

                using (var client = new System.Net.Http.HttpClient())
                using (var testStream = await client.GetStreamAsync("https://cdn.discordapp.com/attachments/572605573821104128/585036231474282515/encoded_20190603023014.txt"))
                    await Context.Channel.SendFileAsync(testStream, "encoded_20190603023014.txt");
                await KitchenChannel.SendMessageAsync("", false, embed.Build());
               
            }

        }

        [Command("Clock"), Alias("clock")]
        public async Task Clockeroni()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I walk over to the clock");
            States.StudyState = States.StatesListKitchen[7];
            if (ClockOpen == false)
            {
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587450890538188810/image1.png?width=1015&height=677");
                embed.AddField("The clock doesn't seem to have any hands, and the glass cover is locked on.", "Maybe there's a way to get inside.");
            } else 
            {
                if (needlesIn == true)
                {
                    embed.AddField("With the needles in, this clock looks pretty normal.", "Well, if you don't count the laser pointer coming out of the middle of it and ointing straight at the cupboard.");
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587450891431444480/image2.png?width=1015&height=677");
                    LightonCans = true;
                } else
                {
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587450890538188810/image1.png?width=1015&height=677");
                    embed.AddField("With the glass open, I have access to the handless clock.", "Maybe there's a way to get it working.");
                }

            }
            await KitchenChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Needles"), Alias("needles")]
        public async Task needles()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Kitchen.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.StudyInventory.Contains("Needles") && needlesIn == false && States.StudyState == States.StatesListKitchen[7] && ClockOpen == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.AddField("I place the needles in place as the clock hands.", "A laser pointer comes out from the middle of the clock, pointing straight at the cupboard at one particular can.");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587450891431444480/image2.png?width=1015&height=677");
                needlesIn = true;
                LightonCans = true;
                await KitchenChannel.SendMessageAsync("", false, embed.Build());
                States.StudyInventory.Remove("Needles");
            }

        }


        //MASTER BEDROOM
        public static bool ensuiteLocked;
        public static bool writingRevealed;
        public static bool mirrorClean;
        public static bool bathDrained;

        [Command("MasterBedroom"), Alias("masterbedroom")]
        public async Task MasterBedroomOverview()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListMaster[0];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I walk across a hallway into the master bedroom.");
            if (ensuiteLocked == true)
            {
                embed.AddField("There’s a king-sized bed in the middle of the room. It’s pure white besides a large red stain in the middle", "The door to what I think is the ensuite is to the right, but it's locked at the moment");
            } else
            {
                embed.AddField("There’s a king-sized bed in the middle of the room. It’s pure white besides a large red stain in the middle", "The door to what I think is the ensuite is to the right, now that it's open, I can check it out.");
            }
            if (States.BedInventory.Contains("Ensuite Key"))
            {
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/588181210266599444/image0.jpg?width=957&height=676");
            } else
            {
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/588181209562087434/image1.jpg?width=957&height=676");
            }
            
            embed.AddField("There’s a standing !shelf on the left side of the room. It's full of statues of various deities.", "And finally a !rightbedside and !leftbedside table on either side of the bed");
            await MasterBedChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Shelf"), Alias("shelf")]
        public async Task StandingShelf()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListMaster[1];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I walk to the standing shelf.");
            if (States.BedInventory.Contains("Ensuite Key"))
            {
                embed.AddField("The statues lay standing and broken on the shelf.", "I already have the key, maybe theres not much more to these deities");
                await MasterBedChannel.SendMessageAsync("", false, embed.Build());
            }
            else
            {
                await MasterBedChannel.SendMessageAsync("", false, embed.Build());
                embed.AddField("There are six statues.", "Maybe theres something in one of them, but which one?");
                EmbedBuilder morrigan = new EmbedBuilder();
                morrigan.WithDescription("The first statue is of tripple godess Morrigan holding a raven");
                morrigan.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587902248521039883/image0.png?width=677&height=677");
                await MasterBedChannel.SendMessageAsync("", false, morrigan.Build());
                EmbedBuilder anubis = new EmbedBuilder();
                anubis.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587510854258393090/image0.png?width=677&height=677");
                anubis.WithDescription("The second, the egyptian god Anubis");
                await MasterBedChannel.SendMessageAsync("", false, anubis.Build());
                EmbedBuilder cronus = new EmbedBuilder();
                cronus.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587902327726276618/image0.png?width=677&height=677");
                cronus.WithDescription("Next, Cronus holding a scythe");
                await MasterBedChannel.SendMessageAsync("", false, cronus.Build());
                EmbedBuilder vishnu = new EmbedBuilder();
                vishnu.WithDescription("Fourth, one of the hindu godess, Vishnu");
                vishnu.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587889957323014164/image0.png?width=677&height=677");
                await MasterBedChannel.SendMessageAsync("", false, vishnu.Build());
                EmbedBuilder lamassu = new EmbedBuilder();
                lamassu.WithDescription("one of Lamassu the assyrian deity");
                lamassu.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587524315835006986/image0.png?width=677&height=677");
                await MasterBedChannel.SendMessageAsync("", false, lamassu.Build());
                EmbedBuilder buddha = new EmbedBuilder();
                buddha.WithDescription("and finally, a statue of Buddha");
                buddha.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587510854841270283/image1.png?width=677&height=677");
                await MasterBedChannel.SendMessageAsync("", false, buddha.Build());
            }
            
            
        }

        [Command("Lamassu"), Alias("lamassu")]
        public async Task StandingShelfCorrect()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;
            if (States.KidRoomState == States.StatesListMaster[1])
            {
                EmbedBuilder embed = new EmbedBuilder();
                if (!States.BedInventory.Contains("Ensuite Key"))
                {
                    embed.WithDescription("I smash the statue on the table.");
                    embed.AddField("Yes, I found a key inside!.", "Maybe it's for the ensuite?");
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587528820618297344/Lamassu.jpg?width=677&height=677");
                    States.BedInventory.Add("Ensuite Key");
                }
                else
                {
                    embed.AddField("The Statue's already broken.", "I don't think theres much more I can get out of it.");
                }
                await MasterBedChannel.SendMessageAsync("", false, embed.Build());
            }

        }

        [Command("Morrigan"), Alias("morrigan", "Anubis", "anubis", "Cronus", "cronus", "Vishnu", "vishnu", "Buddha", "buddha")]
        public async Task StandingShelfWrong()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[1])
            {
                States.KidRoomState = States.StatesListMaster[1];
                EmbedBuilder embed = new EmbedBuilder();
                if (!States.BedInventory.Contains("Ensuite Key"))
                {
                    embed.WithDescription("I smash the statue on the table.");
                    embed.AddField("Oh no, some gas is leaking out from the statue.", "I'm... feeling a .. biit ......... zzzzz");
                    //bot mute and deafen player for 1 minute
                    var gasseduser = Context.User as SocketGuildUser;
                    States.setGasTimer(gasseduser, Master_Bedroom);
                }
                else
                {
                    embed.AddField("I've already got the key from one of the statues.", "I think the other ones could be dangerous.");
                }
                await MasterBedChannel.SendMessageAsync("", false, embed.Build());
            }

        }

        [Command("Right Bedside"), Alias("rightbedside")]
        public async Task RightBedside()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListMaster[2];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I move to the right bedside table.");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587142139482341376/image1.png?width=677&height=677");
            EmbedBuilder embed1 = new EmbedBuilder();
            embed1.AddField("On the table, there's a notebook and a digital clock.", "The time on the clock reads '13:26'");
            embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587450889929752580/image0.png?width=1015&height=677");
            if (writingRevealed == false)
            {
                embed.AddField("the notepad is blank, but it definitely looks like it has been used before, but erased.", "Maybe I can !use an item to find out what's written");
            }
            else
            {
                embed.AddField("With the writing revealed by the crayons, I can now see that it says", "'What do you think the children saw at the top of the stairs?'");
            }
            await MasterBedChannel.SendMessageAsync("", false, embed.Build());
            await MasterBedChannel.SendMessageAsync("", false, embed1.Build());
        }

        [Command("Use"), Alias("use")]
        public async Task UseCrayons(string item)
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[2])
            {
                if (writingRevealed == false && States.BedInventory.Contains("Crayons") && item == "Crayons")
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.AddField("I use the crayons to cover the page, revealing the indentations where the text was written.", "It says, 'What do you think the children saw at the top of the stairs?'");
                    writingRevealed = true;
                    await MasterBedChannel.SendMessageAsync("", false, embed.Build());
                }
                if (writingRevealed == false && States.Inventory.Contains(item) && !(item == "Crayons"))
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithDescription($"I try to use {item} on the page.");
                    embed.AddField("Nothing seems to be happening.", "Maybe I should try something else");
                    await MasterBedChannel.SendMessageAsync("", false, embed.Build());
                }
            }
            

        }

        [Command("Left Bedside"), Alias("leftbedside")]
        public async Task LeftBedside()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListMaster[3];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I move to the left bedside table.");
            embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587142139016904704/image0.png?width=677&height=677");
            embed.AddField("On the table, there's a strange looking telephone. The phone sits atop a rack on a headless human figure", "It only has two buttons on it: '1' and '0'. what kind of binary phone is this? Maybe I can input some kind of code?");
            await MasterBedChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("input")]
        public async Task LeftBedsideAnswer(string numbers)
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[3])
            {
                if (numbers == "01101000" || numbers == "notnumbers")
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithDescription("I put in the numbers.");
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587497537745715210/Phone.jpg?width=1015&height=677");
                    embed.AddField("The phone starts playing a message!", "'Hi Florence, how’s it been? I feel like we haven't seen you in ages. Hmm? How’s Oscar doing? Oh, you should see him! He has so many female friends at the kindergarten, the little Romeo.");
                    embed.AddField("Hmmm? Oh, haha, yeah, I - * takes phone away from ear * - not now sweetie, Mommy’s on the phone.Why don't you go play with Papa and his friends? Haha, kids, right?", "Oh, sorry, right. Um. So, how’s your work going? Still working at the Lapis Hotel? Oh? Is that right?");
                    embed.AddField("I’m sorry… * shouting in the background ** takes phone from ear ** indistinguishable yelling *, oh, sorry about that. I swear to god, why does he have to act like such an “Alpha Male” whenever he has friends over? God.?", "Anyway, about your job.Oh, is that right ? That’s great!When do you start working ? November ?");
                    embed.AddField("Wow, that’s a while away.Are you gonna be okay in the meantime? Are you sure? I would be happy to help.Well...if you say so.", " * more background yelling ** child crying* Oh god. Sorry Florence, I think I’ll need to take care of this one. He somehow flooded the dishwasher. Anyway, hope to see you soon. Mwah. Bye. * beep *'");
                    

                    embed.AddField("What does any of this mean?", "They are speaking so cryptically. They must have chosen these words for a reason.");
                    await MasterBedChannel.SendMessageAsync("", false, embed.Build());
                } else
                {
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithDescription("I put in the numbers.");
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587497537745715210/Phone.jpg?width=1015&height=677");
                    embed.AddField("Nothing seems to be happening.", "Maybe I should try a different set of numbers");
                    await MasterBedChannel.SendMessageAsync("", false, embed.Build());
                }

            }

        }

        [Command("Ensuite"), Alias("ensuite")]
        public async Task Ensuite()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            EmbedBuilder embed = new EmbedBuilder();
            
            if (ensuiteLocked == true)
            {
                if (!States.BedInventory.Contains("Ensuite Key"))
                {
                    embed.WithDescription("The door's locked, I need to find a key to get in.");
                } else
                {
                    EmbedBuilder embed1 = new EmbedBuilder();
                    ensuiteLocked = false;
                    embed.WithDescription("I unlock the door to the ensuite and walk in.");
                    embed.AddField("The whole room is disgusting and grimy, with some kind of liquid staining the walls and tiles.", "The bath is full of a disgusting green liquid. Maybe I need to find some way to drain it.");
                    
                    embed1.AddField("Theres a !basin next to the bath.", "I can't really make out what's over there from here");
                    embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/588181255707820045/image0.jpg?width=809&height=676");
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587824881891278859/image0.png?width=1015&height=677");
                    await MasterBedChannel.SendMessageAsync("", false, embed1.Build());
                    States.KidRoomState = States.StatesListMaster[4];
                }
                    
            } else
            {
                EmbedBuilder embed1 = new EmbedBuilder();
                embed.WithDescription("The door's unlocked, I walk into the ensuite.");
                embed.AddField("The whole room is disgusting and grimy, with some kind of liquid staining the walls and tiles.", "The bath is full of a disgusting green liquid. Maybe I need to find some way to drain it.");
                if (bathDrained == true)
                {
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587824883627589632/image1.png?width=1015&height=677");
                } else
                {
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587824881891278859/image0.png?width=1015&height=677");
                }
                embed1.AddField("Theres a !basin next to the bath.", "I can't really make out what's over there from here");
                embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587825018583646223/image0.png?width=1015&height=677");
                await MasterBedChannel.SendMessageAsync("", false, embed1.Build());
                States.KidRoomState = States.StatesListMaster[4];
            }

            await MasterBedChannel.SendMessageAsync("", false, embed.Build());


        }

        [Command("basin")]
        public async Task Basin()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[4])
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I walk to the sink");
                /*if (mirrorClean == false)
                {
                    embed.AddField("The mirror is covered in grime, but I think there might be something written on it.", "I should try to clean it with something.");
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587824504903041044/image0.png?width=1015&height=677");
                } else
                {*/
                    embed.AddField("The mirror is slightly more clear now, I can make out what's written on it.", "'PEEL BACK THE PIG'");
                    embed.AddField("I can open up the mirror door now", "Inside, there's series of pipes, and one has a a knob on it. Maybe I could turn it?");
                    embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587824505582387210/image1.png?width=1015&height=677");
                //}
                mirrorClean = true;
                
                
                await MasterBedChannel.SendMessageAsync("", false, embed.Build());
            }

        }

        [Command("turn knob"), Alias("Turn Knob", "turnknob", "TurnKnob", "Turn", "turn")]
        public async Task TurnKnob()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[4] && mirrorClean == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I turn the knob on the pipe");
                embed.AddField("The bath begins to empty. Theres something written on the bottom!", "It reads: “For those who feel trapped the frying pan, death will not the end for you. It will merely open another door” ");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587824883627589632/image1.png?width=1015&height=677");

                await MasterBedChannel.SendMessageAsync("", false, embed.Build());

                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.WithDescription("a noose drops down from the cieling");
                await KitchenChannel.SendMessageAsync("", false, embed1.Build());
                nooseDown = true;
            }

        }

        [Command("Toilet"), Alias("toilet")]
        public async Task Toilet()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[4])
            {
                States.KidRoomState = States.StatesListMaster[6];
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I walk over to the toilet");
                embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/587889869448019978/image0.png?width=1015&height=677");
                if (States.BedInventory.Contains("Lounge Key"))
                {
                    embed.AddField("Now that I have the lounge key, I don't feel the need to stay around this disgusting bathroom anymore.", "One step closer to getting out of here.");
                }
                else
                {
                    embed.AddField("I open up the lid, thank goodness there's nothing inside there", "There's a keypad on the side. Maybe this is one of those fancy ones from japan.");
                    embed.AddField("The keypad needs a 3 digit code, and each number refers to the number of flushes the toilet will do", "maybe the code has something to do with water?");
                }
                await MasterBedChannel.SendMessageAsync("", false, embed.Build());
            }

        }

        [Command("483")]
        public async Task ToiletAnswer()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(Master_Bedroom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[6])
            {
                Room4.LoungeRoom = await Context.Guild.CreateRoleAsync("Lounge", null, Color.DarkGrey, false, null);
                Room4.LoungeChannel = await Context.Guild.CreateTextChannelAsync("Lounge", null, null);

                await Room4.LoungeChannel.AddPermissionOverwriteAsync((IRole)Context.Guild.EveryoneRole, OverwritePermissions.DenyAll(Room4.LoungeChannel));
                await Room4.LoungeChannel.AddPermissionOverwriteAsync((IRole)Room4.LivingRoom, OverwritePermissions.DenyAll(Room4.LoungeChannel));
                await Room4.LoungeChannel.AddPermissionOverwriteAsync((IRole)Room4.LoungeRoom, OverwritePermissions.AllowAll(Room4.LoungeChannel));

                EmbedBuilder embed = new EmbedBuilder();
                var user2 = Context.User as SocketGuildUser;
                await user2.AddRoleAsync(Room4.LoungeRoom);
                embed.WithDescription("I input the code");
                embed.AddField("The toilet runs through the sequence of flushes. Suddenly, the compartment lid blows off. I think I got it right!", "There's a key to the !lounge in here.");
                if (nooseDown == true)
                {
                    await user.AddRoleAsync(Room4.LoungeRoom);
                    embed.AddField("No reason to stay here now.", "I think it's time to get out of here.");
                } else
                {
                    embed.AddField("I can get to the next room, but my partner will be trapped.", "I don't think i'll be able to make it out without their help. Maybe I should help them first.");
                }
                await MasterBedChannel.SendMessageAsync("", false, embed.Build());
            }
        }

        //DEBUG


    }
}
