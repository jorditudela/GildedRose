using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GildedRose.Console.Core
{
    public class CustomItem : Item
    {
        public CustomItem(string Name, int SellIn, int Quality)
        {
            this.Name = Name;
            this.SellIn = SellIn;
            this.Quality = Quality;
        }
    }

    public class Plus5DexitryVestItem : CustomItem
    {
        public Plus5DexitryVestItem(int SellIn, int Quality) : base(ItemConsts.Plus5DexitryVest, SellIn, Quality) { }
    }

    public class AgedBrieItem : CustomItem
    {
        public AgedBrieItem(int SellIn, int Quality) : base(ItemConsts.AgedBrie, SellIn, Quality) { }
    }

    public class ElixirOfTheMongoseItem : CustomItem
    {
        public ElixirOfTheMongoseItem(int SellIn, int Quality) : base(ItemConsts.ElixirOfTheMongoose, SellIn, Quality) { }
    }

    public class SulfurasHandOfRagnarosItem : CustomItem
    {
        public SulfurasHandOfRagnarosItem(int SellIn, int Quality) : base(ItemConsts.SulfurasHandOfRagnaros, SellIn, Quality) { }
    }

    public class BackstagePassesToATAFKAL80ETCConcertItem : CustomItem
    {
        public BackstagePassesToATAFKAL80ETCConcertItem(int SellIn, int Quality) : base(ItemConsts.BackstagePassesToATAFKAL80ETCConcert, SellIn, Quality) { }
    }

    public class ConjuredManaCakeItem : CustomItem
    {
        public ConjuredManaCakeItem(int SellIn, int Quality) : base(ItemConsts.ConjuredManaCake, SellIn, Quality) { }
    }
}
