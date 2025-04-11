using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _pixel;

    private List<Asteroid> _asteroids;

    private Ship _ship;

    private Projectile _missle;
    private KeyboardState _prevKeyboardState;

    private List<GameObject> _drawable;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        _drawable = new List<GameObject>();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
        for (int i = 0; i < 3; i++) {
            _drawable.Add( new Asteroid(_pixel, new Vector2(150 + (225 * i), 250) ) );
        }
        _ship = new Ship( _pixel, new Vector2(400, 300) );
        _drawable.Add( _ship );

        _missle = new Projectile( _pixel, new Vector2(-10, -10), 0f );
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach( GameObject item in _drawable) {
            item.Update(gameTime, GraphicsDevice);
        }
        _missle.Update(gameTime, GraphicsDevice);

        // Reset Function for testing purposes, re-initialize the Asteroid
        KeyboardState keyboardState = Keyboard.GetState();
        if (keyboardState.IsKeyDown(Keys.Space) && _prevKeyboardState.IsKeyUp(Keys.Space))
        {
            _missle = new Projectile( _pixel, _ship.GetHardpoint(), _ship.GetRotationAngle() );
        }
        _prevKeyboardState = keyboardState;

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        foreach (GameObject item in _drawable)
        {
            item.Draw(_spriteBatch);
        }
        _missle.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

}
