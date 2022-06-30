using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperMario.Classes.UI
{
    class Label
    {
        private SpriteFont spriteFont;
        private Color color_default;
        public Vector2 Position { get; set; }
        public string Text { get; set; }
        public Color Color { get; set; }
        public Label()
        {
            Position = new Vector2(100, 0);
            Text = "Label";
            Color = Color.White;
            color_default = Color;
        }
        public Label(Vector2 position)
        {
            Position = position;
            Color = Color.White;
            color_default = Color;
        }
        public Label(Vector2 position, string text)
        {
            Text = text;
            Position = position;
            Color = Color.White;
            color_default = Color;
        }
        public void LoadContent(ContentManager content)
        {
            spriteFont = content.Load<SpriteFont>("File");
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont, Text, Position, Color);
        }
        public void ResetColor()
        {
            Color = color_default;
        }
    }
}
