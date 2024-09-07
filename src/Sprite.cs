using System.Numerics;
using Raylib_cs;

public class Sprite
{
    public Texture2D _texture;
    public Vector2 _position { get; set; }
    public Vector2 _origin { get; set; }
    public virtual Rectangle Rect
    {
        get { return new Rectangle(0, 0, _texture.Width, _texture.Height); }
        set {}
    }

    public virtual Rectangle Dest
    {
        get { return new Rectangle((int)_position.X, (int)_position.Y, (float)(_texture.Width * Settings.GetScale()), (float)(_texture.Height * Settings.GetScale())); }
        set {}
    }

    private string textureBasePath = "Resources/";
    public Sprite(string textureName, int posX, int posY, Origin origin)
    {
        _texture = Raylib.LoadTexture(textureBasePath + textureName);
        _position = new Vector2(posX * Settings.GetScale(), posY * Settings.GetScale());
        _origin = GetOriginVecFromEnum(origin);
    }
    public void unloadTexture() {
        Raylib.UnloadTexture(_texture);
    }
    public Vector2 GetOriginVecFromEnum(Origin origin) {
        if(origin == Origin.TOP_LEFT) {
            return new Vector2(0,0);
        } else if(origin == Origin.CENTER) {
            return new Vector2(_texture.Width/2 * Settings.GetScale(), _texture.Height/2 * Settings.GetScale());
        }
        return new Vector2(0,0);        
    }
    public Rectangle GetDestCollisionRec() {
        return new Rectangle((int)_position.X - _origin.X, (int)_position.Y - _origin.Y, _texture.Width * Settings.GetScale(), _texture.Height * Settings.GetScale());
    }

    public Rectangle GetDestCollisionButton()
    {
        return new Rectangle((int)_position.X - _origin.X, (int)_position.Y - _origin.Y, _texture.Width * Settings.GetScale(), _texture.Height / 2 * Settings.GetScale());
    }

    public void DrawSprite(float rotation, Color color, float scale)
    {
        if(scale != 1 && scale != 0) {
            Rectangle newRect = new Rectangle(1024, 1024, Rect.Width, Rect.Height);
            Rectangle newDest = new Rectangle(Dest.X, Dest.Y, Dest.Width * scale, Dest.Height * scale);
            Vector2 newOrigin = new Vector2(_origin.X * scale, _origin.Y * scale);
            Raylib.DrawTexturePro(_texture, newRect, newDest, newOrigin, rotation, color);
        } else {
            Raylib.DrawTexturePro(_texture, Rect, Dest, _origin, rotation, color);
        }
        
    }
}