using System;
using System.Collections.Generic;
using System.Linq;

namespace Gameplay.Skills
{
    public class SkillTree : ISkillTree
    {
        public event Action<SkillType> SkillLearned;
        public event Action<SkillType> SkillForgotten;

        private Dictionary<SkillType, ISkill> _skills;
        private SkillType _initialSkill;

        public SkillTree(SkillsConfigSO skillsConfigSO)
        {
            InitSkills(skillsConfigSO);
        }

        public ISkill GetSkillByType(SkillType type)
        {
            return _skills[type];
        }

        public void LearnSkill(SkillType type)
        {
            var skill = _skills[type];
            skill.IsLearned = true;
            SkillLearned?.Invoke(type);
        }

        public void ForgetSkill(SkillType type)
        {
            var skill = _skills[type];
            if (skill.IsLearned == false) return;

            skill.IsLearned = false;
            SkillForgotten?.Invoke(type);
        }

        public void ForgetAllSkills()
        {
            foreach (var skill in _skills)
            {
                if (skill.Key == _initialSkill) continue;
                ForgetSkill(skill.Key);
            }
        }

        public bool IsSkillLearned(SkillType type)
        {
            return _skills[type].IsLearned;
        }

        public bool CanSkillBeLearned(SkillType type, int playerPoints)
        {
            var skill = _skills[type];

            bool isEnoughPoints = skill.Price <= playerPoints;
            bool isSkillNotLearned = IsSkillLearned(type) == false;
            bool doesSkillHaveLearnedNeighbors = skill.Neighbors.Any(x => x.IsLearned);

            bool canLearn = isEnoughPoints && isSkillNotLearned && doesSkillHaveLearnedNeighbors;

            return canLearn;
        }

        public bool CanSkillBeForgotten(SkillType type)
        {
            if (type == _initialSkill) return false;
            if (IsSkillLearned(type) == false) return false;

            var skill = GetSkillByType(type);

            foreach (var neighbor in skill.Neighbors)
            {
                if (neighbor.IsLearned == false) continue;

                var initialSkill = GetSkillByType(_initialSkill);
                bool areSkillsConnected = CheckIfSkillsConnected(neighbor, initialSkill, skill, new List<ISkill>());
                if (areSkillsConnected == false)
                {
                    return false;
                }
            }

            return true;
        }

        private void InitSkills(SkillsConfigSO skillsConfigSO)
        {
            _initialSkill = skillsConfigSO.InitialSkill;
            _skills = new Dictionary<SkillType, ISkill>();

            ReadSkillsFromConfig(skillsConfigSO);
            PopulateNeighbors(skillsConfigSO);

            LearnSkill(_initialSkill);
        }

        private void PopulateNeighbors(SkillsConfigSO skillsConfigSO)
        {
            foreach (var skillData in skillsConfigSO.Skills)
            {
                _skills[skillData.Type].Neighbors = skillData.Neighbors.Select(GetSkillByType).ToList();
            }
        }

        private void ReadSkillsFromConfig(SkillsConfigSO skillsConfigSO)
        {
            foreach (var skillData in skillsConfigSO.Skills)
            {
                var skill = new Skill(skillData.Type, skillData.Price);
                _skills.Add(skillData.Type, skill);
            }
        }

        private bool CheckIfSkillsConnected(ISkill firstSkill, ISkill secondSkill, ISkill skillToIgnore, List<ISkill> visited)
        {
            //using depth-first search
            if (firstSkill == secondSkill) return true;
            if (firstSkill == skillToIgnore) return false;
            visited.Add(firstSkill);

            foreach (var neighbor in firstSkill.Neighbors)
            {
                if (visited.Contains(neighbor) || firstSkill == skillToIgnore || neighbor.IsLearned == false) continue;

                if (CheckIfSkillsConnected(neighbor, secondSkill, skillToIgnore, visited))
                {
                    return true;
                }
            }

            return false;
        }
    }
}