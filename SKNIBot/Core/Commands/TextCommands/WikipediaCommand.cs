﻿using System.Net;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace SKNIBot.Core.Commands.TextCommands
{
    [CommandsGroup("Tekst")]
    public class WikipediaCommand : BaseCommandModule
    {
        private const string _randomSiteURL = "https://pl.wikipedia.org/wiki/Specjalna:Losowa_strona";

        [Command("wiki")]
        [Description("Wylosuj artykuł z Wikipedii")]
        [Aliases("wikipedia")]
        public async Task Wikipedia(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();

            var request = (HttpWebRequest)WebRequest.Create(_randomSiteURL);

            using (var response = await request.GetResponseAsync())
            {
                await ctx.RespondAsync(response.ResponseUri.ToString());
            }
        }
    }
}
