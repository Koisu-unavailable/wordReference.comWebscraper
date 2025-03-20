using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Flow.Launcher.Plugin.wordReferencePlugin.webscraping
{
    public class webscraper
    {
        public static Tuple<TranslationResult, Translation> GetTranslation(string word, bool onlyFirstRResult, string toFrom)
        {

            string url = $"https://www.wordreference.com/{toFrom}/{word}";
            Console.WriteLine(url);

            HtmlWeb web = new HtmlWeb();

            HtmlDocument document = web.Load(url);

            using (TextWriter writer = File.CreateText("response.html"))
            {
                document.DocumentNode.WriteTo(writer);
            }

            HtmlNode notFoundElement = document.DocumentNode.SelectSingleNode("//*[@id='container']/div/h1");
            HtmlNode translationNotFoundElement = document.DocumentNode.SelectSingleNode("//*[@id='noEntryFound']");
            if (notFoundElement != null || translationNotFoundElement != null)
            {
                return new Tuple<TranslationResult, Translation>(TranslationResult.GeneralError, Translation.Empty());
            }
            var tables = document.DocumentNode.Descendants(0).Where(n => n.HasClass("WRD"));
            using (TextWriter writer = File.CreateText("response.html"))
            {
                tables.First().WriteTo(writer);
            }

            var tbody = tables.First();




            //*[@id="articleWRD"]/table[1]/tbody
            //*[@id="articleWRD"]/table[1]/tbody
            //*[@id='articleWRD']/table[1]/tbody
            if (tbody == null)
            {

                return new Tuple<TranslationResult, Translation>(TranslationResult.GeneralError, Translation.Empty());
            }
            var translationElements = tbody.ChildNodes;
            Dictionary<string, List<string>> translations = new Dictionary<string, List<string>>();
            bool start = false;
            foreach (HtmlNode translationElement in translationElements)
            {
                // skip first 2 as they're headers

                if (Array.IndexOf([.. translationElements], translationElement) < 2)
                {

                    continue;
                }
                string currentWord = "";
                Console.WriteLine(translationElement.OuterHtml);
                if (translationElement.ChildNodes.Where(n => n.HasClass("ToWrd")).Count() == 0){
                    continue;

                }
                if (translationElement.Id.ToLower().Contains(toFrom))
                {

                    currentWord = translationElement.ChildNodes.Where(n => n.HasClass("FrWrd"))
                    .First()
                    .ChildNodes // IT'S BOLDED IN A <strong> ELEMENT AHHHH
                    .First()
                    .InnerText;

                    string transText = translationElement.ChildNodes.Where(n => n.HasClass("ToWrd")).First().InnerText;
                    Console.WriteLine(transText);
                    try
                    {
                        translations.GetValueOrDefault(word).Add(transText);

                    }
                    catch (NullReferenceException ex)
                    {
                        translations.Add(word, []);
                        Console.WriteLine(transText);
                        translations.GetValueOrDefault(word).Add(transText);
                    }


                }
                else
                {
                    if (translationElement.OuterHtml == "\n")
                    {
                        continue;
                    }
                    Console.WriteLine(translationElement.InnerText);
                    string transText = translationElement.ChildNodes.Last().InnerText;
                    Console.WriteLine(transText);
                    try
                    {
                        translations.GetValueOrDefault(word).Add(transText);

                    }
                    catch (NullReferenceException ex)
                    {
                        translations.Add(word, []);
                        Console.WriteLine(transText);
                        translations.GetValueOrDefault(word).Add(transText);
                    }


                }
            }
            return new Tuple<TranslationResult, Translation>(TranslationResult.Success, new Translation(word, translations));
        }
        // the URL of the target Wikipedia page
        public static void Main(string[] args)
        {
            var result = GetTranslation("pee", true, "enfr");
            if (result.Item1 == TranslationResult.GeneralError)
            {
                Console.WriteLine("jdjdjdjdj");
            }
            foreach (List<string> value in result.Item2.translations.Values)
            {
                foreach (var valued in value)
                {
                    Console.WriteLine("ii" + valued);
                }


            }


        }
    }

    public class Translation
    {
        public readonly string originalWord;
        // key: meaning. value: translation
        public readonly Dictionary<string, List<string>> translations;

        public Translation(string originalWord, Dictionary<string, List<string>> translations)
        {
            this.originalWord = originalWord;
            this.translations = translations;
        }
        public static Translation Empty()
        {
            return new Translation("", []);
        }
    }
    public enum TranslationResult
    {
        Success,
        InvalidLanguage,
        WordNotFound,
        GeneralError,
    }
}
