using System.Collections.Generic;
using RuleFramework;
using GildedRose.Console.Core;

namespace GildedRose.Console
{
    public static class Rules
    {
        public static List<RuleBase<Item, ItemIncrement>> Defaults = new List<RuleBase<Item, ItemIncrement>>()
        {
            new RuleBase<Item, ItemIncrement>()
            {
                Name = "At the end of each day our system lowers both values for every item",
                OnExecRule = (x, args) =>
                {
                    args.QualityDelta = -1;
                    args.SellInDelta = -1;
                },
                Order = 10
            },

            new RuleBase<Item, ItemIncrement>()
            {
                Name = "Once the sell by date has passed, Quality degrades twice as fast",
                OnExecRule = (x, args) =>
                {
                    if ((x.SellIn - args.SellInDelta) < 0)
                        args.QualityDelta = 2 * args.QualityDelta;
                },
                Order = 20,
            },

            new RuleBase<Item, ItemIncrement>()
            {
                Name = "The Quality of an item is never negative",
                OnExecRule = (x, args) =>
                {
                    if ((x.Quality + args.QualityDelta) < 0)
                        args.QualityDelta = - args.QualityDelta;
                },
                Order = 60
            },

            new RuleBase<Item, ItemIncrement>()
            {
                Name = "\"Aged Brie\" actually increases in Quality the older it gets",
                Pattern = ItemConsts.AgedBrie,
                OnExecRule = (x, args) =>
                {
                    args.QualityDelta = 1;
                },
                Order = 11
            },

            new RuleBase<Item, ItemIncrement>()
            {
                Name = "The Quality of an item is never more than 50",
                OnExecRule = (x, args) =>
                {
                    if ((x.Quality + args.QualityDelta) > 50)
                        args.QualityDelta = 50 - x.Quality;
                },
                Order = 61
            },

            new RuleBase<Item, ItemIncrement>()
            {
                Name = "\"Sulfuras\", being a legendary item, never has to be sold or decreases in Quality",
                Pattern = ItemConsts.SulfurasHandOfRagnaros,
                OnExecRule = (x, args) =>
                {
                    // No changes at all
                    args.QualityDelta = 0;
                    args.SellInDelta = 0;
                },
                Order = 1,
                StopExecution = true
            },

            new RuleBase<Item, ItemIncrement>()
            {
                Name = "\"Backstage passes\", like aged brie, increases in Quality as it's SellIn " +
                "value approaches; Quality increases by 2 when there are 10 days or less and " +
                "by 3 when there are 5 days or less " +
                "but Quality drops to 0 after the concert",
                Pattern = ItemConsts.BackstagePassesToATAFKAL80ETCConcert,
                OnExecRule = (x, args) =>
                {
                    var targetSellIn = x.SellIn;
                    if (targetSellIn< 0)
                        args.QualityDelta = - x.Quality;
                    else if (targetSellIn <= 5)
                        args.QualityDelta = 3;
                    else if (targetSellIn <= 10)
                        args.QualityDelta = 2;
                    else
                        args.QualityDelta = 1;
                },
                Order = 21,
            },

            new RuleBase<Item, ItemIncrement>()
            {
                Name = "\"Conjured\" items degrade in Quality twice as fast as normal items",
                Pattern = ItemConsts.ConjuredManaCake,
                OnExecRule = (x, args) =>
                {
                    args.QualityDelta = args.QualityDelta* 2;
                },
                Order = 15
            }
        };
    }
}
