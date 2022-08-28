using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Threading;
using TechTalk.SpecFlow;

namespace FinBY.WebTests
{
    [Binding]
    public class ManageTransactionsDefinitions : IDisposable
    {
        private ChromeDriver chromeDriver;
        public ManageTransactionsDefinitions() => chromeDriver = new ChromeDriver();

        [Given(@"I have navigated to FinBy website")]
        public void GivenIHaveNavigatedToFinByWebsite()
        {
            chromeDriver.Navigate().GoToUrl("https://localhost:3000/");
            chromeDriver.Manage().Window.Maximize();
            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(12));
            wait.Until(ExpectedConditions.ElementExists(By.ClassName("container")));
            Assert.IsTrue(chromeDriver.Title.ToLower().Contains("finby"));
        }

        [When(@"I press the transactions menu option")]
        public void WhenIPressTheTransactionsMenuOption()
        {
            var toggle = chromeDriver.FindElement(By.Id("toggle"));
            if (toggle.Enabled && toggle.Displayed)
                toggle.Click();

            var menuOption = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(12)).Until(ExpectedConditions.ElementToBeClickable(By.Id("menuTransaction")));
            menuOption.Click();

            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(1));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("transactionsTable")));
        }

        [Then(@"I should view the list of transactions")]
        public void ThenIShouldViewTheListOfTransactions()
        {
            var transactionsTable = chromeDriver.FindElement(By.Id("transactionsTable"));
            Assert.IsNotNull(transactionsTable);
        }


        [Given(@"I press the transactions menu option")]
        public void GivenIPressTheTransactionsMenuOption()
        {
            var toggle = chromeDriver.FindElement(By.Id("toggle"));
            if (toggle.Enabled && toggle.Displayed)
                toggle.Click();

            var menuOption = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(12)).Until(ExpectedConditions.ElementToBeClickable(By.Id("menuTransaction")));
            menuOption.Click();

            var wait = new WebDriverWait(chromeDriver, TimeSpan.FromSeconds(1));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("transactionsTable")));
        }

        [Given(@"I press the new transaction button")]
        public void GivenIPressTheNewTransactionButton()
        {
            var newTransactionButton = chromeDriver.FindElement(By.Id("newTransactionButton"));
            newTransactionButton.Click();
        }

        [Given(@"fill the transaction info")]
        public void GivenFillTheTransactionInfo()
        {
            chromeDriver.FindElement(By.Id("shortDescription")).SendKeys("New Test Transaction");
            chromeDriver.FindElement(By.Id("description")).SendKeys("Description for the the new Test Transaction for");

            var typePicker  = chromeDriver.FindElement(By.Id("transactionTypeId"));
            SelectElement select = new SelectElement(typePicker);
            select.SelectByText("Casa");

            var addNewTransactionBtn = chromeDriver.FindElement(By.Id("addNewTransactionBtn"));
            addNewTransactionBtn.Click();

            var userId = chromeDriver.FindElement(By.Id("userId"));
            SelectElement selectUser = new SelectElement(userId);
            selectUser.SelectByIndex(1);
            chromeDriver.FindElement(By.Name("amount")).SendKeys("10");
        }

        [When(@"I click on the new transaction button")]
        public void WhenIClickOnTheNewTransactionButton()
        {
            var saveNewTransactionBtn = chromeDriver.FindElement(By.Id("saveNewTransactionBtn"));
            saveNewTransactionBtn.Click();            
        }

        [Then(@"a new transaction with the inputed data should be created")]
        public void ThenANewTransactionWithTheInputedDataShouldBeCreated()
        {
            chromeDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1); //just an example of the use of impicit wait

            var rowElements = chromeDriver.FindElements(By.XPath("//*[@id='transactionsTable']/tbody/tr[td = 'New Test Transaction']/td"));

            rowElements.Should().Contain(x => x.Text == "New Test Transaction");
            rowElements.Should().Contain(x => x.Text == "Casa");
            rowElements.Should().Contain(x => x.Text == "$10.00");
        }

        public void Dispose()
        {
            if (chromeDriver != null)
            {
                chromeDriver.Dispose();
                chromeDriver = null;
            }
        }
    }
}
