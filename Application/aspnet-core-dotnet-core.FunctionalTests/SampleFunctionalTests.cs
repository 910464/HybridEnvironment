using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace SampleWebApplication.FunctionalTests
{
    [TestClass]
    public class SampleFunctionalTests
    {
        private static TestContext testContext;
        private RemoteWebDriver driver;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            SampleFunctionalTests.testContext = testContext;
        }

        [TestInitialize]
        public void TestInit()
        {
            driver = GetChromeDriver();
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(300);
        }

        [TestCleanup]
        public void TestClean()
        {
            driver.Quit();
        }

        [TestMethod]
        public void SampleFunctionalTest1()
        {
            var webAppUrl = testContext.Properties["webAppUrl"].ToString();

            var startTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var endTimstamp = startTimestamp + 60 * 10;
            while (true)
            {
                try
                {
                    driver.Navigate().GoToUrl(webAppUrl);
                    Assert.AreEqual("Home Page - ASP.NET Core", driver.Title, "Expected title to be 'Home Page - ASP.NET Core'");
                    break;
                }
                catch(Exception e)
                {
                    var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    if(currentTimestamp > endTimstamp)
                    {
                        Console.Write("##vso[task.logissue type=error;]Test SampleFunctionalTest1 failed with error: " + e.ToString());
                        throw;
                    }
                    Thread.Sleep(5000);
                }
            }
        }
        [TestMethod]
        public void SampleFunctionalTest2()
        {
            var webAppUrl = testContext.Properties["webAppUrl"].ToString();

            var startTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var endTimestamp = startTimestamp + 60 * 10;

            while (true)
            {
                try
                {
                    driver.Navigate().GoToUrl(webAppUrl);
                   
                    Assert.AreEqual("Azure DevOps Project has been successfully setup V1", driver.FindElementByCssSelector("div[class='description line-1']").Text, "Expected description line  to be 'Azure DevOps Project has been successfully setup in Production V2.0'");
                    
                    break;
                }
                catch(Exception e)
                {
                    var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    if (currentTimestamp > endTimestamp)
                    {
                        Console.Write("##vso[task.logissue type=error;]Test SampleFunctionalTest1 failed with error: " + e.ToString());
                        throw;
                    }
                    Thread.Sleep(5000);
                }
            }
        }
        [TestMethod]
        public void SampleFunctionalTest3()
        {
            var webAppUrl = testContext.Properties["webAppUrl"].ToString();

            var startTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var endTimestamp = startTimestamp + 60 * 10;

            while (true)
            {
                try
                {
                    driver.Navigate().GoToUrl(webAppUrl);
                    Assert.AreEqual("Your ASP.NET Core app is up and running on Azure V1", driver.FindElementByCssSelector("div[class='description line-2']").Text, "Expected description line  to be 'Your ASP.NET Core app is up and running on Azure V1'");
                    break;
                }
                catch(Exception e)
                {
                    var currentTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    if (currentTimestamp > endTimestamp)
                    {
                        Console.Write("##vso[task.logissue type=error;]Test SampleFunctionalTest1 failed with error: " + e.ToString());
                        throw;
                    }
                    Thread.Sleep(5000);
                }
            }
        }

        private RemoteWebDriver GetChromeDriver()
        {
            var path = Environment.GetEnvironmentVariable("ChromeWebDriver");
            var options = new ChromeOptions();
            options.AddArguments("--no-sandbox");

            if (!string.IsNullOrWhiteSpace(path))
            {
                return new ChromeDriver(path, options, TimeSpan.FromSeconds(300));
            }
            else
            {
                return new ChromeDriver(options);
            }
        }
    }
}