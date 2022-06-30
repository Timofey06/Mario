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
        public Texture2D texture;
        public Vector2 position;
        private Rectangle boundingBox;
        private Rectangle destinationRectangle;
        public Rectangle DestinationRectangle { get { return destinationRectangle; } set { destinationRectangle = value; } }
        public Rectangle BoundingBox { get { return boundingBox; } set { destinationRectangle = value; } }
        public Vector2 Position { get { return position; } set { position = value; } }
        public Platform(int x, int y)
        {
            texture = null;
            position = new Vector2(x, y);
        }

        public virtual void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("pl");
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
        }
        public virtual void Update()
        {
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width - 20, texture.Height - 30);
        }
    }
}