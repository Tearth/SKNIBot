﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using SKNIBot.Core.Database;
using SKNIBot.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKNIBot.Core.Commands.PicturesCommands
{
    [CommandsGroup("Obrazki")]
    class CatBoyCommand : BaseCommandModule
    {
        private Random random;

        public CatBoyCommand()
        {
            random = new Random();
        }

        [Command("catboy")]
        [Description("Wyświetla słodkie catboy.")]
        public async Task Neko(CommandContext ctx, [Description("Wzmianka")] DiscordMember member = null)
        {
            await ctx.TriggerTypingAsync();

            using (var databaseContext = new StaticDBContext())
            {
                // String.Equals doesn't work in SQLite provider (comparison is case sensitive) so it must be replaced with DbFunctions.Like().
                var catBoys = databaseContext.SimpleResponses
                    .Where(catboy => catboy.Command.Name == "CatBoy")
                    .Select(p => p.Content)
                    .ToList();

                var wordIndex = random.Next(0, catBoys.Count);
                var response  = catBoys[wordIndex];

                await PostEmbedHelper.PostEmbed(ctx, "Cat boy", member?.Mention, response);
            }
        }

    }
}
