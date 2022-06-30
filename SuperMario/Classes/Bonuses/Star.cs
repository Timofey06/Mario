using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperMario.Classes.Bonuses
{
    class Star:BaseBonus
    {
        public Star(Vector2 pos) : base(pos) { textureName = "star"; }
    }
}
