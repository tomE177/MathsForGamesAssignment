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

        Vector2[] pointlist = new Vector2[4];

        Vector2 halfExtents;
        Vector2 position;
        GameObject[] gameObjects;
        Color debugColour = Color.RED;
        bool collision = false;
        public bool destroySelfOnCollision = false;


        public override void DoAction(float deltaTime)
        {
            if(collision && destroySelfOnCollision)
                attachedTo.Dispose();

            position = attachedTo.GetPositon();
            gameObjects = attachedTo.GetAllGameObjects(attachedTo.game.gameObjects);
            pointlist[0] = (new Vector2(attachedTo.GetPositon().x - (attachedTo.GetSprite().width / 2), attachedTo.GetPositon().y - (attachedTo.GetSprite().height / 2)));
            pointlist[1] = (new Vector2(attachedTo.GetPositon().x - (attachedTo.GetSprite().width / 2), attachedTo.GetPositon().y + (attachedTo.GetSprite().height / 2)));


            pointlist[2] = (new Vector2(attachedTo.GetPositon().x + (attachedTo.GetSprite().width / 2), attachedTo.GetPositon().y - (attachedTo.GetSprite().height / 2)));
            pointlist[3] = (new Vector2(attachedTo.GetPositon().x + (attachedTo.GetSprite().width / 2), attachedTo.GetPositon().y + (attachedTo.GetSprite().height / 2)));

            foreach ( var p in pointlist)
            {
                DrawCircle((int)p.x,(int)p.y,3,Color.RED);
            }
            DrawRec();

            foreach(GameObject gameObject in gameObjects)
            {
                for(int i = 0; i < gameObject.components.Count; ++i)
                {
                    if(gameObject != attachedTo)
                    {
                        if (gameObject.components[i] is Collider)
                        {
                            if (AABBOverlaps(gameObject.components[i] as Collider))
                                if (collision && destroySelfOnCollision)
                                    attachedTo.Dispose();

                        }
                    }
                }
            }

            
        }


        public Collider(GameObject AttachedTo)
        {
            attachedTo = AttachedTo;
            position = attachedTo.GetPositon();

            pointlist[0] = (new Vector2(attachedTo.GetPositon().x - (attachedTo.GetSprite().width/2), attachedTo.GetPositon().y - (attachedTo.GetSprite().height / 2)));
            pointlist[1] =(new Vector2(attachedTo.GetPositon().x - (attachedTo.GetSprite().width / 2), attachedTo.GetPositon().y + (attachedTo.GetSprite().height / 2)));


            pointlist[2] = (new Vector2(attachedTo.GetPositon().x + (attachedTo.GetSprite().width / 2), attachedTo.GetPositon().y - (attachedTo.GetSprite().height / 2)));
            pointlist[3]= (new Vector2(attachedTo.GetPositon().x + (attachedTo.GetSprite().width / 2), attachedTo.GetPositon().y + (attachedTo.GetSprite().height / 2)));

            Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
            Vector2 max = new Vector2(float.MinValue, float.MinValue);

            foreach (var p in pointlist)
            {
                if (p.x < min.x)
                    min.x = p.x;
                if (p.y < min.y)
                    min.y = p.y;
                if (p.x > max.x)
                    max.x = p.x;
                if (p.y > max.y)
                    max.y = p.y;
            }

            //position = (max + min) * 0.5f;
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
            if (p.y > max.y)
                max.y = p.y;

            //position = (max + min) * 0.5f;
            halfExtents = (max - min) * 0.5f;
        }

        public bool PointOverlaps(Vector2 p)
        {
            Vector2 np = p - position;
            np.x = Math.Abs(np.x);
            np.y = Math.Abs(np.y);

            return np.x < halfExtents.x && np.y < halfExtents.y;
        }

        public bool PointOverlapsMethod2(Vector2 p)
        {
            var mn = Min();
            var mx = Max();

            return p.x < mx.x && p.x > mn.x && p.y < mx.y && p.y > mn.y;
        }


        public bool AABBOverlaps(Collider other)
        {
            if (!(Min().x > other.Max().x || Min().y > other.Max().y ||
                Max().x < other.Min().x || Max().y < other.Min().y))
            {
                debugColour = Color.BLUE;
                collision = true;
                other.collision = true;
                return true;
            }
            debugColour = Color.RED;
            collision = false;
            return false;
        }


        public void DrawRec()
        {
            var x = (int)Min().x;
            var y = (int)Min().y;
            var w = (int)halfExtents.x * 2;
            var h = (int)halfExtents.y * 2;
            //Color colour = new Color(255,0,0,255);
            DrawRectangleLines(x, y, w, h, debugColour);
        }

        public Vector2 Min()
        {
            return position - halfExtents;
        }

        public Vector2 Max()
        {
            return position + halfExtents;
        }
    }
}
