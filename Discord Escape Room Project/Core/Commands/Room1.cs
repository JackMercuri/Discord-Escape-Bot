using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Rest;
using System.IO;

namespace EscapeProject.Core.Commands
{

    public class Room1 : ModuleBase<SocketCommandContext>
    {

        public static RestRole Navigator;
        public static RestRole Study;
        public static RestRole Kids_Bedroom;
        public static RestTextChannel GameChannel;
        public static RestTextChannel StudyChannel;
        public static RestTextChannel BedroomChannel;

        [Command("Start"), Alias("start", "INITIALIZE", "START")]
        public async Task Init()
        {
            if (States.state == States.StatesList[0])
            {
                GameChannel = await Context.Guild.CreateTextChannelAsync("NAVIGATION", null, null);
                await GameChannel.AddPermissionOverwriteAsync((IRole) Context.Guild.EveryoneRole, OverwritePermissions.DenyAll(GameChannel));
                Navigator = await Context.Guild.CreateRoleAsync("Navigator",null,Color.Blue,false,null);
                await Context.Channel.SendMessageAsync("ALL PARTICIPATING AGENTS ENTER COMMAND !Join AND FIND 'NAVIGATION' CHANNEL", false, null);
                await GameChannel.AddPermissionOverwriteAsync((IRole)Navigator, OverwritePermissions.AllowAll(GameChannel));

                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("DDA SYSTEMS");   
                Embed.AddField(">>> START TRANSMISSION", "> TO ALL DDA INVESTIGATORS");
                Embed.AddField("> MISSION DETAILS:", "WE COLLABORATED WITH THE POLICE USING THE TECHNOLOGY HERE AT THE DIGITAL DATASCAPE AGENCY TO CREATE A DIGITAL COPY OF THE SUBJECT’S MEMORY. YOUR MISSION AS DDA INVESTIGATORS WILL HAVE YOU NAVIGATING THROUGH THE DIGITAL MINDSCAPE TO RESTORE THE SUBJECT’S MIND AND FIND THE RIGHT MEMORIES TO HELP SOLVE THE CASE. ");
                Embed.AddField("> SUBJECT '00003126'", "FLORENCE CARTER, ADDRESS: 2027 MEADOWLARK DR, CASTRO VALLEY, CA 94546, COUNTRY: USA, DOB: 7 / 14 / 1987, SEX: F, HEIGHT: 5’08");
                Embed.AddField("> DIAGNOSIS:", "SUBJECT IS SUFFERING BOTH RETROGRADE AND DISSOCIATIVE AMNESIA ONSET BY STRESS AND/OR TRAUMA. SUBJECT WAS FOUND IN THE PARKING LOT OF HER WORKPLACE, WANDERING AROUND IN A DISSOCIATIVE FUGUE. WHEN CONFRONTED BY A WORKER, SHE WAS UNABLE TO CONFIRM HER OWN IDENTITY. AFTER BEING ADMITTED TO THE HOSPITAL, HER IDENTITY WAS CONFIRMED. WHILE IN THE HOSPITAL, THE SUBJECT WAS ATTACKED BY AN UNKNOWN ASSAILANT. SHE SUFFERED MINIMAL INJURY. AFTERWARDS, THE POLICE WERE CALLED TO HER LOCATION. THE POLICE DETERMINED ______ BETWEEN THE SUBJECT AND AN ONGOING CASE REGARDING THE SERIAL KILLER _________ “THE ORPHANER”. THE SUBJECT WAS FOUND TO BE _____  ________ ______ OF A CLOSE FRIEND. HER ____________ AND _____ FRIEND’S ________ ________ FOUND MURDERED THE NEXT DAY, LEAVING BEHIND _____________ AGED ______. THERE IS A ______ POSSIBILITY THAT THE SUBJECT ____ ______ EVENTS ___________ WHICH CREATED _______ _______ ENOUGH _________________ ________");
                Embed.AddField("END TRANSMISSION <<<", "WE HIGHLY RECOMENDED ALL INVESTIGATORS REMAIN IN COMMUNICATION THROUGHOUT NAVIGATION");
                Embed.AddField("GOOD LUCK INVESTIGATORS", "TO BEGIN NAVIGATION, TYPE COMMAND !Ready");

                await GameChannel.SendMessageAsync("", false, Embed.Build(), null);
                
                States.isDead = false;
                States.canPlace = false;
                States.isPlaced = false;
                States.bladesOn = true;
                Room4.player1Free = false;
                Room4.player2Free = false;
                foreach (var item in States.Inventory)
                {
                    States.Inventory.Remove(item);
                }
                States.state = States.StatesList[1];
            }

            
        }

        


