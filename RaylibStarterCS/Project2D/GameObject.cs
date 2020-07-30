using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathsLibrary;
using Raylib;

namespace Project2D
{
    class GameObject
    {
        string name;
        string tag;
        int layer;
        public Matrix3 localTransform = new Matrix3();
        public Matrix3 globalTransform = new Matrix3();
        Image sprite;
        Texture2D texture;

        List<GameObject> children = new List<GameObject>();
        GameObject parent = null;


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

        public List<GameObject> GetAllChildren(int index)
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

        public List<GameObject> GetChildrenOnLayer(int Layer)
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

        public int Layer
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


        public Texture2D Texture
        {
            get
            {
                return texture;
            }
            set
            {
                texture = value;
            }
        }

        public Image Sprite
        {
            get
            {
                return sprite;
            }
            set
            {
                sprite = value;
            }
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
                globalTransform = parent.globalTransform * localTransform;
            else
                globalTransform = localTransform;

            foreach (GameObject obj in children)
                obj.UpdateTransform();
        }

        public void SetPosition(float x, float y)
        {
        }
    }
}
