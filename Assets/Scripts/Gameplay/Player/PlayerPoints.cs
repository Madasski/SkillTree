using System;
using Gameplay.Skills;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerPoints : IPlayerPoints
    {
        public event Action<int> PointsChanged;

        private readonly ISkillTree _skillTree;
        private int _points;

        public PlayerPoints(ISkillTree skillTree)
        {
            _skillTree = skillTree;
            _skillTree.SkillLearned += OnSkillLearned;
            _skillTree.SkillForgotten += OnSkillForgotten;
        }

        public int Points => _points;

        public void AddPoints(int amount)
        {
            _points += amount;
            PointsChanged?.Invoke(_points);
        }

        public void RemovePoints(int amount)
        {
            _points -= amount;
            if (_points < 0) _points = 0;
            PointsChanged?.Invoke(_points);
        }

        private void OnSkillForgotten(SkillType obj)
        {
            int skillPrice = _skillTree.GetSkillByType(obj).Price;
            AddPoints(skillPrice);
        }

        private void OnSkillLearned(SkillType type)
        {
            int skillPrice = _skillTree.GetSkillByType(type).Price;
            RemovePoints(skillPrice);
        }
    }
}