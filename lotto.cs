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
        protected static bool lottostatus;

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