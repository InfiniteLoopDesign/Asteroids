using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids;

public class Asteroid
{
    private static readonly Random _rand = new Random();
    private static int MIN_RADIUS = 40;
    private static int MAX_RADIUS = 70;
    private KeyboardState _prevKeyboardState;

    private Texture2D _texture;
    private float _rotation, _rotationAngle;
    private Vector2 _velocity, _position;
    private int _edges;
    private float _scale;

    private List<Vector2> _initialVertices;
    private List<Vector2> _vertices;

    public Asteroid( Texture2D texture ) : this(texture, new(200, 200)) { }

    public Asteroid( Texture2D texture, Vector2 position ) {

        _texture = texture;
        _position = position;
        Init();

    }

    private void Init() {

        _rotation = (float) (0.3f + _rand.NextDouble() * (1.3f - 0.3f));
        _rotation *= _rand.Next(2) == 0 ? 1 : -1;
        _rotationAngle = 0f;
        _velocity = Vector2.Zero;
        _edges = _rand.Next(7, 13);
        _scale = (float) (0.5f + _rand.NextDouble() * (1.5f - 0.5f));
        GenerateVertices();

    }

    private void GenerateVertices() {

        _vertices = new List<Vector2>();
        _initialVertices = new List<Vector2>();

        float angleStep = MathHelper.TwoPi / _edges;

        for (int i = 0; i < _edges; i++)
        {
            float angle = i * angleStep;
            float length = (float)(MIN_RADIUS + _rand.NextDouble() * (MAX_RADIUS - MIN_RADIUS));
            length *= _scale;

            float x = _position.X + (float)Math.Cos(angle) * length;
            float y = _position.Y + (float)Math.Sin(angle) * length;

            _vertices.Add(new Vector2(x, y));
            _initialVertices.Add(new Vector2(x, y));
        }

    }

    public void Update(GameTime gameTime)
    {
        _rotationAngle += _rotation * (float) gameTime.ElapsedGameTime.TotalSeconds; // Update rotation angle

        for (int i = 0; i < _vertices.Count; i++)
        {
            Vector2 relativePos = _initialVertices[i] - _position; // Get relative position
            float cos = (float)Math.Cos(_rotationAngle);
            float sin = (float)Math.Sin(_rotationAngle);

            // Apply 2D rotation transformation
            float xNew = relativePos.X * cos - relativePos.Y * sin;
            float yNew = relativePos.X * sin + relativePos.Y * cos;

            _vertices[i] = new Vector2(_position.X + xNew, _position.Y + yNew);
        }

        // Reset Function for testing purposes, re-initialize the Asteroid
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.R) && _prevKeyboardState.IsKeyUp(Keys.R))
        {
            Init(); 
        }
        _prevKeyboardState = keyboardState;

    }

    public void Draw(SpriteBatch _spriteBatch) {

        Vector2 first = _vertices[0];
        Vector2 last = first;
        foreach( Vector2 vertex in _vertices )
        {
            // DrawLine(_spriteBatch, _position, vertex, Color.Red, 1f);
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