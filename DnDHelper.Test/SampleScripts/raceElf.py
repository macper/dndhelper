import clr
clr.AddReference('DnDHelper.Domain')
import DnDHelper.Domain.Bonuses

def Calculate(character,repository):
    atut = BonusFactory.GetByName("AtutBonus")
    atut.Source = "Rasa"
    atut.Name = "Odporność na zauroczenia"
    skill = BonusFactory.GetByName("SecondarySkillBonus")
    skill.Source = "Rasa"
    skill.Name = "Nasłuchiwanie"
    skill.Value = 2
    character.InitialBonuses.AddRange([atut, skill])
    return