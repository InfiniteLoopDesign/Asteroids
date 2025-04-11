using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids;

public class Projectile : GameObject
{
    
    private static readonly Random _rand = new Random();
    private static int MIN_RADIUS = 40;
    private static int MAX_RADIUS = 70;


    public Projectile( Texture2D texture, Vector2 position, float rotationAngle ) : base(texture, position) {

        _rotationAngle = rotationAngle;
        _velocity = new Vector2(
            (float) Math.Cos(_rotationAngle) * 10,
            (float) Math.Sin(_rotationAngle) * 10
        );
        _thickness = 6f;

    }

    private void Init() {

        float cos = (float)Math.Cos(_rotationAngle);
        float sin = (float)Math.Sin(_rotationAngle);

    }

    public override void Update(GameTime gameTime, GraphicsDevice graphicsDevice) {

        // Update position based on velocity
        _position = _position - _velocity;

    }

    public override void Draw(SpriteBatch _spriteBatch) {

        float offset = _thickness / 2f;
        _spriteBatch.Draw( 
            _texture, 
            new Rectangle(
                (int) (_position.X - offset), 
                (int) (_position.Y - offset), 
                (int) _thickness, 
                (int) _thickness
            ), 
            Color.White 
        );

    }

}