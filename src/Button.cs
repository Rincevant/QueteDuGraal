using System.Numerics;
using Raylib_cs;

public class Button {
    public Sprite _idle;
    public Vector2 _position;
    public int _state;
    public Sound _fxButton;

    public bool disable = false;

    Rectangle sourceRectangle;
    Rectangle destRectangle;

    public Button (string name, int posX, int posY, Origin origin, string fxButtonName) {
        sourceRectangle = new Rectangle();
        destRectangle = new Rectangle();
        _state = 0;
        _position = new Vector2(posX * Settings.GetScale(), posY * Settings.GetScale());
        _fxButton = Raylib.LoadSound("Resources/"+fxButtonName+".wav");
        LoadSpriteButton(name, posX, posY, origin);
    }
    public void DisplayButton() {
        destRectangle = new Rectangle(_position.X, _position.Y, _idle._texture.Width * Settings.GetScale(), (_idle._texture.Height / 2) * Settings.GetScale());

        if (_state == 0) { 
            sourceRectangle = new Rectangle(0, 0, _idle._texture.Width,  _idle._texture.Height / 2 );            
            Raylib.DrawTexturePro(_idle._texture, sourceRectangle, destRectangle, _idle._origin, 0, Color.White);
        } else if(_state == 1) {
            sourceRectangle = new Rectangle(0, _idle._texture.Height / 2, _idle._texture.Width, _idle._texture.Height / 2);
            Raylib.DrawTexturePro(_idle._texture, sourceRectangle, destRectangle, _idle._origin, 0, Color.White);
        }     
    }
    private void LoadSpriteButton(string name, int posX, int posY, Origin origin) {
        _idle = new Sprite(name, posX, posY, origin);
    }
    public void PlaySound() {
        Raylib.PlaySound(_fxButton);
    }

    public void UnloadButton() {
        Console.WriteLine("Unload button");
        _idle.unloadTexture();
    }
    public Rectangle GetDestCollisionRec() {
        return _idle.GetDestCollisionButton();
    }

    public bool IsButtonPressed() {
        if(disable) {
            _state = 0;
            return false;
        }

        Vector2 mousePoint = Raylib.GetMousePosition();
        if (Raylib.CheckCollisionPointRec(mousePoint, GetDestCollisionRec())) {
                // On Hover
                if(_state == 0) {
                    _state = 1;
                }

                // Released
                if(Raylib.IsMouseButtonReleased(MouseButton.Left)) {
                    PlaySound();
                    return true;
                }
                
        } else {
            _state = 0;
        }      
        return false;     
    }
}