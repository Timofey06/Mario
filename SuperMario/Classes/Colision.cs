using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SuperMario.Classes
{
    enum WhereInteractX
    {
        right,left,none
    }
    enum WhereInteractY
    {
        top, bot, none
    }
    class ColisionContainer
    {
       public WhereInteractX colisionX;
       public WhereInteractY colisionY;
    }
    class Colision
    {
        private Texture2D texture;
        private Rectangle colisioBoxX;
        private Rectangle colisioBoxY;
        public Rectangle ColisionBoxX
        {
            get => colisioBoxX;
            set => colisioBoxX = value;

           
        }
        public Rectangle ColisionBoxY
        {
            get => colisioBoxY;
            set => colisioBoxY = value;


        }
        public Colision()
        {
            colisioBoxX = new Rectangle(0, 0, 1, 1);
            colisioBoxY = new Rectangle(0, 0, 1, 1);
        }
        public Colision(Rectangle collisionRectangleX, Rectangle collisionRectangleY)
        {
            colisioBoxX = collisionRectangleX;
            colisioBoxY = collisionRectangleY;
        }
        public void LoadCollisionContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("colision");
            
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, colisioBoxX, Color.White);
            spriteBatch.Draw(texture, colisioBoxY, Color.White);
        }
        public ColisionContainer InteractWhere(Colision colision)
        {
            ColisionContainer container = new ColisionContainer();
            if (!colisioBoxX.Intersects(colision.colisioBoxX))
            {
                container.colisionX = WhereInteractX.none;
            }
            else if (colisioBoxX.X>colision.colisioBoxX.X)
            {
                container.colisionX = WhereInteractX.left;

            }
            else
            {
                container.colisionX = WhereInteractX.right;
            }
            if (!colisioBoxY.Intersects(colision.colisioBoxY))
            {
                container.colisionY = WhereInteractY.none;
            }
            else if (colisioBoxY.Y > colision.colisioBoxY.Y)
            {
                container.colisionY = WhereInteractY.top;

            }
            else
            {
                container.colisionY = WhereInteractY.bot;
            }
            return container;
        }
    }
}
