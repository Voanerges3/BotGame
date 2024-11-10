
using System;
using WOT_START_PLUS.Enums;

namespace WOT_START_PLUS.Events
{
    internal sealed class WindowGroupEventArgs : EventArgs
    {
        public WindowGroupType GroupType { get; }

        public WindowGroupEventArgs(WindowGroupType type) => GroupType = type;
    }
}
