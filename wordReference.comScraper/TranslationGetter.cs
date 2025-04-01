using HtmlAgilityPack;
using wordReferencecomScraper;

namespace wordReferencecomScraper
{
    public class TranslationGetter
    {
        public static string[] Japanese(HtmlNode translationElement)
        {
            HtmlNodeCollection children = translationElement.ChildNodes;

            foreach (HtmlNode child in children)
            {

                if (child.HasClass("ToWrd"))
                {
                    // Console.OutputEncoding = System.Text.Encoding.UTF8; 
                    string[] words = HtmlEntity.DeEntitize(child.FirstChild.InnerText).Split("、");
                    return words;
                }

            }
            return Array.Empty<string>();

        }
        public static string GetCurrentWord(HtmlNode translationElement, string toFrom)
        {
            if (translationElement.Id.ToLower().Contains(toFrom) && translationElement.Id != string.Empty)
            {
                string currentWord = HtmlEntity.DeEntitize(translationElement.ChildNodes
                .ToArray()
                [1]
                .InnerText
                );
                return currentWord;
            }
            throw new NullReferenceException("Current word not found");

        }
        public static string MostLanguages(HtmlNode translationElement)
        {
            {

                HtmlNodeCollection children = translationElement.ChildNodes;

                foreach (HtmlNode child in children)
                {

                    if (child.HasClass("ToWrd"))
                    {
                        // Console.OutputEncoding = System.Text.Encoding.UTF8; 
                        string transText = HtmlEntity.DeEntitize(child.InnerText);
                        return transText;
                    }

                }
                return "";



            }
        }
    }
}