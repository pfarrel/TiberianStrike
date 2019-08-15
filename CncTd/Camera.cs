using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiberianStrike
{
    public class Camera
    {
        private Matrix _transform;
        private Vector2 _pos;
        private int _viewportWidth;
        private int _viewportHeight;
        private int _worldWidth;
        private int _worldHeight;

        public Camera(Viewport viewport, int worldWidth, int worldHeight)
        {
            _pos = Vector2.Zero;
            _viewportWidth = viewport.Width;
            _viewportHeight = viewport.Height;
            _worldWidth = worldWidth;
            _worldHeight = worldHeight;
        }

        #region Properties

        public float Zoom
        {
            get { return 3.0f; }
        }

        public void Move(Vector2 amount)
        {
            _pos += amount;
        }

        public Vector2 Pos
        {
            get { return _pos; }
            set
            {
                float leftBarrier = (float)_viewportWidth * .5f / Zoom;
                float rightBarrier = _worldWidth - (float)_viewportWidth * .5f / Zoom;
                float topBarrier = _worldHeight -(float)_viewportHeight * .5f / Zoom;
                float bottomBarrier = (float)_viewportHeight *.5f / Zoom;
                _pos = value;
                if (_pos.X < leftBarrier)
                    _pos.X = leftBarrier;
                if (_pos.X > rightBarrier)
                    _pos.X = rightBarrier;
                if (_pos.Y > topBarrier)
                    _pos.Y = topBarrier;
                if (_pos.Y < bottomBarrier)
                    _pos.Y = bottomBarrier;
            }
        }

        #endregion

        public Matrix GetTransformation()
        {
            _transform =
               Matrix.CreateTranslation(new Vector3(-_pos.X, -_pos.Y, 0)) *
               Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
               Matrix.CreateTranslation(new Vector3(_viewportWidth * 0.5f, _viewportHeight * 0.5f, 0));

            return _transform;
        }
    }
}
