using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SuperMario.Classes
{
    enum Vzglad
    {
        Pravo, Levo
    }
    enum StatusX
    {
        Stay, Go
    }
    enum StatusY
    {
        Stay, Jump, Fall
    }
    class Player
    {
        private Vector2 position;
        private Texture2D texture;
        private int speed;
        private Vzglad vzglad;
        private Rectangle boundingBox;
        private Rectangle[] collision = new Rectangle[4];
        private int score;
        private Vector2 size;
        private StatusX statusx;
        private StatusY statusy;
        private int curF;
        private bool freecam;
        private int up;
        private int interval;
        private List<FireBall> balls = new List<FireBall>();
        public int Coins { get; set; }
        public List<FireBall> Balls
        {
            get => balls;
            set => balls = value;
        }
        public Vector2 Position
        {
            get => position;

        }
        public Rectangle[] Collision
        {
            get => collision;
        }
        public Player(Vector2 pos)
        {
            position = pos;
            texture = null;
            speed = 3;
            vzglad = Vzglad.Pravo;
            size = new Vector2(16, 16);
            boundingBox = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);
            statusx = StatusX.Stay;
            freecam = true;
            interval = 0;
            Coins = 0;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Mario");
        }
        public void Draw(SpriteBatch brushe)
        {
            Rectangle sourceRectangle = new Rectangle();
            SpriteEffects effect = SpriteEffects.None;
            switch (vzglad)
            {
                case Vzglad.Pravo:
                    break;
                case Vzglad.Levo:
                    effect = SpriteEffects.FlipHorizontally;
                    break;
                default:
                    break;
            }

            if (statusx == StatusX.Stay && statusy == StatusY.Stay)
            {
                sourceRectangle = new Rectangle(0, 8, 16, 16);
            }
            else if (statusx == StatusX.Go && statusy == StatusY.Stay)
            {
                sourceRectangle = new Rectangle(20 * (curF / 10), 8, 16, 16);
            }
            else if (statusy == StatusY.Jump || statusy == StatusY.Fall)
            {
                sourceRectangle = new Rectangle(96, 8, 16, 16);
            }

            brushe.Draw(texture, position, sourceRectangle, Color.White,
                     0, new Vector2(8, 8), 3, effect, 0);

        }
        public void Update(bool top, bool right, bool left, bool down)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            collision[0] = new Rectangle((int)position.X, (int)position.Y, 48, 1);
            collision[1] = new Rectangle((int)position.X + 48, (int)position.Y + 5, 3, 38);
            collision[2] = new Rectangle((int)position.X - 3, (int)position.Y + 5, 3, 38);
            collision[3] = new Rectangle((int)position.X, (int)position.Y + 48, 48, 1);
            if (statusy == StatusY.Fall)
            {
                up = 0;
                if (down)
                {
                    statusy = StatusY.Stay;
                }
                else
                {
                    position.Y += speed;
                }
            }
            else if (statusy == StatusY.Jump)
            {
                if (top || !keyboardState.IsKeyDown(Keys.W))
                {
                    statusy = StatusY.Fall;
                }
                else
                {
                    position.Y -= speed;
                    up += speed;
                    if (up >= 200)
                    {
                        statusy = StatusY.Fall;
                        up = 0;
                    }
                }
            }
            else if (statusy == StatusY.Stay)
            {
                if (!down)
                {
                    statusy = StatusY.Fall;
                }
                else if (keyboardState.IsKeyDown(Keys.W) && !top)
                {
                    statusy = StatusY.Jump;
                }
                up = 0;
            }

            if (!right && keyboardState.IsKeyDown(Keys.D))
            {
                statusx = StatusX.Go;
                position.X += speed;
                curF++;
                vzglad = Vzglad.Pravo;
            }
            else if (!left && keyboardState.IsKeyDown(Keys.A))
            {
                statusx = StatusX.Go;
                position.X -= speed;
                curF++;
                vzglad = Vzglad.Levo;
            }
            else
            {
                curF = 0;
            }

            if (curF >= 31)
            {
                curF = 0;
            }
            if (keyboardState.IsKeyDown(Keys.Space) && interval <= 0)
            {
                interval = 200;
                SpawFireBall();
            }
            else
            {
                interval--;
            }
        }
        private void SpawFireBall()
        {
            FireBall fire = new FireBall(position, vzglad);
            fire.LoadContent(texture);
            balls.Add(fire);
        }

    }
}