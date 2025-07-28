using System;
using System.Collections.Generic;

namespace MyGame.Scripts.Core
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, List<Action<object>>> Handlers = new();

        public static void Subscribe<T>(Action<T> handler) where T : class
        {
            var type = typeof(T);
            if (!Handlers.ContainsKey(type))
            {
                Handlers[type] = new List<Action<object>>();
            }

            Handlers[type].Add(obj => handler(obj as T));
        }

        public static void Publish<T>(T message) where T : class
        {
            var type = typeof(T);
            if (Handlers.TryGetValue(type, out var handler1))
            {
                foreach (var handler in handler1)
                {
                    handler(message);
                }
            }
        }
    }
}

public class ShapeSortedEvent
{
    public const int Points = 1;
}

public class LifeLostEvent
{
    public const int LivesLost = 1;
}
