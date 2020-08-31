using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using MathsLibrary;

namespace Project2D
{
    class Game
    {
        Stopwatch stopwatch = new Stopwatch();

        private long currentTime = 0;
        private long lastTime = 0;
        private float timer = 0;
        private int fps = 1;
        private int frames;

        private float deltaTime = 0.005f;

        List<GameObject> gameObjects = new List<GameObject>();

        GameObject tank = new GameObject();
        SpriteObject tankSprite = new SpriteObject();
        GameObject tankTurret = new GameObject();
        SpriteObject turretSprite = new SpriteObject();

        GameObject bullet = new GameObject();
        SpriteObject bulletSprite = new SpriteObject();

        public Game()
        {
        }

        public void Init()
        {
            stopwatch.Start();
            lastTime = stopwatch.ElapsedMilliseconds;

            if (Stopwatch.IsHighResolution)
            {
                Console.WriteLine("Stopwatch high-resolution frequency: {0} ticks per second", Stopwatch.Frequency);
            }

            tank.Name = "tank";
            tankTurret.Name = "tankTurret";

            tankSprite.Load("..\\Images\\PNG\\Tanks\\tankBlue_outline.png");

            tankSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            //tankSprite.SetPosition(-tankSprite.Width / 2.0f, -tankSprite.Height / 2.0f);
            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);
            tankSprite.Name = "tankSprite";

            turretSprite.Load("..\\Images\\PNG\\Tanks\\barrelBlue_outline.png");

            turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            //turretSprite.SetPosition(-turretSprite.Width / 2.0f, -turretSprite.Width / 4f);
            turretSprite.SetPosition(0, turretSprite.Width / 2f);
            turretSprite.Name = "TurretSprite";

            //bulletSprite.Load("..\\Images\\PNG\\Bullets\\bulletBeige_outline.png");
            //bulletSprite.SetPosition(bulletSprite.Width, 0);
            //bulletSprite.SetRotate((float)(180*Math.PI / 180.0f));
            //bullet.AddChild(bulletSprite);
            //bullet.SetPosition(tank.GetPositon());
            //bullet.SetRotate(turretSprite.GetRotation());


            //bullet.SetPosition(tank.globalTransform.m7, tank.globalTransform.m8);
            //bullet.SetRotate(tankTurret.GetRotation());
            //turretSprite.AddChild(bullet);
            tankTurret.AddChild(turretSprite);
            //tankTurret.AddChild(bullet);
            tank.AddChild(tankSprite);
            tank.AddChild(tankTurret);
            
            tank.SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);

            //spriteObjects.Add(tankSprite);
            //spriteObjects.Add(turretSprite);
            //spriteObjects.Add(bulletSprite);

            //turretSprite.RemoveChild(bullet);

            gameObjects.Add(tank);
            gameObjects.Add(tankTurret);

            //bullet.SetPosition(tankTurret.GetPositon().x, tankTurret.GetPositon().y);



        }

        public void Shutdown()
        {
        }

        public void Update()
        {
            lastTime = currentTime;
            currentTime = stopwatch.ElapsedMilliseconds;
            deltaTime = (currentTime - lastTime) / 1000.0f;
            timer += deltaTime;
            if (timer >= 1)
            {
                fps = frames;
                frames = 0;
                timer -= 1;
            }
            frames++;
            float speed = 100;
            if (IsKeyDown(KeyboardKey.KEY_S))
            {
                tank.Translate(new MathsLibrary.Vector3(-speed * deltaTime, 0, 1));
            }

            if (IsKeyDown(KeyboardKey.KEY_W))
            {
                //var facing = new MathsLibrary.Vector3(tank.localTransform.m7, tank.localTransform.m8, 1) * deltaTime * 100;
                //tank.Translate(new MathsLibrary.Vector3(facing.x,facing.y, facing.z));
                tank.Translate(new MathsLibrary.Vector3(speed * deltaTime, 0, 1));
            }

            if (IsKeyDown(KeyboardKey.KEY_A))
            {
                tank.Rotate(-deltaTime);
            }

            if (IsKeyDown(KeyboardKey.KEY_D))
            {
                tank.Rotate(deltaTime);
            }

            if (IsKeyDown(KeyboardKey.KEY_E))
            {
                tankTurret.Rotate(deltaTime);
            }

            if (IsKeyDown(KeyboardKey.KEY_Q))
            {
                tankTurret.Rotate(-deltaTime);
            }

            if (IsKeyPressed(KeyboardKey.KEY_SPACE))
            {
                //var gam = new GameObject("Bullet", tankTurret.GetForward(60), tankTurret.GetRotation(), "..\\Images\\PNG\\Bullets\\bulletBeige_outline.png");
                gameObjects.Add(new GameObject("Bullet",tankTurret.GetForward(75), tankTurret.GetRotation(), "..\\Images\\PNG\\Bullets\\bulletBeige_outline.png"));
                //float rot = tankTurret.GetRotation();
                //float rots = gam.GetRotation();
            }
            

        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.WHITE);

            DrawText(fps.ToString(), 10, 10, 14, Color.RED);

            //tank.Draw();

            foreach(GameObject go in gameObjects)
            {
                go.OnDraw();
            }

            //var pos = new MathsLibrary.Vector3(tankTurret.globalTransform.m7, tankTurret.globalTransform.m8, tankTurret.globalTransform.m9);
            //pos.Normalize();
            //var pos = tankTurret.globalTransform * new MathsLibrary.Vector3(60,0,1);
            var pos = tankTurret.GetForward(75);

            //DrawPixelV(new Vector2(tankTurret.globalTransform.m7 + pos.x + 60, tankTurret.globalTransform.m8 + pos.y), Color.GREEN);
            DrawPixelV(new Vector2(pos.x, pos.y), Color.GREEN);

            EndDrawing();
        }

    }
}
