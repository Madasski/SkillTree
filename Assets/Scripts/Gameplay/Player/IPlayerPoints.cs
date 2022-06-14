using System;

namespace Gameplay.Player
{
    public interface IPlayerPoints
    {
        event Action<int> PointsChanged;
        int Points { get; }
        void AddPoints(int amount);
        void RemovePoints(int amount);
    }
}