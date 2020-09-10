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

        public List<GameObject> gameObjects = new List<GameObject>();

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

            //create player tank
            var tank = new Tank("Tank", new Vector2(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f), 0, "Blue", this);
            tank.Tag = "Player";
            (tank.GetComponent(typeof(Collider)) as Collider).collisionType = Collider.CollisionType.AABB;
            (tank.GetComponent(typeof(Collider)) as Collider).AABB = new AABB((tank.GetComponent(typeof(Collider)) as Collider));
            gameObjects.Add(tank);


            //create a target tank that will be destroyed on collision with anything
            var targetTank = new Tank("TargetTank", new Vector2(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f - 150), 0, "Red", this);

            //(targetTank.GetComponent(typeof(Collider)) as Collider).DestroySelfOnCollision = true;
            //(targetTank.GetComponent(typeof(Collider)) as Collider).collisionType = Collider.CollisionType.AABB;
            //(targetTank.GetComponent(typeof(Collider)) as Collider).AABB = new AABB((targetTank.GetComponent(typeof(Collider)) as Collider));
            gameObjects.Add(targetTank);

            //create a target tank that can not be destroyed
            var targetTank2 = new Tank("TargetTank2", new Vector2(GetScreenWidth() / 2.0f - 150, GetScreenHeight() / 2.0f - 150), 0, "Green", this);
            gameObjects.Add(targetTank2);

        }

        public void Shutdown()
        {
        }

        public void Update()
        {
            //get delta time
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


            for(int i = 0; i < gameObjects.Count; i++)
            {
                //destroy game objects set to dispose
                if (gameObjects[i].Dispose)
                {
                    gameObjects.Remove(gameObjects[i]);
                }
                else
                {
                    //execute objects instructions
                    gameObjects[i].OnUpdate(deltaTime);

                    //loop through gameObjects components and execute the components instructions
                    for (int j = 0; j < gameObjects[i].Components.Count; j++)
                    {
                        gameObjects[i].Components[j].OnUpdate(deltaTime);
                    }
                    
                }
            } 
        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.WHITE);

            DrawText(fps.ToString(), 10, 10, 14, Color.RED);

            //call draw function on all gameObjects
            foreach(GameObject go in gameObjects)
            {
                go.OnDraw();
            }

            EndDrawing();
        }

    }
}
