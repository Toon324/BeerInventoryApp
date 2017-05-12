using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BeerInventoryApp.Data
{

    // http://motzcod.es/post/94643411707/enhancing-xamarinforms-listview-with-grouping
    public class Grouping<K, T> : ObservableCollection<T>
    {
        public K Key { get; private set; }

        public Grouping(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
                this.Items.Add(item);
        }
    }
}
