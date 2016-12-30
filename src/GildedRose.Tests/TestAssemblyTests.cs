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

        #region Derease Quality
        [Fact]
        public void Decrease_Quality()
        {
            int barQ = 5;
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "bar", SellIn = 10, Quality = barQ}
                }
        );
            app.UpdateQuality();
            Assert.Equal(barQ - 1, app.getItem(0).Quality);
        }
        #endregion

        #region Decrese SellIn
        [Fact]
        public void Decrease_SellIn_All_Items()
        {
            int barS = 4;
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "bar", SellIn = barS, Quality = 20}
                }
            );
            app.UpdateQuality();
            Assert.Equal(barS - 1, app.getItem(0).SellIn);
        }
        #endregion

        #region Decrease Twice as Fast Once Passed Sell Date
        [Fact]
        public void Decrease_Twice_As_Fast_Once_Passed_Sell_Date_Regular_Item()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = -10, Quality = 20}
                }
            );
            app.UpdateQuality();
            Assert.Equal(20 - 2, app.getItem(0).Quality);
        }

        [Fact]
        public void Decrease_Twice_As_Fast_Once_Passed_Sell_Date_Aged_Brie()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "Aged Brie", SellIn = -2, Quality = 0}
                }
            );
            app.UpdateQuality();
            Assert.Equal(0 + 2, app.getItem(0).Quality);
        }

        [Fact]
        public void Decrease_Twice_As_Fast_Once_Passed_Sell_Date_Sulfuras_Legacy_Item()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = -10, Quality = 80},
                }
            );
            app.UpdateQuality();

            Assert.Equal(80 - 0, app.getItem(0).Quality);
        }

        [Fact]
        public void Decrease_Twice_As_Fast_Once_Passed_Sell_Date_Conjured_Item()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "Conjured Mana Cake", SellIn = -3, Quality = 6}
                }
            );
            app.UpdateQuality();
            Assert.Equal(6 - 4, app.getItem(0).Quality);
        }

        #endregion

        #region Quality is never negative
        [Fact]
        public void Quality_Is_Never_Negative_Regular_Item()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = -10, Quality = 0}
                }
            );
            app.UpdateQuality();
            Assert.True(app.getItem(0).Quality >= 0);
        }

        [Fact]
        public void Quality_Is_Never_Negative_AgedBrie()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "Aged Brie", SellIn = -1, Quality = 0},
                }
            );
            app.UpdateQuality();
            Assert.True(app.getItem(0).Quality >= 0);
        }

        [Fact]
        public void Quality_Is_Never_Negative_BackStage_SellIn_as_0()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 0,
                            Quality = 0
                        },
                }
            );
            app.UpdateQuality();
            Assert.True(app.getItem(0).Quality >= 0);
        }

        [Fact]
        public void Quality_Is_Never_Negative_BackStage_SellIn_as_4()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 4,
                            Quality = 0
                        },
                }
            );
            app.UpdateQuality();
            Assert.True(app.getItem(0).Quality >= 0);
        }

        [Fact]
        public void Quality_Is_Never_Negative_BackStage_SellIn_as_9()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 9,
                            Quality = 0
                        },
                }
            );
            app.UpdateQuality();
            Assert.True(app.getItem(0).Quality >= 0);
        }

        [Fact]
        public void Quality_Is_Never_Negative_BackStage_SellIn_as_11()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 11,
                            Quality = 0
                        },
                }
            );
            app.UpdateQuality();
            Assert.True(app.getItem(0).Quality >= 0);
        }

        [Fact]
        public void Quality_Is_Never_Negative_Conjured_Items()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 1}
                }
            );
            app.UpdateQuality();
            Assert.True(app.getItem(0).Quality >= 0);
        }

        #endregion

        #region Aged Brie Increases quality as it gets older
        [Fact]
        public void Aged_Brie_Increases_In_Quality_Getting_Older()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "Aged Brie", SellIn = 5, Quality = 10},
                }
            );
            app.UpdateQuality();
            Assert.Equal(10 + 1, app.getItem(0).Quality);
        }
        #endregion

        #region Quality is never over 50
        [Fact]
        public void Quality_Is_Never_Over_50_Regular_Item()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 50},
                }
            );
            app.UpdateQuality();
            Assert.False(app.getItem(0).Quality > 50);
        }

        [Fact]
        public void Quality_Is_Never_Over_50_Aged_Brie()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "Aged Brie", SellIn = -1, Quality = 50}
                }
            );
            app.UpdateQuality();
            Assert.False(app.getItem(0).Quality > 50);
        }

        [Fact]
        public void Quality_Is_Never_Over_50_BackStage_SellIn_as_0()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 9,
                            Quality = 49
                        }
                }
            );
            app.UpdateQuality();
            Assert.False(app.getItem(0).Quality > 50);
        }

        [Fact]
        public void Quality_Is_Never_Over_50_BackStage_SellIn_as_4()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 4,
                            Quality = 49
                        }
                }
            );
            app.UpdateQuality();
            Assert.False(app.getItem(0).Quality > 50);
        }

        [Fact]
        public void Quality_Is_Never_Over_50_BackStage_SellIn_as_9()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 9,
                            Quality = 49
                        }
                }
            );
            app.UpdateQuality();
            Assert.False(app.getItem(0).Quality > 50);
        }

        [Fact]
        public void Quality_Is_Never_Over_50_BackStage_SellIn_as_11()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 9,
                            Quality = 49
                        }
                }
            );
            app.UpdateQuality();
            Assert.False(app.getItem(0).Quality > 50);
        }

        #endregion

        #region Sulfuras never sold or quality decreased
        [Fact]
        public void Sulfuras_Never_Sold_Or_Quality_Decreased()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                }
            );
            app.UpdateQuality();
            Assert.Equal(0, app.getItem(0).SellIn);
        }

        [Fact]
        public void Sulfuras_Never_Quality_Decreased()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                }
            );
            app.UpdateQuality();
            Assert.Equal(80, app.getItem(0).Quality);
        }
        #endregion

        #region BackStage Quality Increase Rates
        [Fact]
        public void BackStage_Passes_Quality_Increase_Rate_SellIn_as_11()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 11,
                            Quality = 40
                        }
                }
            );
            app.UpdateQuality();
            Assert.Equal(40 + 1, app.getItem(0).Quality);
        }

        [Fact]
        public void BackStage_Passes_Quality_Increase_Rate_SellIn_as_9()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 9,
                            Quality = 40
                        }
                }
            );
            app.UpdateQuality();
            Assert.Equal(40 + 2, app.getItem(0).Quality);
        }

        [Fact]
        public void BackStage_Passes_Quality_Increase_Rate_SellIn_as_4()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 4,
                            Quality = 40
                        }
                }
            );
            app.UpdateQuality();
            Assert.Equal(40 + 3, app.getItem(0).Quality);
        }

        [Fact]
        public void BackStage_Passes_Quality_Increase_Rate_SellIn_as_0()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = 0,
                            Quality = 40
                        }
                }
            );
            app.UpdateQuality();
            Assert.Equal(40 + 3, app.getItem(0).Quality);
        }

        [Fact]
        public void BackStage_Passes_Quality_Increase_Rate_Selling_date_is_passed()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item
                        {
                            Name = "Backstage passes to a TAFKAL80ETC concert",
                            SellIn = -1,
                            Quality = 40
                        }
                }
            );
            app.UpdateQuality();
            Assert.Equal(0, app.getItem(0).Quality);
        }
        #endregion

        #region "Conjured" items degrade in Quality twice as fast as normal items
        [Fact]
        public void Conjured_items_degrade_Quality_twice_fast()
        {
            var app = new Program(
                Items: new List<Item>
                {
                    new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                }
            );
            app.UpdateQuality();
            Assert.Equal(4, app.getItem(0).Quality);
        }
        #endregion
    }
}