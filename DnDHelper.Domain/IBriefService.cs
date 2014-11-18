using System;
using System.Linq;
using System.Text;

namespace DnDHelper.Domain
{
    public interface IBriefService
    {
        void UpdateBriefs(BriefUpdate update);
        void RemoveEnemies();
    }
}
