using HtmlAgilityPack;
using wordReferencecomScraper;

namespace wordReference.comScraper
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
        public static string? getCurrentWord(HtmlNode translationElement, string toFrom){
            if (translationElement.Id.ToLower().Contains(toFrom) && translationElement.Id != string.Empty){
                string currentWord = HtmlEntity.DeEntitize(translationElement.ChildNodes.Where(n => n.HasClass("FrWrd"))
                .First()
                .ChildNodes // IT'S BOLDED IN A <strong> ELEMENT AHHHH
                .First()
                .InnerText);
                return currentWord;
            }
            return null;
            
        }
        public static string mostLanguages(HtmlNode translationElement){
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