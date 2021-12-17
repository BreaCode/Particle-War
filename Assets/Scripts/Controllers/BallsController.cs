using UnityEngine;
using System.Collections.Generic;
namespace ParticleWar
{
    internal sealed class BallsController : IController, IFixed
    {
        private List<Ball> _balls;
        private List<GameObject> _ballObjects;
        private GameData _data;
        private BallsData _ballsData;
        private Checker _checker;
        private Vector3 _mousePos;

        internal BallsController(GameData data, BallsData ballsData, Checker checker)
        {
            _data = data;
            _ballsData = ballsData;
            _checker = checker;
            _balls = ballsData.Balls;
            _ballObjects = ballsData.BallObjects;
            GameEventSystem.current.onDataUpdate += UpdateData;
        }
        public void Fixed(float deltaTime)
        {
            _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mousePos = new Vector3(_mousePos.x, _mousePos.y, 0);
            for (int i = 0; i < _data.MaxBalls; i++)
            {
                Vector3 ballposition = _ballsData.BallObjects[i].transform.position;
                float speed = _ballsData.Balls[i].Speed * deltaTime;
                Vector3 delta = _mousePos - ballposition;
                delta.Normalize();
                _ballsData.BallObjects[i].transform.position = ballposition + (delta * speed);
                _checker.MiddleContactCheck(i);
            }
        }

        private void UpdateData(GameData data, BallsData ballsData)
        {
            _data = data;
            _ballsData = ballsData;
        }

        ~BallsController()
        {
            GameEventSystem.current.onDataUpdate -= UpdateData;
        }

    }

}
