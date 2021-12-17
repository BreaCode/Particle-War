using UnityEngine;
namespace ParticleWar
{
    internal sealed class Starter : MonoBehaviour
    {
        [SerializeField] private GameData _gameData;
        private Controllers _controllers;
        private BallsData _ballsData;
        void Awake()
        {
            Time.timeScale = 1f;
        }
        private void Start()
        {
            _controllers = new Controllers();
            _ballsData = new BallsData();
            new GameInitialization(_controllers, _gameData, _ballsData);
            _controllers.Initialization();
            GameEventSystem.current.GUIUpdate();

            _gameData.Effect = Resources.Load("Effect");
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Execute(deltaTime);
        }

        private void FixedUpdate()
        {
            var deltaTime = Time.deltaTime;
            _controllers.Fixed(deltaTime);
        }

    }
}

