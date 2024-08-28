using System.Numerics;
using Raylib_cs;

public class Tile : Sprite
{
    public Rectangle recSourceTest;
    public override Rectangle Rect
    {
        get { return recSourceTest; }
        set {}
    }

    public Rectangle recDestTest;
    public override Rectangle Dest
    {
        get { return recDestTest; }
        set {}
    }

    public Tile(string textureName, Vector2 position, Origin origin) : base(textureName, (int)position.X, (int)position.Y, origin)
    {
        recSourceTest = new Rectangle(0, 0, 95, 100);
        recDestTest = new Rectangle(500, 500, 190, 200);
    }
}