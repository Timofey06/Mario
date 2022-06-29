using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using SuperMario.Classes;
using SuperMario.Classes.Enemies;
using System.Collections.Generic;
namespace SuperMario
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Player player;
        private Map map = new Map();
        private List<Enemy> enemies = new List<Enemy> { new Enemy1(new Vector2(1000, 500)), new Enemy2(new Vector2(2000, 500)) };
        private bool intersectsTop = false;
        private bool intersectsBottom = false;
        private bool intersectsRight = false;
        private bool intersectsLeft = false;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.IsFullScreen = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map.Initialize();
            player = new Player(new Vector2(100, 500));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            map.LoadContent(Content);
            
            foreach (var item in enemies)
            {
                item.LoadContent(Content);
            }
            player.LoadContent(Content);


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            map.Update(gameTime);
           


            
            UpdateColision();
            
           
            // TODO: Add your update logic here

            base.Update(gameTime);
        }
       
        public void UpdateColision()
        {
            intersectsTop = false;
            intersectsBottom = false;
            intersectsRight = false;
            intersectsLeft = false;
            //enemy and fireball
            for (int i = 0; i < player.Balls.Count; i++)
            {
                player.Balls[i].Update();
                if (!player.Balls[i].Visible)
                {
                    player.Balls.RemoveAt(i);
                    i--;
                }
                foreach (var enemy in enemies)
                {
                    if (i >= 0)
                    {
                        if (enemy.colision.ColisionBoxX.Intersects(player.Balls[i].collision) && enemy.IsAlive)
                        {
                            enemy.IsAlive = false;
                            player.Balls[i].Visible = false;
                        }
                    }
                }
            }
            //player and map
            foreach (var col in map.objectsColision)
            {
                ColisionContainer where = player.ColisionBox.InteractWhere(col);
                if (where.colisionY == WhereInteractY.top)
                {
                    intersectsTop = true;
                }
                else if (where.colisionY == WhereInteractY.bot)
                {
                    intersectsBottom = true;
                }
                if (where.colisionX == WhereInteractX.left)
                {
                    intersectsLeft = true;
                }
                else if (where.colisionX == WhereInteractX.right)
                {
                    intersectsRight = true;
                }
            }

           

            //enemy and map
            foreach (var enem in enemies)
            {
                bool top = false, right = false, left = false, down = false;
                foreach (var col in map.objectsColision)
                {
                    ColisionContainer where = enem.colision.InteractWhere(col);
                    if (where.colisionY == WhereInteractY.top)
                    {
                        top = true;
                    }
                    else if (where.colisionY == WhereInteractY.bot)
                    {
                        down = true;
                    }
                    if (where.colisionX == WhereInteractX.left)
                    {
                        left = true;
                    }
                    else if (where.colisionX == WhereInteractX.right)
                    {
                        right = true;
                    }
                }
                enem.Update(top, right, left, down);
            }
            //player and enimies

            foreach (var enem in enemies)
            {
                
                ColisionContainer where = enem.colision.InteractWhere(player.ColisionBox);
                if (where.colisionY == WhereInteractY.top)
                {
                    enem.IsAlive = false;
                    intersectsBottom = true;
                }
                else if (where.colisionY == WhereInteractY.bot)
                {
                    player.IsAlive = false;
                }
                if (where.colisionX == WhereInteractX.left)
                {
                    player.IsAlive = false;
                }
                else if (where.colisionX == WhereInteractX.right)
                {
                    player.IsAlive = false;
                }
            }
            player.Update(intersectsTop, intersectsRight, intersectsLeft, intersectsBottom);
        }
        protected override void Draw(GameTime gameTime)
        {
           
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            map.Draw(_spriteBatch);
            player.Draw(_spriteBatch);
            foreach (var ball in player.Balls)
            {
                ball.Draw(_spriteBatch);
            }
            foreach (var item in enemies)
            {
                item.Draw(_spriteBatch);
            }
            
            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
