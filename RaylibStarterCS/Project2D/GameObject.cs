using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathsLibrary;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    class GameObject
    {
        string name;
        string tag;
        string layer;
        Game game;
        Matrix3 localTransform = new Matrix3();
        Matrix3 globalTransform = new Matrix3();
        List<GameObject> children = new List<GameObject>();
        GameObject parent = null;
        List<Component> components = new List<Component>();
        bool dispose = false;

        public GameObject()
        {

        }

        public GameObject(string name, MathsLibrary.Vector3 pos, float rotation, string ImageAddress, Game Gam)
        {
            //create objects sprite object
            SpriteObject sprite = new SpriteObject();
            sprite.Load(ImageAddress);
            sprite.Name = name + "_Sprite";
            sprite.SetPosition(-sprite.Width / 2.0f, sprite.Height / 2.0f);
            sprite.SetRotate(-90 * (float)(Math.PI / 180.0f));

            //add the sprite object as a child
            AddChild(sprite);

            //set object position and rotaion
            SetPosition(pos);
            SetRotate(rotation);

            Name = name;
            game = Gam;
        }

        ~GameObject()
        {
            if (parent != null)
            {
                parent.RemoveChild(this);
            }
            foreach (GameObject so in children)
            {
                so.parent = null;
            }
        }

        /// GET/SET ///

        public GameObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public string Layer
        {
            get
            {
                return layer;
            }
            set
            {
                layer = value;
            }
        }

        public String Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public String Tag
        {
            get
            {
                return tag;
            }
            set
            {
                tag = value;
            }
        }

        public Game Game
        {
            get
            {
                return game;
            }
            set
            {
                game = value;
            }
        }

        public List<Component> Components
        {
            get
            {
                return components;
            }
            set
            {
                components = value;
            }
        }

        public bool Dispose
        {
            get
            {
                return dispose;
            }
            set
            {
                dispose = value;
            }
        }

        public virtual void OnUpdate(float delatTime)
        {
        }


        public Matrix3 LocalTransform
        {
            get { return localTransform; }
            set { LocalTransform = value; }
        }

        public Matrix3 GlobalTransform
        {
            get { return globalTransform; }
            set { globalTransform = value; }
        }


        ///Getters///

        //return child at index
        public GameObject GetChild(int index)
        {
            return children[index];
        }

        //return child with name
        public GameObject GetChild(string Name)
        {
            for(int i = 0; i < children.Count(); ++i)
            {
                if (children[i].Name == Name)
                    return children[i];
            }
            return null;
        }

        //return all children
        public List<GameObject> GetAllChildren()
        {
            return children;
        }

        //get children with specific tag
        public List<GameObject> GetChildrenWithTag(string Tag)
        {
            List<GameObject> CWT = new List<GameObject>();
            for (int i = 0; i < children.Count(); ++i)
            {
                if (children[i].Tag == Tag)
                    CWT.Add( children[i]);
            }
            return CWT;
        }

        //get all children on a layer
        public List<GameObject> GetChildrenOnLayer(string Layer)
        {
            List<GameObject> CWT = new List<GameObject>();
            for (int i = 0; i < children.Count(); ++i)
            {
                if (children[i].Layer == Layer)
                    CWT.Add(children[i]);
            }
            return CWT;
        }

        //get number of children
        public int GetChildCount()
        {
            return children.Count();
        }

        //get the objects rotation
        public float GetRotation()
        {
            return (float)Math.Atan2(this.globalTransform.m2, this.globalTransform.m1);
        }

        //get the objects positoin
        public Vector2 GetPosition()
        {
            return new Vector2(globalTransform.m7, globalTransform.m8);
        }

        //get the forward facing vector
        public MathsLibrary.Vector3 GetForward()
        {
            return globalTransform * new MathsLibrary.Vector3(1, 0, 1);
        }

        //get a position forward 
        public MathsLibrary.Vector3 GetForward(float fowardOffset)
        {
            return globalTransform * new MathsLibrary.Vector3(1 * fowardOffset, 0, 1);
        }

        //return a specific component
        public Component GetComponent(Type type)
        {
            if (type == typeof(PhysicsBody))
                return (PhysicsBody)(components.Find(x => x.GetType() == type));

            if (type == typeof(Collider))
                return (Collider)(components.Find(x => x.GetType() == type));

            if (type == typeof(DestroyTimer))
                return (DestroyTimer)(components.Find(x => x.GetType() == type));

            return null;
        }


        //get the texture of an sprite object
        public Texture2D GetSprite()
        {
            for (int i = 0; i < children.Count; ++i)
            {
                if (children[i] is SpriteObject)
                    return (children[i] as SpriteObject).Texture;
            }

            return new Texture2D();
        }

        //get all game objects
        public GameObject[] GetAllGameObjects(List<GameObject> gameObjects)
        {
            return gameObjects.ToArray();
        }


        ///Setters///

        //set position with floats
        public void SetPosition(float x, float y)
        {
            localTransform.SetTranslation(x, y);
            UpdateTransform();
        }

        //set position with a vector2
        public void SetPosition(Vector2 pos)
        {
            localTransform.SetTranslation(pos.x, pos.y);
            UpdateTransform();
        }

        //set position with a vector3
        public void SetPosition(MathsLibrary.Vector3 pos)
        {
            localTransform.SetTranslation(pos.x, pos.y);
            UpdateTransform();
        }

        //set rotaion
        public void SetRotate(float radians)
        {
            localTransform.SetRotateZ(radians);
            UpdateTransform();
        }


        ///Functions///

        public void MakeSureCollisionComponentsAreLast()
        {
            for(int i = 0; i < components.Count; i++)
            {
                if(components[i] is Collider)
                {
                    Collider collider = components[i] as Collider;
                    components.Remove(components[i]);
                    components.Add(collider);
                    return;
                }
            }
        }

        //add an object as a child
        public void AddChild(GameObject child)
        {
            child.Parent = this;
            children.Add(child);
        }

        //remove an object from children
        public void RemoveChild(GameObject child)
        {
            if (children.Remove(child) == true)
            {
                child.parent = null;
            }
        }

        //update global transform for an object and all its children
        public void UpdateTransform()
        {
            if (parent != null)
            {
                //if this object is a child object
                globalTransform = parent.globalTransform * localTransform;
            }
            else
            {
                globalTransform = localTransform;
            }
                
            foreach (GameObject obj in children)
                obj.UpdateTransform();
        }

        
        //rotate
        public void Rotate(float radians)
        {
            localTransform.RotateZ(radians);
            UpdateTransform();
        }

        //move the object by the vector
        public void Translate(MathsLibrary.Vector3 vector3)
        {
            localTransform.Translate(vector3);
            UpdateTransform();
        }


        //tells its children to draw themselves, is recersive
        public virtual void OnDraw()
        {
            //DrawPixelV(new Vector2(globalTransform.m7, globalTransform.m8), Color.RED);
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is SpriteObject)
                {
                    (children[i] as SpriteObject).OnDraw();
                }

                if(children[i] is GameObject)
                {
                    children[i].OnDraw();
                }
            }
        }

        //add a component
        public void AddComponent(Component component)
        {
            if(!components.Contains(component))
                components.Add(component);
        }

        //destroy this object
        public void Destroy()
        {
            dispose = true;
        }

        
    }
}
