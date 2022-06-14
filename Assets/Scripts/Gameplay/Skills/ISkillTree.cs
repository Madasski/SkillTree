using System;

namespace Gameplay.Skills
{
    public interface ISkillTree
    {
        event Action<SkillType> SkillLearned;
        event Action<SkillType> SkillForgotten;

        ISkill GetSkillByType(SkillType type);
        void LearnSkill(SkillType type);
        void ForgetSkill(SkillType type);
        void ForgetAllSkills();

        bool IsSkillLearned(SkillType type);
        bool CanSkillBeLearned(SkillType type, int playerPoints);
        bool CanSkillBeForgotten(SkillType type);
    }
}