using HtmlAgilityPack;
namespace wordReference.comScraper;

public class Webscraper
    {
        private string url = "";
        private HtmlWeb web;
        public Webscraper()
        {
            web = new HtmlWeb();
        }

        private bool wordDoesntExist(HtmlDocument document){
            return document.DocumentNode.SelectSingleNode("//*[@id='noEntryFound']") != null;
        }
        private Dictionary<String, List<String>> TraverseTableForTranslation(HtmlNodeCollection translationElements, string word, string toFrom){
            Dictionary<string, List<string>> translations = new Dictionary<string, List<string>>();
            foreach (HtmlNode translationElement in translationElements)
            {
                // skip first 2 as they're headers

                if (Array.IndexOf([.. translationElements], translationElement) < 2)
                {

                    continue;
                }
                string currentWord = "";
                Console.WriteLine(translationElement.OuterHtml);
                if (translationElement.ChildNodes.Where(n => n.HasClass("ToWrd")).Count() == 0)
                {
                    continue;

                }
                if (translationElement.Id.ToLower().Contains(toFrom))
                {

                    currentWord = HtmlEntity.DeEntitize(translationElement.ChildNodes.Where(n => n.HasClass("FrWrd"))
                    .First()
                    .ChildNodes // IT'S BOLDED IN A <strong> ELEMENT AHHHH
                    .First()
                    .InnerText);

                    string transText = HtmlEntity.DeEntitize(translationElement.ChildNodes.Where(n => n.HasClass("ToWrd")).First().InnerText);
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
                    string transText = HtmlEntity.DeEntitize(translationElement.ChildNodes.Last().InnerText);
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
            return translations;

        }
        public Tuple<TranslationResult, Translation> GetTranslation(string word, string toFrom)
        {

            url = $"https://www.wordreference.com/{toFrom}/{word}";

            HtmlDocument document = web.Load(url);

            HtmlNode notFoundElement = document.DocumentNode.SelectSingleNode("//*[@id='container']/div/h1");

            if (notFoundElement != null)
            {
                return new Tuple<TranslationResult, Translation>(TranslationResult.InvalidLanguage, Translation.Empty());
            }
            if (wordDoesntExist(document)){
                return new Tuple<TranslationResult, Translation>(TranslationResult.WordNotFound, Translation.Empty());
            }
            
            var tables = document.DocumentNode.Descendants(0).Where(n => n.HasClass("WRD"));
            var tbody = tables.First();

            if (tbody == null)
            {
                return new Tuple<TranslationResult, Translation>(TranslationResult.GeneralError, Translation.Empty());
            }

            var translationElements = tbody.ChildNodes;
            
            var translations = TraverseTableForTranslation(translationElements, word, toFrom);

            return new Tuple<TranslationResult, Translation>(TranslationResult.Success, new Translation(word, translations));
        }
    }