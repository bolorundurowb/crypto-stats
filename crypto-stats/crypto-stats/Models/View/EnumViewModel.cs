using System;

namespace crypto_stats.Models.View
{
    public class EnumViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EnumViewModel(Enum @enum)
        {
            Id = Convert.ToInt32(@enum);
            Name = @enum.ToString();
        }
    }
}