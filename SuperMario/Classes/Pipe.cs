using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SuperMario.Classes
{
    class Pipe
    {
        protected Texture2D texture;
        public Vector2 position;
       // protected int speed;
        private Rectangle boundingBox;
        private Rectangle destinationRectangle;
        protected float rotation;
        public Rectangle BoundingBox { get { return boundingBox; } }
        public bool HasBonusRoom { get; set; }
        public bool HasDangerousPlant { get; set; }
        public Texture2D Texture { get { return texture; } }
        // конструктор
        public Pipe(Vector2 position, bool hasbonus, bool hasplant)
        {
            this.position = position;
            texture = null;
            //  speed = 3;
            HasBonusRoom = hasbonus;
            HasDangerousPlant = hasplant;
            rotation = 0;
        }
        // Методы
        public virtual void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("pipe");
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y+20, texture.Width, texture.Height);
            boundingBox = new Rectangle((int)position.X, (int)position.Y-52, texture.Width, texture.Height+80);
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // spriteBatch.Draw(texture, position, Color.White);
            destinationRectangle = new Rectangle((int)position.X, (int)position.Y+20, texture.Width , texture.Height);
            Vector2 origin = new Vector2(destinationRectangle.Width / 2, destinationRectangle.Height / 2);
            spriteBatch.Draw(texture, destinationRectangle, null, Color.White, rotation, origin, SpriteEffects.None, 1);
        }
        public virtual void Update(GameTime gameTime)
        {
            boundingBox = new Rectangle((int)position.X, (int)position.Y-52, texture.Width, texture.Height +80);
        }
    }
}
