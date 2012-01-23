namespace StopLossKata
{
    public class PriceChange
    {
        readonly int _newPrice;

        public PriceChange(int newPrice)
        {
            _newPrice = newPrice;
        }

        public int NewPrice
        {
            get
            {
                return this._newPrice;
            }
        }
    }
}