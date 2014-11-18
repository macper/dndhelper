namespace DnDHelper.Domain
{
    public class Setting : BaseEntity
    {
        public object Value { get; set; }

        public Setting()
        {
        }

        public Setting(string name, object value)
        {
            Name = name;
            Value = value;
        }
    }
}