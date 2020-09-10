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

        public virtual void OnUpdate(float deltaTime)
        {
            
        }
    }

    //This component moves objects either uniformly or non uniformly by velocity and acceleration
    class PhysicsBody : Component
    {
        public enum PhysicsMoveType
        {
            Uniform,//does not use acceleration
            NonUniform//uses acceleration
        }

        public PhysicsMoveType MoveType = PhysicsMoveType.Uniform;

        Vector2 velocity = new Vector2(0,0);
        Vector2 acceleration = new Vector2(0, 0);

        public PhysicsBody(GameObject AttachedTo)
        {
            //set the game object it is attached to
            attachedTo = AttachedTo;
        }


        ///Get/Set///
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

        //Execute its instuctions
        public override void OnUpdate(float deltaTime)
        {
            switch (MoveType)
            {
                case PhysicsMoveType.Uniform:
                    //move without acceloration
                    attachedTo.Translate(new MathsLibrary.Vector3(velocity.x * deltaTime, velocity.y * deltaTime,1));
                    break;
                case PhysicsMoveType.NonUniform:
                    //add acceleration onto velocity
                    velocity = velocity + acceleration * deltaTime;
                    //move with acceleration
                    attachedTo.Translate(new MathsLibrary.Vector3(velocity.x * deltaTime, velocity.y * deltaTime, 1));
                    break;
            }
        }
    }

    //destroys the object its attached to after a defined time period
    class DestroyTimer : Component
    {
        public float timer = 0;

        public DestroyTimer(float Timer, GameObject AttachTo)
        {
            attachedTo = AttachTo;
            timer = Timer;
        }

        //destroys object attached to once timer <= 0
        public override void OnUpdate(float deltaTime)
        {
            timer -= deltaTime;
            if(timer <= 0)
            {
                attachedTo.Destroy();
            }
        }
    }


    //This component adds a collider (circle or square) to the object and detects collisions
    class Collider : Component
    {
        public enum CollisionType
        {
            AABB,//square
            Circle
        }

        public CollisionType collisionType = CollisionType.AABB;

        //the 4 corner points of the objects texture
        Vector2[] pointList = new Vector2[4];

        Vector2 position;
        GameObject[] gameObjects;
        Color debugColour = Color.RED;
        bool collision = false;
        bool destroySelfOnCollision = false;
        public bool isTrigger;


        AABB aabb;
        CircleCollider circle;

        //create the collider
        public Collider(CollisionType colType, GameObject AttachedTo)
        {
            attachedTo = AttachedTo;
            position = attachedTo.GetPosition();

            //create the collider and set the collision type
            switch (colType)
            {
                case CollisionType.AABB:
                    aabb = new AABB(this);
                    collisionType = CollisionType.AABB;
                    break;
                case CollisionType.Circle:
                    circle = new CircleCollider(this);
                    collisionType = CollisionType.Circle;
                    break;
            }
        }

        public Vector2[] Pointlist
        {
            get
            {
                return pointList;
            }
            set
            {
                pointList = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

        public GameObject[] GameObjects
        {
            get
            {
                return gameObjects;
            }
            set
            {
                gameObjects = value;
            }
        }

        public Color DebugColour
        {
            get
            {
                return debugColour;
            }
            set
            {
                debugColour = value;
            }
        }

        public bool Collision
        {
            get
            {
                return collision;
            }
            set
            {
                collision = value;
            }
        }

        public bool DestroySelfOnCollision
        {
            get
            {
                return destroySelfOnCollision;
            }
            set
            {
                destroySelfOnCollision = value;
            }
        }

        public AABB AABB
        {
            get
            {
                return aabb;
            }
            set
            {
                aabb = value;
            }
        }

        public CircleCollider Circle
        {
            get
            {
                return circle;
            }
            set
            {
                circle = value;
            }
        }

        //execute the components instructions
        public override void OnUpdate(float deltaTime)
        {
            //destroy attached to object if a collision has occured and destroySelfOnCollision is true
            if (collision && destroySelfOnCollision)
                attachedTo.Destroy();

            //set Position to the object attached to Position
            Position = attachedTo.GetPosition();

            //get all the gameworld objects
            gameObjects = attachedTo.GetAllGameObjects(attachedTo.Game.gameObjects);

            //check for collisions
            switch (collisionType)
            {
                case CollisionType.AABB:
                    aabb.Update();
                    break;
                case CollisionType.Circle:
                    circle.Update();
                    break;
            }

        }
    }



    // Axis Aligned Bounding Box
    class AABB
    {
        Collider parent;
        Vector2 halfExtents;

        public AABB(Collider Parent)
        {
            parent = Parent;

            //set the 4 points of the box from the min and max points of the texture
            parent.Pointlist[0] = (new Vector2(parent.attachedTo.GetPosition().x - (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y - (parent.attachedTo.GetSprite().height / 2)));
            parent.Pointlist[1] = (new Vector2(parent.attachedTo.GetPosition().x - (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y + (parent.attachedTo.GetSprite().height / 2)));
            parent.Pointlist[2] = (new Vector2(parent.attachedTo.GetPosition().x + (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y - (parent.attachedTo.GetSprite().height / 2)));
            parent.Pointlist[3] = (new Vector2(parent.attachedTo.GetPosition().x + (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y + (parent.attachedTo.GetSprite().height / 2)));

            //get the min x/y and max x/y points
            Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
            Vector2 max = new Vector2(float.MinValue, float.MinValue);

            foreach (var p in parent.Pointlist)
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

            //calculate the x/y lenght of the box then divide by 2
            halfExtents = (max - min) * 0.5f;
        }

        public Collider Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        public Vector2 HalfExtents
        {
            get
            {
                return halfExtents;
            }
            set
            {
                halfExtents = value;
            }
        }

        //add a point to the point list
        public void AddPoint(Vector2 p)
        {
            Vector2 min = Min();
            Vector2 max = Max();

            //recalculate the min and max points
            if (p.x < min.x)
                min.x = p.x;
            if (p.y < min.y)
                min.y = p.y;
            if (p.x > max.x)
                max.x = p.x;
            if (p.y > max.y)
                max.y = p.y;

            //recalculate the half extents
            halfExtents = (max - min) * 0.5f;
        }

        //check if point overlaps into the box
        public bool PointOverlaps(Vector2 p)
        {
            Vector2 np = p - parent.Position;
            np.x = Math.Abs(np.x);
            np.y = Math.Abs(np.y);

            return np.x < parent.Position.x && np.y < parent.Position.y;
        }

        //check if point overlaps into the box
        public bool PointOverlapsMethod2(Vector2 p)
        {
            var mn = Min();
            var mx = Max();

            return p.x < mx.x && p.x > mn.x && p.y < mx.y && p.y > mn.y;
        }

        //check if a collision with another bounding box has occured
        public bool AABBOverlaps(Collider other)
        {
            //check if the min or max of the other box are inside the box
            if (!(Min().x > other.AABB.Max().x || Min().y > other.AABB.Max().y ||
                Max().x < other.AABB.Min().x || Max().y < other.AABB.Min().y))
            {
                //if it is then collision has occured
                parent.DebugColour = Color.BLUE;
                parent.Collision = true;
                other.Collision = true;
                return true;
            }
            parent.DebugColour = Color.RED;
            parent.Collision = false;
            return false;
        }

        public void AABBCollisionResponse(Collider other)
        {

            Vector2 dist = parent.Position - other.Position;

            if(Math.Abs(dist.x / (halfExtents.x * 2)) > Math.Abs(dist.y / (halfExtents.y * 2)))
            {
                //x axis longer than the y axis
                var thisPos = parent.attachedTo.GetPosition();
                if (dist.x < 0)//left
                    parent.attachedTo.SetPosition(other.attachedTo.GetPosition().x - (halfExtents.x *2), thisPos.y);
                else
                    parent.attachedTo.SetPosition(other.attachedTo.GetPosition().x + (halfExtents.x*2), thisPos.y);

            }
            else
            {
                //y axis longer than x axis
                var thisPos = parent.attachedTo.GetPosition();
                if (dist.y > 0)//down
                    parent.attachedTo.SetPosition(thisPos.x, other.attachedTo.GetPosition().y + (halfExtents.y*2 ));
                else//up
                    parent.attachedTo.SetPosition(thisPos.x, other.attachedTo.GetPosition().y - (halfExtents.y*2));


            }
        }


        //debug draw the AABB
        public void DrawRec()
        {
            var x = (int)Min().x;
            var y = (int)Min().y;
            var w = (int)halfExtents.x * 2;
            var h = (int)halfExtents.y * 2;
            //Color colour = new Color(255,0,0,255);
            DrawRectangleLines(x, y, w, h, parent.DebugColour);
        }

        //Calculate the min
        public Vector2 Min()
        {
            return parent.Position - halfExtents;
        }

        //Calculate the max
        public Vector2 Max()
        {
            return parent.Position + halfExtents;
        }

        //find the closest point the the AABB
        public Vector2 ClosestPoint(Vector2 point)
        {
            return Vector2.Clamp(point, Min(), Max());
        }

        //check for collision with a circle collider
        bool CircleCollision(CircleCollider other)
        {
            //get the distance between the closest point on the AABB to the circle
            Vector2 diff = ClosestPoint(other.Parent.Position) - other.Parent.Position;

            // if the distance is less or equal to the circle radius^2
            if (diff.LengthSquared() <= (other.Radius * other.Radius))
            {
                //collision has occured
                parent.DebugColour = Color.BLUE;
                parent.Collision = true;
                other.Parent.Collision = true;
                return true;
            }
            parent.DebugColour = Color.RED;
            parent.Collision = false;
            return false;
        }


        void CircleCollisionResolution(CircleCollider other)
        {
            Vector2 closestPoint = ClosestPoint(parent.Position);
            Vector2 diff = other.Parent.attachedTo.GetPosition() - closestPoint;
            float length = diff.Length();

            if (Math.Abs(diff.x / (HalfExtents.x * 2)) > Math.Abs(diff.y / (HalfExtents.y * 2)))
            {
                //x vector longer than y
                if (other.Parent.attachedTo.GetPosition().x > parent.attachedTo.GetPosition().x)
                {
                    //we are right
                    parent.attachedTo.SetPosition(parent.attachedTo.GetPosition().x + (other.Radius - length + 1), parent.attachedTo.GetPosition().y);
                }
                else
                {
                    //we are left
                    parent.attachedTo.SetPosition(parent.attachedTo.GetPosition().x - (other.Radius - length + 1), parent.attachedTo.GetPosition().y);
                }
            }
            else
            {
                //y longer than x
                if (other.Parent.attachedTo.GetPosition().y > parent.attachedTo.GetPosition().y )
                {
                    //we are below
                    parent.attachedTo.SetPosition(parent.attachedTo.GetPosition().x, parent.attachedTo.GetPosition().y + (other.Radius - length + 1));
                }
                else
                {
                    //we are above
                    parent.attachedTo.SetPosition(parent.attachedTo.GetPosition().x, parent.attachedTo.GetPosition().y - (other.Radius - length + 1));
                }
            }
        }

        public void Update()
        {
            //update where the points position is
            parent.Pointlist[0] = (new Vector2(parent.attachedTo.GetPosition().x - (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y - (parent.attachedTo.GetSprite().height / 2)));
            parent.Pointlist[1] = (new Vector2(parent.attachedTo.GetPosition().x - (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y + (parent.attachedTo.GetSprite().height / 2)));


            parent.Pointlist[2] = (new Vector2(parent.attachedTo.GetPosition().x + (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y - (parent.attachedTo.GetSprite().height / 2)));
            parent.Pointlist[3] = (new Vector2(parent.attachedTo.GetPosition().x + (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y + (parent.attachedTo.GetSprite().height / 2)));

            //draw a circle for each of the points
            foreach (var p in parent.Pointlist)
            {
                DrawCircle((int)p.x, (int)p.y, 3, Color.RED);
            }

            //draw the AABB
            DrawRec();

            //loop through each gameobject checking for collisions
            foreach (GameObject gameObject in parent.GameObjects)
            {
                //loop through the gameobjects components
                for (int i = 0; i < gameObject.Components.Count; ++i)
                {
                    //make sure this game object is not the gameobject this collider is attached to
                    if (gameObject != parent.attachedTo)
                    {
                        //check if the component is a collider
                        if (gameObject.Components[i] is Collider)
                        {
                            //check collider type
                            if ((gameObject.Components[i] as Collider).collisionType == Collider.CollisionType.AABB)
                            {
                                if(parent.collisionType == Collider.CollisionType.AABB)
                                {
                                    //if the object checking has an AABB collider
                                    if (AABBOverlaps(gameObject.Components[i] as Collider))
                                    {
                                        //collision has occured
                                        if (parent.Collision && parent.DestroySelfOnCollision)
                                        {
                                            parent.attachedTo.Destroy();
                                        }

                                        if (!parent.isTrigger && !(gameObject.Components[i] as Collider).isTrigger)
                                            AABBCollisionResponse(gameObject.Components[i] as Collider);
                                        return;
                                    }
                                }
                                
                            }else if((gameObject.Components[i] as Collider).collisionType == Collider.CollisionType.Circle)
                            {
                                //if the object checking has an Circle collider
                                if (CircleCollision((gameObject.Components[i] as Collider).Circle))
                                {
                                    //collision has occured
                                    if (parent.Collision && parent.DestroySelfOnCollision)
                                    {
                                        parent.attachedTo.Destroy();
                                    }

                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    class CircleCollider
    {
        float radius;
        Collider parent;

        public CircleCollider(Collider Parent)
        {
            //set parent collider
            parent = Parent;

            //set the 4 points of the box from the min and max points of the texture to create the circle around
            parent.Pointlist[0] = (new Vector2(parent.attachedTo.GetPosition().x - (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y - (parent.attachedTo.GetSprite().height / 2)));
            parent.Pointlist[1] = (new Vector2(parent.attachedTo.GetPosition().x - (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y + (parent.attachedTo.GetSprite().height / 2)));
            parent.Pointlist[2] = (new Vector2(parent.attachedTo.GetPosition().x + (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y - (parent.attachedTo.GetSprite().height / 2)));
            parent.Pointlist[3] = (new Vector2(parent.attachedTo.GetPosition().x + (parent.attachedTo.GetSprite().width / 2), parent.attachedTo.GetPosition().y + (parent.attachedTo.GetSprite().height / 2)));

            //get the min and max points of the box
            Vector2 min = new Vector2(float.MaxValue, float.MaxValue);
            Vector2 max = new Vector2(float.MinValue, float.MinValue);

            for(int i = 0; i < parent.Pointlist.Length; i++)
            {
                min = Vector2.Min(min, parent.Pointlist[i]);
                max = Vector2.Max(max, parent.Pointlist[i]);
            }

            //set the circle radius around the box
            radius = (parent.Position - max).Length();

        }

        public float Radius
        {
            get
            {
                return radius;
            }
            set
            {
                radius = value;
            }
        }

        public Collider Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }

        //collision with a point
        bool PointCollision(Vector2 point)
        { 
            if ((point - parent.attachedTo.GetPosition()).LengthSquared() <= (Math.Pow(radius, 2)))
            {
                //collision occured
                parent.DebugColour = Color.BLUE;
                return true;
            }
            parent.DebugColour = Color.RED;
            return false;
        }

        //check for collsion with another circle collider
        bool CircleCollision(CircleCollider other)
        {
            //if distance between the 2 circles is less than the 2 circles radie combined to the power of 2
            if ((other.parent.Position - parent.Position).LengthSquared() <= (Math.Pow(radius + other.radius, 2)))
            {
                //collision has occured
                parent.DebugColour = Color.BLUE;
                parent.Collision = true;
                other.parent.Collision = true;
                return true;
            }
            parent.Collision = false;
            parent.DebugColour = Color.RED;
            return false;
        }

        void CircleCollisionResolution(CircleCollider other)
        {
            var dist = parent.attachedTo.GetPosition() - other.parent.attachedTo.GetPosition();
            var length = (other.parent.Position - parent.Position).Length();
            Vector2 unit = new Vector2(dist.x / length, dist.y / length);
            parent.attachedTo.SetPosition(other.parent.attachedTo.GetPosition().x + (radius + other.radius + 1) * unit.x, other.parent.attachedTo.GetPosition().y + (radius + other.radius + 1) * unit.y);
        }

        //draw circle collider
        void DebugCircle()
        {
            DrawCircle((int)parent.Position.x, (int)parent.Position.y,radius,parent.DebugColour);
        }

        //check for collision with an AABB collider
        bool AABBCollision(AABB aaBB)
        {
            //get the distance between the closest point on the AABB to the circle collider
            Vector2 diff = aaBB.ClosestPoint(parent.Position) - parent.Position;

            //if the distance is less than the radius of the circle squared
            if (diff.LengthSquared() <= (radius * radius))
            {
                //collision has occured
                parent.Collision = true;
                parent.DebugColour = Color.BLUE;
                aaBB.Parent.Collision = true;
                return true;
            }
            parent.Collision = false;
            parent.DebugColour = Color.RED;
            return false;
        }

        void AABBCollisionResolution(AABB aaBB)
        {
            Vector2 closestPoint = aaBB.ClosestPoint(parent.Position);
            Vector2 diff = aaBB.ClosestPoint(parent.Position) - parent.Position;
            float length = diff.Length();

            //is the x value of diff vector longer than y
            if (Math.Abs(diff.x / (aaBB.HalfExtents.x * 2)) > Math.Abs(diff.y / (aaBB.HalfExtents.y * 2)))
            {
                //x is longer than y
                if (parent.attachedTo.GetPosition().x > aaBB.Parent.attachedTo.GetPosition().x)
                {
                    //we are right
                    parent.attachedTo.SetPosition(parent.attachedTo.GetPosition().x + (radius - length + 1), parent.attachedTo.GetPosition().y);
                }
                else
                {
                    //we are left
                    parent.attachedTo.SetPosition(parent.attachedTo.GetPosition().x - (radius - length + 1), parent.attachedTo.GetPosition().y );
                }
            }
            else
            {
                //y longer than x
                if (parent.attachedTo.GetPosition().y > aaBB.Parent.attachedTo.GetPosition().y)
                {
                    //we are below
                    parent.attachedTo.SetPosition(parent.attachedTo.GetPosition().x, parent.attachedTo.GetPosition().y + (radius - length + 1));
                }
                else
                {
                    //we are above
                    parent.attachedTo.SetPosition(parent.attachedTo.GetPosition().x, parent.attachedTo.GetPosition().y - (radius - length + 1));
                }
            }
        }

        //execute instuctions
        public void Update()
        {
            //draw cicle collider
            DebugCircle();

            //loop through all gameobjects checking for a collision
            foreach (GameObject gameObject in parent.GameObjects)
            {
                //loop through this gameobjects components
                for (int i = 0; i < gameObject.Components.Count; ++i)
                {
                    //make sure this game object is not the gameobject this collider is attached to
                    if (gameObject != parent.attachedTo)
                    {
                        //check if the component is a collider
                        if (gameObject.Components[i] is Collider)
                        {
                            //check if the other collider is a circle collider or AABB collider
                            if ((gameObject.Components[i] as Collider).collisionType == Collider.CollisionType.Circle)
                            {
                                //if circle collider check for collision with another circle collider
                                if (CircleCollision((gameObject.Components[i] as Collider).Circle))
                                {
                                    //collision has occured
                                    if (parent.Collision && parent.DestroySelfOnCollision)
                                    {
                                        parent.attachedTo.Destroy();
                                        
                                    }

                                    if (!parent.isTrigger && !(gameObject.Components[i] as Collider).isTrigger)
                                    {
                                        CircleCollisionResolution((gameObject.Components[i] as Collider).Circle);
                                    }
                                    return;
                                }
                            }else 
                            if((gameObject.Components[i] as Collider).collisionType == Collider.CollisionType.AABB)
                            {
                                //the other collider is AABB
                                //check for collision with an AABB collider
                                if (AABBCollision((gameObject.Components[i] as Collider).AABB))
                                {
                                    //collision has occured
                                    if (parent.Collision && parent.DestroySelfOnCollision)
                                    {
                                        parent.attachedTo.Destroy();
                                        
                                    }

                                    if (!parent.isTrigger && !(gameObject.Components[i] as Collider).isTrigger)
                                    {
                                        AABBCollisionResolution((gameObject.Components[i] as Collider).AABB);
                                    }
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
