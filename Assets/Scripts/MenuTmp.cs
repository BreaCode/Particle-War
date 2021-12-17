using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
//Я сильно торопился и не стал пока делать это нормальным контроллером
//Но если нужно могу сделать
namespace ParticleWar
{
    public sealed class MenuTmp : MonoBehaviour
    {
        private GameObject _startScreen;
        private GameObject _menu;
        private GameObject _gui;
        private GameObject _looseScreen;
        private Starter _starter;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _retryButton;
        [SerializeField] private Slider _starterBalls;
        [SerializeField] private Slider _addingBalls;
        [SerializeField] private TMP_Text _starterBallsText;
        [SerializeField] private TMP_Text _addingBallsText;
        [SerializeField] private TMP_Text _round;
        [SerializeField] private TMP_Text _counter;
        [SerializeField] private TMP_Text _score;
        [SerializeField] private TMP_Text _looseScore;
        [SerializeField] private GameData _data;


        private void Start()
        {
            //Поиск по названиям это очень плохо, но данный класс я переделаю если потребуется
            _startScreen = GameObject.Find("StartScreen");
            _menu = GameObject.Find("PauseMenu");
            _gui = GameObject.Find("GUI");
            _looseScreen = GameObject.Find("Loose");
            _starter = GameObject.Find("[SETUP]").GetComponent<Starter>();
            _startButton.onClick.AddListener(StartGame);
            _continueButton.onClick.AddListener(Continue);
            _restartButton.onClick.AddListener(Restart);
            _retryButton.onClick.AddListener(Restart);
            _quitButton.onClick.AddListener(Close);
            _starterBalls.onValueChanged.AddListener((float value) => { SetStarterBalls((int)value); });
            _starterBallsText.text = $"50";
            _addingBalls.onValueChanged.AddListener((float value) => { SetAddingBalls((int)value); });
            _addingBallsText.text = $"0";
            _gui.SetActive(false);
            _menu.SetActive(false);
            _looseScreen.SetActive(false);
            GameEventSystem.current.onGUIUpdate += GUIUpdate;
            GameEventSystem.current.onLoose += Loose;
        }

        void Update()
        {
            //Это должно быть в инпуте
            if (Input.GetButtonDown("Pause"))
            {
                Time.timeScale = 0f;
                _menu.SetActive(true);
                _gui.SetActive(false);
            }
        }

        private void Loose()
        {
            _gui.SetActive(false);
            _looseScreen.SetActive(true);
            Time.timeScale = 0f;
        }

        private void GUIUpdate()
        {
            _round.text = $"Round : {_data.Round}";
            _counter.text = $"{_data.ActiveBalls} из {_data.MaxBalls}";
            _score.text = $"Score : {_data.Score}";
            _looseScore.text = _score.text;
        }
        private void StartGame()
        {
            _startScreen.SetActive(false);
            _gui.SetActive(true);
            _starter.enabled = !_starter.enabled;
        }
        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        private void Continue()
        {
            Time.timeScale = 1f;
            _menu.SetActive(false);
            _gui.SetActive(true);
        }
        private void Close()
        {
            Application.Quit();
        }

        private void SetStarterBalls(int amount)
        {
            _starterBallsText.text = $"{amount}";
            _data.MaxBalls = amount;
        }
        private void SetAddingBalls(int amount)
        {
            _addingBallsText.text = $"{amount}";
            _data.AddBalls = amount;
        }

        private void OnDisable()
        {
            GameEventSystem.current.onGUIUpdate -= GUIUpdate;
            GameEventSystem.current.onLoose -= Loose;
        }
    }
}
