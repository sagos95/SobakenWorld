namespace BlazorUI.Services
{
    public static class SafeExecutionExtensions
    {
        private static Lazy<UiNotificationBus> _uiNotificationManager = new(UiNotificationBus.GetInstance());

        public static async void AsSafe(this Task task, Action<Exception>? onError = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
                _uiNotificationManager.Value.Send(ex);
            }
        }
    }

    public static class Call
    {
        private static Lazy<UiNotificationBus> _uiNotificationManager = new(UiNotificationBus.GetInstance());

        public static async void AsSafe(Func<Task> task, Action<Exception>? onError = null)
        {
            try
            {
                await task();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);                
                _uiNotificationManager.Value.Send(ex);
            }
        }

        public static void AsSafe(Action action, Action<Exception>? onError = null)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
                _uiNotificationManager.Value.Send(ex);
            }
        }

        public static async Task AsSafeAsync(Func<Task> task, Action<Exception>? onError = null)
        {
            try
            {
                await task();
            }
            catch (Exception ex)
            {
                onError?.Invoke(ex);
                _uiNotificationManager.Value.Send(ex);
            }
        }
    }
}
