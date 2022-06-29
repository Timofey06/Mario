using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SuperMario.Classes
{
    abstract class Enemy
    {
         protected Vector2 position;
         protected Rectangle bound;
         
         protected Texture2D texture;
         protected int speed;
         protected Vzglad vzglad;
         protected StatusX statusx;
         protected StatusY statusy;
         protected bool isAlive;
         public Colision colision { get; set; }
         public Vector2 Position
         {
            get => position;
            set => position=value;
         }
        public bool IsAlive
        {
            get => isAlive;
            set => isAlive = value;
        }
        
        abstract public void LoadContent(ContentManager manager);
        abstract public void Draw(SpriteBatch brushe);
        abstract public void Update(bool top,bool right, bool left, bool down);
        
    }
}
