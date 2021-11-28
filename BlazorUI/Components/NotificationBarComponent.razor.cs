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
using BlazorUI.Services;

namespace BlazorUI.Components
{
    public partial class NotificationBarComponent
    {
        [Inject]
        UiNotificationBus? UiNotificationBus { get; set; }

        public NotificationInfo? CurrentNotification { get; set; }

        protected override void OnInitialized()
        {
            UiNotificationBus?.Subscribe(nameof(NotificationBarComponent), OnNotificationReceived);
            base.OnInitialized();
        }

        protected override void OnAfterRender(bool firstRender)
        {
            base.OnAfterRender(firstRender);
        }

        private void CloseNotification()
        {
            CurrentNotification = null;
        }

        private async void OnNotificationReceived(object? sender, NotificationInfo info)
        {
            if (CurrentNotification == null)
            {
                CurrentNotification = info;
            }
            else
            {
                CurrentNotification = CurrentNotification with { InterfaceMessage = CurrentNotification.InterfaceMessage + "\n" + info.InterfaceMessage };
            }
            await InvokeAsync(() => StateHasChanged());
        }

        private LogLevel GetLogLevel(NotificationCategories notificationCategory)
            => notificationCategory switch
            {
                NotificationCategories.Error => LogLevel.Error,
                NotificationCategories.Warning => LogLevel.Warning,
                _ => LogLevel.Information
            };
    }
}