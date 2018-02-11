﻿namespace Modix.Modules
{
    using System;
    using System.Threading.Tasks;
    using Discord.Commands;
    using Modix.Utilities;

    [Group("random"), Name("Random"), Summary("A bunch of random commands"), ApplyChannelLimits]
    public class RandomModule : ModuleBase
    {
        private static readonly Random Random = new Random();

        [Command("number"), Summary("Gets a random number")]
        public async Task RandomNumber(int min = 0, int max = 10)
        {
            if (min >= max)
            {
                await Context.Channel.SendMessageAsync("Maximum number must be greater than the minimum number");
                return;
            }

            var number = Random.Next(min, max);

            await Context.Channel.SendMessageAsync(number.ToString());
        }

        [Command("coin"), Summary("Flips a coin")]
        public async Task FlipCoin()
        {
            var coin = Random.Next(0, 2);

            if (coin == 0)
            {
                await Context.Channel.SendMessageAsync("heads");
            }
            else
            {
                await Context.Channel.SendMessageAsync("tails");
            }
        }
    }
}