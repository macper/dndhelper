import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import *

def Calculate(character,object):
    value = int(object)
    effect = ServiceContainer.GetInstance[RepositorySet]().Get[EffectDefinition]().GetElementByName("Potężny").CreateItem()
    effect.Bonuses.Clear()
    attackBonus = BonusFactory.GetByName("AttackBonus", "Potężny")
    attackBonus.Value = -value
    attackBonus.Melee = True
    effect.Bonuses.Add(attackBonus)

    damageBonus = BonusFactory.GetByName("DamageBonus", "Potężny")
    damageBonus.Amount = Damage()
    damageBonus.Amount.AddElement(DamageBone(1, 1.5 * value))
    effect.Bonuses.Add(damageBonus)
    effect.Duration = 1

    character.Controller.AddEffect(effect)
    return