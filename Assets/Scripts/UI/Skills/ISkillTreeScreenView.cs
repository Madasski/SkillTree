using System;
using System.Collections.Generic;

namespace UI.Skills
{
    public interface ISkillTreeScreenView
    {
        event Action GainPointsPressed;
        event Action LearnSkillPressed;
        event Action ForgetSkillPressed;
        event Action ForgetAllSkillsPressed;
        IEnumerable<ISkillItemView> SkillItems { get; }
        void UpdatePointsIndicator(int playerSkillPoints);
        void UpdatePriceIndicator(int skillPrice);
        void ActivateLearnButton();
        void DeactivateLearnButton();
        void ActivateForgetButton();
        void DeactivateForgetButton();
    }
}