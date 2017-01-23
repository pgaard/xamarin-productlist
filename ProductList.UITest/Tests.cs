using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace ProductList.UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            this.app = AppInitializer.StartApp(this.platform);
        }

        [Test]
        public void AppLaunches()
        {
            this.app.Screenshot("First screen.");
        }

        [Test]
        public void Repl()
        {
            this.app.Repl();
        }

        [Test]
        public void DoASearch()
        {
            this.app.WaitForElement(c => c.Marked("action_bar_root"));
            this.app.Tap(c => c.Marked("Search")); // depends on text
            this.app.EnterText(c => c.Marked("tst_search_bar"), "hardware");            
            this.app.PressEnter();
            this.app.WaitForElement(c => c.Marked("tst_image_hitcount"));
            this.app.Screenshot("Search Results");
        }
    }
}

