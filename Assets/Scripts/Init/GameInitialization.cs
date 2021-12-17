namespace ParticleWar
{
    internal sealed class GameInitialization
    {
        public GameInitialization(Controllers controllers, GameData data, BallsData ballsData)
        {
            DataInitialization.Initialization(data, ballsData);
            Spawner spawner = new Spawner(data, ballsData);
            RoundChanger changer = new RoundChanger(data, ballsData, spawner);
            Checker checker = new Checker(data, ballsData, changer);
            InputController input = new InputController(checker);
            BallsController ballsController = new BallsController(data, ballsData, checker);
            controllers.Add(input);
            controllers.Add(ballsController);
        }
    }
}
