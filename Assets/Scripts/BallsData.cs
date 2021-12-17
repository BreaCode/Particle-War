using UnityEngine;
using System.Collections.Generic;
namespace ParticleWar
{
    internal sealed class BallsData
    {
        public ObjectPool Pool;
        public List<Collider2D> Colliders;
        public List<GameObject> BallObjects;
        public GameObject CentralSquare;
        public List<Ball> Balls;
    }
}