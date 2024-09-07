using Raylib_cs;
using System.Numerics;
class TextArea
{
    public string text;
    Vector2 origin;
    Vector2 position;
    int width;
    int height;

    int maxLength;
    int framesCounter = 0;

    protected virtual Rectangle textBox
    {
        get { return new Rectangle((int)position.X * Settings.GetScale(), (int)position.Y * Settings.GetScale(), (float)(width * Settings.GetScale()), (float)(height * Settings.GetScale())); }
        set { }
    }
    public TextArea(int posX, int posY, int widthTextAre, int heightTextArea, Origin ori, int max)
    {
        maxLength = max;
        width = widthTextAre;
        height = heightTextArea;
        position = new Vector2(posX, posY);
        origin = GetOriginVecFromEnum(ori);
        text = "";
    }

    public void DisplayTextArea()
    {
        // Draw principal rectangle
        Raylib.DrawRectanglePro(textBox, origin, 0, Color.LightGray);

        // Draw outliner
        if (IsTextAreaOnOver())
        {
            Rectangle rec = new Rectangle(textBox.X - origin.X, textBox.Y - origin.Y, textBox.Width, textBox.Height);
            Raylib.DrawRectangleLinesEx(rec, 3, Color.Beige);
        }
        else {
            Raylib.DrawRectangleLines((int)textBox.X - (int)origin.X, (int)textBox.Y - (int)origin.Y, (int)textBox.Width, (int)textBox.Height, Color.DarkGray);
        }

        Raylib.DrawText(text, (int)textBox.X + 5 - (int)origin.X, (int)textBox.Y + 8 - (int)origin.Y, (int)(40 * Settings.GetScale()), Color.Black);

        if (IsTextAreaOnOver()) {
            framesCounter++;
            if (((framesCounter / 20) % 2) == 0 && text.Length != maxLength) {
                Raylib.DrawText("_", (int)((textBox.X + (8 * Settings.GetScale()) - (int)origin.X)) + (int)(Raylib.MeasureText(text, 40) * Settings.GetScale()), (int)((textBox.Y + (12 * Settings.GetScale()) - (int)origin.Y)), (int)(40 * Settings.GetScale()), Color.Brown);
            }
        }
        else {
            framesCounter = 0;
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
            return new Vector2(width / 2 * Settings.GetScale(), height / 2 * Settings.GetScale());
        }
        return new Vector2(0, 0);
    }

    public Rectangle GetDestCollisionTextAreaRec()
    {
        return new Rectangle((int)textBox.X - origin.X, (int)textBox.Y - origin.Y, textBox.Width * Settings.GetScale(), textBox.Height * Settings.GetScale());
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
                if ((key >= 32) && (key <= 125) && (text.Length < maxLength))
                {
                    text += (char)key;
                }

                key = Raylib.GetCharPressed();  // Check next character in the queue
            }

            if (Raylib.IsKeyPressed(KeyboardKey.Backspace) && text.Length >= 1)
            {                
                text = text.Substring(0, text.Length - 1);
            }
        }
    }
}