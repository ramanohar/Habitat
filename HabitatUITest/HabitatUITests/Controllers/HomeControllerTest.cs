using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Drawing.Imaging;
using OpenQA.Selenium.Firefox;
using System.Threading;

namespace HabitatUITests.Controllers
{
    [TestClass]
    public class HomePageUITests
    {

        //System.setProperty("webdriver.firefox.marionette","G:\\Selenium\\Firefox driver\\geckodriver.exe");
        private IWebDriver webDriver;

        [TestInitialize]
        public void Setup()
        {
            if (ConfigurationHelper.BrowserType == (int)BrowserType.Firefox)
            {
                webDriver = new FirefoxDriver();
            }
            else
            {
                webDriver = new ChromeDriver(ConfigurationHelper.ChromeDrive);
            }
            bool exists = System.IO.Directory.Exists(ConfigurationHelper.FolderPicture);

            if (!exists)
            {
                System.IO.Directory.CreateDirectory(ConfigurationHelper.FolderPicture);
            }
        }

        [TestMethod]
        public void UC001_LoadHomePage()
        {
            webDriver.Navigate().GoToUrl(ConfigurationHelper.SiteUrl);
            bool result = true;
            try
            {
                result = (webDriver.Title.ToString().Contains("Habitat Sitecore Framework"));
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(result, true);
           CloseBrowser();
        }


        [TestMethod]
        public void UC002_HomePageVerifyCarouselCount()
        {
            webDriver.Navigate().GoToUrl(ConfigurationHelper.SiteUrl);
            bool result = true;
            try
            {
                result = (webDriver.FindElement(By.ClassName("carousel-indicators")).FindElements(By.TagName("li")).Count == 4);
            }
            catch
            {
                result = false;
            }

            Assert.AreEqual(result, true);
            CloseBrowser();
        }

        [TestMethod]
        public void UC003_LoadForgotPasswordPage()
        {
            webDriver.Navigate().GoToUrl(ConfigurationHelper.ForgotPasswordUrl);
            bool result = true;
            try
            {
                result = (webDriver.Title.ToString().Contains("Forgot Password"));
            }
            catch
            {
                result = false;
            }
            Assert.AreEqual(result, true);
            CloseBrowser();
        }


        private void SaveScreenshot(Screenshot screenshot, string fileName)
        {
            screenshot.SaveAsFile(string.Format("{0}{1}", ConfigurationHelper.FolderPicture, fileName), ImageFormat.Png);
        }

        private void CloseBrowser()
        {
            webDriver.Quit();
            Thread.Sleep(1000);
        }
    }
}
