using UnityEngine;
using System.Collections.Generic;

namespace ParticleWar
{
    internal sealed class Spawner
    {
        private GameData _data;
        private BallsData _ballsData;
        private ObjectPool _pool;
        private int _poolSize;
        private List<Ball> _balls;
        private List<GameObject> _ballObjects;
        private List<Collider2D> _colliders;
        private System.Random _rand;

        public Spawner(GameData data, BallsData ballsData)
        {
            GameEventSystem.current.onBallCreate += CreateBall;
            GameEventSystem.current.onDataUpdate += UpdateData;
            _rand = new System.Random();
            _data = data;
            _ballsData = ballsData;
            _poolSize = data.MaxBalls;
            _balls = ballsData.Balls;
            _ballObjects = ballsData.BallObjects;
            _colliders = ballsData.Colliders;
            _pool = ballsData.Pool;

            for (int i = 0; i < _poolSize; i++)
            {
                ballsData.BallObjects.Add(_pool.Pop());
                ballsData.Colliders.Add(ballsData.BallObjects[i].GetComponent<Collider2D>());
                _pool.Push(ballsData.BallObjects[i]);
            }
            for (int i = 0; i < data.MaxBalls; i++)
            {
                CreateBall(_data.ActiveBalls);
            }
            CreateMiddleSquare();
        }

        internal void Spawn(GameData data, BallsData ballsData)
        {
            _ballObjects = ballsData.BallObjects;
            _colliders = ballsData.Colliders;
            _balls = ballsData.Balls;
            for (int i = 0; i < data.MaxBalls; i++)
            {
                ballsData.BallObjects.Add(_pool.Pop());
                ballsData.Colliders.Add(ballsData.BallObjects[i].GetComponent<Collider2D>());
                _pool.Push(ballsData.BallObjects[i]);
            }
            for (int i = 0; i < data.MaxBalls; i++)
            {
                CreateBall(_data.ActiveBalls);
            }
        }

        private void CreateMiddleSquare()
        {
            _ballsData.CentralSquare = GameObject.Instantiate(_data.CentralSquarePrefab, _data.OriginTransform);
        }
        private void CreateBall(int i)
        {
            _balls.Add(new Ball(_rand));
            _ballObjects[i] = _pool.Pop();
            _colliders[i] = _ballObjects[i].GetComponent<Collider2D>();
            _ballObjects[i].transform.position = _balls[i].StartPos;
            _ballObjects[i].GetComponent<SpriteRenderer>().color = _balls[i].Color;
            _ballObjects[i].transform.localScale = new Vector3(_balls[i].Scale, _balls[i].Scale, 0);
            _data.ActiveBalls++;
        }

        private void UpdateData(GameData data, BallsData ballsData)
        {
            _data = data;
            _ballsData = ballsData;
            _poolSize = data.MaxBalls;
            _balls = ballsData.Balls;
            _ballObjects = ballsData.BallObjects;
            _colliders = ballsData.Colliders;
            _pool = ballsData.Pool;
        }

        ~Spawner()
        {
            GameEventSystem.current.onDataUpdate -= UpdateData;
            GameEventSystem.current.onBallCreate -= CreateBall;
        }
    }
}

