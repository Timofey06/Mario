using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperMario.Classes.Bonuses
{
    class GreenMashrum:ClassicMashrum
    {
        public GreenMashrum(Vector2 pos) : base(pos) { textureName = "greenMashrum"; }
    }
}
