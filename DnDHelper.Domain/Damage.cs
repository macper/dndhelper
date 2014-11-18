using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace DnDHelper.Domain
{
    [Serializable]
    public class Damage
    {
        public List<DamageBone> Elements { get; set; }

        public Damage()
        {
            Elements = new List<DamageBone>();
        }

        public Damage(IEnumerable<DamageBone> bones)
        {
            Elements = new List<DamageBone>(bones);
        }

        public void AddElement(DamageBone damageBone)
        {
            var dmg = Elements.SingleOrDefault(e => e.DamageType == damageBone.DamageType && e.MaxValue == damageBone.MaxValue);
            if (!dmg.IsEmpty)
            {
                dmg.Count += damageBone.Count;
            }
            else
            {
                Elements.Add(damageBone);
            }
        }

        public void AddElement(Damage damage)
        {
            if (damage == null || !damage.Elements.Any())
                return;

            foreach (var element in damage.Elements)
            {
                var existingBone = Elements.FirstOrDefault(e => e.MaxValue == element.MaxValue && e.DamageType == element.DamageType);
                if (!existingBone.IsEmpty)
                {
                    Elements.RemoveAll(p => p.MaxValue == existingBone.MaxValue);
                    existingBone.Count += element.Count;
                    Elements.Add(existingBone);
                }
                else
                {
                    Elements.Add(element);
                }
            }
            
        }

        public void AddElements(IEnumerable<Damage> damages)
        {
            foreach (var damage in damages)
            {
                AddElement(damage);
            }
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var damageBoneGroup in Elements.OrderByDescending(e => e.MaxValue).GroupBy(e => e.DamageType))
            {
                foreach (var damageBone in damageBoneGroup)
                {
                    builder.AppendFormat("{0}{1}{2}", damageBone.Count > 0 ? "+" : "", damageBone.Count, damageBone.MaxValue == 1 ? "" : "K" + damageBone.MaxValue);
                }
                builder.AppendFormat("[{0}]", damageBoneGroup.Key);
            }
            return builder.ToString().TrimStart('+');
        }

        public static bool TryParse(string value,out Damage outDmg)
        {
            outDmg = new Damage();
            var pattern = @"([+]{0,1}(?<count>[0-9])*K(?<bones>[0-9])*[+]{0,1}(?<additional>[0-9]*)[[](?<type>[A-Ÿ]*)])";
            var regex = new Regex(pattern);
            var matches = regex.Matches(value);
            if (matches.Count == 0)
                return false;
            

            foreach (Match match in matches)
            {
                var type = match.Groups["type"].Value;
                if (!EnumsDictionary.DamageTypes.Any(e => (string)e.Value == type))
                {
                    return false;
                }

                var bone = new DamageBone() { DamageType = type };
                if (!match.Groups["bones"].Success)
                {
                    return false;
                }
                var tmpInt = 0;
                if (int.TryParse(match.Groups["bones"].Value, out tmpInt))
                {
                    bone.MaxValue = tmpInt;
                }

                if (match.Groups["count"].Success)
                {
                    if (int.TryParse(match.Groups["count"].Value, out tmpInt))
                        bone.Count = tmpInt;
                }
                else
                {
                    bone.Count = 1;
                }
                outDmg.Elements.Add(bone);
                if (match.Groups["additional"].Success)
                {
                    if (int.TryParse(match.Groups["additional"].Value, out tmpInt))
                    {
                        outDmg.Elements.Add(new DamageBone() { Count = tmpInt, DamageType = type, MaxValue = 1 });
                    }
                }
            }
            
            return true;
        }

        public static Damage Parse(string input)
        {
            var outDmg = new Damage();
            TryParse(input, out outDmg);
            return outDmg;
        }
    }
}