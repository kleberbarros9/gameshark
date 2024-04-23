using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class Player
{
    private Texture2D[] _images;
    private int _index;
    private Rectangle _position;
    private const float SPEED = 200;
    private Timer _timer;
    private Rectangle _movementBounds;
    public int Score { get; set; }

        public Rectangle Position
    {
        get { return _position; }
    }

    public void LoadContent(ContentManager content)
    {
        _images = new Texture2D[5]
        {
            content.Load<Texture2D>("nave/nave1"), content.Load<Texture2D>("nave/nave2"),
            content.Load<Texture2D>("nave/nave3"), content.Load<Texture2D>("nave/nave4"),
            content.Load<Texture2D>("nave/nave5")
        };
    }

    public void Initialize()
    {
        _index = 0;
        _position = new Rectangle(200, 200, _images[0].Width+10, _images[0].Height+10);
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
        Vector2 direction = Vector2.Zero;

        if (Input.GetKey(Keys.W))
        {
            direction.Y = direction.Y - 1.0f;
        }
        if (Input.GetKey(Keys.S))
        {
            direction.Y = direction.Y + 1.0f;
        }
        if (Input.GetKey(Keys.A))
        {
            direction.X = direction.X - 1.0f;
        }
        if (Input.GetKey(Keys.D))
        {
            direction.X = direction.X + 1.0f;
        }

        Vector2 offset = Vector2.Zero;

        if (direction != Vector2.Zero)
        {
            direction.Normalize();
            offset = direction * SPEED * deltaTime;

            Rectangle newPosition = _position;
            newPosition.X += (int)offset.X;
            newPosition.Y += (int)offset.Y;

            if (newPosition.X > _movementBounds.X && newPosition.Right < _movementBounds.Right)
            {
                _position.X = newPosition.X;
                offset.X = 0;
            }
            if (newPosition.Y > _movementBounds.Y && newPosition.Bottom < _movementBounds.Bottom)
            {
                _position.Y = newPosition.Y;
                offset.Y = 0;
            }
        }

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
        if (_index > 4)
        {
            _index = 0;
        }
    }
}
