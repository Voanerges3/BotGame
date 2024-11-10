

using System.Collections.Generic;
using System;

namespace WOT_START_PLUS.Models.Configuration
{
    internal sealed class GameWindowManagerModel
    {
        public Dictionary<int, string> WindowHandles { get; set; } // заменить на SortedDictionary и с отладкой запустить, чтобы посмотреть, какой первый элемент и почему, 
                                                                   // он вместо 1 первым вызывает 12-15 окно

        public List<string> WindowHandlesValue { get; set; }
    }
}
