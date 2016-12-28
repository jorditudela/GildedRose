using System.Collections.Generic;

namespace GildedRose.Console
{
    public class Program
    {
        public IList<Item> Items;

        RuleExecutor<Item, ItemIncrement> ruler = new RuleExecutor<Item, ItemIncrement>();

        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
                          {
                              Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

                          };


            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public Program()
        {
            ruler.Add(new RuleBase<Item, ItemIncrement>()
            {
                Name = "At the end of each day our system lowers both values for every item",
                Pattern = @".*",
                OnExecRule = (x, args) =>
                {
                    args.QualityDelta = -1;
                    args.SellInDelta = -1;
                },
                Order = 10 
            });

            ruler.Add(new RuleBase<Item, ItemIncrement>()
            {
                Name = "Once the sell by date has passed, Quality degrades twice as fast",
                Pattern = @".*",
                OnExecRule = (x, args) =>
                {
                    if ((x.SellIn - args.SellInDelta) < 0)
                        args.QualityDelta = 2 * args.QualityDelta;
                },
                Order = 20,
            });

            ruler.Add(new RuleBase<Item, ItemIncrement>()
            {
                Name = "The Quality of an item is never negative",
                Pattern = @".*",
                OnExecRule = (x, args) =>
                {
                    if ((x.Quality + args.QualityDelta) < 0)
                        args.QualityDelta = - args.QualityDelta;
                },
                Order = 60
            });

            ruler.Add(new RuleBase<Item, ItemIncrement>()
            {
                Name = "\"Aged Brie\" actually increases in Quality the older it gets",
                Pattern = @"Aged Brie",
                OnExecRule = (x, args) =>
                {
                    args.QualityDelta = 1;
                },
                Order = 11
            });

            ruler.Add(new RuleBase<Item, ItemIncrement>()
            {
                Name = "The Quality of an item is never more than 50",
                Pattern = @".*",
                OnExecRule = (x, args) =>
                {
                    if ((x.Quality + args.QualityDelta) > 50)
                        args.QualityDelta = 50 - x.Quality;
                },
                Order = 61
            });

            ruler.Add(new RuleBase<Item, ItemIncrement>()
            {
                Name = "\"Sulfuras\", being a legendary item, never has to be sold or decreases in Quality",
                Pattern = @"Sulfuras.*",
                OnExecRule = (x, args) =>
                {
                    // No changes at all
                    args.QualityDelta = 0;
                    args.SellInDelta = 0;
                },
                Order = 1,
                StopExecution = true
            });

            ruler.Add(new RuleBase<Item, ItemIncrement>()
            {
                Name = "\"Backstage passes\", like aged brie, increases in Quality as it's SellIn " +
                "value approaches; Quality increases by 2 when there are 10 days or less and " + 
                "by 3 when there are 5 days or less " + 
                "but Quality drops to 0 after the concert",
                Pattern = @"Backstage passes.*",
                OnExecRule = (x, args) =>
                {
                    var targetSellIn = x.SellIn;
                    if (targetSellIn < 0)
                        args.QualityDelta = - x.Quality;
                    else if (targetSellIn <= 5)
                        args.QualityDelta = 3;
                    else if (targetSellIn <= 10)
                        args.QualityDelta = 2;
                    else
                        args.QualityDelta = 1;
                },
                Order = 21,
            });

        }

        public void UpdateQuality()
        {
            foreach (Item item in Items) {
                ItemIncrement itemIncrements = new ItemIncrement()
                {
                    QualityDelta = 0,
                    SellInDelta = 0
                };
                ruler.ExecuteRules(item, itemIncrements);
                item.SellIn += itemIncrements.SellInDelta;
                item.Quality += itemIncrements.QualityDelta;
            };
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
