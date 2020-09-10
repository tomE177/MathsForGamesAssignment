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
    class Bullet : GameObject
    {
        public Bullet(string name, MathsLibrary.Vector3 pos, float rotation, Game Gam)
        {
            //create the bullets sprite object
            SpriteObject sprite = new SpriteObject();
            sprite.Load("..\\Images\\PNG\\Bullets\\bulletBeige_outline.png");
            sprite.Name = name + "_Sprite";
            sprite.SetPosition(sprite.Height - sprite.Height / 2, -10);
            sprite.SetRotate(90 * (float)(Math.PI / 180.0f));

            //set the sprite object as a child of the bullet
            AddChild(sprite);

            //set the bullet position and rotation
            SetPosition(pos);
            SetRotate(rotation);

            Name = name;
            Game = Gam;

            ///add and set up the components
            //add the physics move component
            AddComponent(new PhysicsBody(this));

            //set the move type of the physics move to non uniform (uses acceleration)
            (GetComponent(typeof(PhysicsBody)) as PhysicsBody).MoveType = PhysicsBody.PhysicsMoveType.NonUniform;

            //set the x velocity to 500
            (GetComponent(typeof(PhysicsBody)) as PhysicsBody).Velocity = new Vector2(500, 0);

            //set the x acceleration to 10
            (GetComponent(typeof(PhysicsBody)) as PhysicsBody).Acceleration = new Vector2(10, 0);

            //add a destroy timer with a 2 second timer
            AddComponent(new DestroyTimer(2f, this));

            //add a collider set for AABB
            AddComponent(new Collider(Collider.CollisionType.AABB, this));

            //set the bullet to destroy itself when it collides with another collider
            (GetComponent(typeof(Collider)) as Collider).DestroySelfOnCollision = true;
            (GetComponent(typeof(Collider)) as Collider).isTrigger = true;


            MakeSureCollisionComponentsAreLast();
        }
    }
}
