using System.Xml.Serialization;
namespace DnDHelper.Domain
{
    public class AppSetting : BaseEntity
    {
        public string Value { get; set; }

        //public override void Serialize()
        //{
        //    if (Name == Const.AppSettings.Time)
        //        Value = ServiceContainer.GetInstance<IGameTimer>().CurrentTime.ToString();
        //}
    }
}