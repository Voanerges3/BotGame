

using System.Collections.Generic;
using WOT_START_PLUS.Enums;

namespace WOT_START_PLUS.Models.Configuration
{
    internal sealed class GameWindowGroupModel
    {
        public GroupDataModel FirstWindowGroup { get; set; }
        public GroupDataModel SecondWindowGroup { get; set; }
        public GroupDataModel ThirdWindowGroup { get; set; }
        public GroupDataModel FourthWindowGroup { get; set; }

        public GroupDataModel GetGroup(WindowGroupType windowGroupType)
        {
            switch (windowGroupType)
            {
                case WindowGroupType.FirstWindowGroup:
                    return FirstWindowGroup;
                case WindowGroupType.SecondWindowGroup:
                    return SecondWindowGroup;
                case WindowGroupType.ThirdWindowGroup:
                    return ThirdWindowGroup;
                case WindowGroupType.FourthWindowGroup:
                    return FourthWindowGroup;
                default: return null;
            }
        }
    }
}
