using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Text;
using SuperMario.Classes;
using SuperMario.Classes.Enemies;
using SuperMario.Classes.UI;
namespace SuperMario
{
    public enum GameState
    {
        Game, GameOver
    }
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Map map = new Map();
        private Player player = new Player(new Vector2(500, 500));
        private bool intersectsTop = false;
        private bool intersectsBottom = false;
        private bool intersectsRight = false;
        private bool intersectsLeft = false;
        private List<Enemy> enemies = new List<Enemy>();
        private List<Vector2> enemies_pos = new List<Vector2>();
        private Random r = new Random();
        private Texture2D square;
        private HUD hud = new HUD();
        private Song song;
        public static GameState gameState = GameState.Game;
        private GameOver gameOver = new GameOver();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            //_graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferWidth = 1920;
            _graphics.PreferredBackBufferHeight = 1080;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            map.Initialize();
            GenerateEnemies();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            map.LoadContent(Content);
            square = Content.Load<Texture2D>("square");
            player.LoadContent(Content);
            hud.LoadContent(Content);
            song = Content.Load<Song>("baseMusik");
            // начинаем проигрывание мелодии
            MediaPlayer.Volume = 50;
            MediaPlayer.Play(song);
            // повторять после завершения
            MediaPlayer.IsRepeating = true;
        }
        
       
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch (gameState)
            {
                case GameState.Game:
                    map.Update(gameTime);
                    intersectsTop = false;
                    intersectsBottom = false;
                    intersectsRight = false;
                    intersectsLeft = false;
                    for (int i = 0; i < player.Balls.Count; i++)
                    {
                        player.Balls[i].Update();
                        if (!player.Balls[i].Visible)
                        {
                            player.Balls.RemoveAt(i);
                            i--;
                        }
                    }
                    for (int i = 0; i < 4; i++)
                    {
                        if (map.groundboundingBox.Intersects(player.Collision[i]))
                        {
                            ChangeIntersects(i);
                        }
                    }
                    foreach (var o in map.Pipes)
                    {
                        for (int i = 0; i < 4; i++)
                        {
                            if (o.BoundingBox.Intersects(player.Collision[i]))
                            {
                                ChangeIntersects(i);
                            }
                        }
                    }
                    foreach (var o in map.Platforms)
                    {
                        foreach (var p in o)
                        {
                            for (int i = 0; i < 4; i++)
                            {
                                if (p.BoundingBox.Intersects(player.Collision[i]))
                                {
                                    ChangeIntersects(i);
                                }
                            }
                        }
                    }
                    player.Update(intersectsTop, intersectsRight, intersectsLeft, intersectsBottom);
                    hud.Update(gameTime, player.Coins);
                    break;
                case GameState.GameOver:
                    gameOver.Draw(_spriteBatch);
                    break;
                default:
                    break;
            }
            
            base.Update(gameTime);
        }
        private void ChangeIntersects(int i)
        {
            switch (i)
            {
                case 0:
                    intersectsTop = true;
                    break;
                case 1:
                    intersectsRight = true;
                    break;
                case 2:
                    intersectsLeft = true;
                    break;
                case 3:
                    intersectsBottom = true;
                    break;
                default:
                    break;
            }
        }
        public void GenerateEnemies()
        {
            int max= (int)map.Pipes[1].position.X -48;
            int min=(int)map.Pipes[0].position.X+48;
            for (int i = 0; i < map.Pipes.Count-2; i++)
            {
                enemies_pos.Add(new Vector2(r.Next(min, max + 1), 900));
                min = (int)map.Pipes[i+1].position.X + 60;
                max = (int)map.Pipes[i+2].position.X - 50;
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            map.Draw(_spriteBatch);
            player.Draw(_spriteBatch);
            foreach (var ball in player.Balls)
            {
                ball.Draw(_spriteBatch);
            }
            foreach (var item in enemies_pos)
            {
                _spriteBatch.Draw(square, item, Color.White);
            }
            hud.Draw(_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
