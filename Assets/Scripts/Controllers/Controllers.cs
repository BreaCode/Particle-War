using System.Collections.Generic;

namespace ParticleWar
{
    internal sealed class Controllers : IController, IInitialization, IExecute, ICleaner
    {
        private readonly List<IInitialization> _initializeControllers;
        private readonly List<IExecute> _executeControllers;
        private readonly List<IFixed> _fixedControllers;
        private readonly List<ICleaner> _cleanupControllers;

        internal Controllers()
        {
            _initializeControllers = new List<IInitialization>(8);
            _executeControllers = new List<IExecute>(8);
            _fixedControllers = new List<IFixed>(8);
            _cleanupControllers = new List<ICleaner>(8);
        }

        internal Controllers Add(IController controller)
        {
            if (controller is IInitialization initializeController)
            {
                _initializeControllers.Add(initializeController);
            }

            if (controller is IExecute executeController)
            {
                _executeControllers.Add(executeController);
            }

            if (controller is IFixed fixedExecuteController)
            {
                _fixedControllers.Add(fixedExecuteController);
            }

            if (controller is ICleaner cleanupController)
            {
                _cleanupControllers.Add(cleanupController);
            }

            return this;
        }

        public void Initialization()
        {
            for (var index = 0; index < _initializeControllers.Count; ++index)
            {
                _initializeControllers[index].Initialization();
            }
        }

        public void Execute(float deltaTime)
        {
            for (var index = 0; index < _executeControllers.Count; ++index)
            {
                _executeControllers[index].Execute(deltaTime);
            }
        }
        public void Fixed(float deltaTime)
        {
            for (var index = 0; index < _fixedControllers.Count; ++index)
            {
                _fixedControllers[index].Fixed(deltaTime);
            }
        }

        public void Clean()
        {
            for (var index = 0; index < _cleanupControllers.Count; ++index)
            {
                _cleanupControllers[index].Clean();
            }
        }
    }
}
