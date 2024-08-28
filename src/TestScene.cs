using System.Numerics;
using Raylib_cs;

public class TestScene : IScene
{

    Tile spriteSheet;
    public TestScene() : base(ListeScene.SCENE_TEST) {

    }
    public override void Draw()
    {
         // Start
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.White);  

        spriteSheet.DrawSprite(0, Color.White);   

        // End
        Raylib.EndDrawing();
    }

    public override void LoadScene()
    {
        // Load texture / sprites
        spriteSheet = new Tile("spriteSheet.png", new Vector2(Settings.windowWidth/2,Settings.windowHeight/2), Origin.CENTER);
    }

    public override void UnloadScene()
    {
    }

    public override void Update()
    {
        

        int index = (int)(Raylib.GetTime() * 8 % 5);

        if(Raylib.GetTime() >= 1) {
           
        }
        if(Raylib.IsKeyDown(KeyboardKey.Right)) {
            spriteSheet.recDestTest = new Rectangle(spriteSheet.recDestTest.X + 5, spriteSheet.recDestTest.Y, 190, 200);
        } 
            
        spriteSheet.recSourceTest = new Rectangle(index * 90, spriteSheet.recSourceTest.Y , 95, 100);

            //Console.WriteLine(spriteSheet.Rect.X + " " + spriteSheet.Rect.Y);
        
    }
}