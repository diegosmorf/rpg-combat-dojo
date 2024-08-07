namespace CodingDojo.Combat.Characters
{
    public class InfoPoints
    {
        protected int currentValue;
        private readonly int minValue;
        private readonly int maxValue;

        public int Value
        {
            get { return currentValue; }

            set
            {
                if (value < minValue)
                    currentValue = minValue;                
                else
                    if (value > maxValue)
                        currentValue = maxValue;
                    else
                        currentValue = value;
            }
        }

        public InfoPoints(int maxValue): this(maxValue, 0, maxValue)
        {
        }       

        public InfoPoints(int currentValue = 0, int minValue = 0, int maxValue = 0)
        {
            this.currentValue = currentValue;
            this.minValue = minValue;
            this.maxValue = maxValue;
        }
    }
}