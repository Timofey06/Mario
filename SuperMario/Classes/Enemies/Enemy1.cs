﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SuperMario.Classes.Enemies
{

    class Enemy1:Enemy
    {
        private int CurF;
        private int time;
        private bool die;
        public Enemy1(Vector2 pos)
        {
            CurF = 0;
            position = pos;
            vzglad = Vzglad.Levo;
            statusx = StatusX.Go;
            statusy = StatusY.Stay;
            IsAlive = true;
            speed = 1;
            time = 30;
            die = false;
            colision = new Colision();
        }
        public override void LoadContent(ContentManager manager)
        {
            texture = manager.Load<Texture2D>("Enemys");
            colision.LoadCollisionContent(manager);
        }
        public override void Draw(SpriteBatch brushe)
        {
            if (isAlive)
            {
                bound = new Rectangle(0 + 17 * (CurF / 10), 16, 16, 17);
                brushe.Draw(texture, position, bound, Color.White, 0, new Vector2(8, 8),3, SpriteEffects.None, 0);
            }
            else if (!die)
            {
                bound = new Rectangle(36, 16, 16, 17);
                brushe.Draw(texture, position, bound, Color.White, 0, new Vector2(8, 8), 3, SpriteEffects.None, 0);
            }
            //colision.Draw(brushe);
            
        }
        public override void Update(bool top, bool right, bool left, bool down)
        {
            if (IsAlive)
            {
                colision.ColisionBoxX = new Rectangle((int)position.X - 32, (int)position.Y - 24, 64, 48);
                colision.ColisionBoxY = new Rectangle((int)position.X - 24, (int)position.Y - 32, 48, 60);

                if (statusy == StatusY.Fall)
                {
                    if (down)
                    {
                        statusy = StatusY.Stay;
                    }
                    else
                    {
                        position.Y += speed;
                    }
                }

                if (statusx == StatusX.Go)
                {
                    if (vzglad == Vzglad.Levo && !left)
                    {
                        position.X -= speed;
                    }
                    else if (vzglad == Vzglad.Levo)
                    {
                        vzglad = Vzglad.Pravo;
                    }
                    else if (vzglad == Vzglad.Pravo && !right)
                    {
                        position.X += speed;
                    }
                    else
                    {
                        vzglad = Vzglad.Levo;
                    }

                    if (!down)
                    {
                        statusy = StatusY.Fall;
                    }
                }
                CurF++;
                if (CurF >= 20)
                {
                    CurF = 0;
                }
                if (top)
                {
                    IsAlive = false;
                }
            }
            else
            {
                time--;
                if (time<0)
                {
                    die = true;
                }
                colision.ColisionBoxX = new Rectangle(Vector2.Zero.ToPoint(), new Point(0, 0));
                colision.ColisionBoxY = new Rectangle(Vector2.Zero.ToPoint(), new Point(0, 0));
            }
            
        }
    }
}
