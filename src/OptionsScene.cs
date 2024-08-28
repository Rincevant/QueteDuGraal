using Raylib_cs;

public class OptionsScene : IScene
{
    private static SceneManager sceneManager;

    // Sprites
    Sprite optionsFrame;

    // Constructeur qui porte le nom de la scene
    public OptionsScene() : base(ListeScene.OPTIONS)
    {
        sceneManager = SceneManager.GetInstance();
    }

    public override void Draw() {

        // Start
        //Raylib.BeginDrawing();

        optionsFrame.DrawSprite(0, Color.White);

        // End
        //Raylib.EndDrawing();
    }

    public override void LoadScene() {
        
        // Sprites
        optionsFrame = new Sprite("optionFrame.png", 640, 360, Origin.CENTER);
    }

    public override void Update() {
        
    }

    public override void UnloadScene() {
        Console.WriteLine("Unload scene " + ListeScene.OPTIONS);
        optionsFrame.unloadTexture();
    }

}
