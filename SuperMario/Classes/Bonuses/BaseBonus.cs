using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperMario.Classes.Bonuses
{
    
    class BaseBonus
    {
        protected Texture2D texture;
        protected string textureName;
        public Vector2 position { get; set; }
        public Texture2D Texture { get { return texture; } }
        
        public BaseBonus(Vector2 pos)
        {
            position = pos;
        }
        public virtual void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(textureName);

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
        }
    }
}
