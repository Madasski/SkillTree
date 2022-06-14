using System;
using Gameplay.Skills;
using UnityEngine;

namespace UI.Skills
{
    public class SkillItem : ISkillItem
    {
        public event Action<ISkillItem> Pressed;

        private readonly ISkillItemView _view;
        private readonly Color _learnedColor = Color.green;
        private readonly Color _notLearnedColor = Color.blue;

        public SkillType Type => _view.Type;

        public SkillItem(ISkillItemView view)
        {
            _view = view;
        }

        public void Init()
        {
            _view.SetText(_view.Type.ToString());
            _view.Pressed += OnPress;
        }

        public void SetState(SkillItemState state)
        {
            var color = state switch
            {
                SkillItemState.Learned => _learnedColor,
                SkillItemState.NotLearned => _notLearnedColor,
                _ => Color.black
            };

            _view.SetColor(color);
        }

        private void OnPress()
        {
            Pressed?.Invoke(this);
        }
    }
}