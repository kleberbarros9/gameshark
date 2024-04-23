using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class GameScreen : IScreen
{
    private GameObject _background;

    public void LoadContent(ContentManager content)
    {
        Texture2D backgroundImage = content.Load<Texture2D>("background");
        _background = new GameObject(backgroundImage);

        _player = new Player();
        _player.LoadContent(Content);
    }

    public void Initialize()
    {
    }

    public void Update(float deltaTime)
    {
        if (Input.GetKeyDown(Keys.Escape))
        {
            Globals.GameInstance.ChangeScreen(EScreen.Menu);
        }
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        _background.Draw(spriteBatch);
    }
}
