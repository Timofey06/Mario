using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading;
namespace SuperMario.Classes.UI
{
    class HUD
    {
        Label coins_lbl;
        Label timer_lbl;
        double timer=60;
        
        public HUD()
        {
            coins_lbl = new Label(new Vector2(100,50));
            timer_lbl = new Label(new Vector2(200, 50));
            Thread thr = new Thread(ChangeTimer);
            thr.Start();
        }
        public void Update(GameTime time, int coins)
        {
            coins_lbl.Text = "Coins:\n"+coins.ToString();
            
        }
        private  void ChangeTimer()
        {
            while (true)
            {
                Thread.Sleep(1000);
                timer -= 1;
                timer_lbl.Text = "Time:\n" + timer.ToString();
            }
            

        }
        public void LoadContent(ContentManager content)
        {
            coins_lbl.LoadContent(content);
            timer_lbl.LoadContent(content);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            coins_lbl.Draw(spriteBatch);
            timer_lbl.Draw(spriteBatch);
        }
    }
}
