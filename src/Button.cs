using System.Numerics;
using Raylib_cs;

public class Button {
    public Sprite idle;
    public Vector2 _position;
    public int _state;
    public Sound fxButton;

    public bool disable = false;

    Rectangle sourceRectangle;
    Rectangle destRectangle;

    public Button (string name, int posX, int posY, Origin origin, string fxButtonName) {
        sourceRectangle = new Rectangle();
        destRectangle = new Rectangle();
        _state = 0;
        _position = new Vector2(posX * Settings.getScale(), posY * Settings.getScale());
        fxButton = Raylib.LoadSound("Resources/"+fxButtonName+".wav");
        loadSpriteButton(name, posX, posY, origin);
    }
    public void displayButton() {
        destRectangle = new Rectangle(_position.X, _position.Y, idle._texture.Width * Settings.getScale(), (idle._texture.Height / 2) * Settings.getScale());

        if (_state == 0) { 
            sourceRectangle = new Rectangle(0, 0, idle._texture.Width,  idle._texture.Height / 2 );            
            Raylib.DrawTexturePro(idle._texture, sourceRectangle, destRectangle, idle._origin, 0, Color.White);
        } else if(_state == 1) {
            sourceRectangle = new Rectangle(0, idle._texture.Height / 2, idle._texture.Width, idle._texture.Height / 2);
            Raylib.DrawTexturePro(idle._texture, sourceRectangle, destRectangle, idle._origin, 0, Color.White);
        }     
    }
    private void loadSpriteButton(string name, int posX, int posY, Origin origin) {
        idle = new Sprite(name, posX, posY, origin);
    }
    public void PlaySound() {
        Raylib.PlaySound(fxButton);
    }

    public void UnloadButton() {
        Console.WriteLine("Unload button");
        idle.unloadTexture();
    }
    public Rectangle GetDestCollisionRec() {
        return idle.GetDestCollisionButton();
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