namespace BlazorUI.Models
{
    public class ImageSpotInfo
    {
        public string ImageUrl { get; }
        public IReadOnlyCollection<Spot> GoodSpots { get; }

        public ImageSpotInfo(string imageUrl, IReadOnlyCollection<Spot> goodSpots)
        {
            ImageUrl = imageUrl;
            GoodSpots = goodSpots;
        }

        public bool IsThisAGoodSpot(decimal x, decimal y)        
            => GoodSpots.Any(p => Distance(x, y, p) <= p.Accuracy);        

        private static int Distance(decimal x, decimal y, Spot p)
            => (int)Math.Sqrt(Math.Pow((double)(x - p.X), 2) + Math.Pow((double)(y - p.Y), 2));
    }
}
