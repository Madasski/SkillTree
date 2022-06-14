using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Skills
{
    public class SkillTreeScreenView : MonoBehaviour, ISkillTreeScreenView
    {
        public event Action GainPointsPressed;
        public event Action LearnSkillPressed;
        public event Action ForgetSkillPressed;
        public event Action ForgetAllSkillsPressed;

        private const string PointsIndicatorText = "POINTS: ";
        private const string PriceIndicatorText = "SKILL PRICE: ";

        [SerializeField] private List<SkillItemView> _skillItems;
        [SerializeField] private Text _pointsIndicator;
        [SerializeField] private Text _priceIndicator;

        [SerializeField] private Button _gainPointsButton;
        [SerializeField] private Button _learnSkillButton;
        [SerializeField] private Button _forgetSkillButton;
        [SerializeField] private Button _forgetAllSkillsButton;

        public IEnumerable<ISkillItemView> SkillItems => new List<ISkillItemView>(_skillItems);

        private void OnEnable()
        {
            _gainPointsButton.onClick.AddListener(OnGainPointsPressed);
            _learnSkillButton.onClick.AddListener(OnLearnPressed);
            _forgetSkillButton.onClick.AddListener(OnForgetPressed);
            _forgetAllSkillsButton.onClick.AddListener(OnForgetAllPressed);
        }

        private void OnDisable()
        {
            _gainPointsButton.onClick.RemoveListener(OnGainPointsPressed);
            _learnSkillButton.onClick.RemoveListener(OnLearnPressed);
            _forgetSkillButton.onClick.RemoveListener(OnForgetPressed);
            _forgetAllSkillsButton.onClick.RemoveListener(OnForgetAllPressed);
        }

        public void UpdatePointsIndicator(int playerSkillPoints)
        {
            _pointsIndicator.text = PointsIndicatorText + playerSkillPoints;
        }

        public void UpdatePriceIndicator(int skillPrice)
        {
            _priceIndicator.text = PriceIndicatorText + skillPrice.ToString();
        }

        public void ActivateLearnButton()
        {
            _learnSkillButton.interactable = true;
        }

        public void DeactivateLearnButton()
        {
            _learnSkillButton.interactable = false;
        }

        public void ActivateForgetButton()
        {
            _forgetSkillButton.interactable = true;
        }

        public void DeactivateForgetButton()
        {
            _forgetSkillButton.interactable = false;
        }

        private void OnGainPointsPressed()
        {
            GainPointsPressed?.Invoke();
        }

        private void OnLearnPressed()
        {
            LearnSkillPressed?.Invoke();
        }

        private void OnForgetPressed()
        {
            ForgetSkillPressed?.Invoke();
        }

        private void OnForgetAllPressed()
        {
            ForgetAllSkillsPressed?.Invoke();
        }
    }
}