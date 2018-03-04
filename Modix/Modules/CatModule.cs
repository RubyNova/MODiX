﻿namespace Modix.Modules
{
    using Discord.Commands;
    using Services.Cat;
    using System;
    using System.Threading.Tasks;

    public enum Media
    {
        Picture, // 0
        Gif // 1
    }

    [Group("cat"), Summary("Cat Related Commands")]
    public class CatModule : ModuleBase
    {
        private static Media _mediaType;
        private readonly ICatService _catService;

        public CatModule(ICatService catService)
        {
            _catService = catService;
        }

        [Command(RunMode = RunMode.Async)]
        public async Task Cat(string param = "")
        {
            string message;

            // It can take a Media type parameter however the command has to be !cat picture or !cat gif all of the time. 
            // If !cat is used, it says too few parameters passed and fails. 
            // I want to retain !cat functionality. 
            if (string.IsNullOrWhiteSpace(param))
            {
                _mediaType = Media.Picture;
            }
            else if (string.Equals("gif", param, StringComparison.OrdinalIgnoreCase))
            {
                _mediaType = Media.Gif;
            }
            else
            {
                await Context.Channel.SendMessageAsync("Use `!cat` or `!cat gif`");
                return;
            }

            //await _catService.

            // Send the link
            //await Context.Channel.SendMessageAsync(message);
        }

        [Command("poke")]
        public async Task BuildCache()
        {
            _catService.Poke();
        }
    }
}