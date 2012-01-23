namespace StopLossKata
{
    public class PriceChanged
    {
        readonly int _newPrice;

        public PriceChanged(int newPrice)
        {
            _newPrice = newPrice;
        }

        public int NewPrice
        {
            get
            {
                return _newPrice;
            }
        }
    }
}