using UnityEngine;
using System.Collections.Generic;
namespace ParticleWar
{
    internal sealed class RoundChanger
    {
        GameData _data;
        BallsData _ballsData;
        Spawner _spawner;
        internal RoundChanger(GameData data, BallsData ballsData, Spawner spawner)
        {
            _data = data;
            _ballsData = ballsData;
            _spawner = spawner;
        }
        internal void Change()
        {
            _data.MaxBalls += _data.AddBalls;
            _data.ActiveBalls = 0;
            _ballsData.Balls = null;
            _ballsData.Colliders = null;
            _ballsData.BallObjects = null;
            _ballsData.Colliders = new List<Collider2D>(_data.MaxBalls);
            _ballsData.Balls = new List<Ball>(_data.MaxBalls);
            _ballsData.BallObjects = new List<GameObject>(_data.MaxBalls);
            _ballsData.Pool = new ObjectPool(_data.BallPrefab, _data.OriginTransform);
            GameEventSystem.current.DataUpdate(_data, _ballsData);
            _data.Round++;
            _spawner.Spawn(_data, _ballsData);
            GameEventSystem.current.GUIUpdate();
        }
        internal void PoolChange()
        {
            _ballsData.Colliders = new List<Collider2D>();
            _ballsData.BallObjects = new List<GameObject>();
            _ballsData.Pool = new ObjectPool(_data.BallPrefab, _data.OriginTransform);
        }
    }

}
