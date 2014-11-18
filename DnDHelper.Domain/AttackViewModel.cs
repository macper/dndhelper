namespace DnDHelper.Domain
{
    public class AttackViewModel
    {
        public string Damage { get; set; }
        public int ToHit { get; set; }
        public bool Done { get; set; }
        public string Name { get; set; }
        public string MissileName { get; set; }

        public override string ToString()
        {
            return string.Format("Traf: {0}, Obż: {1}, Wyk: {2} [{3}]", ToHit, Damage, Done ? "Tak" : "Nie", Name);
        }
    }
}