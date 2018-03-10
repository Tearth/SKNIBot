﻿using System.IO;
using System.Net;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using Newtonsoft.Json;
using SKNIBot.Core.Containers;

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
            await ctx.TriggerTypingAsync();

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

            await ctx.RespondWithFileAsync(stream, "inu.jpg");
        }
    }
}
