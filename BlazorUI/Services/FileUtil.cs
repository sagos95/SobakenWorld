using Microsoft.JSInterop;

namespace BlazorUI.Services
{
    public class FileUtil
    {
        private readonly IJSRuntime _js;

        public FileUtil(IJSRuntime js)
        {
            _js = js;
        }

        public async Task SaveAs(string filename, string text)
        {
            var bytes = System.Text.Encoding.UTF8.GetBytes(text);

            await _js.InvokeAsync<object>(
                "saveAsFile",
                filename,
                Convert.ToBase64String(bytes));
        }
    }
}
