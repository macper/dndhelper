namespace DnDHelper.Domain
{
    public interface ICustomSerializable
    {
        void Serialize();
        void Deserialize();
    }
}