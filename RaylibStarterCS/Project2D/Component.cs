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
    class Component
    {
        public GameObject attachedTo;

        public virtual void DoAction(float deltaTime)
        {
            
        }
    }

    class PhysicsMove : Component
    {
        public enum PhysicsMoveType
        {
            Uniform,
            NonUniform
        }

        public PhysicsMoveType MoveType = PhysicsMoveType.Uniform;

        Vector2 velocity = new Vector2(0,0);
        Vector2 acceleration = new Vector2(0, 0);

        public PhysicsMove(GameObject AttachedTo)
        {
            attachedTo = AttachedTo;
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        public Vector2 Acceleration
        {
            get
            {
                return acceleration;
            }
            set
            {
                acceleration = value;
            }
        }

        public override void DoAction(float deltaTime)
        {
            switch (MoveType)
            {
                case PhysicsMoveType.Uniform:
                    attachedTo.Translate(new MathsLibrary.Vector3(velocity.x * deltaTime, velocity.y * deltaTime,1));
                    break;
                case PhysicsMoveType.NonUniform:
                    velocity = velocity + acceleration * deltaTime;
                    attachedTo.Translate(new MathsLibrary.Vector3(velocity.x * deltaTime, velocity.y * deltaTime, 1));
                    break;
            }
        }
    }

    class DestroyTimer : Component
    {
        public float timer = 0;

        public DestroyTimer(float Timer, GameObject AttachTo)
        {
            attachedTo = AttachTo;
            timer = Timer;
        }

        public override void DoAction(float deltaTime)
        {
            timer -= deltaTime;
            if(timer <= 0)
            {
                attachedTo.Dispose();
            }
        }
    }

    class Collider : Component
    {
        public enum CollisionType
        {
            AABB,
        }

        public CollisionType collisionType = CollisionType.AABB;

        Vector2 halfExtents;

        public Collider(Vector2[] listOfPoints, GameObject AttachedTo)
        {
            attachedTo = AttachedTo;
            Vector2 min = Min();
            Vector2 max = Max();

            foreach(var p in listOfPoints)
            {
                if (p.x < min.x)
                    min.x = p.x;
                if (p.y < min.y)
                    min.y = p.y;
                if (p.x > max.x)
                    max.x = p.x;
                if (p.y < max.y)
                    max.y = p.y;
            }

            halfExtents = (max - min) * 0.5f;
        }

        public void AddPoint(Vector2 p)
        {
            Vector2 min = Min();
            Vector2 max = Max();


            if (p.x < min.x)
                min.x = p.x;
            if (p.y < min.y)
                min.y = p.y;
            if (p.x > max.x)
                max.x = p.x;
            if (p.y < max.y)
                max.y = p.y;
        }

        public Vector2 Min()
        {
            return attachedTo.GetPositon() - halfExtents;
        }

        public Vector2 Max()
        {
            return attachedTo.GetPositon() + halfExtents;
        }
    }
}
