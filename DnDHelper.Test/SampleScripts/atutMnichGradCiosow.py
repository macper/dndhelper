import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import *

def Calculate(character,repository,atut):
    level = character.Level
    armor = character.GetItemByPosition(ItemPosition.Torso)
    if (armor == None):
        character.CustomAttacks.RemoveAll(lambda a:a.Name == "Atak (pięść)")
        customAttack = CustomAttack()
        customAttack.Name = "Atak (pięść)"
        bonus = BonusFactory.GetByName("DamageBonus")
        damage = Damage()
        if (level <= 3):
            damage.Elements.Add(DamageBone(8,1))
        if (level > 3 and level <= 7):
            damage.Elements.Add(DamageBone(6,2))
        if (level > 7 and level <= 11):
            damage.Elements.Add(DamageBone(8,2))
        if (level > 11 and level <= 15):
            damage.Elements.Add(DamageBone(6,3))
        if (level > 15):
            damage.Elements.Add(DamageBone(8,4))           
        bonus.Amount = damage
        customAttack.Bonuses.Add(bonus)
        character.CustomAttacks.Add(customAttack)

        character.CustomAttacks.RemoveAll(lambda a:a.Name == "Atak (grad ciosów)")
        customAttack = CustomAttack()
        customAttack.Name = "Atak (grad ciosów)"
        bonus = BonusFactory.GetByName("DamageBonus")
        bonus.Amount = damage
        customAttack.Bonuses.Add(bonus)

        bonus = BonusFactory.GetByName("AttackBonus")
        bonus.Value = -2
        customAttack.Bonuses.Add(bonus)

        bonus = BonusFactory.GetByName("NumberOfAttacksBonus")
        bonus.Value = 1
        customAttack.Bonuses.Add(bonus)
        character.CustomAttacks.Add(customAttack)
    return