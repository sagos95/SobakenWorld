namespace BlazorUI.Models
{
    public class ImageSearchGameEngine
    {
        private List<ImageSpotInfo> _imageSpots = new();
        private List<string> _alreadyUsedUrls = new();
        private Random _random = new();

        public ImageSpotInfo? GetNextRandomImageInfo()
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
