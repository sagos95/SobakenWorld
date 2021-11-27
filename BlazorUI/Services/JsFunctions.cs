using BlazorUI.Models;
using Microsoft.JSInterop;

namespace BlazorUI.Services
{
    public class JsFunctions
    {
        private record JsImageSize
        {
            public int width { get; set; }
            public int height { get; set; }
        }
        private readonly IJSRuntime _js;

        public JsFunctions(IJSRuntime js)
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

        public async Task<ImageSize> GetImageSize(string elementId)
        {            
            var size = await _js.InvokeAsync<JsImageSize>("getImageSize", elementId);

            return new ImageSize(size.width, size.height);
        }
    }
}
