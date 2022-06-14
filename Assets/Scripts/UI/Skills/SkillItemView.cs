using System;
using Gameplay.Skills;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Skills
{
    public interface ISkillItemView
    {
        event Action Pressed;
        SkillType Type { get; }
        void SetText(string text);
        void SetColor(Color color);
    }

    [RequireComponent(typeof(Button))]
    public class SkillItemView : MonoBehaviour, ISkillItemView
    {
        public event Action Pressed;

        [SerializeField] private SkillType _type;
        [SerializeField] private Text _text;
        private Button _button;

        public SkillType Type => _type;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OnPress);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnPress);
        }

        private void OnPress()
        {
            Pressed?.Invoke();
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void SetColor(Color color)
        {
            var colors = _button.colors;
            colors.normalColor = color;
            _button.colors = colors;
        }
    }
}