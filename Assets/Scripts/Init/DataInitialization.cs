using UnityEngine;
using System.Collections.Generic;

namespace ParticleWar
{
    internal sealed class DataInitialization
    {
        public static void Initialization(GameData data, BallsData ballsData)
        {
            data.OriginTransform = GameObject.Find("SpawnPoint").transform;
            data.Score = 0;
            data.Round = 0;
            data.ActiveBalls = 0;
            ballsData.Pool = new ObjectPool(data.BallPrefab, data.OriginTransform);
            ballsData.Colliders = new List<Collider2D>(data.MaxBalls);
            ballsData.Balls = new List<Ball>(data.MaxBalls);
            ballsData.BallObjects = new List<GameObject>(data.MaxBalls);
        }
    }
}