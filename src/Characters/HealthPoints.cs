namespace CodingDojo.Combat.Characters
{
    public class HealthPoints(int currentValue = 0, int minValue = 0, int maxValue = 0)
    {
        protected int currentValue = currentValue;
        private readonly int minValue = minValue;
        private readonly int maxValue = maxValue;

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

        public HealthPoints(int maxValue) : this(maxValue, 0, maxValue)
        {
        }
    }
}