using System.Collections.Generic;
using Gameplay.Player;
using Gameplay.Skills;

namespace UI.Skills
{
    public class SkillTreeScreen
    {
        private const int PointsPerGetPointButtonPress = 1;

        private readonly ISkillTree _skillTree;
        private readonly ISkillTreeScreenView _view;
        private readonly IPlayerPoints _playerPoints;
        private readonly Dictionary<SkillType, ISkillItem> _skillItems;

        private ISkillItem _currentSkillItem;

        public SkillTreeScreen(IViewFactory viewFactory, IPlayerPoints playerPoints, ISkillTree skillTree)
        {
            _playerPoints = playerPoints;
            _skillTree = skillTree;
            _view = viewFactory.CreateSkillTreeScreenView();
            _view.DeactivateLearnButton();
            _view.DeactivateForgetButton();

            _skillItems = new Dictionary<SkillType, ISkillItem>();

            foreach (var skillItemView in _view.SkillItems)
            {
                var skillItem = new SkillItem(skillItemView);
                skillItem.Init();
                var isLearned = skillTree.GetSkillByType(skillItem.Type).IsLearned;
                var state = isLearned ? SkillItemState.Learned : SkillItemState.NotLearned;
                skillItem.SetState(state);
                _skillItems.Add(skillItem.Type, skillItem);
            }

            Subscribe();
        }

        private void Subscribe()
        {
            _view.LearnSkillPressed += OnLearnPressed;
            _view.ForgetSkillPressed += OnForgetPressed;
            _view.GainPointsPressed += OnGainPointsPressed;
            _view.ForgetAllSkillsPressed += OnForgetAllPressed;

            _skillTree.SkillForgotten += OnSkillForgotten;

            foreach (var skillItem in _skillItems)
            {
                skillItem.Value.Pressed += SetCurrentSkillItem;
            }

            _playerPoints.PointsChanged += OnPlayerPointsChanged;
        }

        private void SetCurrentSkillItem(ISkillItem skillItem)
        {
            _currentSkillItem = skillItem;
            int skillPrice = _skillTree.GetSkillByType(skillItem.Type).Price;
            _view.UpdatePriceIndicator(skillPrice);

            UpdateSkillDependentButtons();
        }

        private void OnPlayerPointsChanged(int newAmount)
        {
            UpdateLearnButton();
            _view.UpdatePointsIndicator(_playerPoints.Points);
        }

        private void OnGainPointsPressed()
        {
            _playerPoints.AddPoints(PointsPerGetPointButtonPress);
        }

        private void OnLearnPressed()
        {
            _skillTree.LearnSkill(_currentSkillItem.Type);
            _currentSkillItem.SetState(SkillItemState.Learned);
            UpdateSkillDependentButtons();
        }

        private void OnForgetPressed()
        {
            _skillTree.ForgetSkill(_currentSkillItem.Type);
            _currentSkillItem.SetState(SkillItemState.NotLearned);
            UpdateSkillDependentButtons();
        }

        private void OnForgetAllPressed()
        {
            _skillTree.ForgetAllSkills();
            UpdateSkillDependentButtons();
        }

        private void OnSkillForgotten(SkillType skill)
        {
            _skillItems[skill].SetState(SkillItemState.NotLearned);
        }

        private void UpdateLearnButton()
        {
            if (_currentSkillItem == null) return;

            if (_skillTree.CanSkillBeLearned(_currentSkillItem.Type, _playerPoints.Points))
            {
                _view.ActivateLearnButton();
            }
            else
            {
                _view.DeactivateLearnButton();
            }
        }

        private void UpdateForgetButton()
        {
            if (_currentSkillItem == null) return;

            if (_skillTree.CanSkillBeForgotten(_currentSkillItem.Type))
            {
                _view.ActivateForgetButton();
            }
            else
            {
                _view.DeactivateForgetButton();
            }
        }

        private void UpdateSkillDependentButtons()
        {
            UpdateLearnButton();
            UpdateForgetButton();
        }
    }
}