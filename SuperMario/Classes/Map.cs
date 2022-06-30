using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SuperMario.Classes.Bonuses;
namespace SuperMario.Classes
{
    class Map
    {
        public Rectangle groundboundingBox { get; set; }
        private Random r = new Random();
        public List<Platform> Ground { get; set; }
        public List<Pipe> Pipes { get; set; }
        public List<List<Platform>> Platforms { get; set; }
        private List<Coin> coins = new List<Coin>();
        private int numOfScreensOnMap = 3;
        private ContentManager m;
        private Texture2D bonusTexture;
        //private List<BonusBox> bonusBoxes = new List<BonusBox>();
       // public List<object> objects { get; set; }
       
        public void Initialize()
        {
            Ground = new List<Platform>();
            Pipes = new List<Pipe>();
            Platforms = new List<List<Platform>>();
            MakeGround();
            GeneratePipes();
            GenerateCoins();
            //foreach (var i in Pipes)
            //{
            //    objects.Add(i);
            //}
         //   objects= new List<object>();
        }
        public void MoveX(int x)
        {
            foreach (var i in Ground)
            {
                i.position.X += x;
            }
            foreach (var i in Pipes)
            {
                i.position.X += x;
            }
            foreach (var i in Platforms)
            {
                foreach (var item in i)
                {
                    item.position.X += x;
                }
                
            }
        }
        private void GenerateCoins()
        {
            int x;
            int y;
            int pos_y=0;
            for (int i = 0; i < 100; i++)
            {
                x = r.Next(55, 1920*numOfScreensOnMap-50);
                y = r.Next(0, 3);
                switch (y)
                {
                    case 0:
                        pos_y = 900;
                        break;
                    case 1:
                        pos_y = 860;
                        break;
                    case 2:
                        pos_y = 820;
                        break;
                    default:
                        break;
                }
                coins.Add(new Coin(new Vector2(x, pos_y)));
            }
        }
        public virtual void LoadContent(ContentManager manager)
        {
            foreach (var i in Ground)
            {
                i.LoadContent(manager);
            }
            foreach (var i in Pipes)
            {
                i.LoadContent(manager);
            }
            foreach (var i in coins)
            {
                i.LoadContent(manager);
            }
            bonusTexture = manager.Load<Texture2D>("bonus");
            m = manager;
            GeneratePlatforms();

            //foreach (var i in Platforms)
            //{
            //    foreach (var p in i)
            //    {
            //        objects.Add(p);
            //    }
            //}
        }
        public void GeneratePlatforms()
        {
            int max = 250;
            while (max < 1920 * numOfScreensOnMap)
            {
                int number = r.Next(0, 2);
                if (number==0)
                {
                    number = r.Next(0, 2);
                    int length = r.Next(0, 5);
                    int x = r.Next(max - 245, max + 1)
;                    if (number == 0)
                       Platforms.Add( MakePlatform(length + 1, x, 860));
                    else
                        Platforms.Add(MakePlatform(length + 1, x,700));
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int length = r.Next(0, 5);
                        int x = r.Next(max - 245, max + 1) ;
                        Platforms.Add(MakePlatform(length + 1, x, 860-i*40));
                    }
                }
                max += 320;

            }
        }
        private List<Platform> MakePlatform(int length, int x, int y)
        {
            List<Platform> pl = new List<Platform>();
            for (int i = 0; i < length; i++)
            {
                int isBonus = r.Next(0, 4);
                if (isBonus==0)
                {
                    int bonus = r.Next(0, 3);
                    BonusBox p= new BonusBox(x, y);
                    p.texture = bonusTexture;
                    p.Bonus = (BonusKinds)bonus;
                    p.BoundingBox = p.DestinationRectangle;
                    pl.Add(p);
                }
                else
                {
                    Platform p = new Platform(x, y);
                    p.texture = m.Load<Texture2D>("pl");
                    //p.DestinationRectangle = new Rectangle(x, y-50, p.texture.Width, p.texture.Height);
                    p.BoundingBox = p.DestinationRectangle;
                    pl.Add(p);
                }
                
                x += 48;
            }
            return pl;
        }
            private void GeneratePipes()
            {
                int max = 300;
                while (max < 1920 * numOfScreensOnMap)
                {
                    int height = r.Next(0, 3);
                    int y = 0;
                    switch (height)
                    {
                        case (0):
                            y = 900;
                            break;
                        case (1):
                            y = 948;
                            break;
                        case (2):
                            y = 990;
                            break;
                        default:
                            break;
                    }
                    int bonus = r.Next(0, 15);
                    int plant = r.Next(0, 10);
                    bool good = false;
                    bool bad = false;
                    if (bonus == 0)
                    {
                        good = true;
                        // y = 0;
                    }
                    else if (plant == 0)
                    {
                        bad = true;
                        //  y = 50;
                    }
                    Pipes.Add(new Pipe(new Vector2(r.Next(max - 300, max + 1), y), good, bad));
                    max += 400;

                }
            }
            public void MakeGround()
            {
                int y = 960;
                for (int i = 0; i < 2; i++)
                {
                    int x = 0;
                    while (x < 1920 * numOfScreensOnMap)
                    {
                        Ground.Add(new Platform(x, y));
                        x += 48;
                    }
                    y += 48;
                }
                groundboundingBox = new Rectangle(0,1000,1920, 152);
            }
            public virtual void Update(GameTime game)
            {
                foreach (var i in Pipes)
                {
                    i.Update(game);
                }
            foreach (var i in Platforms)
            {
                foreach (var p in i)
                {
                    p.Update();
                }
                
            }
        }
        public virtual void Draw(SpriteBatch s)
        {
            foreach (var i in coins)
            {
                i.Draw(s);
            }
            foreach (var i in Platforms)
            {
                foreach (var p in i)
                {
                    p.Draw(s);
                }
            }
            foreach (var i in Pipes)
            {
                i.Draw(s);
            }
            foreach (var i in Ground)
            {
                i.Draw(s);
            }
            
        }
    }
}
