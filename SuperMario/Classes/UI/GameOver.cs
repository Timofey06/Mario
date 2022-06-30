using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SuperMario.Classes.UI
{
    class GameOver
    {
        private Label label;
        public Vector2 position;
        private bool isReset = false;

        public bool IsReset { get { return isReset; } set { isReset = value; } }
        public GameOver()
        {
            position = new Vector2(250, 250);
            label = new Label(position, "GameOver");
        }
        public void LoadContent(ContentManager content)
        {
            label.LoadContent(content);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            label.Draw(spriteBatch);
        }
        public void Update()
        {
            // проверка
            //if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            //{
            //    Game1.gameState = GameState.Menu;
            //    isReset = true;
            //}
        }
    }
}
