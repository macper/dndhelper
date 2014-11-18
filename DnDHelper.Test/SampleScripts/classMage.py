import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import DamageTypes

def Calculate(character,repository):
    source = "Klasa"
    level = character.Level
    bonusAtutes = level / 5
    character.SkillPointsLeft.Atutes += bonusAtutes
    at = BonusFactory.GetByName("AtutBonus")
    at.AtutName = "[Tp]Zapisanie zwoju"
    character.Controller.AddBonusOriginal(at)
    return