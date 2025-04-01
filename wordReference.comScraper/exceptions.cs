namespace wordReferenceExceptions
{

    [Serializable]
    public class InvalidLanguageException : Exception
    {
        public InvalidLanguageException() { }
        public InvalidLanguageException(string message) : base(message) { }
        public InvalidLanguageException(string message, System.Exception inner) : base(message, inner) { }
        protected InvalidLanguageException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }

    [System.Serializable]
    public class GeneralTranslationException : System.Exception
    {
        public GeneralTranslationException() { }
        public GeneralTranslationException(string message) : base(message) { }
        public GeneralTranslationException(string message, System.Exception inner) : base(message, inner) { }
        protected GeneralTranslationException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}