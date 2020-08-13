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
        public void Load(string filename)
        {
            Image img = LoadImage(filename);
            texture = LoadTextureFromImage(img);
        }

        public override void OnDraw()
        {
            float rotation = (float)Math.Atan2(this.globalTransform.Axis[0].xyz[1], this.globalTransform.Axis[0].xyz[0]);
            DrawTextureEx(Texture, new Vector2(this.globalTransform.Axis[2].xyz[0], this.globalTransform.Axis[2].xyz[1]), rotation * (float)(180.0f / Math.PI), 1, Color.WHITE);
            DrawPixelV(new Vector2(globalTransform.Axis[2].xyz[0], globalTransform.Axis[2].xyz[1]), Color.RED);
        }
    }
}
