using wordReferencecomScraper;


Console.OutputEncoding = System.Text.Encoding.UTF8;


var translations =  new Webscraper().GetTranslation("test", "enja").translations;
foreach (string key in translations.Keys){
    Console.WriteLine(key);
    foreach (string value in translations[key]){
        Console.WriteLine(value);
    }
}