using Raylib_cs;
using System.Numerics;
class TextArea
{
    public string _text;
    Vector2 _origin;
    Vector2 _position;
    int _width;
    int _height;

    int _maxLength;
    int _framesCounter = 0;

    protected virtual Rectangle TextBox
    {
        get { return new Rectangle((int)_position.X * Settings.GetScale(), (int)_position.Y * Settings.GetScale(), (float)(_width * Settings.GetScale()), (float)(_height * Settings.GetScale())); }
        set { }
    }

    protected virtual Vector2 Position
    {
        get { return new Vector2(_position.X * Settings.GetScale(), _position.Y * Settings.GetScale()); }
        set { }
    }

    protected virtual Vector2 OriginTextArea
    {
        get { return new Vector2(_origin.X * Settings.GetScale(), _origin.Y * Settings.GetScale()); }
        set { }
    }


    public TextArea(int posX, int posY, int widthTextAre, int heightTextArea, Origin ori, int max)
    {
        _maxLength = max;
        _width = widthTextAre;
        _height = heightTextArea;
        _position = new Vector2(posX, posY);
        _origin = GetOriginVecFromEnum(ori);
        _text = "";
    }

    public void DisplayTextArea()
    {
        // Draw principal rectangle
        Raylib.DrawRectanglePro(TextBox, OriginTextArea, 0, Color.LightGray);

        // Draw outliner
        if (IsTextAreaOnOver())
        {
            Rectangle rec = new Rectangle(TextBox.X - OriginTextArea.X, TextBox.Y - OriginTextArea.Y, TextBox.Width, TextBox.Height);
            Raylib.DrawRectangleLinesEx(rec, 3, Color.Beige);
        }
        else {
            Raylib.DrawRectangleLines((int)TextBox.X - (int)OriginTextArea.X, (int)TextBox.Y - (int)OriginTextArea.Y, (int)TextBox.Width, (int)TextBox.Height, Color.DarkGray);
        }

        Raylib.DrawText(_text, (int)TextBox.X + 5 - (int)OriginTextArea.X, (int)TextBox.Y + 8 - (int)OriginTextArea.Y, (int)(40 * Settings.GetScale()), Color.Black);

        if (IsTextAreaOnOver()) {
            _framesCounter++;
            if (((_framesCounter / 20) % 2) == 0 && _text.Length != _maxLength) {
                Raylib.DrawText("_", (int)((TextBox.X + (8 * Settings.GetScale()) - (int)OriginTextArea.X)) + (int)(Raylib.MeasureText(_text, 40) * Settings.GetScale()), (int)((TextBox.Y + (12 * Settings.GetScale()) - (int)OriginTextArea.Y)), (int)(40 * Settings.GetScale()), Color.Brown);
            }
        }
        else {
            _framesCounter = 0;
        }
    }

    public Vector2 GetOriginVecFromEnum(Origin origin)
    {
        if (origin == Origin.TOP_LEFT)
        {
            return new Vector2(0, 0);
        }
        else if (origin == Origin.CENTER)
        {
            return new Vector2(_width / 2, _height / 2);
        }
        return new Vector2(0, 0);
    }

    public Rectangle GetDestCollisionTextAreaRec()
    {
        return new Rectangle((int)TextBox.X - _origin.X, (int)TextBox.Y - _origin.Y, TextBox.Width * Settings.GetScale(), TextBox.Height * Settings.GetScale());
    }

    public bool IsTextAreaOnOver()
    {
        if (Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), GetDestCollisionTextAreaRec())) { 
            return true;
        }
        return false;
    }

    public void UpdateTextArea()
    {
        if (IsTextAreaOnOver())
        {
            // Get char pressed (unicode character) on the queue
            int key = Raylib.GetCharPressed();

            // Check if more characters have been pressed on the same frame
            while (key > 0)
            {
                // NOTE: Only allow keys in range [32..125]
                if ((key >= 32) && (key <= 125) && (_text.Length < _maxLength))
                {
                    _text += (char)key;
                }

                key = Raylib.GetCharPressed();  // Check next character in the queue
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Backspace) && _text.Length >= 1)
            {                
                _text = _text.Substring(0, _text.Length - 1);
            }
        }
    }
}