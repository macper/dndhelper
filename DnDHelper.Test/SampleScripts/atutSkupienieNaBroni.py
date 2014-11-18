import clr
clr.AddReference('DnDHelper.Domain')
from DnDHelper.Domain.Bonuses import BonusFactory
from DnDHelper.Domain import *

def Calculate(character,repository,atut):
    rightWeapon = character.GetItemByPosition(ItemPosition.RightHand)
    leftWeapon = character.GetItemByPosition(ItemPosition.LeftHand)
    ApplyForWeapon(character, rightWeapon, atut)
    ApplyForWeapon(character, leftWeapon, atut)
    return

def ApplyForWeapon(character, item, atut):
    if (item == None):
        return

    if (item.Definition.PrototypeName != atut.AdditionalInfo and item.Name != atut.AdditionalInfo):
        return

    bonus = BonusFactory.GetByName("AttackBonus", "Atut: Skupienie na broni")
    bonus.Value = 1;
    weaponBonus = BonusFactory.GetByName("WeaponBonus", "Atut: Skupienie na broni")
    weaponBonus.Item = item
    weaponBonus.Bonus = bonus
    character.Controller.AddBonus(weaponBonus)
    return
