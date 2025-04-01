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
    [TestMethod]
    public void EnJaTest(){
        Assert.AreEqual("テスト ", new Webscraper().GetTranslation("test", "enja").FirstResult());

    }
    [TestMethod]
    public void EnDetest(){
        Assert.AreEqual("Schularbeit, Klassenarbeit Nf", new Webscraper().GetTranslation("test", "ende").FirstResult());
    }
    
}