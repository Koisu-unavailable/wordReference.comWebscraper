

namespace wordReference.comScraper
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
            return new Translation("", []);
        }
    }
}