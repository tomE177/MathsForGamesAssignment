using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;
using MathsLibrary;

namespace Project2D
{
    class Tank : GameObject
    {
        float speed = 100;
        GameObject tankTurret;

        //set up tank
        public Tank(string name, Vector2 pos, float rotation, string Colour, Game Gam)
        {
            //Create and set up tanks sprite object
            SpriteObject sprite = new SpriteObject();
            sprite.Load("..\\Images\\PNG\\Tanks\\tank"+ Colour + "_outline.png");
            sprite.Name = name + "_Sprite";
            sprite.SetPosition(-sprite.Width / 2.0f, sprite.Height / 2.0f);
            sprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            AddChild(sprite);

            //Create tank turret object
            tankTurret = new GameObject();
            //set up Turrets Sprite object
            SpriteObject turretSprite = new SpriteObject();
            turretSprite.Load("..\\Images\\PNG\\Tanks\\barrel" + Colour + "_outline.png");
            turretSprite.SetRotate(-90 * (float)(Math.PI / 180.0f));
            turretSprite.SetPosition(0, turretSprite.Width / 2f);
            turretSprite.Name = "TurretSprite";

            //add the turret sprite to the tankturret object
            tankTurret.AddChild(turretSprite);

            //add tank turret to the tank as a child
            AddChild(tankTurret);

            //set position and rotation of the tank
            SetPosition(pos);
            SetRotate(rotation);
            Name = name;
            Game = Gam;

            //add a collider
            AddComponent(new Collider(Collider.CollisionType.Circle, this));

            //add the physics move component
            AddComponent(new PhysicsBody(this));

            MakeSureCollisionComponentsAreLast();
        }

        public float Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
            }
        }

        public override void OnUpdate(float deltaTime)
        {
            if(Tag == "Player")
            {
                if (IsKeyDown(KeyboardKey.KEY_S))
                {
                    //set velocity to -speed, aka backwards
                    (GetComponent(typeof(PhysicsBody)) as PhysicsBody).Velocity = new Vector2(-speed, 0);
                }

                if (IsKeyReleased(KeyboardKey.KEY_S))
                {
                    //set velocity back to 0
                    (GetComponent(typeof(PhysicsBody)) as PhysicsBody).Velocity = new Vector2(0, 0);
                }

                if (IsKeyDown(KeyboardKey.KEY_W))
                {
                    //set velocity to speed, aka forward
                    (GetComponent(typeof(PhysicsBody)) as PhysicsBody).Velocity = new Vector2(speed, 0);
                }

                if (IsKeyReleased(KeyboardKey.KEY_W))
                {
                    //set the velocity to 0
                    (GetComponent(typeof(PhysicsBody)) as PhysicsBody).Velocity = new Vector2(0, 0);
                }

                
                if (IsKeyDown(KeyboardKey.KEY_A))
                {
                    //rotate the tank left
                    Rotate(-deltaTime);
                }

                if (IsKeyDown(KeyboardKey.KEY_D))
                {
                    //rotate the tank right
                    Rotate(deltaTime);
                }

                if (IsKeyDown(KeyboardKey.KEY_E))
                {
                    //rotate the tanks turret right
                    tankTurret.Rotate(deltaTime);
                }

                if (IsKeyDown(KeyboardKey.KEY_Q))
                {
                    //rotate the tanks turret left
                    tankTurret.Rotate(-deltaTime);
                }

                if (IsKeyPressed(KeyboardKey.KEY_SPACE))
                {
                    //create and fire a bullet
                    Game.gameObjects.Add(new Bullet("Bullet", tankTurret.GetForward(80), tankTurret.GetRotation(),Game));
                }
            }
        }
    }
}
