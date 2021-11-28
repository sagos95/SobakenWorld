using System.Collections.Concurrent;

namespace BlazorUI.Services
{
    public enum NotificationCategories
    {
        Info,
        Warning,
        Error
    }

    public record NotificationInfo(Exception? Exception, NotificationCategories NotificationCategory = NotificationCategories.Info, string? InterfaceMessage = null);

    public class UiNotificationBus
    {
        private static IServiceProvider _serviceProvider = default!;

        private readonly ConcurrentDictionary<string, EventHandler<NotificationInfo>> _subscriptions;
        private event EventHandler<NotificationInfo>? OnNotification;

        public UiNotificationBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _subscriptions = new();
        }

        public static UiNotificationBus GetInstance() => _serviceProvider.GetRequiredService<UiNotificationBus>();

        public void Send(string Message, NotificationCategories notificationCategory = NotificationCategories.Info)
        {
            OnNotification?.Invoke(sender: null, new(Exception: null, notificationCategory, Message));
        }

        public void Send(Exception Exception, string? Message = null)
        {
            var msg = string.IsNullOrEmpty(Message) ? Exception.Message : Message;
            OnNotification?.Invoke(sender: null, new(Exception, NotificationCategories.Error, msg));
        }

        public bool RemoveSubscription(string subscriptionName)
        {
            var removeResult = _subscriptions.TryRemove(subscriptionName, out var action);
            if (removeResult)
            {
                OnNotification -= action;
            }
            return removeResult;
        }

        public bool Subscribe(string subscriptionName, EventHandler<NotificationInfo> action)
        {
            if (_subscriptions.TryGetValue(subscriptionName, out var existingAction))
            {
                RemoveSubscription(subscriptionName);
            }

            var addResult = _subscriptions.TryAdd(subscriptionName, action);
            if (addResult)
            {
                OnNotification += action;
            }
            return addResult;
        }
    }
}
