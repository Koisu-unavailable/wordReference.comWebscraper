namespace wordReferenceTests;
using wordReferencecomScraper;
[TestClass]
public class testErrors
{
    [TestMethod]
    public void TestLangNotFound()
    {
        Assert.ThrowsException<exceptions.InvalidLanguageException>(() => new Webscraper().GetTranslation("e", "pepepe"));
    }
    [TestMethod]
    public void TestWordNotFound()
    {
        Assert.ThrowsException<ArgumentException>(() => new Webscraper().GetTranslation("jkfvjkfvjh", "enfr"));
    }
}