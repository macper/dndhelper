import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import *

def Calculate(character,repository,atut):
    level = character.Level
    armor = character.GetItemByPosition(ItemPosition.Torso)
    if (armor == None):
        bonus = BonusFactory.GetByName("ACBonus", "Atut: Premia do KP (mnich)")
        bonus.ACType = "Naturalny pancerz"
        bonusFromWisdom = Rules.GetStandardBonus(character.CurrentStats.Wisdom)
        if (bonusFromWisdom > 0):
            bonus.Value = bonusFromWisdom
        bonus.Value += level / 5
        character.Controller.AddBonusOriginal(bonus)
    return