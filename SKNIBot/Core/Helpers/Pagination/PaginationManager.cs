﻿using System.Text.RegularExpressions;
using DSharpPlus.EventArgs;

namespace SKNIBot.Core.Helpers.Pagination
{
    public class PaginationManager
    {
        public const string LeftEmojiName = ":arrow_left:";
        public const string RightEmojiName = ":arrow_right:";

        private const string PaginationPrompt = "Strona";

        public string GeneratePaginationHeader(int currentPage, int pagesCount)
        {
            var paginationData = new PaginationData
            {
                CurrentPage = currentPage,
                PagesCount = pagesCount
            };

            return GeneratePaginationHeader(paginationData);
        }

        public string GeneratePaginationHeader(PaginationData paginationData)
        {
            return $"{PaginationPrompt} {paginationData.CurrentPage} z {paginationData.PagesCount}.";
        }

        public PaginationData ParsePaginationHeader(string header)
        {
            var matches = Regex.Matches(header, $"{PaginationPrompt}\\s(?<currentPage>\\d*)\\sz\\s(?<pagesCount>\\d*)");
            var currentPage = int.Parse(matches[0].Groups["currentPage"].Value);
            var pagesCount = int.Parse(matches[0].Groups["pagesCount"].Value);

            return new PaginationData
            {
                CurrentPage = currentPage,
                PagesCount = pagesCount
            };
        }

        public void UpdatePagination(PaginationData paginationData, MessageReactionAddEventArgs reactionEvent)
        {
            switch (reactionEvent.Emoji.GetDiscordName())
            {
                case LeftEmojiName:
                {
                    if (paginationData.CurrentPage > 1)
                    {
                        paginationData.CurrentPage--;
                    }

                    break;
                }
                case RightEmojiName:
                {
                    if (paginationData.CurrentPage < paginationData.PagesCount)
                    {
                        paginationData.CurrentPage++;
                    }

                    break;
                }
            }
        }
    }
}
