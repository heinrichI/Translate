using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace Resource
{ 
    // When you implement IEnumerable, you must also implement IEnumerator.
    public class DictionaryEnum : IEnumerator<KeyValuePair<string, string>>
    {
        private List<KeyValuePair<string, string>> _list;

        // Enumerators are positioned before the first element
        // until the first MoveNext() call.
        int position = -1;

        public DictionaryEnum(Dictionary<string, System.Resources.ResXDataNode> dict)
        {
            _list = dict.Select(d => new KeyValuePair<string, string>(
                d.Key, 
                d.Value.GetValue((ITypeResolutionService)null).ToString())).ToList();
        }

        public bool MoveNext()
        {
            position++;
            return (position < _list.Count);
        }

        public void Reset()
        {
            position = -1;
        }

        public void Dispose()
        {
        }

        public KeyValuePair<string, string> Current
        {
            get
            {
                try
                {
                    return _list[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        object IEnumerator.Current => Current;
    }
}
