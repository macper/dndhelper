using System;

namespace DnDHelper.Domain
{
    public class EnemyBrief
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Health { get; set; }

        public TypeOfChange TypeOfChange { get; set; }

        public static EnemyBrief Create(BattleCharacter character, TypeOfChange change)
        {
            return new EnemyBrief
            {
                Name = character.Name,
                Id = character.Character.Id.Value,
                Health = FormatHealth(100 * ((decimal) character.Character.Life/(decimal) character.Character.CurrentStats.HP)),
                TypeOfChange = change
            };
        }

        public int ChangeId { get; set; }

        private static string FormatHealth(decimal healthPrc)
        {
            if (healthPrc == 100M)
                return "Pe³nia zdrowia";

            if (healthPrc > 80)
                return "Lekkie obra¿enia";

            if (healthPrc > 50)
                return "Œrednie obra¿enia";

            if (healthPrc > 20)
                return "Powa¿ne obra¿enia";

            if (healthPrc > 0)
                return "Na skraju œmierci";

            return "Nie ¿yje";
        }
    }
}