namespace BlazorUI.Models
{
    public class ImageSpotInfo
    {
        public string ImageUrl { get; }
        public IReadOnlyCollection<Spot> TargetSpots { get; }
        public string InfoText { get; }
        public string ErrorText { get; }
        public ImageSize ImageSize { get; }

        public ImageSpotInfo(string imageUrl, IReadOnlyCollection<Spot> targetSpots, string infoText, string errorText, ImageSize imageSize)
        {
            ImageUrl = imageUrl;
            TargetSpots = targetSpots;
            InfoText = infoText;
            ErrorText = errorText;
            ImageSize = imageSize;
        }

        public bool IsThisAGoodSpot(decimal x, decimal y)        
            => TargetSpots.Any(p => Distance(x, y, p) <= p.Accuracy);        

        private static decimal Distance(decimal x, decimal y, Spot p)
            => (decimal)Math.Sqrt(Math.Pow((double)(x - p.X), 2) + Math.Pow((double)(y - p.Y), 2));
    }
}
