using System;
using System.Linq;

namespace DnDHelper.Domain
{
    [Serializable]
    public class EquipItem
    {
        public ItemPosition Position { get; set; }
        public Item Item { get; set; }
        public string PositionName { get { return EnumsDictionary.ItemPositions.Single(s => s.Value == Position).Name; } }

        public override string ToString()
        {
            return string.Format("{0} : {1}", Position, Item);
        }

        public EquipItem()
        {
        }

        public EquipItem(ItemPosition position, Item item)
        {
            
            this.Position = position;
            this.Item = item;
        }
    }
}