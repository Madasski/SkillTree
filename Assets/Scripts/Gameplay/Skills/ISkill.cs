using System.Collections.Generic;

namespace Gameplay.Skills
{
    public interface ISkill
    {
        int Price { get; }
        bool IsLearned { get; set; }
        List<ISkill> Neighbors { get; set; }
    }
}