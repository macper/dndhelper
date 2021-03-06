﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DnDHelper
{
    public class Battle
    {
        public int Turn { get; set; }
        public List<Character> Members { get; set; }
        public Character ActiveMember { get; set; }
        private Helper _helper;

        public Battle(Helper helper)
        {
            Turn = 0;
            Members = new List<Character>();
            _helper = helper;
        }

        public void AddMember(Character character)
        {
            Members.Add(character);
            Members.Sort();
        }

        public void Start()
        {
            if (Members.Count == 0)
            {
                return;
            }
            Turn = 1;
            ActiveMember = Members[0];
            ActiveMember.IsActiveMember = true;
        }

        public void NextMember()
        {
            if (Turn == 0)
            {
                return;
            }

            int index = Members.IndexOf(ActiveMember);
            if (index == Members.Count - 1)
            {
                NewTurn();
                return;
            }
            ActiveMember.IsActiveMember = false;
            ActiveMember = Members[++index];
            if (!ActiveMember.IsAlive && ActiveMember.CurrentStats.HP < -10)
            {
                NextMember();
            }
            ActiveMember.IsActiveMember = true;
        }

        private void NewTurn()
        {
            Turn++;
            ActiveMember.IsActiveMember = false;
            ActiveMember = Members[0];
            ActiveMember.IsActiveMember = true;
            foreach (Character character in Members)
            {
                for (int i = 0; i < character.Effects.Count; i++)
                {
                    Effect efekt = character.Effects[i];
                    if (efekt.Duration != null)
                    {
                        efekt.Duration--;
                        if (efekt.Duration == 0)
                        {
                            character.Effects.Remove(efekt);
                        }
                    }
                }
            }
            _helper.CurrentTime.AddSeconds(6);
        }

        public AttackInfo Atak(Character target, Attack atak)
        {
            return new AttackInfo() { ToHit = (target.CurrentStats.AC - atak.ToHit).ToString() };
        }

        public void DoDamage(Character target, int damage)
        {
            target.CurrentStats.HP -= damage;
        }

        public void DoDamage(Character attacker, Character target, int damage)
        {
            target.CurrentStats.HP -= damage;
            if (target.CurrentStats.HP <= 0)
            {
                attacker.Kills.Add(new KilledCreature() { Date = _helper.CurrentTime, Name = target.Name, Race = target.Race });
            }
        }
    }

    public class AttackInfo
    {
        public string ToHit { get; set; }
    }
}
