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
        GameObject tank = new GameObject();
        SpriteObject tankSprite = new SpriteObject();
        GameObject tankTurret = new GameObject();
        SpriteObject turretSprite = new SpriteObject();
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

            tankSprite.SetPosition(-tankSprite.Width / 2.0f, -tankSprite.Height / 2.0f);
            tankSprite.Name = "tankSprite";

            turretSprite.Load("..\\Images\\PNG\\Tanks\\barrelBlue_outline.png");

            turretSprite.SetPosition(-turretSprite.Width / 2.0f, -turretSprite.Width / 4f);
            turretSprite.Name = "TurretSprite";



            tankTurret.AddChild(turretSprite);
            tank.AddChild(tankSprite);
            tank.AddChild(tankTurret);
            
            tank.SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);

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
                tank.Translate(new MathsLibrary.Vector3(0, -speed * deltaTime, 1));
            }

            if (IsKeyDown(KeyboardKey.KEY_W))
            {
                tank.Translate(new MathsLibrary.Vector3(0, speed * deltaTime, 1));
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

        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.WHITE);

            DrawText(fps.ToString(), 10, 10, 14, Color.RED);

            tank.Draw();

            EndDrawing();
        }

    }
}
