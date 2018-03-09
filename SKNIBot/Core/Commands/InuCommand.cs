﻿using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Net;
using Newtonsoft.Json;
using SKNIBot.Core.Settings;
using System.IO;

namespace SKNIBot.Core.Commands
{
    [CommandsGroup]
    public class InuCommand
    {
        [Command("pies")]
        [Description("Display some cute dogs.")]
        [Aliases("inu", "dog")]
        public async Task Inu(CommandContext ctx)
        {
            var client = new WebClient();
            DogContainer dogContainer;

            do
            {
                var dog = client.DownloadString("https://random.dog/woof.json");
                dogContainer = JsonConvert.DeserializeObject<DogContainer>(dog);
            }
            while (dogContainer.Url.Split('.')[dogContainer.Url.Split('.').Length - 1] != "jpg");

            var dogPicture = client.DownloadData(dogContainer.Url);
            var stream = new MemoryStream(dogPicture);

            await ctx.TriggerTypingAsync();
            await ctx.RespondWithFileAsync(stream, "inu.jpg");
        }
    }
}
