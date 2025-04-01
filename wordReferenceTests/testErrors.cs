namespace wordReferenceTests;
using wordReferencecomScraper;
[TestClass]
public class testErrors
{
    [TestMethod]
    public void TestLangNotFound()
    {
        Assert.ThrowsException<wordReferenceExceptions.InvalidLanguageException>(() => new Webscraper().GetTranslation("e", "pepepe"));
    }
    [TestMethod]
    public void TestWordNotFound()
    {
        Assert.ThrowsException<ArgumentException>(() => new Webscraper().GetTranslation("jkfvjkfvjh", "enfr"));
    }
    [TestMethod]
    public void ArgNull(){
        Assert.ThrowsException<ArgumentNullException>(() => new Webscraper().GetTranslation(null, null));
        Assert.ThrowsException<ArgumentNullException>(() => new Webscraper().GetTranslation(null, ""));
        Assert.ThrowsException<ArgumentNullException>(() => new Webscraper().GetTranslation("", ""));
        Assert.ThrowsException<ArgumentNullException>(() => new Webscraper().GetTranslation("", null));
    }
}