        [Command("Join"), Alias("join")]
        public async Task Join()
        {
            if (States.state == States.StatesList[1])
            {
                await ((SocketGuildUser)Context.User).AddRoleAsync(Navigator, null);
                await ((SocketGuildUser)Context.User).ModifyAsync(x =>
                {
                    x.Nickname = "Florence Carter";
                });
            }
        }

        
        [Command("Ready"), Alias("Room","room","return","back")]
        public async Task Room()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (!(States.state == States.StatesList[0]))
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/580187703832739892/image0.jpg?width=949&height=676");
                if (States.state == States.StatesList[1])
                {
                    Embed.WithDescription("You awaken in a room");
        
                } else
                {
                    Embed.WithDescription("You return to the centre of the room");
                }
                Embed.AddField("The room is dark, with only one !light to illuminate", "I can see a kitchen area, with some !cupboards, a sink with a !garbagedisposal system, a set of !cups on a table, and an electronics !panel");
                Embed.AddField("Theres an office !desk on the other side of the room with what looks like a few smaller items around it", "Finally, there's a !whiteboard with some writing on it and a filing cabinet next to it");
                
                await GameChannel.SendMessageAsync("", false, Embed.Build());
                
                EmbedBuilder Embed1 = new EmbedBuilder();
                if (States.isPlaced == true)
                {
                    Embed1.AddField("Above the desk is a large B written on the wall in red...", "I really hope that's not blood.");
                    Embed1.WithImageUrl("https://media.discordapp.net/attachments/577662483590938627/579501522837176351/unknown.png?width=724&height=469");
                } else
                {
                    Embed1.AddField("Above the desk is a large 3 written on the wall in red...", "I really hope that's not blood.");
                    Embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579443606155296770/3.jpg?width=1015&height=677");
                }
                
               
                await GameChannel.SendMessageAsync("", false, Embed1.Build());

