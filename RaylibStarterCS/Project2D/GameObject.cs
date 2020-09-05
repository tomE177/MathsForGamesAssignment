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
        public Game game;
        public Matrix3 localTransform = new Matrix3();
        public Matrix3 globalTransform = new Matrix3();
        List<GameObject> children = new List<GameObject>();
        GameObject parent = null;
        bool collisions = false;
        public List<Component> components = new List<Component>();
        public bool dispose = false;

        public GameObject()
        {

        }

        public GameObject(string name, MathsLibrary.Vector3 pos, float rotation, string ImageAddress)
        {
            SpriteObject sprite = new SpriteObject();
            sprite.Load("..\\Images\\PNG\\Bullets\\bulletBeige_outline.png");
            sprite.Name = name + "_Sprite";
            sprite.SetPosition(sprite.Height - sprite.Height/2, -10);
            sprite.SetRotate(90 * (float)(Math.PI / 180.0f));
            AddChild(sprite);
            SetPosition(pos);
            SetRotate(rotation);
            Name = name;
        }

        public GameObject(string name, MathsLibrary.Vector3 pos, float rotation, string ImageAddress, Game Gam)
        {
            SpriteObject sprite = new SpriteObject();
            sprite.Load("..\\Images\\PNG\\Bullets\\bulletBeige_outline.png");
            sprite.Name = name + "_Sprite";
            sprite.SetPosition(sprite.Height - sprite.Height / 2, -10);
            sprite.SetRotate(90 * (float)(Math.PI / 180.0f));
            AddChild(sprite);
            SetPosition(pos);
            SetRotate(rotation);
            Name = name;
            game = Gam;
        }

        public GameObject(Vector2 pos, float rotation, Image image, Texture2D texture )
        {
            SpriteObject sprite = new SpriteObject();
            sprite.Sprite = image;
            sprite.Texture = texture;
            AddChild(sprite);
            SetPosition(pos);
            SetRotate(rotation);
        }

        public GameObject(string name ,Vector2 pos, float rotation, Image image, Texture2D texture)
        {
            SpriteObject sprite = new SpriteObject();
            sprite.Sprite = image;
            sprite.Texture = texture;
            sprite.Name = name + "_Sprite";
            AddChild(sprite);
            SetPosition(pos);
            SetRotate(rotation);
            Name = name;
        }

        public GameObject(string name, MathsLibrary.Vector3 pos, float rotation, Image image)
        {
            SpriteObject sprite = new SpriteObject();
            sprite.Sprite = image;
            sprite.Load(image);
            sprite.Name = name + "_Sprite";
            AddChild(sprite);
            SetPosition(pos);
            SetRotate(rotation);
            Name = name;
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

        public GameObject Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public GameObject GetChild(int index)
        {
            return children[index];
        }

        public GameObject GetChild(string Name)
        {
            for(int i = 0; i < children.Count(); ++i)
            {
                if (children[i].Name == Name)
                    return children[i];
            }
            return null;
        }

        public List<GameObject> GetAllChildren()
        {
            return children;
        }

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

        public int GetChildCount()
        {
            return children.Count();
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

        public bool Collisions
        {
            get
            {
                return Collisions;
            }
            set
            {
                collisions = value;
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


        public void AddChild(GameObject child)
        {
            child.Parent = this;
            children.Add(child);
        }

        public void RemoveChild(GameObject child)
        {
            if (children.Remove(child) == true)
            {
                child.parent = null;
            }
        }

        public void UpdateTransform()
        {
            if (parent != null)
            {
                globalTransform = parent.globalTransform * localTransform;
            }
            else
            {
                globalTransform = localTransform;
            }
                
            foreach (GameObject obj in children)
                obj.UpdateTransform();
        }

        public void SetPosition(float x, float y)
        {
            localTransform.SetTranslation(x,y);
            UpdateTransform();
        }

        public void SetPosition(Vector2 pos)
        {
            localTransform.SetTranslation(pos.x, pos.y);
            UpdateTransform();
        }

        public void SetPosition(MathsLibrary.Vector3 pos)
        {
            localTransform.SetTranslation(pos.x, pos.y);
            UpdateTransform();
        }

        public void SetRotate(float radians)
        {
            localTransform.SetRotateZ(radians);
            UpdateTransform();
        }

        public void Rotate(float radians)
        {
            localTransform.RotateZ(radians);
            UpdateTransform();
        }

        public void Translate(MathsLibrary.Vector3 vector3)
        {
            localTransform.Translate(vector3);
            UpdateTransform();
        }


        public virtual void OnDraw()
        {
            DrawPixelV(new Vector2(globalTransform.m7, globalTransform.m8), Color.RED);
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is SpriteObject)
                {
                    (children[i] as SpriteObject).OnDraw();
                }
            }
        }

        public float GetRotation()
        {
            return (float)Math.Atan2(this.globalTransform.m2, this.globalTransform.m1);
            //return (float)Math.Atan2(this.globalTransform.m2, this.globalTransform.m1) * (float)(180.0f / Math.PI);

        }

        public Vector2 GetPositon()
        {
            return new Vector2(globalTransform.m7, globalTransform.m8);
        }

        public MathsLibrary.Vector3 GetForward()
        {
            return globalTransform * new MathsLibrary.Vector3(1, 0, 1);
        }

        public MathsLibrary.Vector3 GetForward(float fowardOffset)
        {
            return globalTransform * new MathsLibrary.Vector3(1*fowardOffset, 0, 1);
        }

        public void AddComponent(Component component)
        {
            if(!components.Contains(component))
                components.Add(component);
        }

        public Component GetComponent(Type type)
        {
            if (type == typeof(PhysicsMove))
                return (PhysicsMove)(components.Find(x => x.GetType() == type));

            if (type == typeof(Collider))
                return (Collider)(components.Find(x => x.GetType() == type));

            if (type == typeof(DestroyTimer))
                return (DestroyTimer)(components.Find(x => x.GetType() == type));

            return null;
        }

        public void Dispose()
        {
            dispose = true;
        }

        public Texture2D GetSprite()
        {
            for(int i = 0; i < children.Count; ++i)
            {
                if (children[i] is SpriteObject)
                    return (children[i] as SpriteObject).Texture;
            }

            return new Texture2D();
        }

        public GameObject[] GetAllGameObjects(List<GameObject> gameObjects)
        {
            return gameObjects.ToArray();
        }
    }
}
