using wordReferenceExceptions;
using HtmlAgilityPack;
namespace wordReferencecomScraper;

public class Webscraper
{
    private string url = "";
    private readonly HtmlWeb web;
    public Webscraper()
    {
        web = new HtmlWeb();
    }

    private bool WordDoesntExist(HtmlDocument document)
    {
        return document.DocumentNode.SelectSingleNode("//*[@id='noEntryFound']") != null;
    }
    private static string? GetFirstUntranslatedWord(HtmlNodeCollection translationElements, string toFrom)
    {
        foreach (HtmlNode translationElement in translationElements)
        {
            try{
                return TranslationGetter.GetCurrentWord(translationElement, toFrom);
            }
            catch (NullReferenceException){
                continue;
            }
            
        }
        return null; // pls don't happen
    }
    private Dictionary<string, List<string>> TraverseTableForTranslation(HtmlNodeCollection translationElements, string word, string toFrom)
    {

        Dictionary<string, List<string>> translations = new();
        string currentWord = GetFirstUntranslatedWord(translationElements, toFrom) ?? throw new NullReferenceException("Current word was null"); // shouldn't happen
        foreach (HtmlNode translationElement in translationElements)
        {
            // skip first 2 as they're headers

            if (Array.IndexOf(translationElements.ToArray(), translationElement) < 2)
            {

                continue;
            }
            if (!translationElement.ChildNodes.Where(n => n.HasClass("ToWrd")).Any())
            {
                // not actually a translation
                continue;
            }
            try{
                currentWord = TranslationGetter.GetCurrentWord(translationElement, toFrom);
            }
            catch(NullReferenceException){
                // do nothing
            }
            switch (toFrom[2..4])
            {
                case "ja":
                    try
                    {
                        TranslationGetter.Japanese(translationElement).ToList().ForEach(translatedWord => translations[currentWord].Add(translatedWord));

                    }
                    catch (KeyNotFoundException ex)
                    {
                        translations.Add(currentWord, new List<string>());
                        TranslationGetter.Japanese(translationElement).ToList().ForEach(translatedWord => translations[currentWord].Add(translatedWord));
                    }
                    continue;
                default:
                    try
                    {
                        translations[currentWord].Add(TranslationGetter.MostLanguages(translationElement));

                    }
                    catch (KeyNotFoundException ex)
                    {
                        translations.Add(currentWord, new List<string>());
                        translations[currentWord].Add(TranslationGetter.MostLanguages(translationElement));
                    }
                    break;

            }

        }
        return translations;

    }
    public string Define(){
        throw new NotImplementedException("We currently don't suppot retrieving the definitions of words left.");
    }


    public Translation GetTranslation(string word, string toFrom)
    {
        if (word == null || word == "")
        {
            throw new ArgumentNullException(nameof(word));
        }
        if (toFrom == null || toFrom == "")
        {
            throw new ArgumentNullException(nameof(toFrom));
        }
        if (toFrom[2..4] == toFrom[0..2]){
            throw new ArgumentException("Cannot translate language to itself. Did you want to define?");
        }
        url = LinkGenerator.GenrateLink(toFrom[0..2], toFrom[2..4], word);

        HtmlDocument document = web.Load(url);

        HtmlNode notFoundElement = document.DocumentNode.SelectSingleNode("//*[@id='container']/div/h1");

        if (notFoundElement != null)
        {
            throw new InvalidLanguageException($"{toFrom} invalid");
        }
        if (WordDoesntExist(document))
        {
            throw new ArgumentException($"{word} isn't a valid word or wordreference.com doesn't have it yet");
        }

        var tables = document.DocumentNode.Descendants(0).Where(n => n.HasClass("WRD"));
        if (!tables.Any())
        {
            throw new GeneralTranslationException($"tbody was null");
        }
        var tbody = tables.First();
        var translationElements = tbody.ChildNodes;

        var translations = TraverseTableForTranslation(translationElements, word, toFrom);

        return new Translation(word, translations);
    }
}