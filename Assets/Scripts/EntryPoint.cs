using Gameplay.Player;
using Gameplay.Skills;
using UI;
using UI.Skills;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] private ViewFactory _viewFactory;
    [SerializeField] private SkillsConfigSO _skillsConfig;

    private ISkillTree _skillTree;
    private IPlayerPoints _playerPoints;
    private SkillTreeScreen _skillTreeScreen;

    private void Awake()
    {
        _skillTree = new SkillTree(_skillsConfig);
        _playerPoints = new PlayerPoints(_skillTree);

        _skillTreeScreen = new SkillTreeScreen(_viewFactory, _playerPoints, _skillTree);
    }
}