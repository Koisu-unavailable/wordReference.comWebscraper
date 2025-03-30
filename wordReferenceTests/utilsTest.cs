namespace wordReferenceTests;
using wordReferencecomScraper;

[TestClass]
public class UtilsTest{
    [TestMethod]
    public void RemoveDupesTest(){
        Dictionary<string, int> dupes = new()
        {
            { "hi", 1 },
            { "he", 3 },
            { "hehe", 3 }
        };
        Dictionary<string, int> noDupes = new()
        {
            { "hi", 1 },
            { "he", 3 },
        };
        Assert.AreEqual(Utils.RemoveDuplicatesFromDict(dupes), noDupes);
    }
}