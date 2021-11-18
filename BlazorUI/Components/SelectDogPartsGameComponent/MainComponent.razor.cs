using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using BlazorUI;
using BlazorUI.Shared;
using BlazorUI.Components.SelectDogPartsGameComponent.States;
using BlazorUI.Models;

namespace BlazorUI.Components.SelectDogPartsGameComponent
{
    public partial class MainComponent
    {
        List<ImageSpotInfo> _imageSpots = new();
        List<string> _alreadyUsedUrls = new();
        Random _random = new();

        public enum GameStates
        {
            Start,
            InGame,
            Finish
        }

        GameStates _gameState = GameStates.Start;

        public async Task SetGameState(GameStates newState)
        {
            _gameState = newState;
            await InvokeAsync(StateHasChanged);
        } 
        
        public async Task FinishGame(/*data*/)
        {
            _gameState = GameStates.Finish;
            await InvokeAsync(StateHasChanged);
        }

        public ImageSpotInfo? GetRandomSpotInfo()
        {
            var availableImageInfos = _imageSpots
                .Where(s => !_alreadyUsedUrls.Contains(s.ImageUrl))
                .ToArray();

            if (availableImageInfos.Any())
            {
                var randomImageNumber = _random.Next(availableImageInfos.Length);
                return availableImageInfos[randomImageNumber];
            }

            return null;
        }
    }
}