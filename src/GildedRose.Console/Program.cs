using System.Collections.Generic;
using GildedRose.Console.Core;
using RuleFramework;
using System.Threading.Tasks;

namespace GildedRose.Console
{
    public class Program
    {
        IList<Item> Items;

        public Item getItem(int index)
        {
            return this.Items[index];
        }

        RuleExecutor<Item, ItemIncrement> ruler;

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program(
                Items: new List<Item>
                {
                    new Plus5DexitryVestItem(10, 20),
                    new AgedBrieItem(2, 0),
                    new ElixirOfTheMongoseItem(5, 7),
                    new SulfurasHandOfRagnarosItem(0, 80),
                    new BackstagePassesToATAFKAL80ETCConcertItem(15, 20),
                    new ConjuredManaCakeItem(3, 6)
                },
                rules: Rules.Defaults
            );
            app.UpdateQuality();

            System.Console.ReadKey();
        }

        public Program(List<Item> Items, List<RuleBase<Item, ItemIncrement>> rules)
        {
            this.Items = Items;
            ruler = new RuleExecutor<Item, ItemIncrement>(
                rules: rules,
                onGetKey: (item) => item.Name 
            );
        }

        public void UpdateQuality()
        {
            Parallel.ForEach(Items, (item) => 
            {
                ItemIncrement itemIncrements = new ItemIncrement()
                {
                    QualityDelta = 0,
                    SellInDelta = 0
                };
                ruler.ExecuteRules(item, itemIncrements);
                item.SellIn += itemIncrements.SellInDelta;
                item.Quality += itemIncrements.QualityDelta;
            });
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
