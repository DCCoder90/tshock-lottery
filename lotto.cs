﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Terraria;
using TShockAPI;
using Hooks;

namespace Lotto
{
    [APIVersion(1, 11)]

    public class Lotto : TerrariaPlugin
    {

        #region Variables

        public static bool lottostatus;
        public static bool removewinner = true;
        public static int winnings;
        public static string[] players;
        public static int odds;
        public static List<string> items = new List<string>();
        public static List<int> playerindex = new List<int>();

        #endregion

        #region Information

        public override Version Version
        {
            get { return new Version("0.0.1"); }
        }

        public override string Name
        {
            get { return "Lotto"; }
        }

        public override string Author
        {
            get { return "Darkvengance aka Sildaekar"; }
        }

        public override string Description
        {
            get { return "Allows for a random lottery!"; }
        }

        #endregion

        public Lotto(Main game)
            : base(game)
        {
            Order = 4;
        }

        #region Initialization

        public override void Initialize()
        {
            Commands.ChatCommands.Add(new Command("lotto", lotto, "lotto"));
        }


        public void OnGreetPlayer(int who, HandledEventArgs e)
        {
            Lotto.playerindex.Add(who);
            //Lotto.players[who] = "playername";
        }

        public void OnLeave(int ply)
        {
            Lotto.playerindex.Remove(ply);
            Lotto.players[ply] = "";
        }

        #endregion

        #region Lottery

        public static void drawing()
        {
            int r, winner, t;
            string item;
            Random rand, rand2;

            rand=new Random();
            rand2 = new Random();

            r = rand.Next(Lotto.playerindex.Count);
            winner=Lotto.playerindex[r];

            t = rand.Next(Lotto.items.Count);
            item = Lotto.items[t];

            if (removewinner)
            {
                Lotto.playerindex.Remove(winner);
            }

            //Get player by index
            //TSPlayer winn=TShock.Utils.
        }

        #endregion

        #region Commands

        protected void lotto(CommandArgs args)
        {
            TSPlayer player = args.Player;
            string cmd, param;
            if (args.Parameters.Count <= 0)
            {
                player.SendMessage("Invalid Syntax! Usage: /lotto help");
                return;
            }
            else
            {
                cmd = args.Parameters[0].ToLower();
                param = args.Parameters[1].ToLower();
            }

            switch (cmd)
            {
                case "help":
                    player.SendMessage("Usage: /lotto status [bool] - Turns the lottery on or off");
                    player.SendMessage("Usage: /lotto add [item] - Adds an item to the lottery winnings");
                    player.SendMessage("Usage: /lotto remove [item] - Removes an item from the lottery winnings");
                    player.SendMessage("Usage: /lotto win [1-9] - Number of items to give to winner");
                    player.SendMessage("Usage: /lotto odds [0-9] - Odds the lottery will be triggered");
                break;

                case "status":
                    if (param == "true")
                    {
                        lottostatus = true;
                    }else if(param=="false"){
                        lottostatus = false;
                    }
                break;

                case "add":
                    var items = TShock.Utils.GetItemByIdOrName(param);
                    if (items.Count == 0)
                    {
                        player.SendMessage("Invalid item type!", Color.Red);
                        break;
                    }
                    else if (items.Count > 1)
                    {
                        player.SendMessage(string.Format("More than one ({0}) item matched!", items.Count), Color.Red);
                        break;
                    }
                    var item=items[0];
                    Lotto.items.Add(item.name);
                    player.SendMessage(string.Format("Item {0} added to lottery winnings!",item.name));
                break;

                case "remove":
                    var itemsr = TShock.Utils.GetItemByIdOrName(param);
                    if (itemsr.Count == 0)
                    {
                        player.SendMessage("Invalid item type!", Color.Red);
                        break;
                    }
                    else if (itemsr.Count > 1)
                    {
                        player.SendMessage(string.Format("More than one ({0}) item matched!", itemsr.Count), Color.Red);
                        break;
                    }
                    var itemr=itemsr[0];
                    Lotto.items.Remove(itemr.name);
                    player.SendMessage(string.Format("Item {0} removed from lottery winnings!",itemr.name));
                break;

                case "win":
                    int win = Convert.ToInt32(param);
                    if (win > 9)
                    {
                        player.SendMessage("Winnings set too high! Must be set 0-9");
                        break;
                    }
                    Lotto.winnings = win;
                break;

                case "odds":
                    int odds = Convert.ToInt32(param);
                    if (odds > 9)
                    {
                        player.SendMessage("Odds set too high! Must be set 0-9");
                        break;
                    }
                    Lotto.odds = odds;
                break;
                    
            }

        }

        #endregion
    }
}