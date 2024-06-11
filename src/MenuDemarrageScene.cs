using System.Numerics;
using Raylib_cs;

class MenuDemarrageScene : IScene
{    
    private static SceneManager sceneManager;  

    // Constructeur qui porte le nom de la scene
    public MenuDemarrageScene() : base(ListeScene.SCENE_DEMARRAGE) {
        sceneManager = SceneManager.GetInstance();
    }

    Button buttonStart;
    Button buttonQuit;
    Sprite background;
    
    Music music; 

    public override void Draw()
    {
        // Start
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);
        
        background.DrawSprite(0, Color.White);
        Raylib.DrawText("Quête du graal", Settings.windowWidth/2 - 120, 100, 30, Color.White);
        
        buttonStart.displayButton();
        buttonQuit.displayButton();
        
        // End
        Raylib.EndDrawing();
    }

    public override void LoadScene()
    {
        Console.WriteLine("Scene Demarrage load");

        // Load texture / sprites
        background = new Sprite("background.png", new Vector2(Settings.windowWidth/2,Settings.windowHeight/2), Origin.CENTER);

        // Load Object in scene
        buttonStart = new Button("buttonStart", new Vector2(Settings.windowWidth/2,Settings.windowHeight/2), Origin.CENTER, "buttonStartSound");
        buttonQuit = new Button("buttonQuit", new Vector2(Settings.windowWidth/2,Settings.windowHeight/2 + 100), Origin.CENTER, "buttonStartSound");

        // Music
        music = Raylib.LoadMusicStream("resources/Shadowed_Catacombs.mp3");
        Raylib.SetMusicVolume(music, (float)0.5);
        Raylib.PlayMusicStream(music);
    }

    public override void Update()
    {
        // Play music
        Raylib.UpdateMusicStream(music);
        
        // Buttons
        if (buttonStart.IsButtonPressed()) {
            sceneManager.AddScene(new DungeonScene());
        }

        if (buttonQuit.IsButtonPressed()) {
            Raylib.WaitTime(0.5);
            Game.quit = true;
        }           
    }
    public override void UnloadScene() {
        Console.WriteLine("Unload scene " + ListeScene.SCENE_DEMARRAGE);
        background.unloadTexture();
        buttonStart.UnloadButton();        
    }
}