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

            
            tank.Sprite = LoadImage("..\\Images\\PNG\\Tanks\\tankBlue_outline.png");
            tank.Texture = LoadTextureFromImage(tank.Sprite);
            //tank.Matrix3 = new Matrix3(new MathsLibrary.Vector3(0,0,0),new MathsLibrary.Vector3(0,0,0),new MathsLibrary.Vector3(0,0,0));

            //tank.Matrix3.RotateX(20);
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


        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.WHITE);

            DrawText(fps.ToString(), 10, 10, 14, Color.RED);

            //DrawTexture(tank.Texture, 
            //    GetScreenWidth() / 2 - tank.Texture.width / 2, GetScreenHeight() / 2 - tank.Texture.height / 2, Color.WHITE);

            //DrawTextureEx(tank.Texture, new Vector2((int)tank.matrix3.Axis[2].xyz[0], (int)tank.matrix3.Axis[2].xyz[1]), 0, 1,Color.WHITE);
            //DrawTexture(tank.Texture, (int)tank.matrix3.Axis[2].xyz[0], (int)tank.matrix3.Axis[2].xyz[1], Color.WHITE);

            EndDrawing();
        }

    }
}
