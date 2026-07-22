using System;
using System.Collections.Generic;

[Serializable]
public class SerializableDictionary<K, V>
{
    public List<KeyValuePair<K, V>> dictionary = new();

    public Dictionary<K, V> ToDict()
    {
        Dictionary<K, V> dictionary = new();

        foreach(KeyValuePair<K, V> pair in this.dictionary)
        {
            dictionary.Add(pair.key, pair.value);
        }

        return dictionary;
    }
}
