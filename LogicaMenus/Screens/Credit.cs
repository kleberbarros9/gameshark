using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Credit : IScreen
{
    private GameObject _creditScreen;

    public void LoadContent(ContentManager content)
    {
        Texture2D creditImg = content.Load<Texture2D>("creditos");
        _creditScreen = new GameObject(creditImg);
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
        _creditScreen.Draw(spriteBatch);
    }
}
