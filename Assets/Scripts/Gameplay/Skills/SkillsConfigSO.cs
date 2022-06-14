using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Skills
{
    [CreateAssetMenu(fileName = "SkillsConfigSO", menuName = "Skills Config", order = 0)]
    public class SkillsConfigSO : ScriptableObject
    {
        [SerializeField] private List<SkillData> _skills;
        [SerializeField] private SkillType _initialSkill;

        public List<SkillData> Skills => _skills;
        public SkillType InitialSkill => _initialSkill;
    }
}