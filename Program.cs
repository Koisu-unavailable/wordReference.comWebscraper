using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Flow.Launcher.Plugin.wordReferencePlugin.webscraping
{
    public class webscraper
    {
        public static Tuple<TranslationResult, Translation> GetTranslation(string word, bool onlyFirstRResult, string toFrom){

        string url = $"https://www.wordreference.com/{toFrom}/{word}";

        HtmlWeb web = new HtmlWeb();
        
        HtmlDocument document = web.Load(url);
        HtmlNode notFoundElement = document.DocumentNode.SelectSingleNode("//*[@id='container']/div/h1");
        HtmlNode translationNotFoundElement = document.DocumentNode.SelectSingleNode("//*[@id='noEntryFound']");
        if (notFoundElement == null || translationNotFoundElement == null){
            return null;
        }
        HtmlNodeCollection translationElements = document.DocumentNode.SelectNodes($"//*[@id='articleWRD']/table[1]/tbody");
        Dictionary<string, List<string>> translations = new Dictionary<string, List<string>>();
        foreach (HtmlNode translationElement in translationElements){
            // skip first 2 as they're headers
            if (Array.IndexOf(translationElements.ToArray(), translationElement) == 0 || 
            Array.IndexOf(translationElements.ToArray(), translationElement) == 1 ){
                continue;
            }


            List<string> translationList = new List<string>();
            string currentWord = "";
            foreach (HtmlNode node in translationElement.ChildNodes){
                if (node.Id.ToLower().Contains(toFrom)){
                    translationList = [];
                    currentWord = node.QuerySelector("//*td[1]").ChildNodes[0].InnerText; // it's bolded for some reason AHHHHHHH
                    translationList.Add(node.QuerySelector("//*/td[3]").InnerText);
                }
            }
        }
        return new Tuple<TranslationResult, Translation>(TranslationResult.Success, new Translation("word", null));
        }
        // the URL of the target Wikipedia page
    public static void Main(string[] args){
        GetTranslation("js", true, "hjkdfhjkfdhjkdf");
    }
    }

    public record Translation
    {
        readonly string  originalWord;
        // key: meaning. value: translation
        readonly Dictionary<string, List<string>> translations; 
        
        public Translation(string originalWord, Dictionary<string, List<string>> translations){
            this.originalWord = originalWord;
            this.translations = translations;
        }
    }
    public enum TranslationResult{
        Success,
        InvalidLanguage,
        WordNotFound,
    }
}
