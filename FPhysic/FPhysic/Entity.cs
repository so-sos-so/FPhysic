using System;
using System.Collections.Generic;
using FMath;

namespace FPhysic
{
    public abstract class Entity
    {
        public FPVector2 Position { get; set; } = FPVector2.zero;
        public FPInt Rotation { get; set; } = 0;
        public FPVector2 Scale { get; set; } = FPVector2.one;
        public FPVector2 Forward { get; set; } = new FPVector2(0, 1);
        public FPVector2 Right { get; set; } = new FPVector2(1, 0);

        private Dictionary<Type, List<object>> components = new Dictionary<Type, List<object>>();
        
        public T GetComponent<T>() where T : class
        {
            var type = typeof(T);
            List<object> list = null;
            foreach (var keyValue in components)
            {
                var componentType = keyValue.Key;
                var components = keyValue.Value;
                if (componentType != type && !type.IsAssignableFrom(componentType)) continue;
                list = components;
                break;
            }
            if (list == null || list.Count <= 0) return null;
            return list[0] as T;
        }

        public void AddComponent<T>(T component) where T : class
        {
            if (!components.TryGetValue(typeof(T), out var list))
            {
                list = new List<object>();
                components[typeof(T)] = list;
            }
            list.Add(component);
        }

        internal void TransformIdentity()
        {
            Position = FPVector2.zero;
            Rotation = 0;
            Forward = new FPVector2(0, 1);
            Right = new FPVector2(1, 0);
            Scale = FPVector2.one;
        }
    }
}