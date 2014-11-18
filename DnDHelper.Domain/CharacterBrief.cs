using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper.Domain
{
    public class CharacterBrief
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public int CurrentHP { get; set; }

        public int MaxHP { get; set; }

        public int CurrentAC { get; set; }

        public List<EffectBrief> Effects { get; set; }

        public int ChangeId { get; set; }

        public TypeOfChange TypeOfChange { get; set; }

        public static CharacterBrief Create(Character character, TypeOfChange change)
        {
            return new CharacterBrief
            {
                CurrentAC = character.CurrentStats.AC.Total,
                CurrentHP = character.Life,
                Effects = character.Effects.Select(s => new EffectBrief
                {
                    Name = s.Name,
                    Remaining = s.Duration.HasValue ? DescriptionsDictionary.GetDurationDescription(s.Duration.Value) : "(stały)"
                }).ToList(),
                Id = character.Id.Value,
                MaxHP = character.CurrentStats.HP,
                Name = character.Name,
                TypeOfChange = change
            };
        }



    }

    public class EffectBrief
    {
        public string Name { get; set; }

        public string Remaining { get; set; }
    }
}
