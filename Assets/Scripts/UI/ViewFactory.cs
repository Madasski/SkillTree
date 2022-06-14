using UI.Skills;
using UnityEngine;

namespace UI
{
    public class ViewFactory : MonoBehaviour, IViewFactory
    {
        [SerializeField] private SkillTreeScreenView _skillTreeScreenViewPrefab;

        public ISkillTreeScreenView CreateSkillTreeScreenView()
        {
            return Instantiate(_skillTreeScreenViewPrefab, transform);
        }
    }
}