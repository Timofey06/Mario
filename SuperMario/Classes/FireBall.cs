using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SuperMario.Classes
{
    class FireBall
    {
        private Texture2D texture;

        private Vector2 position;
        private int speed;
        private int CurF;
        public Rectangle bound;
        Vzglad napravlenie;
        public bool Visible = true;
        public FireBall(Vector2 pos, Vzglad napravlenie)
        {
            speed = 6;
            position = pos;

            bound = new Rectangle(0, 0, 8, 8);
            texture = null;
            this.napravlenie = napravlenie;

            CurF = 0;


        }
        public void LoadContent(Texture2D texture)
        {
            this.texture = texture;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            bound = new Rectangle(172 + 10 * (CurF / 10), 77, 8, 8);
            SpriteEffects effect = SpriteEffects.None;
            if (napravlenie == Vzglad.Levo)
            {
                effect = SpriteEffects.FlipHorizontally;
            }
            spriteBatch.Draw(texture, position, bound, Color.White, 0, new Vector2(4, 4), 3, effect, 0);
        }
        public void Update()
        {
            CurF++;
            if (CurF >= 31)
            {
                CurF = 0;
            }
            if (napravlenie == Vzglad.Pravo)
            {
                position.X += speed;
            }
            else
            {
                position.X -= speed;
            }

            if (position.X >= 1920)
            {
                Visible = false;
            }
            else if (position.X <= -8)
            {
                Visible = false;
            }
        }
    }
}