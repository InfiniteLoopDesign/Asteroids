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

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {

        _asteroids = new List<Asteroid>();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _pixel = new Texture2D(GraphicsDevice, 1, 1);
        _pixel.SetData(new[] { Color.White });
        for (int i = 0; i < 3; i++) {
            _asteroids.Add( new Asteroid(_pixel, new Vector2(150 + (225 * i), 250) ) );
        }
        _ship = new Ship( _pixel, new Vector2( 400, 300 ) );
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach( Asteroid asteroid in _asteroids ) {
            asteroid.Update(gameTime, GraphicsDevice);
        }

        _ship.Update(gameTime, GraphicsDevice);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        _spriteBatch.Begin();

        foreach( Asteroid asteroid in _asteroids ) {
            // asteroid.Draw(_spriteBatch);
        }

        _ship.Draw(_spriteBatch);

        _spriteBatch.End();

        base.Draw(gameTime);
    }

}
