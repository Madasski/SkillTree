using UI.Skills;

namespace UI
{
    public interface IViewFactory
    {
        ISkillTreeScreenView CreateSkillTreeScreenView();
    }
}