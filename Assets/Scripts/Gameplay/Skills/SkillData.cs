using System;
using System.Collections.Generic;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.Skills
{
    [Serializable]
    public class SkillData
    {
        [SerializeField] private int _price;
        [SerializeField] private SkillType _type;
        [SerializeField] private List<SkillType> _neighbors;

        public int Price => _price;
        public SkillType Type => _type;
        public List<SkillType> Neighbors => _neighbors;
    }
}