using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic; // Necessário para usar List

namespace MonoGameEngine;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Player _player;
    private List<Enemy> _enemies; // Lista de inimigos
    private Texture2D _background;
    private Rectangle _scenePosition;
    private Camera _camera;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _player = new Player();
        _player.LoadContent(Content);

        // Inicializar a lista e adicionar inimigos
        _enemies = new List<Enemy>
        {
            new Enemy(500, 100),
            new Enemy(500, 150), 
            new Enemy(400, 100), 
            new Enemy(600, 200), 

        };
        foreach (var enemy in _enemies)
        {
            enemy.LoadContent(Content);
        }

        _background = Content.Load<Texture2D>("background");
    }

    protected override void Initialize()
    {
        base.Initialize();
        Globals.SCREEN_WIDTH = _graphics.PreferredBackBufferWidth;
        Globals.SCREEN_HEIGHT = _graphics.PreferredBackBufferHeight;

        _scenePosition = new Rectangle
        (
            (Globals.SCREEN_WIDTH - _background.Width) / 2,
            Globals.SCREEN_HEIGHT - _background.Height,
            _background.Width, _background.Height
        );

        _camera = new Camera();
        _player.Initialize();
        foreach (var enemy in _enemies)
        {
            enemy.Initialize();
        }
        Console.WriteLine("Initial Score: " + _player.Score);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Vector2 playerOffset = _player.Update(deltaTime);
        foreach (var enemy in _enemies)
        {
            enemy.Update(deltaTime);
        }

        CheckCollisions(); // Método para verificar colisões.

        _camera.Update(playerOffset, ref _scenePosition);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, _scenePosition, Color.White);
        _player.Draw(_spriteBatch);

        foreach (var enemy in _enemies)
        {
            if (enemy.IsAlive)
            {
                enemy.Draw(_spriteBatch);
            }
        }

        _spriteBatch.End();

        base.Draw(gameTime);
    }

    private void CheckCollisions()
    {
        foreach (var enemy in _enemies)
        {
            if (enemy.IsAlive && _player.Position.Intersects(enemy.Position))
            {
                enemy.IsAlive = false;
                _player.Score += 1;
                Console.WriteLine("Score Updated: " + _player.Score);
            }
        }
    }
}
