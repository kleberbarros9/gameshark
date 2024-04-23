using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Enemy
{
    private Texture2D[] _images;
    private int _index;
    private Rectangle _position;
    private const float SPEED = 200;
    private Timer _timer;
    private Rectangle _movementBounds;


    private bool _isAlive = true; // Inicialmente o inimigo está vivo.

    public bool IsAlive
    {
        get { return _isAlive; }
        set { _isAlive = value; }
    }

    public Rectangle Position
    {
        get { return _position; }
    }


    public int X { get; private set; }
    public int Y { get; private set; }

    // Construtor que aceita dois floats como parâmetros
    public Enemy(int x, int y)
    {
        X = x;
        Y = y;
    }



    public void LoadContent(ContentManager content)
    {
        _images = new Texture2D[8]
        {
            content.Load<Texture2D>("shark/shark01"), 
            content.Load<Texture2D>("shark/shark02"), 
            content.Load<Texture2D>("shark/shark03"), 
            content.Load<Texture2D>("shark/shark04"), 
            content.Load<Texture2D>("shark/shark05"), 
            content.Load<Texture2D>("shark/shark06"), 
            content.Load<Texture2D>("shark/shark07"), 
            content.Load<Texture2D>("shark/shark08")
        };
    }

    public void Initialize()
    {
        _index = 0;
        _position = new Rectangle(this.X, this.Y, _images[0].Width, _images[0].Height);
        _timer = new Timer();
        _timer.Start(IncrementIndex, 0.075f, true);
        _movementBounds = new Rectangle
        (
            Globals.SCREEN_WIDTH/8, Globals.SCREEN_HEIGHT/4,
            (Globals.SCREEN_WIDTH/4) * 3, (Globals.SCREEN_HEIGHT/4) * 3
        );
    }


        public Vector2 Update(float deltaTime)
    {


        Vector2 offset = Vector2.Zero;

        _timer.Update(deltaTime);

        return offset;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_images[_index], _position, Color.White);
    }

    private void IncrementIndex()
    {
        _index++;
        if (_index > 7)
        {
            _index = 0;
        }
    }
}
