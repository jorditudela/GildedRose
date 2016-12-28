using Xunit;
using GildedRose.Console;
using System.Collections.Generic;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        [Fact]
        public void TestTheTruth()
        {
            Assert.True(true);
        }

        [Fact]
        public void Decrease_Quality_All_Items()
        {
            int fooQ = 10, barQ = 5;
            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "foo", SellIn = 10, Quality = fooQ},
                    new Item {Name = "bar", SellIn = 10, Quality = barQ}
                }
            };
            app.UpdateQuality();
            Assert.Equal(
                new List<int>() { fooQ - 1, barQ - 1}, 
                new List<int>() { app.Items[0].Quality, app.Items[1].Quality }
            );
        }

        [Fact]
        public void Decrease_SellIn_All_Items()
        {
            int fooS = 10, barS = 4;
            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "foo", SellIn = fooS, Quality = 20},
                    new Item {Name = "bar", SellIn = barS, Quality = 20}
                }
            };
            app.UpdateQuality();
            Assert.Equal(
                new List<int>() { fooS - 1, barS - 1 },
                new List<int>() { app.Items[0].SellIn, app.Items[1].SellIn }
            );
        }

        [Fact]
        public void Decrease_Twice_As_Fast_Once_Passed_Sell_Date()
        {
            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = -10, Quality = 20},
                    new Item {Name = "Aged Brie", SellIn = -2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = -5, Quality = 7},
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -10, Quality = 80},
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = -15,
                            Quality = 20
                        },
                    //new Item {Name = "Conjured Mana Cake", SellIn = -3, Quality = 6}
                }
            };
            app.UpdateQuality();

            Assert.Equal(
                new List<int>() { 20 - 2, 0 + 2, 7 - 2, 80 - 0, 0, /*6 - 4*/ },
                new List<int>() {
                    app.Items[0].Quality,
                    app.Items[1].Quality,
                    app.Items[2].Quality,
                    app.Items[3].Quality,
                    app.Items[4].Quality,
                    //app.Items[5].Quality
                }
            );
        }

        [Fact]
        public void Quality_Is_Never_Negative()
        {
            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = -10, Quality = 0},
                    new Item {Name = "Aged Brie", SellIn = -2, Quality = 0},
                    new Item {Name = "Elixir of the Mongoose", SellIn = -5, Quality = 0},
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = -15,
                            Quality = 0
                        },
                    new Item {Name = "Conjured Mana Cake", SellIn = -3, Quality = 0}
                }
            };
            app.UpdateQuality();
            Assert.Equal(
                new List<int>() { 0, 0 + 2, 0, 0, 0 },
                new List<int>() {
                    app.Items[0].Quality,
                    app.Items[1].Quality,
                    app.Items[2].Quality,
                    app.Items[3].Quality,
                    app.Items[4].Quality
                }
            );
        }

        [Fact]
        public void Aged_Brie_Increases_In_Quality_Getting_Older()
        {
            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "Aged Brie", SellIn = 5, Quality = 10},
                }
            };
            app.UpdateQuality();
            Assert.Equal(
                new List<int>() { 10 + 1},
                new List<int>() {
                    app.Items[0].Quality
                }
            );
        }

        [Fact]
        public void Quality_Is_Never_Over_50()
        {
            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 50},
                    new Item {Name = "Aged Brie", SellIn = -1, Quality = 50},
                    new Item {Name = "Elixir of the Mongoose", SellIn = 10, Quality = 50},
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 9,
                            Quality = 49
                        },
                    //new Item {Name = "Conjured Mana Cake", SellIn = 10, Quality = 50}
                }
            };
            app.UpdateQuality();
            Assert.Equal(
                new List<int>() { 50 - 1, 50 + 0, 50 - 1, 49 + 1/*, 50 - 2*/ },
                new List<int>() {
                    app.Items[0].Quality,
                    app.Items[1].Quality,
                    app.Items[2].Quality,
                    app.Items[3].Quality,
                    //app.Items[4].Quality
                }
            );
        }

        [Fact]
        public void Sulfuras_Never_Sold_Or_Quality_Decreased()
        {
            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                }
            };
            app.UpdateQuality();
            Assert.Equal(
                new List<int>() { 0, 80 },
                new List<int>() {
                    app.Items[0].SellIn,
                    app.Items[0].Quality,
                }
            );
        }

        [Fact]
        public void BackStage_Passes_Quality_Increase_Rate()
        {
            var app = new Program()
            {
                Items = new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 11,
                            Quality = 40
                        },
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 10,
                            Quality = 40
                        },
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 5,
                            Quality = 40
                        },
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = -1,
                            Quality = 40
                        },
                }
            };
            app.UpdateQuality();
            Assert.Equal(
                new List<int>() { 40 + 1 , 40 + 2, 40 +3, 0 },
                new List<int>() {
                    app.Items[0].Quality,
                    app.Items[1].Quality,
                    app.Items[2].Quality,
                    app.Items[3].Quality,
                }
            );
        }
    }
}