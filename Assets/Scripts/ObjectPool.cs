using System.Collections.Generic;
using UnityEngine;

namespace ParticleWar
{
    internal sealed class ObjectPool
    {
        private readonly Stack<GameObject> _stack = new Stack<GameObject>();
        private readonly GameObject _prefab;
        private readonly Transform _parent;

        public ObjectPool(GameObject prefab, Transform parent)
        {
            _prefab = prefab;
            _parent = parent;
        }

        public void Push(GameObject go)
        {
            _stack.Push(go);
            go.SetActive(false);
        }

        public GameObject Pop()
        {
            GameObject go;
            if (_stack.Count == 0)
            {
                go = GameObject.Instantiate(_prefab, _parent);
            }
            else
            {
                go = _stack.Pop();
            }
            go.SetActive(true);
            return go;
        }
    }
}

