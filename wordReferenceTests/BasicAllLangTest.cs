using wordReferencecomScraper;

namespace wordReferenceTests;

[TestClass]
public class BasicAllLangTest
{
    [TestMethod]
    public void EnFrTest()
    {
        Assert.AreEqual("interro nf", new Webscraper().GetTranslation("test", "enfr").FirstResult());
    }
    [TestMethod]
    public void FrEnTest(){
        Assert.AreEqual("bell n", new Webscraper().GetTranslation("cloche", "fren").FirstResult());
    }
    
}