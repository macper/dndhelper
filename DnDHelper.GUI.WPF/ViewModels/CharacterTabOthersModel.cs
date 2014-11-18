using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using DnDHelper.Domain;
using DnDHelper.Domain.Bonuses;

namespace DnDHelper.GUI.WPF.ViewModels
{
    public class CharacterTabOthersModel : CharacterTabBaseModel
    {


        public string Name
        {
            get { return _name; }
            set { _name = value; PropertyHasChanged("KilledCreatures"); PropertyHasChanged("Count"); }
        }

        public string Race
        {
            get { return _race; }
            set { _race = value; PropertyHasChanged("KilledCreatures"); PropertyHasChanged("Count"); }
        }

        public DateTime? DateFrom
        {
            get { return _dateFrom; }
            set { _dateFrom = value; PropertyHasChanged("KilledCreatures"); PropertyHasChanged("Count"); }
        }

        private string _name;
        private string _race;
        private DateTime? _dateFrom;

        

        public ObservableCollection<KilledCreature> KilledCreatures
        {
            get
            {
                return new ObservableCollection<KilledCreature>(Character.Kills.Where(k => (Name == null || k.Name.ToLower().StartsWith(Name.ToLower())) &&
                    (Race == null || k.Race.ToLower().StartsWith(Race.ToLower())) &&
                    (DateFrom == null || k.Date >= DateFrom)).OrderBy(b => b.Date));
            }
        }

        public int Count
        {
            get { return KilledCreatures.Count; }
        }

        public CharacterTabOthersModel(Character character) : base(character)
        {
            DateFrom = ServiceContainer.GetInstance<AppFacade>().GameTimer.CurrentTime.AddMonths(-3);
        }

        public int OverallCount
        {
            get
            {
                return Character.Kills.Count();
            }
        }

        public string MostPowerfullKill
        {
            get
            {
                var creature = Character.Kills.OrderByDescending(k => k.Level).FirstOrDefault();
                if (creature == null)
                    return string.Empty;
                return string.Format("{0},{1} (lvl. {2}) - {3}", creature.Name, creature.Race, creature.Level, creature.Date);
            }
        }
    }
}
