using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameEngine;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Player _player;
    private Enimy _enimy;
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

        _enimy = new Enimy();
        _enimy.LoadContent(Content);

        _background = Content.Load<Texture2D>("background");
    }

    protected override void Initialize()
    {
        base.Initialize();
        
        Globals.SCREEN_WIDTH = _graphics.PreferredBackBufferWidth;
        Globals.SCREEN_HEIGHT = _graphics.PreferredBackBufferHeight;

        _scenePosition = new Rectangle
        (
            (Globals.SCREEN_WIDTH - _background.Width) / 2, Globals.SCREEN_HEIGHT - _background.Height,
            _background.Width, _background.Height
        );

        _camera = new Camera();
        _player.Initialize();
        _enimy.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
        Vector2 playerOffset = _player.Update(deltaTime);
        Vector2 enimeOffset = _enimy.Update(deltaTime);
        _camera.Update(playerOffset, ref _scenePosition);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        _spriteBatch.Draw(_background, _scenePosition, Color.White);
        _player.Draw(_spriteBatch);
        _enimy.Draw(_spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
