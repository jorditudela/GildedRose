using Xunit;
using GildedRose.Console;
using GildedRose.Console.Core;
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
                    new CustomItem("bar", 10, barQ)
                },
                rules: Rules.Defaults
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
                    new CustomItem("bar", barS, 20)
                },
                rules: Rules.Defaults
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
                    new Plus5DexitryVestItem(-10, 20)
                },
                rules: Rules.Defaults
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
                    new AgedBrieItem(-2, 0)
                },
                rules: Rules.Defaults
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
                    new SulfurasHandOfRagnarosItem(-10, 80)
                },
                rules: Rules.Defaults
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
                    new ConjuredManaCakeItem( -3, 6)
                },
                rules: Rules.Defaults
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
                    new Plus5DexitryVestItem(  -10,  0)
                },
                rules: Rules.Defaults
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
                    new AgedBrieItem( -1,  0)
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 0, 0),
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 4, 0),
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 9, 0),
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 11, 0),
                },
                rules: Rules.Defaults
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
                    new ConjuredManaCakeItem( 3, 1)
                },
                rules: Rules.Defaults
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
                    new AgedBrieItem( 5,  10)
                },
                rules: Rules.Defaults
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
                    new Plus5DexitryVestItem(  10,  50)
                },
                rules: Rules.Defaults
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
                    new AgedBrieItem( -1,  5)
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 9, 49)
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 4, 49)
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 9, 49)
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 9, 49)
                },
                rules: Rules.Defaults
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
                    new SulfurasHandOfRagnarosItem( 0,  80)
                },
                rules: Rules.Defaults
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
                    new SulfurasHandOfRagnarosItem( 0,  80)
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 11, 40)
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 9, 40)
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 4, 40)
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( 0, 40)
                },
                rules: Rules.Defaults
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
                    new BackstagePassesToATAFKAL80ETCConcertItem( -1, 40)
                },
                rules: Rules.Defaults
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
                    new ConjuredManaCakeItem( 3, 6)
                },
                rules: Rules.Defaults
            );
            app.UpdateQuality();
            Assert.Equal(4, app.getItem(0).Quality);
        }
        #endregion
    }
}