using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids;

public class Asteroid : GameObject
{
    
    private static readonly Random _rand = new Random();
    private static int MIN_RADIUS = 40;
    private static int MAX_RADIUS = 70;
    private KeyboardState _prevKeyboardState;



    public Asteroid( Texture2D texture ) : base(texture, new(200, 200)) { }

    public Asteroid( Texture2D texture, Vector2 position ) : base(texture, new(200, 200))
    {

        debug_init_position = position;
        Init();

    }

    private void Init() {

        _rotation = (float) (0.3f + _rand.NextDouble() * (1.3f - 0.3f));
        _rotation *= _rand.Next(2) == 0 ? 1 : -1;
        _rotationAngle = 0f;
        _velocity = new Vector2( 
            (float) (-3f + _rand.NextDouble() * (6f)),
            (float) (-3f + _rand.NextDouble() * (6f))
        );
        _edges = _rand.Next(7, 13);
        _scale = (float) (0.5f + _rand.NextDouble() * (1.5f - 0.5f));
        _thickness = 2f;
        GenerateVertices();        
        UpdateDrawVertices();

    }

    private void GenerateVertices() {

        _vertices = new List<Vector2>();
        _offsetVertices = new List<Vector2>();

        float angleStep = MathHelper.TwoPi / _edges;

        for (int i = 0; i < _edges; i++)
        {
            float angle = i * angleStep;
            float length = (float)(MIN_RADIUS + _rand.NextDouble() * (MAX_RADIUS - MIN_RADIUS));
            length *= _scale;
            if ( maxLength < length )
                maxLength = length;

            float x = (float)Math.Cos(angle) * length;
            float y = (float)Math.Sin(angle) * length;

            _vertices.Add(new Vector2(x, y));
            _offsetVertices.Add(new Vector2(x, y));
        }

    }

    private void UpdateDrawVertices() {
        // Update the draw vertex positions based on position and rotation
        for (int i = 0; i < _vertices.Count; i++)
        {
            float cos = (float)Math.Cos(_rotationAngle);
            float sin = (float)Math.Sin(_rotationAngle);

            // Apply 2D rotation transformation
            float xNew = _offsetVertices[i].X * cos - _offsetVertices[i].Y * sin;
            float yNew = _offsetVertices[i].X * sin + _offsetVertices[i].Y * cos;

            _vertices[i] = _position + new Vector2(xNew, yNew);
        }
    }

    public override void Update(GameTime gameTime, GraphicsDevice graphicsDevice)
    {

        // Update position based on velocity
        _position = _position + _velocity;

        // Loop position of Asteroids off screen
        if ( _position.Y < 0-maxLength )  
            _position.Y = graphicsDevice.Viewport.Height+maxLength;
        if ( _position.Y > graphicsDevice.Viewport.Height+maxLength )  
            _position.Y = 0-maxLength;
        if ( _position.X < 0-maxLength )  
            _position.X = graphicsDevice.Viewport.Width+maxLength;
        if ( _position.X > graphicsDevice.Viewport.Width+maxLength )  
            _position.X = 0-maxLength;

        // Rotation angle variable
        _rotationAngle += _rotation * (float) gameTime.ElapsedGameTime.TotalSeconds;
        UpdateDrawVertices();

        // Reset Function for testing purposes, re-initialize the Asteroid
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.R) && _prevKeyboardState.IsKeyUp(Keys.R))
        {
            _position = debug_init_position;
            Init(); 
        }
        _prevKeyboardState = keyboardState;

    }

}