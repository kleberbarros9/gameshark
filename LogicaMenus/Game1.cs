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
    private IScreen _menuScreen;
    private IScreen _gameScreen;
    private IScreen _creditScreen;
    private IScreen _currentScreen;

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

    public void ChangeScreen(EScreen screenType)
    {
        switch (screenType)
        {
            case EScreen.Menu:
                _currentScreen = _menuScreen;
                break;
            case EScreen.Game:
                _currentScreen = _gameScreen;
                break;
                
                case EScreen.Credit:
                _currentScreen = _creditScreen;
                break;
        }

        _currentScreen.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _menuScreen = new MenuScreen();
        _menuScreen.LoadContent(Content);
        
         _creditScreen = new Credit();
        _creditScreen.LoadContent(Content);

        _gameScreen = new GameScreen();
        _gameScreen.LoadContent(Content);

        _currentScreen = _menuScreen;
    }

    protected override void Initialize()
    {
        base.Initialize();

        Globals.SCREEN_WIDTH = _graphics.PreferredBackBufferWidth;
        Globals.SCREEN_HEIGHT = _graphics.PreferredBackBufferHeight;
        Globals.GameInstance = this;

        _currentScreen.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        _currentScreen.Update((int)gameTime.ElapsedGameTime.TotalSeconds);

        Input.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _currentScreen.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
