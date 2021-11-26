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
        public enum GameStates
        {
            WaitingStart,
            InGame,
            Finish
        }

        GameStates _gameState = GameStates.WaitingStart;

        public ImageSearchGameEngine? CurrentGame { get; private set; }

        public async Task SetGameState(GameStates newState)
        {
            if (_gameState == GameStates.WaitingStart && newState == GameStates.InGame)
            {
                //CurrentGame = //factory.create
            }
            _gameState = newState;
            await InvokeAsync(StateHasChanged);
        } 
        
        public async Task FinishGame(/*data*/)
        {
            _gameState = GameStates.Finish;
            await InvokeAsync(StateHasChanged);
        }        
    }
}