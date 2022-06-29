using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SuperMario.Classes
{
    public class Platform
    {
        int speed;
        public Texture2D texture;
        Vector2 position;
        Vector2 position2;
        private Rectangle boundingBox;
        private Rectangle destinationRectangle;
        public Rectangle DestinationRectangle { get { return destinationRectangle; } set { destinationRectangle = value; } }
        public Rectangle BoundingBox { get { return boundingBox; } set { destinationRectangle = value; } }
        public Platform(int x, int y)
        {
            speed = 7;
            texture = null;
            position = new Vector2(x, y);
            // position2 = new Vector2(-800, 0);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("platform");
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(texture, position, Color.White);
            //  _spriteBatch.Draw(texture, position2, Color.White);
        }
        public void Update()
        {
            /*position.X += speed;
            position2.X += speed;
            if (position.X >= 800)
            {
                position.X = 0;
                position2.X = -800;
            }*/
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width - 20, texture.Height - 30);
        }
    }
}

