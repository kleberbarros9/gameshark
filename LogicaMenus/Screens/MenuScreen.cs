using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class MenuScreen : IScreen
{
    private Button _playButton;
    private Button _exitButton;
private Button _creditButton;
     private GameObject background;
    public void LoadContent(ContentManager content)
    {
        _playButton = new Button(content.Load<Texture2D>("play_button"), Play);
        _exitButton = new Button(content.Load<Texture2D>("exit_button"), Exit);
        _creditButton = new Button(content.Load<Texture2D>("creditosButton"), Credit);

       Texture2D backgroundImg = content.Load<Texture2D>("praia");
        background = new GameObject(backgroundImg);
    }

    public void Initialize()
    {
        _playButton.Position = new Point((Globals.SCREEN_WIDTH - _playButton.Bounds.Width)/2, 200);
        _exitButton.Position = new Point((Globals.SCREEN_WIDTH - _exitButton.Bounds.Width)/2, 250);
        _creditButton.Position = new Point((Globals.SCREEN_WIDTH - _exitButton.Bounds.Width)/2, 300);
    }

    public void Update(float deltaTime)
    {
        _playButton.Update(deltaTime);
        _exitButton.Update(deltaTime);
        _creditButton.Update(deltaTime);

    }

    public void Draw(SpriteBatch spriteBatch)
    {
        background.Draw(spriteBatch);
        _playButton.Draw(spriteBatch);
        _exitButton.Draw(spriteBatch);
        _creditButton.Draw(spriteBatch);

    }

    private void Play()
    {
        Globals.GameInstance.ChangeScreen(EScreen.Game);
    }

    private void Exit()
    {
        Globals.GameInstance.Exit();
    }

 private void Credit()
 {
    Globals.GameInstance.ChangeScreen(EScreen.Credit);
 }

}
