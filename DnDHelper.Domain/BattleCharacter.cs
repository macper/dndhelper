using System;

namespace DnDHelper.Domain
{
    public class BattleCharacter : IComparable<BattleCharacter>
    {
        public Character Character { get; set; }
        public bool IsClone { get; set; }
        public string GroupName { get; set; }
        public int Initiative { get; set; }
        public int TotalInitiative { get { return Initiative + Character.CurrentStats.Initiative; } }
        public bool IsActive { get; set; }
        public bool Live { get { return this.Character.Life > 0; } }
        public string BattleName { get; set; }

        public string Name { get { return BattleName; } }

        private bool _shouldNotify;

        public bool SendNotify
        {
            get
            {
                return _shouldNotify;
            }
            set
            {
                _shouldNotify = value;
                var queueManager = ServiceContainer.GetInstance<BriefServiceQueueManager>();
                if (value)
                {
                    queueManager.AddEnemyBriefCharacter(this, TypeOfChange.Insert);
                }
                else
                {
                    queueManager.AddEnemyBriefCharacter(this, TypeOfChange.Delete);
                }
            }

        }

        public int CompareTo(BattleCharacter other)
        {
            var diff = other.TotalInitiative - TotalInitiative;
            return diff == 0 ? other.Name.CompareTo(Name) : diff;
        }
    }
}