using System;
using System.Collections.Generic;
using System.Text;

namespace GoldenLion.Entity
{
    class SelectableItemWrapper<T>
    {
        public T Item { get; set; }
        public bool IsSelected { get; set; }
    }
}
