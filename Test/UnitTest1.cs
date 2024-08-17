using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Saucedemo
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver(); //�������� ���������� �������� ������������
        }

        [Test]
        public void Authorization() //����� ����� ����������� � ������� �������
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com");
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user"); 
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();
            Assert.IsTrue(driver.Url.Equals("https://www.saucedemo.com/inventory.html"));
        }
        [Test]
        public void AuthorizationFalse() //����� ����� ����������� � ������������� �������
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com");
            driver.FindElement(By.Id("user-name")).SendKeys("user");
            driver.FindElement(By.Id("password")).SendKeys("user");
            driver.FindElement(By.Id("login-button")).Click();
            Assert.IsFalse(driver.Url.Equals("https://www.saucedemo.com/inventory.html"));
        }

        [Test]
        public void AuthorizationNoData() //����� ����� ����������� ��� ����� ������
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com");
            driver.FindElement(By.Id("login-button")).Click();
            Assert.IsFalse(driver.Url.Equals("https://www.saucedemo.com/inventory.html"));
        }

        [Test]
        public void Add() //����� ����� ���������� ������ � �������
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com");
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light")).Click();
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            IList<IWebElement> cartItems = driver.FindElements(By.ClassName("cart_item"));
            Assert.IsTrue(cartItems.Count == 2);
        }


        [Test]
        public void Edit() //����� ����� �������������� �������
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com");
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light")).Click();
            driver.FindElement(By.ClassName("shopping_cart_link")).Click(); 
            driver.FindElement(By.Id("remove-sauce-labs-backpack")).Click();
            IList<IWebElement> cartItems = driver.FindElements(By.ClassName("cart_item"));
            Assert.IsTrue(cartItems.Count == 1);
        }

        [Test]
        public void Checkout() //����� ����� ���������� ������ 
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com");
            driver.FindElement(By.Id("user-name")).SendKeys("standard_user");
            driver.FindElement(By.Id("password")).SendKeys("secret_sauce");
            driver.FindElement(By.Id("login-button")).Click();

            driver.Navigate().GoToUrl("https://www.saucedemo.com/inventory.html");
            driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack")).Click();
            driver.FindElement(By.ClassName("shopping_cart_link")).Click();
            IList<IWebElement> cartItems = driver.FindElements(By.ClassName("cart_item"));

            driver.FindElement(By.Id("checkout")).Click();
            driver.FindElement(By.Id("first-name")).SendKeys("Lex");
            driver.FindElement(By.Id("last-name")).SendKeys("Dingis");
            driver.FindElement(By.Id("postal-code")).SendKeys("644076"); 

            driver.FindElement(By.Id("continue")).Click(); 
            driver.FindElement(By.Id("finish")).Click();
            Assert.IsTrue(driver.Url.Equals("https://www.saucedemo.com/checkout-complete.html"));
        }
        

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}