using Raylib_cs;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;
public class Text
{

    public string _text;
    public Vector2 _position;
    public Color _color;
    public int _fontSize;

    public virtual Vector2 Position
    {
        get { return new Vector2(_position.X * Settings.GetScale(), _position.Y * Settings.GetScale()); }
        set { }
    }

    public virtual int FontSize
    {
        get { return (int)(_fontSize * Settings.GetScale()); }
        set { }
    }

    public Text(string text, int posX, int posY, int fontSize, Color color)
    {
        _text = text;
        _position = new Vector2(posX, posY);
        _fontSize = fontSize;
        _color = color;
    }

    public void DrawTexte()
    {
        Raylib.DrawText(_text, (int)Position.X, (int)Position.Y, FontSize, _color);
    }

    public void DrawTexteWithData(List<string> datas)
    {
        int dataIndex = 0;
        string textToDisplay = _text;
        while (textToDisplay.IndexOf("{}") != -1) {
            int index = textToDisplay.IndexOf("{}");
            textToDisplay = textToDisplay.Substring(0, index) + datas[dataIndex] + textToDisplay.Substring(index + 2);
            dataIndex++;
        }
        Raylib.DrawText(textToDisplay.Replace("{}", "data"), (int)Position.X, (int)Position.Y, FontSize, _color);
    }
}
