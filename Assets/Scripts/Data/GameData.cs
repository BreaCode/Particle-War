using UnityEngine;

namespace ParticleWar
{
    //Лучше было сделать иначе, если потребуется переделаю
    [CreateAssetMenu(fileName = "Data", menuName = "Create data")]
    internal sealed class GameData : ScriptableObject
    {
        public Transform OriginTransform;
        public GameObject BallPrefab;
        public GameObject CentralSquarePrefab;
        public UnityEngine.Object Effect;
        public int ActiveBalls;
        public int MaxBalls;
        public int AddBalls;
        public int Score;
        public int Round;
    }
}

