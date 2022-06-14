using System.Collections.Generic;

namespace Gameplay.Skills
{
    public class Skill : ISkill
    {
        private readonly int _price;

        private bool _isLearned;
        private SkillType _type;
        private List<ISkill> _neighbors;

        public Skill(SkillType type, int price)
        {
            _price = price;
            _type = type;
        }

        public bool IsLearned
        {
            get => _isLearned;
            set => _isLearned = value;
        }

        public int Price => _price;

        public List<ISkill> Neighbors
        {
            get => _neighbors;
            set => _neighbors = value;
        }
    }
}