using Raylib_cs;
using System.Numerics;
public class Text
{

    public string _text;
    public Vector2 _position;
    public Color _color;
    public int _fontSize;

    
    public Text(string text, int posX, int posY, int fontSize, Color color)
    {
        _text = text;
        _position = new Vector2(posX * Settings.getScale(), posY * Settings.getScale());
        _fontSize = (int)(fontSize * Settings.getScale());
        _color = color;
    }

    public void DrawTexte()
    {
        Raylib.DrawText(_text, (int)_position.X, (int)_position.Y, _fontSize, _color);
    }

    public void DrawTexteWithData(string data)
    {
        Raylib.DrawText(_text.Replace("{}", data), (int)_position.X, (int)_position.Y, _fontSize, _color);
    }
}
