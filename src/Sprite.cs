using System.Numerics;
using Raylib_cs;

public class Sprite
{
    public Texture2D _texture;
    public Vector2 _position { get; set; }
    public Vector2 _origin { get; set; }
    public Rectangle Rect
    {
        get { return new Rectangle(0, 0, _texture.Width, _texture.Height); }
    }

    public Rectangle Dest
    {
        get { return new Rectangle((int)_position.X, (int)_position.Y, _texture.Width, _texture.Height); }
    }

    private string textureBasePath = "resources/";
    public Sprite(string textureName, Vector2 position, Origin origin)
    {
        _texture = Raylib.LoadTexture(textureBasePath + textureName);
        _position = position;
        _origin = GetOriginVecFromEnum(origin);
    }
    public void unloadTexture() {
        Raylib.UnloadTexture(_texture);
    }
    public Vector2 GetOriginVecFromEnum(Origin origin) {
        if(origin == Origin.TOP_LEFT) {
            return new Vector2(0,0);
        } else if(origin == Origin.CENTER) {
            return new Vector2(_texture.Width/2, _texture.Height/2);
        }
        return new Vector2(0,0);        
    }
    public Rectangle GetDestCollisionRec() {
        return new Rectangle((int)_position.X - _origin.X, (int)_position.Y - _origin.Y, _texture.Width, _texture.Height);
    }

    public void DrawSprite(float rotation, Color color)
    {
        Raylib.DrawTexturePro(_texture, Rect, Dest, _origin, rotation, color);
    }
}