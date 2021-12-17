using UnityEngine;
namespace ParticleWar
{
    internal sealed class InputController : IController, IFixed
    {
        private Vector3 _mousepos;
        private Checker _checker;

        public Vector3 MousePos
        {
            get { return _mousepos; }
        }

        internal InputController(Checker checker)
        {
            _checker = checker;
        }

        public void Fixed(float deltaTime)
        {
            _mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            _mousepos = new Vector3(_mousepos.x, _mousepos.y, 0);
            _checker.LooseCheck(_mousepos);
        }
    }
}

