import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import *

def Calculate(character,repository,atut):
    at = BonusFactory.GetByName("ThrowBonus", "Paladyn - boska łaska")
    charismaBonus = Rules.GetStandardBonus(character.CurrentStats.Charisma)
    at.BonusType.WillThrow += charismaBonus
    at.BonusType.ReflexThrow += charismaBonus
    at.BonusType.EnduranceThrow += charismaBonus
    character.Controller.AddBonus(at)
    return