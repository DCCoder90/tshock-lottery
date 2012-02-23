﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Terraria;
using TShockAPI;
using Hooks;

namespace Flogoff
{
    [APIVersion(1, 11)]

    public class Lotto : TerrariaPlugin
    {
        public static bool lottostatus;
        public static int winnings;
        public static int odds;
        public static List<string> items = new List<string>;

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
            get { return "Allows for a radom lottery!"; }
        }

        public Lotto(Main game)
            : base(game)
        {
            Order = -1;
        }

        public override void Initialize()
        {
            //Hooks.ServerHooks.Chat += OnChat;
            //NetHooks.SendData += OnSendData;
            Commands.ChatCommands.Add(new Command("lotto", lotto, "lotto"));
            //Commands.ChatCommands.Add(new Command("flogoff", flogoff, "flogoff"));
        }

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
                    //string item;
                    var items = TShock.Utils.GetItemByIdOrName(param);
                    if (items.Count == 0)
                    {
                        args.Player.SendMessage("Invalid item type!", Color.Red);
                    }
                    else if (items.Count > 1)
                    {
                        args.Player.SendMessage(string.Format("More than one ({0}) item matched!", items.Count), Color.Red);
                    }
                break;

                case "remove":
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


        /*protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Hooks.ServerHooks.Chat -= OnChat;
                NetHooks.SendData -= OnSendData;
            }
            base.Dispose(disposing);
        }*/
    }
}