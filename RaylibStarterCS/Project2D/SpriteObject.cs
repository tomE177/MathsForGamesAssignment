using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib;
using static Raylib.Raylib;

namespace Project2D
{
    class SpriteObject : GameObject
    {
        Texture2D texture = new Texture2D();
        Image sprite = new Image();
        public float Width
        {
            get { return texture.width; }
        }
        public float Height
        {
            get { return texture.height; }
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

        public SpriteObject()
        {
        }

        public SpriteObject(List<SpriteObject> spriteObjects)
        {
            spriteObjects.Add(this);
        }

        public void Load(string filename)
        {
            Image img = LoadImage(filename);
            texture = LoadTextureFromImage(img);
        }

        public void Load(Image image)
        {
            Image img = image;
            texture = LoadTextureFromImage(img);
        }

        //Draw itself and call draw function on all of its children
        public override void OnDraw()
        {
            //get rotation
            float rotation = (float)Math.Atan2(this.GlobalTransform.m2, this.GlobalTransform.m1);
            DrawTextureEx(Texture, new Vector2(this.GlobalTransform.m7, this.GlobalTransform.m8), rotation * (float)(180.0f / Math.PI), 1, Color.WHITE);

            //get a list of all children
            var children = GetAllChildren();
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i] is SpriteObject)
                {
                    (children[i] as SpriteObject).OnDraw();
                }
            }

        }

    }
}
