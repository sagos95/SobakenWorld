namespace BlazorUI.Models
{
    public class ImageSearchGameScene
    {
        private List<ImageSpotInfo> _imageSpots;
        private List<string> _alreadyUsedUrls;
        private Random _random = new();

        public ImageSearchGameScene(List<ImageSpotInfo> imageSpots)
        {
            _imageSpots = imageSpots;
            _alreadyUsedUrls = new();
            _random = new();
        }

        public ImageSpotInfo? GetNextRandomImageInfo()
        {
            var availableImageInfos = _imageSpots
                .Where(s => !_alreadyUsedUrls.Contains(s.ImageUrl))
                .ToArray();

            if (availableImageInfos.Any())
            {
                var randomImageNumber = _random.Next(availableImageInfos.Length);
                var result = availableImageInfos[randomImageNumber];
                _alreadyUsedUrls.Add(result.ImageUrl);
                return result;
            }

            return null;
        }
    }
}
