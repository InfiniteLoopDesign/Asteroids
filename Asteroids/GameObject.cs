using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids
{
    public class GameObject
    {

        protected Texture2D _texture;
        protected float _rotation, _rotationAngle;
        protected Vector2 _velocity, _position;
        protected int _edges;
        protected float _scale;
        protected float maxLength;

        protected List<Vector2> _offsetVertices;
        protected List<Vector2> _vertices;

        protected Vector2 debug_init_position;


        // Constuctor
        public GameObject( Texture2D texture, Vector2 position )
        {
            _texture = texture;
            _position = position;
        }


        // Update Method
        public virtual void Update(GameTime gameTime, GraphicsDevice graphicsDevice) { }

        // Draw method
        public virtual void Draw(SpriteBatch _spriteBatch)
        {

            Vector2 first = _vertices[0];
            Vector2 last = first;
            foreach (Vector2 vertex in _vertices)
            {
                DrawLine(_spriteBatch, _position, vertex, Color.Red, 1f);
                DrawLine(_spriteBatch, last, vertex, Color.White, 2f);
                last = vertex;

            }

            DrawLine(_spriteBatch, _vertices[^1], first, Color.White, 2f);

        }

        // Helper function to draw a line between 2 points
        private void DrawLine(SpriteBatch _spriteBatch, Vector2 start, Vector2 end, Color color, float thickness = 1f)
        {
            Vector2 edge = end - start;
            float angle = (float)Math.Atan2(edge.Y, edge.X);
            float length = edge.Length();

            _spriteBatch.Draw(_texture,
                new Rectangle((int)start.X, (int)start.Y, (int)length, (int)thickness),
                null,
                color,
                angle,
                new Vector2(0, thickness / 2),
                SpriteEffects.None,
                0
            );
        }

    }
}
