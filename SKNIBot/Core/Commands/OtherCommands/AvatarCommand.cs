﻿using System.IO;
using System.Net;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using SKNIBot.Core.Helpers;

namespace SKNIBot.Core.Commands.ModerationCommands
{
    [CommandsGroup("Różne")]
    public class AvatarCommand : BaseCommandModule
    {
        [Command("awatar")]
        [Description("Pokazuje awatar użytkownika.")]
        public async Task Avatar(CommandContext ctx, [Description("Użytkownik, którego awatar chcesz.")] DiscordMember member = null)
        {
            await ctx.TriggerTypingAsync();
            
            if (member == null)
            {
                await PostEmbedHelper.PostEmbed(ctx, "Awatar", ctx.User.Mention, ctx.User.AvatarUrl);
            }
            else
            {
                await PostEmbedHelper.PostEmbed(ctx, "Awatar", member.Mention, member.AvatarUrl);
            }
        }

    }
}
