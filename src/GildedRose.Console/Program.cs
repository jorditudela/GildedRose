﻿using System.Collections.Generic;

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
                },
                rules: GildedRoseRules.Defaults
            );
            app.UpdateQuality();

            System.Console.ReadKey();
        }

        public Program(List<Item> Items, List<RuleBase<Item, ItemIncrement>> rules)
        {
            this.Items = Items;
            ruler = new RuleExecutor<Item, ItemIncrement>(
                rules: rules
            );
        }

        public void UpdateQuality()
        {
            foreach (Item item in Items)
            {
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
