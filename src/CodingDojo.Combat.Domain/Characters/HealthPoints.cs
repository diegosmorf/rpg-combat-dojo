namespace CodingDojo.Combat.Domain.Characters
{
    public class HealthPoints(int currentValue = 0, int minValue = 0, int maxValue = 0)
    {
        protected int currentValue = currentValue;
        public int MinValue { get; protected set; } = minValue;
        public int MaxValue { get; protected set; } = maxValue;

        public int Value
        {
            get { return currentValue; }

            set
            {
                if (value < MinValue)
                    currentValue = MinValue;
                else
                    if (value > MaxValue)
                        currentValue = MaxValue;
                else
                    currentValue = value;
            }
        }

        public HealthPoints(int maxValue) : this(maxValue, 0, maxValue)
        {
        }

        public void Reset()
        {
            Value = MaxValue;
        }

        public void Reset(int maxValue)
        {
            MaxValue = maxValue;
            Reset();
        }
    }
}