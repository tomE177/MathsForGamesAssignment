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

        GameObject tank = new GameObject();
        SpriteObject tankSprite = new SpriteObject();
        GameObject tankTurret = new GameObject();
        SpriteObject turretSprite = new SpriteObject();


        GameObject tank2 = new GameObject();
        SpriteObject tankSprite2 = new SpriteObject();
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

            tankSprite.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);
            tankSprite.Name = "tankSprite";

            turretSprite.Load("..\\Images\\PNG\\Tanks\\barrelBlue_outline.png");

            turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));

            turretSprite.SetPosition(0, turretSprite.Width / 2f);
            turretSprite.Name = "TurretSprite";

            
            tankTurret.AddChild(turretSprite);

            tank.AddChild(tankSprite);
            tank.AddChild(tankTurret);
            
            tank.SetPosition(GetScreenWidth() / 2.0f, GetScreenHeight() / 2.0f);


            gameObjects.Add(tank);
            gameObjects.Add(tankTurret);

            tank.AddComponent(new Collider(tank));



            tank2.Name = "tank2";

            tankSprite2.Load("..\\Images\\PNG\\Tanks\\tankBlue_outline.png");

            tankSprite2.SetRotate(-90 * (float)(Math.PI / 180.0f));

            tankSprite2.SetPosition(-tankSprite.Width / 2.0f, tankSprite.Height / 2.0f);
            tankSprite2.Name = "tankSprite2";
            tank2.AddChild(tankSprite2);

            tank2.SetPosition(GetScreenWidth() / 2.0f, (GetScreenHeight() / 2.0f) - 150);

            gameObjects.Add(tank2);
            tank2.AddComponent(new Collider(tank2));
            //(tank2.GetComponent(typeof(Collider)) as Collider).destroySelfOnCollision = true;

            foreach(GameObject go in gameObjects)
            {
                go.game = this;
            }
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
                GameObject bullet = new GameObject("Bullet", tankTurret.GetForward(75), tankTurret.GetRotation(), "..\\Images\\PNG\\Bullets\\bulletBeige_outline.png", this);
                bullet.Layer = "Physics";
                bullet.Collisions = true;
                bullet.AddComponent(new PhysicsMove(bullet));
                (bullet.GetComponent(typeof(PhysicsMove)) as PhysicsMove).MoveType = PhysicsMove.PhysicsMoveType.NonUniform;
                (bullet.GetComponent(typeof(PhysicsMove)) as PhysicsMove).Velocity = new Vector2(500,0);
                (bullet.GetComponent(typeof(PhysicsMove)) as PhysicsMove).Acceleration = new Vector2(10,0);
                bullet.AddComponent(new DestroyTimer(2f,bullet));
                bullet.AddComponent(new Collider(bullet));
                (bullet.GetComponent(typeof(Collider)) as Collider).destroySelfOnCollision = true;
                gameObjects.Add(bullet);
            }

            for(int i = 0; i < gameObjects.Count; i++)
            {
                if(gameObjects[i].dispose)
                    gameObjects.Remove(gameObjects[i]);
            }

            foreach (GameObject go in gameObjects)
            {

                for (int i = 0; i < go.components.Count; i++)
                {
                    go.components[i].DoAction(deltaTime);
                }
                
            }
        }

        public void Draw()
        {
            BeginDrawing();

            ClearBackground(Color.WHITE);

            DrawText(fps.ToString(), 10, 10, 14, Color.RED);


            foreach(GameObject go in gameObjects)
            {
                go.OnDraw();
            }

            var pos = tankTurret.GetForward(75);

            DrawPixelV(new Vector2(pos.x, pos.y), Color.GREEN);

            EndDrawing();
        }

    }
}
