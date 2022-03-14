using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using SqlTryEditorTests.POM;
using System.Threading;

namespace SqlTryEditorTests
{
    public class Tests
    {
        private IWebDriver _driver;
        private readonly By _entryField = By.CssSelector(".CodeMirror-code");
        private readonly By _buttonRun = By.CssSelector(".ws-btn");
        private readonly string _sqlCommand = "SELECT * FROM Customers;";

        [SetUp]
        public void Setup()
        {
            _driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            _driver.Navigate().GoToUrl("https://www.w3schools.com/sql/trysql.asp?filename=trysql_asc");
            _driver.Manage().Window.Maximize();
            
        }

        [Test]
        public void GetAddressOfContact_CorrectConnection()
        {
            //»нициализируем экшены и метод
            var mainMenuObjects = new MainPageObjects(_driver);
            Actions actions = new Actions(_driver);
            //ќбъ€влем переменные дл€ поиска селекторов
            var field = _driver.FindElement(_entryField);
            var btn = _driver.FindElement(_buttonRun);
            //ќбъ€влем тексты сравниваемых текстов
            var contactNameValue = "Giovanni Rovelli";
            var addressValue = "Via Ludovico il Moro 22";
            
            actions.KeyDown(field, Keys.Control)
                .SendKeys("a")
                .KeyUp(Keys.Control)
                .SendKeys(Keys.Backspace)
                .SendKeys(_sqlCommand)
                .Click(btn);
            actions.Build().Perform();
            //ќтладка ожидани€, нужно прочитать про ожидани€ подробнее
            Thread.Sleep(200);
            Assert.That(mainMenuObjects.GetTextForTableValue(contactNameValue), Is.EqualTo(addressValue));
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}