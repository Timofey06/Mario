using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperMario.Classes.Bonuses
{
    class Coin:BaseBonus
    {
        public Coin(Vector2 pos):base(pos)
        {
            textureName = "money";
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, 3, SpriteEffects.None, 0);
        }
       
    }
}
