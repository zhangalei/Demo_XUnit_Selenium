using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

using Xunit;

namespace Demo
{
    public class LoginTests : IDisposable
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        //Constructor to initiate browser instance

        public LoginTests()
        {

            try
            {
                driver = new ChromeDriver();
                //driver = new FirefoxDriver();
                baseURL = "http://opensource.demo.orangehrmlive.com/";
                verificationErrors = new StringBuilder();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while starting chrome..." + e);
            }
        }

        [Fact]
        public void TheLoginTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.Id("txtUsername")).Clear();
            driver.FindElement(By.Id("txtUsername")).SendKeys("Admin");
            driver.FindElement(By.Id("txtPassword")).Clear();
            driver.FindElement(By.Id("txtPassword")).SendKeys("admin");
            driver.FindElement(By.Id("btnLogin")).Click();
            driver.FindElement(By.Id("welcome")).Click();
            //for (int second = 0; ; second++)
            //{
            //    if (second >= 60) Assert.True(false, "timeout");
            //    try
            //    {
            //        if (IsElementPresent(By.LinkText("Logout"))) break;
            //    }
            //    catch (Exception)
            //    { }
            Thread.Sleep(1000);
            //}
            driver.FindElement(By.LinkText("Logout")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
        //Stopping browser instance
        public void Dispose()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception while stopping chrome..." + e);
            }
        }
    }
}