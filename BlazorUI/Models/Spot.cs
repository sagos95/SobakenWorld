namespace BlazorUI.Models
{
    public record Spot(decimal X, decimal Y, decimal Accuracy);

    public record EditingSpot
    {
        public decimal X { get; set; }
        public decimal Y { get; set; }
        public decimal Accuracy { get; set; }

        public EditingSpot() {}

        public EditingSpot(decimal x, decimal y, decimal accuracy)
        {
            X = x;
            Y = y;
            Accuracy = accuracy;
        }

        public Spot BuildSpot() 
            => new Spot(X, Y, Accuracy);
    }
}
