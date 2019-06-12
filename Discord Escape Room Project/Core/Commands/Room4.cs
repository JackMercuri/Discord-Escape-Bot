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

    public class Room4 : ModuleBase<SocketCommandContext>
    {
        public static RestRole LivingRoom;
        public static RestRole LoungeRoom;
        public static RestTextChannel LivingRoomChannel;
        public static RestTextChannel LoungeChannel;

        public static bool player1Free;
        public static bool player2Free;

        //LIVING ROOM

        public static bool TVon;
        public static bool dvdWrong;
        public static bool GrateOpen;
        public static bool Q1Answered;
        public static bool Q2Answered;
        public static bool Q3Answered;
        public static bool Q4Answered;
        public static bool Q5Answered;

        [Command("Living Room"), Alias("living room")]
        public async Task LivingRoomOverview()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LivingRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListKitchen[8];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I move to the centre of the room.");
            embed.AddField("There's no way back to the previous rooms", "This room is clean and starkly white. Like the other rooms, there aren't any windows here");
            embed.AddField("There's two large couches, centred around a coffee table.", "There’s an entertainment !unit across from the couches with a big flat-screen tv, heaps of picture frames, shelfloads full of dvds, and other shelves decorated with various items. This is the kind of place I could relax, If it weren't for the whole death and trap thing.");
            embed.AddField("To my left, there's a sleek, gas-lit fireplace with a grate over it", "Maybe this will finally be a way out");
            await LivingRoomChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Unit"), Alias("unit","Unit")]
        public async Task EntertainmentUnit()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LivingRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListKitchen[9];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I move to the unit.");
            if (TVon == false)
            {
                embed.AddField("The TV won't seem to turn on, but it is connected to a power source", "Perhaps I need to participate in more of these silly games to turn it on?");
            } else
            {
                embed.AddField("With the TV now on, I can see what the deal is with these dvds", "Maybe there's some order to all this madness.");
            }
            embed.AddField("There are various picture frames, most are family shots, while some focus on a young boy, and others on a baby.", "There are rows of dvds, most of them are popular films, but some have completely white covers, with nothing written them but numbers ranging from 1 to 10. Maybe I can insert these into the dvd player.");
            States.StudyInventory.Add("dvd-1");
            States.StudyInventory.Add("dvd-2");
            States.StudyInventory.Add("dvd-3");
            States.StudyInventory.Add("dvd-4");
            States.StudyInventory.Add("dvd-5");
            States.StudyInventory.Add("dvd-6");
            States.StudyInventory.Add("dvd-7");
            States.StudyInventory.Add("dvd-8");
            States.StudyInventory.Add("dvd-9");
            States.StudyInventory.Add("dvd-10");

            await LivingRoomChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Insert"), Alias("insert")]
        public async Task Insert(string dvdnumber)
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LivingRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.StudyState == States.StatesListKitchen[9] && TVon == true)
            {
                if (States.StudyInventory.Contains(dvdnumber))
                {
                    States.dvdlist.Add($"{dvdnumber}");
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.WithDescription($"I insert {dvdnumber} into the machine");
                    await LivingRoomChannel.SendMessageAsync("", false, embed.Build());
                    States.StudyInventory.Remove(dvdnumber);
                }
                if (States.dvdlist[0] == "dvd-7" && States.dvdlist[1] == "dvd-9" && States.dvdlist[2] == "dvd-2" && States.dvdlist[3] == "dvd-4" && States.dvdlist[4] == "dvd-10" && States.dvdlist[5] == "dvd-8" && States.dvdlist[6] == "dvd-6" && States.dvdlist[7] == "dvd-5" && States.dvdlist[8] == "dvd-3")
                {
                    EmbedBuilder embed1 = new EmbedBuilder();
                    embed1.WithDescription("Once the last dvd's in, I hear a sound from the fireplace");
                    embed1.AddField("The grate cover has opened up.", "Maybe the fireplace is the way out of here!");
                    await LivingRoomChannel.SendMessageAsync("", false, embed1.Build());
                    GrateOpen = true;
                    EmbedBuilder embed2 = new EmbedBuilder();
                    embed2.WithDescription("The bricks covering the fireplace blow up and crack into tiny pieces");
                    embed2.AddField("I can see a light coming from the fireplace. Is this finally a way !out?", "I could always stay and try to find out what what's going on in this place");
                    await LoungeChannel.SendMessageAsync("", false, embed2.Build());
                }
                if (States.dvdlist.Count == 9 && !(States.dvdlist[0] == "dvd-7") || !(States.dvdlist[1] == "dvd-9") || !(States.dvdlist[2] == "dvd-2") || !(States.dvdlist[3] == "dvd-4") || !(States.dvdlist[4] == "dvd-10") || !(States.dvdlist[5] == "dvd-8") || !(States.dvdlist[6] == "dvd-6") || !(States.dvdlist[7] == "dvd-5") || !(States.dvdlist[8] == "dvd-3"))
                {
                    EmbedBuilder embed1 = new EmbedBuilder();
                    embed1.WithDescription("Nothing seems to be happening");
                    embed1.AddField("Hmmm ... something's not right.", "I think I got the order wrong. I should !grabdvd and try again.");
                    dvdWrong = true;

                    await LivingRoomChannel.SendMessageAsync("", false, embed1.Build());
                }
            }
            if (TVon == false)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I try to insert a disk.");
                embed.AddField("the dvd player won't accept it, simply sticking the disk tray back out after going in", "Perhaps I need to turn the tv on first.");
                await LivingRoomChannel.SendMessageAsync("", false, embed.Build());
            }

        }

        [Command("grabdvd"), Alias("GrabDVD", "grab dvd", "Grab DVD")]
        public async Task grabDVD()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Room3.Kitchen.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (dvdWrong == true)
            {
                foreach (var plate in States.dvdlist)
                {
                    States.StudyInventory.Add(plate);
                    States.dvdlist.Remove(plate);
                }
                dvdWrong = false;
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.WithDescription("I grab all the dvds out of the player.");
                await LivingRoomChannel.SendMessageAsync("", false, embed1.Build());
            }


        }

        [Command("Fireplace"), Alias("fireplace")]
        public async Task LivingRoomFirePlace()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LivingRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.StudyState = States.StatesListKitchen[8];
            if (GrateOpen == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I crawl into the fireplace.");
                embed.AddField("I think this is it! I clamber my way up the chimney", "Suddenly, my eyes are blinded by a white light. Not this again?");
                await LivingRoomChannel.SendMessageAsync("", false, embed.Build());
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.Color = Color.Red;
                embed1.AddField("Well done. You are now free.", "https://discord.gg/UvWRxnZ");
                await Task.Delay(3000).ContinueWith(t => LivingRoomChannel.SendMessageAsync("", false, embed1.Build()));
                player1Free = true;

                await Task.Delay(10000).ContinueWith(t => FinishDestroy());
            } else
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I move to the fireplace.");
                embed.AddField("It's covered by some kind of metal grate, but the chimney's open!", "if I can get the grate open, maybe this will be a way out!");
                await LivingRoomChannel.SendMessageAsync("", false, embed.Build());
            }

        }

        //LOUNGE

        public static bool Tape1Watched;
        public static bool Tape2Watched;
        public static bool Tape3Watched;
        public static bool Tape4Watched;
        public static bool Tape5Watched;
        public static bool figureInRoom;
        public static bool attackfigure;


        [Command("Lounge"), Alias("lounge")]
        public async Task LoungeOverview()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LoungeRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListMaster[7];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I walk into the lounge room.");
            embed.AddField("The room is red from ceiling to floor. Everything is wet to the touch and there’s some kind of liquid dripping from the ceiling.", "There are the remnants of what looks like used to be couches; now just torn up masses of fabric, stuffing and springs");
            embed.AddField("There are jagged pieces of glass all over the floor.", "maybe I coud use a piece in case I need to defend myself or something.");
            embed.AddField("There’s an !entertainment-unit across from the torn up couches with an old CRT TV.", "To my left is what seems to be an old fireplace that has since been bricked up..");
            embed.AddField("There’s something carved into the bricks of the fireplace. It reads:", "MONITORING DOWN  -  YET UNTOLD.");
            await LoungeChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Entertainment-Unit"), Alias("entertainment-unit")]
        public async Task EntertainmentUnitLounge()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LoungeRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            States.KidRoomState = States.StatesListMaster[8];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("I move to the entertainment unit.");
            embed.AddField("Alongside the small television, theres some picture frames, a shelf full of VHS tapes, and other shelves decorated with bones", "Well if I wasn't feeling at home before, I definately am now.");
            embed.AddField("There are five VHS tapes in the shelves below. Tape1 reads “Security”, Tape2 reads “1 to 10”, the next reads “TV Instructions”, Tape4 reads “7/12/98”, and the last reads “final message”", "Maybe I should check out what's on each tape.");
            await LoungeChannel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Tape1"), Alias("Security")]
        public async Task Tape1()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LoungeRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[8])
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I insert the security tape.");
                embed.AddField("It seems to be security camera footage of the building I'm in.", "There are six screens that say “STDY”, “BDRM1”, “BDRM2”, “KTCHN”, “LVNGR” “HLWY”. For some reason, the screen for the living room is black, and says “OFFLINE”.");
                embed.AddField("Besides the rooms you haven’t been in, the rooms all seem the same as you left them, except for the hallway you just left. There’s now a shadowy figure standing in the hallway. They turn to face the camera, and put a hand up to their face.", "The tape ends...");
                await LoungeChannel.SendMessageAsync("", false, embed.Build());

                Tape1Watched = true;

                await Task.Delay(5000).ContinueWith(t => AllTapesWatched());
            }
        }

        [Command("Tape2"), Alias("1 to 10")]
        public async Task Tape2()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LoungeRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[8])
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I insert the '1 to 10' tape.");
                embed.AddField("It starts with a black screen with white text, that says “1”, and underneath it is this piece of morse code: '-. --- .-. -- .- .-..' ", "The rest of the tape is as follows: 2; - .... .. ... ");
                embed.AddField("3; .--. .- ... - ", "4; .. …");
                embed.AddField("5; .... . .-. .......", "6; .- -");
                embed.AddField("7; -.-- --- ..- .......", "8; .-.. --- --- -.- .. -. --.");
                embed.AddField("9; - .... .. -. -.- ", "10; -. --- .-. -- .- .-..");
                embed.WithFooter("The tape ends...");
                await LoungeChannel.SendMessageAsync("", false, embed.Build());

                Tape2Watched = true;

                await Task.Delay(5000).ContinueWith(t => AllTapesWatched());
            }
        }

        [Command("Tape3"), Alias("TV Instructions")]
        public async Task Tape3()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LoungeRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[8])
            {
                var allusers = Context.Guild.Users;
                var ThingoID = user.Guild.GetRole(LivingRoom.Id);
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I insert the 'TV Instructions' tape.");
                embed.AddField("It looks like a recording of a quiz show. Something is strange, however. There are two contestants. There’s a woman sitting in a chair, her name-card reading “Florence”.", "There’s a TV on a pedestal to her right. The TV screen is displaying an image of security footage of a room labelled “LVNGR”.");
                
                await LoungeChannel.SendMessageAsync("", false, embed.Build());
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.AddField("There’s a lady in a flashy dress with incredibly white teeth, who smiles at the camera and then begins to speak.", "“It’s now time for our next segmeeeeeent! Our friend in the living room will be answering the questions being answered by our lovely guest tonight, Florence Carter!”");
                embed1.AddField("The lady hands Florence a set of cue cards. “Now Florence, tell the audience what our topic for tonight is !”", "There’s suddenly a card on the screen that reads “Unsolved murders”");
                embed1.AddField($"'And our wonderful question master, please welcome {user}'", "Enjoy the show!");
                await Task.Delay(3000).ContinueWith(t => LoungeChannel.SendMessageAsync("", false, embed1.Build()));
                EmbedBuilder embed2 = new EmbedBuilder();
                embed2.AddField("Alllllllllllright, are we ready for question 1", "Q1: Name a body part that went missing in the Dyatlov Pass Incident?");
                await Task.Delay(3000).ContinueWith(t => LoungeChannel.SendMessageAsync("", false, embed2.Build()));


                await Task.Delay(5000).ContinueWith(t => AllTapesWatched());
            }
        }

        [Command("A tounge and two eyes"), Alias("a tounge and two eyes", "tounge", "eyes", "two eyes and a tounge")]
        public async Task Question1Answer()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LivingRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("Cooooorrect. Time for the next question!");
            EmbedBuilder embed1 = new EmbedBuilder();
            embed1.AddField("Cooorect. Now, second verse, same as the first", "Q2: Where were the dismembered remains of an architect found in the Inokashira Park incident?");
            Q1Answered = true;

            await LivingRoomChannel.SendMessageAsync("", false, embed.Build());
            await LoungeChannel.SendMessageAsync("", false, embed1.Build());
        }

        [Command("A garbage can"), Alias("a bin", "Bin", "Trash Can", "Garbage Can", "a trash can")]
        public async Task Question2Answer()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LivingRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;
            if (Q1Answered == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("Cooooorrect. Well done!");
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.AddField("Cooorect, how's that for modern house design!", "Q3: How many of the Cleveland Torso Murderer’s victims had their identities verified?");
                Q2Answered = true;

                await LivingRoomChannel.SendMessageAsync("", false, embed.Build());
                await LoungeChannel.SendMessageAsync("", false, embed1.Build());
            }

        }

        [Command("3"), Alias("Three", "three")]
        public async Task Question3Answer()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LivingRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (Q2Answered == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("Cooooorrect. three's a crowd am I right!");
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.AddField("Cooorect, getting close now!", "Q4: In what year were the dismembered remains of Catrine da Costa found?");
                Q3Answered = true;

                await LivingRoomChannel.SendMessageAsync("", false, embed.Build());
                await LoungeChannel.SendMessageAsync("", false, embed1.Build());
            }

        }

        [Command("1984"), Alias("Nineteen Eighty Four", "three")]
        public async Task Question4Answer()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LivingRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;
            if (Q3Answered == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("Cooooorrect. you truely are the greatest!");
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.AddField("Cooorect, now, final question!", "Q5: Which body parts were lost in the investigation of the Hinterkaifeck murders?");
                Q4Answered = true;

                await LivingRoomChannel.SendMessageAsync("", false, embed.Build());
                await LoungeChannel.SendMessageAsync("", false, embed1.Build());
            }

        }

        [Command("Heads"), Alias("heads", "the heads", "The Heads")]
        public async Task Question5Answer()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LivingRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;
            if (Q4Answered == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("DING DING DING DING DING, We Have a winner!");
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.AddField("Now, please thank our question master", "Please let our guest know they have won a brand new flat screen TV with a state of the art dvd player");
                Q5Answered = true;
                TVon = true;
                EmbedBuilder embed2 = new EmbedBuilder();
                embed2.AddField("The screen cuts to static for a few seconds", "The tape ends here...");
                
                await LivingRoomChannel.SendMessageAsync("", false, embed.Build());
                await LoungeChannel.SendMessageAsync("", false, embed1.Build());
                Tape3Watched = true;

                await Task.Delay(5000).ContinueWith(t => AllTapesWatched());
            }

        }

        [Command("Tape4"), Alias("7/12/98")]
        public async Task Tape4()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LoungeRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[8])
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I insert the dated tape.");
                embed.AddField("It looks to be camcorded footage of a child’s birthday party. I can see several children running around, having a good time. Then the camera moves to an older teenage girl, using a hose to put water in an inflatable pool.", "The girl turns to look at the camera, stops what she was doing, and starts to walk towards the cameraman.");
                embed.AddField("They walk inside the house together, and then the camera makes a sharp turn towards the top of the stairs, where a decapitated head is found.", "There’s a scream, and the camera falls to the floor. The tape ends...");
                await LoungeChannel.SendMessageAsync("", false, embed.Build());
                Tape4Watched = true;

                await Task.Delay(5000).ContinueWith(t => AllTapesWatched());
            }
        }

        [Command("Tape5"), Alias("final message")]
        public async Task Tape5()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LoungeRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (States.KidRoomState == States.StatesListMaster[8])
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I insert the final tape.");
                embed.AddField("it’s a video of a man’s face. His eyes are closed and he’s leaning forwards. The video intercuts with black screens with white text on them.", "They say:");
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.Color = Color.Red;
                embed1.WithAuthor("Man");
                embed1.AddField("“Oscar, Olivia”", "“I’m sorry. I wish i were a better father to you.”");
                embed1.AddField("“I’m sorry I had to be taken away.”", "“I wish i had never yelled at you.”");
                EmbedBuilder embed2 = new EmbedBuilder();
                embed2.AddField("It cuts back to the video.", "The man’s face is now staring right at the camera, with a disturbing smile on his face.");
                EmbedBuilder embed3 = new EmbedBuilder();
                embed3.Color = Color.Red;
                embed3.WithAuthor("Man");
                embed3.AddField("“Of course, that never happened, did it Florence?”", "“When did they ever have the chance to make a video like that?”");
                embed3.AddField("“Your mind will come up with anything it can to ease the pain.”", "“You can’t trust what you see in here. Florence is lying to you.”");
                EmbedBuilder embed4 = new EmbedBuilder();
                embed4.AddField("The video cuts back on last time, zooming out to a shot of the man above his waist. His head falls off his shoulders.", "The tape ends...");

                await LoungeChannel.SendMessageAsync("", false, embed.Build());
                await LoungeChannel.SendMessageAsync("", false, embed1.Build());
                await Task.Delay(3000).ContinueWith(t => LoungeChannel.SendMessageAsync("", false, embed2.Build()));
                await Task.Delay(7000).ContinueWith(t => LoungeChannel.SendMessageAsync("", false, embed3.Build()));
                await Task.Delay(10000).ContinueWith(t => LoungeChannel.SendMessageAsync("", false, embed4.Build()));

                Tape5Watched = true;

                await Task.Delay(5000).ContinueWith(t => AllTapesWatched());
            }
        }

        [Command("Out"), Alias("out", "flight", "Flight")]
        public async Task LoungeEscape()
        {
            var user = Context.User as SocketGuildUser;
            var RoleID = user.Guild.GetRole(LoungeRoom.Id);
            if (!user.Roles.Contains(RoleID)) return;

            if (GrateOpen == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I crawl into the fireplace.");
                embed.AddField("I think this is it! I clamber my way up the chimney", "Suddenly, my eyes are blinded by a white light. Not this again?");
                await LoungeChannel.SendMessageAsync("", false, embed.Build());
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.Color = Color.Red;
                embed1.AddField("Well done. You are now free.", "https://discord.gg/UvWRxnZ");
                await Task.Delay(3000).ContinueWith(t => LoungeChannel.SendMessageAsync("", false, embed1.Build()));
                player2Free = true;

                await Task.Delay(10000).ContinueWith(t => FinishDestroy());
            }


        }

        public async Task AllTapesWatched()
        {
            if (Tape1Watched && Tape2Watched && Tape3Watched && Tape4Watched && Tape5Watched)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("A dark figure appears in the darkness.");
                embed.AddField("They're still as a rock, not moving an inch. This is it, fight or flight!", "What do I do, sit here, attack. I'm so afraid.");
                await LoungeChannel.SendMessageAsync("", false, embed.Build());
                figureInRoom = true;
                attackfigure = false;
            }
        }

        [Command("Fight"), Alias("fight", "Attack", "attack")]
        public async Task Fight()
        {
            if (figureInRoom == true)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I try to attack the figure with the scalpel.");
                embed.AddField("The figure grabs my hand tightly as I try to stab them", "They twist my arm and plunge the blade into my chest");
                embed.AddField("I can feel myself loosing grasp on everything", "I think this might be the end");
                await LoungeChannel.SendMessageAsync("", false, embed.Build());
                attackfigure = true;
                player2Free = true;

                await Task.Delay(10000).ContinueWith(t => FinishDestroy());
            }
        }

        public async Task FigureWait()
        {
            if (figureInRoom == true && attackfigure == false)
            {
                EmbedBuilder embed = new EmbedBuilder();
                embed.WithDescription("I wait as the figure waits in the dark corner.");
                embed.AddField("Finally, they begin to calmly awaken", "All they do is say 'Florence is lying to you'");
                await LoungeChannel.SendMessageAsync("", false, embed.Build());
                EmbedBuilder embed1 = new EmbedBuilder();
                embed1.AddField("A White light blinds my vision. I think this is the way out.", "https://discord.gg/UvWRxnZ");
                await Task.Delay(3000).ContinueWith(t => LoungeChannel.SendMessageAsync("", false, embed1.Build()));
                var user = Context.User as SocketGuildUser;
                await user.RemoveRoleAsync(LoungeRoom);
                player2Free = true;

                await LoungeChannel.DeleteAsync();
                await LoungeRoom.DeleteAsync();

                await Task.Delay(20000).ContinueWith(t => FinishDestroy());
            }
        }

        public async Task FinishDestroy()
        {
            if (player1Free && player2Free)
            {
                await Room1.StudyChannel.DeleteAsync();
                await Room1.BedroomChannel.DeleteAsync();
                await Room1.Kids_Bedroom.DeleteAsync();
                await Room1.Study.DeleteAsync();
                await Room3.KitchenChannel.DeleteAsync();
                await Room3.MasterBedChannel.DeleteAsync();
                await Room3.Master_Bedroom.DeleteAsync();
                await Room3.Kitchen.DeleteAsync();
                await LoungeChannel.DeleteAsync();
                await LivingRoomChannel.DeleteAsync();
                await LoungeRoom.DeleteAsync();
                await LivingRoom.DeleteAsync();
            }
        }
    }
}
