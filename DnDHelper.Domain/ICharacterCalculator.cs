namespace DnDHelper.Domain
{
    public interface ICharacterCalculator
    {
        void CalculateInitialStats(Character character);
        void Calculate(Character character);
        void ApplySkillBonus(Character character, Skill skill);
    }
}