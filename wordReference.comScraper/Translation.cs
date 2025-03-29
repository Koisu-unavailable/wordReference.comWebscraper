

using System.Runtime.ExceptionServices;

namespace wordReferencecomScraper
{
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
            return new Translation("", new Dictionary<string, List<string>>());
        }
        public string FirstResult(){
            return translations.Values.First().First();
        }
    }
}