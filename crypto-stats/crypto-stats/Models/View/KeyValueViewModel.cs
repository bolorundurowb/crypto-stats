namespace crypto_stats.Models.View
{
    public class KeyValueViewModel
    {
        public KeyValueViewModel(int value, string name)
        {
            Value = value;
            Name = name;
        }

        public int Value { get; set; }

        public string Name { get; set; }

        internal bool IsInvalid() => Value < 0;
    }
}
