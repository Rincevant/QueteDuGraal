using System.Numerics;
using Raylib_cs;

public class DungeonScene : IScene
{
    private static SceneManager sceneManager;

    Sprite background;

    Button buttonWalk;

    // Constructeur qui porte le nom de la scene
    public DungeonScene() : base(ListeScene.SCENE_DUNGEON){
        sceneManager = SceneManager.GetInstance();
    }

    
    public override void Draw()
    {
        // Start
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);
        
        //Raylib.DrawText("DUNGEON SCENE", 12, 12, 20, Color.Black);

        background.DrawSprite(0, Color.White);

        Raylib.DrawText("Le cimetière", Settings.windowWidth/2 - 100, 100, 30, Color.White);

        Rectangle textBox = new Rectangle(Settings.windowWidth / 2, Settings.windowWidth / 2, 800, 400);

        Raylib.DrawRectanglePro(textBox, new Vector2(400,200), 0, Color.DarkGray);

        Raylib.DrawText("Vous découvrez le cimetière. Une brume epaisse vous entoure.", (int)textBox.X - 400, (int)textBox.Y - 200, 20, Color.White);
        Raylib.DrawText("Vous avancer et vous trouver un coffre.", (int)textBox.X - 400, (int)textBox.Y - 170, 20, Color.White);

        buttonWalk.displayButton();

        
        
        
        // End
        Raylib.EndDrawing();
    }   
    
    public override void LoadScene()
    {        
        Console.WriteLine("Scene dungeon load");

        // Load texture / sprites
        background = new Sprite("background.png", new Vector2(Settings.windowWidth/2,Settings.windowHeight/2), Origin.CENTER);

        buttonWalk = new Button("buttonStart", new Vector2(Settings.windowWidth/2, 800), Origin.CENTER, "buttonStartSound");
    }

    public override void UnloadScene()
    {        
        Console.WriteLine("unload content dungeon scene");
    }

    public override void  Update()
    {
        buttonWalk.IsButtonPressed();
    }
}