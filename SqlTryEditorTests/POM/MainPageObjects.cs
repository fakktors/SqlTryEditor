using OpenQA.Selenium;
using System.Linq;

namespace SqlTryEditorTests.POM
{
    class MainPageObjects
    {
        private IWebDriver _webDriver;

        public MainPageObjects(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public string GetAddressOfContactName(IWebElement contactNameValue)
        {
            var addressValue = contactNameValue.FindElement(By.XPath("following-sibling::td[1]")).Text;
            return addressValue;
        }
        public string GetTextForTableValue(string contactNameValue)
        {
            var valuesOfContactNamesColumn = _webDriver.FindElements(By.CssSelector("tbody td:nth-child(3)"));
            var addresses = valuesOfContactNamesColumn
                .Where(contactName => contactName.Text
                .Contains(contactNameValue))
                .Select(contactName => GetAddressOfContactName(contactName))
                .ToArray();
            return addresses.Single();
        }
    }
}