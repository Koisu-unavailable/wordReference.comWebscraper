using System;
using System.Runtime.CompilerServices;

namespace wordReferencecomScraper
{
    public class Utils
    {
        [Obsolete("Doesn't work, don't care")]
        public static Dictionary<K, V> RemoveDuplicatesFromDict<K, V>(Dictionary<K, V> dict) where K: notnull where V : notnull // todo: make this word
        {
            V[] known = new V[dict.Values.Count];
            Dictionary<K, V> newdict = new();
            int index = 0;
            foreach (var key in dict.Keys){
                if (known.Contains(dict[key])){
                    continue;
                }
                newdict[key] = dict[key];
                known[index] = dict[key];
            }
            return newdict;
        }
    }
}