using exceptions;

namespace wordReferencecomScraper
{
    public class LinkGenerator
    {
        public static string GenrateLink(string to, string from, string word){
            // verify arguements
            if (to.Length > 2){
                throw new InvalidLanguageException($"{to} is not a valid language.");
            }
            else if (from.Length > 2){
                throw new InvalidLanguageException($"{from} isn't a valid language");
            }

            // generate links
            // I do believe it is only these ones that are weird. bumass website.
            if (to == "es"){
                return $"https://www.wordreference.com/es/translation.asp?tranword={word}"; // WHY IS THIS DIFFERENT RAHHHHHH
            }
            else if(from == "es" && to == "en"){
                return $"https://www.wordreference.com/es/en/translation.asp?spen={word}";
            }
            else{
                return $"https://www.wordreference.com/{to+from}/{word}";
            }
        }
    }
}