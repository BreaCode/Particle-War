using UnityEngine;
using System.Collections.Generic;
namespace ParticleWar
{
    internal sealed class Checker
    {
        private GameData _data;
        private BallsData _ballsData;
        private RoundChanger _changer;
        private List<Collider2D> _ballsColliders;
        private List<GameObject> _ballObjects;
        private Collider2D _centerCollider;
        private ObjectPool _pool;


        internal Checker(GameData data, BallsData ballsData, RoundChanger changer)
        {
            GameEventSystem.current.onDataUpdate += UpdateData;
            _data = data;
            _ballsData = ballsData;
            _changer = changer;
            _ballsColliders = ballsData.Colliders;
            _ballObjects = ballsData.BallObjects;
            _centerCollider = ballsData.CentralSquare.GetComponent<Collider2D>();
            _pool = ballsData.Pool;
        }
        internal void LooseCheck(Vector3 mousePos)
        { 
            for (int i = 0; i < _ballsColliders.Count; i++)
            {
                if (_ballsColliders[i].bounds.Contains(mousePos))
                {
                    GameEventSystem.current.Loose();
                }
            }
            if (_centerCollider.bounds.Contains(mousePos))
            {
                GameEventSystem.current.Loose();
            }
        }

        internal void MiddleContactCheck(int i)
        {
            if (_ballsColliders[i].IsTouching(_centerCollider))
            {
                _data.Score++;
                BallDestroy(_ballObjects[i], i);
            }
        }

        private void BallDestroy(GameObject ball, int index)
        {
            GameObject effect = (GameObject)GameObject.Instantiate(_data.Effect);
            Object.Destroy(effect, 2f);
            effect.transform.position = ball.transform.position;
            _pool.Push(ball);
            _data.ActiveBalls--;
            GameEventSystem.current.GUIUpdate();
            if (_data.ActiveBalls == 0)
            {
                _changer.Change();
            }
        }

        private void UpdateData(GameData data, BallsData ballsData)
        {
            _data = data;
            _ballsData = ballsData;
            _ballsColliders = ballsData.Colliders;
            _ballObjects = ballsData.BallObjects;
            _pool = ballsData.Pool;
        }

        ~Checker()
        {
            GameEventSystem.current.onDataUpdate -= UpdateData;
        }
    }
}