                States.state = States.StatesList[2];
            }


        }

        [Command("cupboards"), Alias("Cupboard","Cupboards","cupboard")]
        public async Task Cupboard()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[2])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("Some ordinary cupboards");


                if (States.Inventory.Contains("Blue Plate"))
                {
                    Embed.AddField("Theres nothing else in the cupboard", "Maybe I should check around the !Room some more");
                    Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/580223686884458499/Cupboard_Empty.jpg?width=1015&height=677");
                } else
                {
                    Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579432378200752148/Cupboard.jpg?width=1015&height=677");
                    EmbedBuilder Embed1 = new EmbedBuilder();
                    States.Inventory.Add("Blue Plate");
                    Embed1.AddField("I found a blueplate! Might come in handy", "Maybe I should check around the !Room some more");
                    Embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579434742571728926/Blue_Plate.jpg?width=1015&height=677");
                    await GameChannel.SendMessageAsync("", false, Embed1.Build());
                }

                await GameChannel.SendMessageAsync("", false, Embed.Build());

                States.state = States.StatesList[4];
            }


        }

        [Command("GarbageDisposal"), Alias("garbagedisposal")]
        public async Task GarbageDisposal()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;
            if (States.state == States.StatesList[2])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("A garbage disposal system");
                Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579438254810595338/Garbage_Disposal.jpg?width=1015&height=677");

                if ((States.bladesOn == true) && !(States.Inventory.Contains("Red Chip")))
                {
                    Embed.AddField("It looks like there might be something under the blades!", "I could keep checking around the !Room some more");
                    Embed.AddField("Or ... maybe I could try to !grab it", "Although I'll definately cut myself if I do that. Wouldn't want to bleed to death.");

                } else if ((States.bladesOn == false) && !(States.Inventory.Contains("Red Chip")))
                {
                    Embed.AddField("With the blades off, I can definately see something under the blades!", "It might be important, I could !grab it incase I need it, or keep checking around the !Room some more");
                }
                if (States.Inventory.Contains("Red Chip"))
                {
                    Embed.AddField("I can't see anything else in the garbage disposal", "Maybe I should check around the !Room some more");
                }

                await GameChannel.SendMessageAsync("", false, Embed.Build());


                States.state = States.StatesList[5];
            }


        }

        [Command("Grab"), Alias("grab")]
        public async Task Grab()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;
            if ((States.state == States.StatesList[5]) && !(States.Inventory.Contains("Red Chip")))
            {
                States.Inventory.Add("Red Chip");
                
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("I Stick my hand through the blades");
                Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579490459911585841/Red_Chip_Chip.jpg?width=1015&height=677");
                if (States.bladesOn == true)
                {
                    Embed.AddField("Aha, I found a red chip!", "My hands are bleeding profusely. I'm starting to feel a bit woosy");

                    if (States.timerStarted == false)
                    {
                        States.setTimer();
                    }


                } else
                {
                    Embed.AddField("Aha, I found a red chip!", "And my hands remain untouched by the blades");
                }
                await GameChannel.SendMessageAsync("", false, Embed.Build());

            }
        }

        [Command("Panel"), Alias("panel")]
        public async Task ElectricPanel()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[2])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("A panel");
                if (!(States.Inventory.Contains("Red Key")))
                {
                    Embed.AddField("The door is locked, and there's a red ring around the lock", "Maybe I should check around the !Room some more");
                }
                else
                {
                    Embed.AddField("I unlocked the door with the red key", "There's a code lock with buttons 1-9 and a screen separated into 2 digits");
                    Embed.AddField("Maybe the write code will give me a clue", "Or better yet, open up a way out of here!");
                    Embed.WithFooter("To imput a code type '!*Code*'");
                }

                await GameChannel.SendMessageAsync("", false, Embed.Build());



                States.state = States.StatesList[6];

            }

        }

        [Command("87"), Alias("code87")]
        public async Task PanelAnswer1()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[6])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                if (States.Inventory.Contains("Red Key") && States.bladesOn == true)
                {
                    Embed.AddField("I input the code", "I hear a noise over near the sink. It looks like the garbage disposal has turned off!");
                    States.bladesOn = false;
                } else if (States.bladesOn == false)
                {
                    Embed.AddField("I input the code", "I don't think anything happened.");
                }



                await GameChannel.SendMessageAsync("", false, Embed.Build());



            }

        }

        [Command("42"), Alias("code42")]
        public async Task PanelAnswer2()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[6])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                if (States.Inventory.Contains("Red Key") && !States.Inventory.Contains("Blue Chip"))
                {
                    Embed.AddField("I input the code", "A Hatch opened up!");
                    Embed.AddField("Inside there's a blue chip. I'll take it incase I need it", "Maybe it could be put somewhere else in the room");
                    Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579484253692362772/Blue_Chip.jpg?width=1015&height=677");
                    States.Inventory.Add("Blue Chip");
                }
                await GameChannel.SendMessageAsync("", false, Embed.Build());
            }

        }



        [Command("Cups"), Alias("cups")]
        public async Task Cups()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[2])
            { 
                if (!States.Inventory.Contains("Gold Key"))
                {
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithAuthor("Florence Carter");
                    Embed.WithDescription("Six cups in a row on top of the kitchen counter, the first three cups on the left are full with an opaque liquid and the three cups on the right are empty");
                    Embed.AddField("You can only move one glass to make a row of alternately full and empty glasses", "Which one will you move?");
                    Embed.AddField("Theres a note below the cups: 'migrate one to find the pattern you seek,", "but only one will not make you weak'");
                    Embed.AddField("'the key to your survival waits to be moved,", "so choose wisely, or else you are doomed'");
                    Embed.WithFooter("Choose a specific cup with '!*cup number*'");
                    Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/580010103172759572/image0.jpg?width=956&height=484");

                    await GameChannel.SendMessageAsync("", false, Embed.Build());
                }
                else
                {
                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.WithAuthor("Florence Carter");
                    Embed.WithDescription("The six cups remain on the counter, the're now set in a pattern");
                    Embed.AddField("I've already got the key from here", "Is there anything else to do here?");
                    await GameChannel.SendMessageAsync("", false, Embed.Build());
                }



                States.state = States.StatesList[7];
            }

        }

        [Command("2"), Alias("two")]
        public async Task CupsAnswer()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[7])
            {

                if (!States.Inventory.Contains("Gold Key"))
                {
                    EmbedBuilder Embed1 = new EmbedBuilder();
                    Embed1.WithAuthor("Florence Carter");
                    Embed1.WithDescription("I moved the second cup");
                    Embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/580013041718919168/image0.jpg?width=956&height=484");
                    await GameChannel.SendMessageAsync("", false, Embed1.Build());

                    EmbedBuilder Embed = new EmbedBuilder();
                    Embed.AddField("What's that, I found a gold key stuck to the bottom of the cup", "Maybe I should check around the !Room some more");
                    Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579455753476112385/Gold_Key.jpg?width=1015&height=677");
                    States.Inventory.Add("Gold Key");
                    await GameChannel.SendMessageAsync("", false, Embed.Build());
                } else
                {
                    EmbedBuilder Embed1 = new EmbedBuilder();
                    Embed1.WithAuthor("Florence Carter");
                    Embed1.WithDescription("The cup is empty now");
                    Embed1.AddField("I don't think there's anythign else here.", "Maybe I should check around the !room some more.");
                }

            }

        }

        [Command("1"), Alias("one","3","three","5","five")]
        public async Task CupsAnswerWrong()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[7])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("I move the cup");
                Embed.AddField("Hmmm, nothing seems to be happening", "I think I made the wrong choice. I'll reset the cups to their original order.");
                
                await GameChannel.SendMessageAsync("", false, Embed.Build());

            }

        }

        [Command("phur"), Alias("4", "four", "6", "six")]
        public async Task CupsAreEmpty()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[7])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("Nothing is in these Cups");
                Embed.AddField("Did I pick the wrong cup?", "Maybe I should pick another one");
                await GameChannel.SendMessageAsync("", false, Embed.Build());
            }
        }

        [Command("Light"), Alias("light")]
        public async Task Light()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[2])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("I climb up to the light using the office chair");
                Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579493460814462980/Office_light.jpg?width=1015&height=677");
                if (!(States.Inventory.Contains("Screwdriver")))
                {
                    Embed.AddField("There's a cover over the light, I might be able to take it off, but not without some tools", "Maybe I should check around the !Room some more");
                }
                else
                {
                    Embed.AddField("I can !open that cover", "Maybe it will give me another clue");
                }

                await GameChannel.SendMessageAsync("", false, Embed.Build());


                States.state = States.StatesList[8];
            }


        }

        [Command("Open"), Alias("open")]
        public async Task Open()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if ((States.state == States.StatesList[8]) && States.Inventory.Contains("Screwdriver"))
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("The cover comes off after I take out a few screws");
                Embed.AddField("Looks like theres some empty space below the light", "Maybe I can put something there");
                Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579499285113077780/Office_light_uncovered.jpg?width=1015&height=677");
                await GameChannel.SendMessageAsync("", false, Embed.Build());

                States.canPlace = true;

            } else if (!(States.Inventory.Contains("Screwdriver")))
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.AddField("I don't have anything to help me take the cover off", "Maybe I can find something elsewhere in the !Room");
                await GameChannel.SendMessageAsync("", false, Embed.Build());
            }

        }

        [Command("BluePlate"), Alias("blueplate")]
        public async Task Place()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if ((States.state == States.StatesList[8]) && (States.canPlace == true) && (States.Inventory.Contains("Blue Plate")))
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("I place the blue plate under the light");
                Embed.AddField("Under the blacklight, the plate reveals some writing on the whiteboard.", "That 3 on the wall looks like something else now");
                Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579500498558648329/Blacklight.jpg?width=1015&height=677");
                await GameChannel.SendMessageAsync("", false, Embed.Build());
                States.isPlaced = true;
            }
        }

        [Command("GoldKey"), Alias("RedKey", "RedChip", "BlueChip", "ScrewDriver")]
        public async Task PlaceWrong()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if ((States.state == States.StatesList[8]) && States.canPlace == true)
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("I place the item under the light");
                Embed.AddField("Nothing seems to be happening", "maybe I should try with a different item");
                await GameChannel.SendMessageAsync("", false, Embed.Build());

                States.canPlace = true;

            }
        }

        [Command("Whiteboard"), Alias("whiteboard")]
        public async Task Whiteboard()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[2])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                if (States.isPlaced == true)
                {
                    Embed.WithDescription("I approach the whiteboard, the writing is a lot clearer now.");
                    Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579522071244308485/Whiteboard.jpg?width=1015&height=677");

                    Embed.AddField("Written on the whiteboard is some sort of riddle", "The riddle reads 'A bomb has been set in an American office building with 20 floors starting from the Lobby and ending at the Roof.");
                    Embed.AddField("The police arrested three people that they suspect of placing the bomb, but know that only two people set it", "There’s only enough time to search one floor, so the police asked the suspects where they placed the bomb ");
                    Embed.AddField("The first man, an **American**, says it reminds me of a number from a horror movie. What was it? Friday? Saturday?", "The second man, an **Australian**, says it’s on a floor with an odd number");
                    Embed.AddField("The third man, a Canadian, says it’s in the Basement", "Two of them are telling the truth, one of them is lying");
                    Embed.AddField("Which floor is the bomb on?", "Hmm, a puzzle indeed");
                }
                else
                {
                    Embed.WithDescription("I approach the whiteboard, There's nothing written on it.");
                    Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/580225450157277184/Whiteboard_Clean.jpg?width=1015&height=677");
                }
                


                await GameChannel.SendMessageAsync("", false, Embed.Build());

                EmbedBuilder Embed1 = new EmbedBuilder();
                Embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/578497006650261505/Cabinet.jpg?width=1015&height=677");
                Embed1.AddField("Theres also the filing cabinet, with quite a few files inside", "There's a note on the side that reads, 'Organisation is the bomb!");

               

                await GameChannel.SendMessageAsync("", false, Embed1.Build());

                EmbedBuilder Embed2 = new EmbedBuilder();
                Embed2.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579933100813844483/3.jpg?width=895&height=644");
                Embed2.AddField("Inside the filing cabinet, there's a bunch of different files each with there own names.", "Maybe I should inspect these ... carefully");
                Embed2.WithFooter("inspect a specific file with '!*File Name*'");
                await GameChannel.SendMessageAsync("", false, Embed2.Build());

                States.state = States.StatesList[9];
            }

        }

        [Command("13B"), Alias("13b")]
        public async Task FileAnswer()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[9])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("I pull up and open file 13B");
                Embed.AddField("The document is a pice of paper with the number 42 written in large text", "Maybe it's another clue");
                Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579933089690681357/1.jpg?width=743&height=676");

                await GameChannel.SendMessageAsync("", false, Embed.Build());
            }
        }

        [Command("1A"), Alias("4B","6C","15E","3G","3H", "19H", "8K", "13K", "11N", "4S", "20Z" )]
        public async Task FileWrongAnswer()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[9])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("I pull up and open the file");
                Embed.AddField("The document blows up in my face", "my face is soaked in blood, I need to get out of here before I bleed to death");
                //update timer when hitting the wrong file 
                if (States.timerStarted == false)
                {
                    States.setTimer();
                }
                


                await GameChannel.SendMessageAsync("", false, Embed.Build());
            }
        }

        [Command("Desk"), Alias("desk")]
        public async Task Desk()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if ((States.state == States.StatesList[2]) || (States.state == States.StatesList[11]) || (States.state == States.StatesList[12]) || (States.state == States.StatesList[13]) || (States.state == States.StatesList[14]) || (States.state == States.StatesList[15]))
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("A large wooden desk");
                Embed.AddField("The desk has a few objects on it, as well as a !drawer", "Theres a small !paperweight in front of a !picture, and finally a !laptop");
                if (States.Inventory.Contains("Screwdriver"))
                {
                    Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/580225198498775061/Desk_Ver2.jpg?width=1015&height=677");
                }
                else
                {
                    Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579468320806404096/Desk.jpg?width=1015&height=677");
                }
                
                await GameChannel.SendMessageAsync("", false, Embed.Build());

                States.state = States.StatesList[10];
            }

        }

        [Command("DeskDrawer"), Alias("deskdrawer","Drawer","drawer")]
        public async Task DeskDrawer()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[10])
            { 
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("A desk with one drawer");
                Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579451471976857620/Desk_Drawer.jpg?width=1015&height=677");
                if (!(States.Inventory.Contains("Gold Key")))
                {
                    Embed.AddField("It looks like the drawer is locked, and there's a gold ring around the lock", "Maybe I should check around the !Room some more");
                }
                else
                {
                    Embed.AddField("I open up the drawer with the gold key", "Theres a sequence of numbers with a car covering one");
                    Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579933105821974528/5.jpg?width=500&height=485");
                }

                await GameChannel.SendMessageAsync("", false, Embed.Build());


                States.state = States.StatesList[11];
            }

        }

        [Command("Paperweight"), Alias("paperweight")]
        public async Task Paperweight()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[10])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("A small paperweight");
                if (!(States.Inventory.Contains("Screwdriver")))
                {
                    Embed.AddField("On closer inspection, it looks really old and fragile, with a few cracks forming on the surface", "Perhaps theres more to this paperweight?");
                    Embed.AddField("It looks like I might be able to smash it.", "Maybe I break it with my fist?");
                    Embed.WithImageUrl("https://cdn.discordapp.com/attachments/575130189281886218/579470387654426634/Paperweight.jpg");
                    await GameChannel.SendMessageAsync("", false, Embed.Build());
                } else
                {
                    EmbedBuilder Embed1 = new EmbedBuilder();
                    Embed1.AddField("The paperweight remains in pieces on the desk", "I don't think there's anything special about it anymore");
                    Embed1.WithImageUrl("https://cdn.discordapp.com/attachments/575130189281886218/579472314157105180/Paperweight_Broken.jpg");
                    await GameChannel.SendMessageAsync("", false, Embed1.Build());
                }



                States.state = States.StatesList[12];
            }

        }

        [Command("Break"), Alias("break", "smash", "Smash")]
        public async Task Break()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if ((States.state == States.StatesList[12]) && !(States.Inventory.Contains("Screwdriver")))
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("I broke the paperweight with my fist");
                Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579472314157105180/Paperweight_Broken.jpg?width=726&height=484");
                await GameChannel.SendMessageAsync("", false, Embed.Build());

                EmbedBuilder Embed1 = new EmbedBuilder();
                Embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579473894738493443/Screwdriver.jpg?width=1015&height=677");
                Embed1.AddField("There's a screwdriver inside! Better take this for later", "I don't think theres anything else in the paperweight.");
                States.Inventory.Add("Screwdriver");
                await GameChannel.SendMessageAsync("", false, Embed1.Build());

            }


        }

        [Command("Picture"), Alias("picture")]
        public async Task Picture()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[10])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("A picture frame with an photo of ... something inside");
                Embed.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579485673199763467/Photo.jpg?width=1015&height=677");
                await GameChannel.SendMessageAsync("", false, Embed.Build());
                if (!(States.Inventory.Contains("Screwdriver")))
                {
                    Embed.AddField("On closer inspection, the image is creased in a very odd shape", "I could take a look if I had something to yank the picture out");
                }
                else
                {
                    if (!States.Inventory.Contains("Red Key"))
                    {
                        EmbedBuilder Embed1 = new EmbedBuilder();
                        Embed1.AddField("The image is creased in a very odd shape", "I used the screwdriver to open the picture frame.");
                        Embed1.WithImageUrl("https://media.discordapp.net/attachments/575130189281886218/579486962184880128/Red_Key.jpg?width=1015&height=677");
                        Embed1.AddField("Aha, a red key fell out!I'll take this incase I need it.", "I should probably return to the !desk now.");
                        States.Inventory.Add("Red Key");
                        await GameChannel.SendMessageAsync(" ", false, Embed1.Build());
                    }

                }



                States.state = States.StatesList[13];
            }
        }

        [Command("Laptop"), Alias("laptop")]
        public async Task Laptop()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[10])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("I open up the laptop, revealing a black screen");
                Embed.AddField("The laptop has a few slots for chips", "Maybe I need these to turn it on");

                if (!(States.Inventory.Contains("Red Chip") && States.Inventory.Contains("Blue Chip")))
                {
                    Embed.AddField("It looks like the laptop needs something more to turn on ", "Maybe I should check around the !room some more");
                }
                else
                {
                    Embed.AddField("I place both chips into the slots and the laptop boots up", "A button appears on the screen, and a prompt telling me to !click it");
                }

                if ((States.Inventory.Contains("Blue Chip") && !(States.Inventory.Contains("Red Chip")) ) || (States.Inventory.Contains("Red Chip") && !(States.Inventory.Contains("Blue Chip"))))
                {
                    Embed.AddField("I put in the chip but nothing seems to be happening.", "Maybe I need both chips for it to work?");
                }
                await GameChannel.SendMessageAsync("", false, Embed.Build());


                States.state = States.StatesList[14];
            }

        }

        [Command("Escape"), Alias("escape")]
        public async Task Escape()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;
            //CHANGE STATE BACK TO 16
            if (States.state == States.StatesList[16])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithAuthor("Florence Carter");
                Embed.WithDescription("With the door open, I run out into the outside");
                Embed.AddField("my eyes finally adapt to the blinding light", "There's nothing around but white ... nothingness, What's going on?");

                await GameChannel.SendMessageAsync("", false, Embed.Build());

                EmbedBuilder Embed2 = new EmbedBuilder();
                Embed2.WithAuthor("DDA SYSTEMS");
                Embed2.WithDescription("SYSTEM ERROR - NAVIGATION SOFTWARE OVERRIDE DETECTED");
                Embed2.AddField("SYSTEM RESET >...FAILURE", "ERROR>>ERR&@R...*SD)!@#SCSD...ESC@P3");

                await Task.Delay(5000).ContinueWith(t => GameChannel.SendMessageAsync("", false, Embed2.Build()));

                EmbedBuilder Embed1 = new EmbedBuilder();
                Embed1.WithAuthor("Intruder");
                Embed1.WithColor(Color.Red);
                Embed1.WithDescription("Congratulations, detectives, how clever you all are for solving such enigmatic puzzles.");
                Embed1.AddField("What, did you really think you could navigate a mind without any interference", "Why are you doing this, do you really think it will help?");

                await Task.Delay(5000).ContinueWith(t => GameChannel.SendMessageAsync("", false, Embed1.Build()));

                EmbedBuilder Embed3 = new EmbedBuilder();
                Embed3.WithAuthor("Intruder");
                Embed3.WithColor(Color.Red);
                Embed3.WithDescription("Let's see how you cope without your partners");

                await Task.Delay(5000).ContinueWith(t => GameChannel.SendMessageAsync("", false, Embed3.Build()));

                States.state = States.StatesList[17];


                //setup for room 2
                Study = await Context.Guild.CreateRoleAsync("Study", null, Color.DarkGreen, false, null);
                Kids_Bedroom = await Context.Guild.CreateRoleAsync("Kids Bedroom", null, Color.Orange, false, null);


                await Task.Delay(2000).ContinueWith(t => SetupRoom2());

                await Task.Delay(7000).ContinueWith(t =>
                {
                    GameChannel.DeleteAsync();
                    Navigator.DeleteAsync();
                });
            }

        }

        public async Task SetupRoom2()
        {
            StudyChannel = await Context.Guild.CreateTextChannelAsync("Study", null, null);

            await StudyChannel.AddPermissionOverwriteAsync((IRole)Context.Guild.EveryoneRole, OverwritePermissions.DenyAll(StudyChannel));
            await StudyChannel.AddPermissionOverwriteAsync((IRole)Kids_Bedroom, OverwritePermissions.DenyAll(StudyChannel));
            await StudyChannel.AddPermissionOverwriteAsync((IRole)Study, OverwritePermissions.AllowAll(StudyChannel));

            BedroomChannel = await Context.Guild.CreateTextChannelAsync("Kids Bedroom", null, null);
            

            await BedroomChannel.AddPermissionOverwriteAsync((IRole)Context.Guild.EveryoneRole, OverwritePermissions.DenyAll(BedroomChannel));
            await BedroomChannel.AddPermissionOverwriteAsync((IRole)Study, OverwritePermissions.DenyAll(BedroomChannel));
            await BedroomChannel.AddPermissionOverwriteAsync((IRole)Kids_Bedroom, OverwritePermissions.AllowAll(BedroomChannel));

            States.StudyState = States.StatesListStudy[0];
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithDescription("It looks like I'm trapped in a study.");
            embed.AddField("It's locked, but at least there's a door out of here", "It looks like theres an electronic !lock on the door");
            embed.AddField("There's a few things around me. There's a large !map of the earth in the middle of the room", "There's also a !workdesk with a few things on it like a computer, and some !bookshelves");
            await Room1.StudyChannel.SendMessageAsync("", false, embed.Build());

            States.KidRoomState = States.StatesListKids[0];
            EmbedBuilder embed1 = new EmbedBuilder();
            embed1.WithDescription("Looks like I'm stuck in a kids bedroom");
            embed1.AddField("The door's locked with a !codelock, but at least there's a way out", "In the room are two !beds, a !bedsidetable with a bunch of stuff on it, some !posters on the wall, and a !toybox in the corner ");
            embed1.AddField("On top of the door, there's a wooden letter K", "In the room are two !beds, a !bedsidetable with a bunch of stuff on it, some !posters on the wall, and a !toybox in the corner ");
            await Room1.BedroomChannel.SendMessageAsync("", false, embed1.Build());

            foreach (var people in Context.Guild.Users)
            {
                var NavID1 = people.Guild.GetRole(Navigator.Id);
                if (people.Roles.Contains(NavID1))
                {
                    States.users.Add(people);
                }
            }

            for (var i = 0; i < States.users.Count; i++)
            {
                await States.users[i].RemoveRoleAsync(Navigator, null);
                if (i < States.users.Count / 2)
                {
                    await ((SocketGuildUser)States.users[i]).AddRoleAsync(Study, null);
                }
                else
                {
                    await ((SocketGuildUser)States.users[i]).AddRoleAsync(Kids_Bedroom, null);
                }

                await (States.users[i]).ModifyAsync(x =>
                {
                    x.Nickname = null;
                });

            }



            Room2.BookshelvesOpen = false;
            Room2.ComputerOpen = false;

            Room2.password1 = false;
            Room2.password2 = false;
            Room2.password3 = false;
            Room2.password4 = false;

            Room2.studyDoorOpen = false;

            await GameChannel.DeleteAsync();
            await Navigator.DeleteAsync();



        }


        [Command("Inventory"), Alias("inventory")]
        public async Task Inventory()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;
        

            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithAuthor("Florence Carter");
            Embed.WithDescription("Here is everything I have:"); 
          foreach (string item in States.Inventory)
           {
                Embed.AddField(item, "-----------------");
            }
            await GameChannel.SendMessageAsync("", false, Embed.Build());

        }

        [Command("Click"), Alias("click")]
        public async Task LaptopPuzzle()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;

            if (States.state == States.StatesList[14])
            {
                States.setLaptopTimer();
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.WithDescription("You have 60 seconds to find and enter the code, good luck");
                Embed.AddField("||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||💀||", "||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||🙃|| ||🙃|| ||🙃|| ||💀||");
                Embed.AddField("||💀|| ||🙃|| ||💀|| ||🙃|| ||💀|| ||💀|| ||🙃|| ||💀|| ||💀|| ||💀||", "||💀|| ||🙃|| ||💀|| ||🙃|| ||💀|| ||💀|| ||🙃|| ||💀|| ||💀|| ||💀||");
                Embed.AddField("||💀|| ||🙃|| ||🙃|| ||🙃|| ||💀|| ||💀|| ||🙃|| ||🙃|| ||🙃|| ||💀||", "||💀|| ||💀|| ||💀|| ||🙃|| ||💀|| ||💀|| ||💀|| ||💀|| ||🙃|| ||💀||");
                Embed.AddField("||💀|| ||💀|| ||💀|| ||🙃|| ||💀|| ||💀|| ||💀|| ||💀|| ||🙃|| ||💀||", "||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||🙃|| ||🙃|| ||🙃|| ||💀||");
                Embed.AddField("||💀|| ||💀|| ||🙃|| ||🙃|| ||🙃|| ||💀|| ||💀|| ||💀|| ||💀|| ||💀||", "||💀|| ||💀|| ||🙃|| ||💀|| ||🙃|| ||💀|| ||💀|| ||🙃|| ||🙃|| ||🙃||");
                Embed.AddField("||💀|| ||💀|| ||🙃|| ||🙃|| ||🙃|| ||💀|| ||💀|| ||💀|| ||💀|| ||🙃||", "||💀|| ||💀|| ||💀|| ||💀|| ||🙃|| ||💀|| ||💀|| ||🙃|| ||🙃|| ||🙃||");
                Embed.AddField("||💀|| ||💀|| ||💀|| ||💀|| ||🙃|| ||💀|| ||💀|| ||🙃|| ||💀|| ||💀||", "||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||💀|| ||🙃|| ||🙃|| ||🙃||");
                await GameChannel.SendMessageAsync("", false, Embed.Build());
                States.state = States.StatesList[15];
            }


        }

        [Command("4592"), Alias("code4692")]
        public async Task LaptopAnswer()
        {
            var user = Context.User as SocketGuildUser;
            var NavID = user.Guild.GetRole(Navigator.Id);
            if (!user.Roles.Contains(NavID)) return;
            if (States.state == States.StatesList[15])
            {
                EmbedBuilder Embed = new EmbedBuilder();
                Embed.AddField("I input the code", "The timer stopped counting down!");
                Embed.AddField("The wall next to the desk opens up. ", "Finally, a way out, time to !escape");

                await GameChannel.SendMessageAsync("", false, Embed.Build());
                States.state = States.StatesList[16];
                States.LaptopTimer.Close();
                States.LaptopTimer.Stop();

                States.bleedTimer.Close();
                States.bleedTimer.Stop();
            }

        }




    }
}
