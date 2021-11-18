namespace BlazorUI.Models
{
    public class ImageSpotInfo
    {
        public string ImageUrl { get; }
        public IReadOnlyCollection<Point> GoodSpots { get; }
        public int AccuracyPx { get; }

        public ImageSpotInfo(string imageUrl, IReadOnlyCollection<Point> goodSpots, int accuracyPx = 40)
        {
            ImageUrl = imageUrl;
            GoodSpots = goodSpots;
            AccuracyPx = accuracyPx;
        }

        public bool IsThisAGoodSpot(Point point)        
            => GoodSpots.Any(p => Distance(point, p) <= AccuracyPx);        

        private static int Distance(Point point, Point p)
            => (int)Math.Sqrt(Math.Pow((double)(point.X - p.X), 2) + Math.Pow((double)(point.Y - p.Y), 2));
    }
}
