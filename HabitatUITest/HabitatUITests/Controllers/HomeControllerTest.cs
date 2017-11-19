using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System.Drawing.Imaging;
using OpenQA.Selenium.Firefox;
using System.Threading;
using Applitools;
using Applitools.Selenium;


namespace HabitatUITests.Controllers
{
    [TestClass]
    public class HomePageUITests
    {
       // public Eyes eyes = new Eyes();
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

            
           // eyes.ApiKey = "l64hbuXc2lxij1vHN0emmKavNATg9Vi7OEuepVC106x48110";
        }
        //    bool exists = System.IO.Directory.Exists(ConfigurationHelper.FolderPicture);

        //    if (!exists)
        //    {
        //        System.IO.Directory.CreateDirectory(ConfigurationHelper.FolderPicture);
        //    }
        //}

        [TestMethod]
        public void UC001_LoadHomePage()
        {
            webDriver.Navigate().GoToUrl(ConfigurationHelper.SiteUrl);
            
            bool result = true;
            try
            {
                //eyes.Open(webDriver, "Habitat", "UC001_LoadHomePage");
                //eyes.CheckWindow("Test1");

                result = (webDriver.Title.ToString().Contains("Habitat Sitecore"));

                //eyes.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
            }
           finally
            {
               
                Assert.AreEqual(true, true);
                CloseBrowser();
                
            }

        }


        [TestMethod]
        public void UC002_LoadRegisterPage()
        {
            webDriver.Navigate().GoToUrl(ConfigurationHelper.RegisterUrl);

            bool result = true;
            try
            {
                //eyes.Open(webDriver, "Habitat", "UC002_HomePageVerifyCarouselCount");
                // eyes.CheckWindow("Test2");
                result = (webDriver.Title.ToString().Contains("Register"));
                //eyes.Close();
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                Assert.AreEqual(true, true);
                CloseBrowser();
            }

        }

        [TestMethod]
        public void UC003_LoadForgotPasswordPage()
        {
            webDriver.Navigate().GoToUrl(ConfigurationHelper.ForgotPasswordUrl);
           
            bool result = true;
            try
            {
                //eyes.Open(webDriver, "Habitat", "UC003_LoadForgotPasswordPage");
               // eyes.CheckWindow("Test3");
                result = (webDriver.Title.ToString().Contains("Forgot Password"));
               // eyes.Close();
            }
            catch(Exception ex)
            {
                result = false;
            }
           finally
            {
                Assert.AreEqual(true, true);
                CloseBrowser();
            }
        }


        //private void SaveScreenshot(Screenshot screenshot, string fileName)
        //{
        //    screenshot.SaveAsFile(string.Format("{0}{1}", ConfigurationHelper.FolderPicture, fileName), ImageFormat.Png);
        //}

        private void CloseBrowser()
        {
            webDriver.Quit();
            //eyes.AbortIfNotClosed();
            Thread.Sleep(2000);
        }
    }
}
