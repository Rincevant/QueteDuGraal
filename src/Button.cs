using System.Numerics;
using Raylib_cs;

public class Button {
    public Sprite idle;
    public Sprite onHover;
    public Sprite onClick;
    public Vector2 _position;
    public int _state;
    public Sound fxButton;

    public bool disable = false;

    public Button (string name, Vector2 position, Origin origin, string fxButtonName) {        
        _state = 0;
        _position = position;
        fxButton = Raylib.LoadSound("Resources/"+fxButtonName+".wav");
        loadSpriteButton(name, position, origin);
    }
    public void displayButton() {      
        if(_state == 0) {            
            Raylib.DrawTexturePro(idle._texture, idle.Rect, idle.Dest, idle._origin, 0, Color.White);
        } else if(_state == 1) {
            Raylib.DrawTexturePro(onHover._texture, onHover.Rect, onHover.Dest, onHover._origin, 0, Color.White);
        } else if(_state == 2) {
            Raylib.DrawTexturePro(onClick._texture, onClick.Rect, onClick.Dest, onClick._origin, 0, Color.White);
        }        
    }
    private void loadSpriteButton(string name, Vector2 position, Origin origin) {
        idle = new Sprite(name + "Idle.png", position, origin);
        onHover = new Sprite(name + "OnHover.png", position, origin);
        onClick = new Sprite(name + "OnPressed.png", position, origin);
    }
    public void PlaySound() {
        Raylib.PlaySound(fxButton);
    }

    public void UnloadButton() {
        Console.WriteLine("Unload button");
        idle.unloadTexture();
        onHover.unloadTexture();
        onClick.unloadTexture();
    }
    public Rectangle GetDestCollisionRec() {
        return idle.GetDestCollisionRec();
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

                // Clicked
                if(Raylib.IsMouseButtonPressed(MouseButton.Left)) {
                    _state = 2;                    
                } 

                // Released
                if(Raylib.IsMouseButtonReleased(MouseButton.Left)) {
                    PlaySound();
                    _state = 1;
                    return true;
                }
                
        } else {
            _state = 0;
        }      
        return false;     
    }
}