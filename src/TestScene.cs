using System.Numerics;
using Raylib_cs;

public class TestScene : IScene
{

    Sprite spriteSheet;
    Texture2D texture;
    public TestScene() : base(ListeScene.SCENE_TEST) {

    }
    public override void Draw()
    {
         // Start
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Blue);

        //spriteSheet.DrawSprite(0, Color.White);  
        Rectangle rec = new Rectangle(0, 0, texture.Width, texture.Height);
        //Rectangle dest = new Rectangle(640, 360, (float)(texture.Width * Settings.getScale()), (float)(texture.Height * Settings.getScale()));
        Rectangle dest = new Rectangle(640, 360, texture.Width, texture.Height);
        //Vector2 origin = new Vector2(texture.Width / 2 * Settings.getScale(), texture.Height / 2 * Settings.getScale());
        Vector2 origin = new Vector2(0,0);

        Raylib.DrawTexturePro(texture, rec, dest, origin, 0, Color.White);

        Raylib.DrawLine(640, 0, 640, 2000, Color.Brown);

        Raylib.DrawText("ver 2", 50, 50, 20, Color.Blue);

        // End
        Raylib.EndDrawing();
    }

    public override void LoadScene()
    {
        // Load texture / sprites
        //spriteSheet = new Sprite("background.png", 640, 360, Origin.CENTER);
        texture = Raylib.LoadTexture("Resources/" + "background.png");
    }

    public override void UnloadScene()
    {
    }

    public override void Update()
    {
        

        /*int index = (int)(Raylib.GetTime() * 8 % 5);

        if(Raylib.GetTime() >= 1) {
           
        }
        if(Raylib.IsKeyDown(KeyboardKey.Right)) {
            spriteSheet.recDestTest = new Rectangle(spriteSheet.recDestTest.X + 5, spriteSheet.recDestTest.Y, 190, 200);
        } 
            
        spriteSheet.recSourceTest = new Rectangle(index * 90, spriteSheet.recSourceTest.Y , 95, 100);*/

            //Console.WriteLine(spriteSheet.Rect.X + " " + spriteSheet.Rect.Y);
        
    }

    public override void SignalToScene(string actionName)
    {
        throw new NotImplementedException();
    }
}