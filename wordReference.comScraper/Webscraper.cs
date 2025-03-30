using System.Reflection.Metadata;
using exceptions;
using HtmlAgilityPack;
using Microsoft.VisualBasic;
namespace wordReferencecomScraper;

public class Webscraper
{
    private string url = "";
    private HtmlWeb web;
    private HtmlDocument  test;
    public Webscraper()
    {
        web = new HtmlWeb();
        test = new HtmlDocument();

    }

    private bool wordDoesntExist(HtmlDocument document)
    {
        return document.DocumentNode.SelectSingleNode("//*[@id='noEntryFound']") != null;
    }
    private Dictionary<String, List<String>> TraverseTableForTranslation(HtmlNodeCollection translationElements, string word, string toFrom)
    {
    
        Dictionary<string, List<string>> translations = new Dictionary<string, List<string>>();
        foreach (HtmlNode translationElement in translationElements)
        {
            // skip first 2 as they're headers

            if (Array.IndexOf(translationElements.ToArray(), translationElement) < 2)
            {

                continue;
            }
            if (translationElement.ChildNodes.Where(n => n.HasClass("ToWrd")).Count() == 0)
            {
                // not actually a translation
                continue;
            }

            string currentWord = TranslationGetter.GetCurrentWord(translationElement, toFrom) ?? word;
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
                        translations.Add(word, new List<string>());
                        translations[currentWord].Add(TranslationGetter.MostLanguages(translationElement));
                    }
                    break;

            }
            
        }
        return translations;

    }
    public Translation GetTranslation(string word, string toFrom)
    {

        url = $"https://www.wordreference.com/{toFrom}/{word}";

        HtmlDocument document = web.Load(url);

        HtmlNode notFoundElement = document.DocumentNode.SelectSingleNode("//*[@id='container']/div/h1");

        if (notFoundElement != null)
        {
            throw new InvalidLanguageException($"{toFrom} invalid");
        }
        if (wordDoesntExist(document))
        {
            throw new ArgumentException($"{word} isn't a valid word or wordreference.com doesn't have it yet");
        }

        var tables = document.DocumentNode.Descendants(0).Where(n => n.HasClass("WRD"));
        var tbody = tables.First() ?? throw new GeneralTranslationException($"tbody was null");
        var translationElements = tbody.ChildNodes;

        var translations = TraverseTableForTranslation(translationElements, word, toFrom);

        return new Translation(word, translations);
    }
}