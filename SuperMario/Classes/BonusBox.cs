using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SuperMario.Classes
{
    enum BonusKinds { Coin, Classicmashrum, Greenmashrum }
    class BonusBox:Platform
    {
        private Rectangle boundingBox;
        public BonusKinds Bonus { get; set; }
        public BonusBox(int x,int y): base(x, y)
        {
            
        }
        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("bonus");
            boundingBox = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
        }
    }
}
