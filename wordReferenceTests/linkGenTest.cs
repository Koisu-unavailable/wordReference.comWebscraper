using wordReferencecomScraper;

namespace wordReferenceTests;

[TestClass]
public class LinkGenTest
{
    [TestMethod]
    public void LinkGenJa()
    {
        Assert.AreEqual("https://www.wordreference.com/enja/test", LinkGenerator.GenrateLink("en", "ja", "test"));
    }
    [TestMethod]
    public void LinkGenFr(){
        Assert.AreEqual("https://www.wordreference.com/enfr/test", LinkGenerator.GenrateLink("en", "fr", "test"));
    }
}