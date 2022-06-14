using System;
using Gameplay.Skills;

namespace UI.Skills
{
    public interface ISkillItem
    {
        event Action<ISkillItem> Pressed;
        SkillType Type { get; }

        void Init();
        void SetState(SkillItemState state);
    }
}