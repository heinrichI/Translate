using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BusinessLogic
{
    public class DictionarySerializer<TKey, TValue>
    {
        [XmlType(TypeName = "Item")]
        public class Item
        {
            [XmlAttribute("key")]
            public TKey Key;
            [XmlAttribute("value")]
            public TValue Value;
        }

        private XmlSerializer _serializer = new XmlSerializer(typeof(Item[]), new XmlRootAttribute("Dictionary"));

        public Dictionary<TKey, TValue> Deserialize(string filename)
        {
            using (FileStream stream = new FileStream(filename, FileMode.Open))
            using (XmlReader reader = XmlReader.Create(stream))
            {
                Item[] desirialized = (Item[])_serializer.Deserialize(reader);
                var dict = new Dictionary<TKey, TValue>();

                foreach (var item in desirialized)
                {
                    if (dict.ContainsKey(item.Key))
                        throw new Exception($"{filename} already contain key {item.Key} {item.Value}");

                    dict.Add(item.Key, item.Value);
                }

                return dict;
                //return desirialized.ToDictionary(p => p.Key, p => p.Value);
            }
        }

        public void Serialize(string filename, Dictionary<TKey, TValue> dictionary)
        {
            using (var writer = new StreamWriter(filename))
            {
                _serializer.Serialize(writer, dictionary.Select(p => new Item() { Key = p.Key, Value = p.Value }).ToArray());
            }
        }
    }
}
