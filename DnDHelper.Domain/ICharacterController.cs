using DnDHelper.Domain.Bonuses;

namespace DnDHelper.Domain
{
    public interface ICharacterController
    {
        OperationResult EquipItem(Character character, EquipItem item, ItemPosition position);
        OperationResult UnEquipItem(Character character, ItemPosition position);
        OperationResult AddBonus(Character character, BaseBonus bonus, bool initial = true);
        OperationResult RemoveBonus(Character character, BaseBonus bonus, bool initial = true);
        OperationResult ChangeSecondarySkill(Character character, string skillName, int value, bool current = false);
        OperationResult AddAtute(Character character, Atut atut, bool current = false);
        OperationResult RemoveAtute(Character character, Atut atut, bool current = false);
        OperationResult AddEffect(Character character, Effect effect);
        OperationResult RemoveEffect(Character character, Effect effect);
    }
}