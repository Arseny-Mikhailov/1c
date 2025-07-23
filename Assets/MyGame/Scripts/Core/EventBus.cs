using System;

namespace MyGame.Scripts.Core
{
    public static class EventBus
    {
        public static Action<ShapeType> OnShapeDropped;
        public static Action OnLifeLost, OnScoreAdded;
    }
}