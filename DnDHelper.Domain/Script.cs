using System;
using System.Xml.Serialization;

namespace DnDHelper.Domain
{
    public class Script : BaseEntity
    {
        public string TypeName { get; set; }
        [XmlIgnore]
        public Type Type
        {
            get { return Type.GetType(TypeName); } 
            set { TypeName = value.FullName; }
        }
        public string Content { get; set; }

        public ScriptContext ScriptContext { get; set; }

        public string ParamName { get; set; }

        public Script()
        {
        }

        public Script( ScriptContext context )
        {
            ScriptContext = context;
            switch (ScriptContext)
            {
                case Domain.ScriptContext.Atut:
                    Type = typeof( Action<Character, RepositorySet, Atut> );
                    break;

                case Domain.ScriptContext.Race:
                    Type = typeof( Action<Character, RepositorySet, Race> );
                    break;

                case Domain.ScriptContext.Custom:
                    Type = typeof( Action<Character, object> );
                    break;

                case Domain.ScriptContext.Effect:
                    Type = typeof( Action<Character, RepositorySet, Effect> );
                    break;

                case Domain.ScriptContext.Item:
                    Type = typeof( Action<Character, RepositorySet, Item> );
                    break;

                case Domain.ScriptContext.Class:
                    Type = typeof( Action<Character, RepositorySet> );
                    break;
            }
        }
    }

    public enum ScriptContext
    {
        Race,
        Atut,
        Skill,
        Item,
        Custom,
        Effect,
        Class
    }
}