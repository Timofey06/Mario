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
        public Vector2 position { get; set; }
        // protected int speed;
        private Rectangle boundingBox;
        private Rectangle sourceRectangle;
        protected float rotation;
        private Colision colision;
        public Colision ColsionBox
        {
            get => colision;
            set => colision = value;
        }
        public bool HasBonusRoom { get; set; }
        public bool HasDangerousPlant { get; set; }
        // конструктор
        public Pipe(Vector2 position, bool hasbonus, bool hasplant)
        {
            this.position = position;
            texture = null;
            //  speed = 3;
            HasBonusRoom = hasbonus;
            HasDangerousPlant = hasplant;
            rotation = 0;
            colision = new Colision();
            
        }
        // Методы
        public virtual void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("pipe");
            
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height );
            sourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            colision.LoadCollisionContent(content);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(texture, position, sourceRectangle, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            //colision.Draw(spriteBatch);
            
            
        }
        public virtual void Update(GameTime gameTime)
        {
            
            boundingBox = new Rectangle((int)position.X+2, (int)position.Y , texture.Width-4, texture.Height);
            colision.ColisionBoxX = boundingBox;
            colision.ColisionBoxY = boundingBox;
        }
    }
}