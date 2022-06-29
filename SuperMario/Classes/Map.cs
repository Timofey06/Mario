using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SuperMario.Classes
{
    class Map
    {
        public Rectangle groundboundingBox { get; set; }
        Colision colision = new Colision();
        public Colision Colision
        {
            get => colision;
            set => colision = value;
        }
        private Random r = new Random();
        public List<Platform> Ground { get; set; }
        public List<Pipe> Pipes { get; set; }
        public List<List<Platform>> Platforms { get; set; }
        private int numOfScreensOnMap = 3;
        private ContentManager m;
        public List<Colision> objectsColision { get; set; }
        // public List<object> objects { get; set; }

        public void Initialize()
        {
            Ground = new List<Platform>();
            Pipes = new List<Pipe>();
            Platforms = new List<List<Platform>>();
            objectsColision = new List<Colision>();
            MakeGround();
            GeneratePipes();
            //foreach (var i in Pipes)
            //{
            //    objects.Add(i);
            //}
            //   objects= new List<object>();
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
            m = manager;
            GeneratePlatforms();
            colision.LoadCollisionContent(manager);

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
                if (number == 0)
                {
                    number = r.Next(0, 2);
                    int length = r.Next(0, 5);
                    int x = r.Next(max - 245, max + 1)
; if (number == 0)
                        Platforms.Add(MakePlatform(length + 1, x, 900));
                    else
                        Platforms.Add(MakePlatform(length + 1, x, 860));
                }
                else
                {
                    for (int i = 0; i < 2; i++)
                    {
                        int length = r.Next(0, 5);
                        int x = r.Next(max - 245, max + 1);
                        Platforms.Add(MakePlatform(length + 1, x, 900 - i * 40));
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
                Platform p = new Platform(x, y);
                p.texture = m.Load<Texture2D>("platform");
                //p.DestinationRectangle = new Rectangle(x, y-50, p.texture.Width, p.texture.Height);
                p.BoundingBox = p.DestinationRectangle;
                x += 47;
                pl.Add(p);
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
                        y = 800;
                        break;
                    case (1):
                        y = 848;
                        break;
                    case (2):
                        y = 896;
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
                Pipes.Add(new Pipe(new Vector2(r.Next(max -300, max + 1), y), good, bad));
                max += 380;

            }
        }
        public void MakeGround()
        {
            int y = 980;
            for (int i = 0; i < 8; i++)
            {
                int x = 0;
                while (x < 1920 * numOfScreensOnMap)
                {
                    Ground.Add(new Platform(x, y));
                    x += 48;
                }
                y += 19;
            }
            Point l = new Point(0, 1100);
            //  Point s = new Point(1920, 1080);
            groundboundingBox = new Rectangle(0, 980, 1920, 152);
            colision.ColisionBoxX = groundboundingBox; 
            colision.ColisionBoxY = groundboundingBox;
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
            foreach (var pipe in Pipes)
            {
                objectsColision.Add(pipe.ColsionBox);
            }
            objectsColision.Add(colision);
        }
        public virtual void Draw(SpriteBatch s)
        {
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
            //colision.Draw(s);
        }
    }
}