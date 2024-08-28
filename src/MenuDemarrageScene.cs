using Raylib_cs;

class MenuDemarrageScene : IScene
{    
    private static SceneManager sceneManager;  

    // Constructeur qui porte le nom de la scene
    public MenuDemarrageScene() : base(ListeScene.SCENE_DEMARRAGE) {
        sceneManager = SceneManager.GetInstance();
    }

    // Buttons
    Button buttonStart;
    Button buttonQuit;
    Button buttonOptions;

    // Sprites
    Sprite background;
    
    // Texts
    Text titre;

    // Sounds
    Music music;

    // Scenes Enfant
    IScene optionsScene;
    
    bool optionsWindows = false;

    public override void Draw()
    {
        // Start
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);
        
        background.DrawSprite(0, Color.White);

        titre.DrawTexte();
        
        buttonStart.displayButton();
        buttonQuit.displayButton();
        buttonOptions.displayButton();

        if (optionsWindows) {
            sceneManager.GetSceneByName(ListeScene.OPTIONS).Draw();
        }

        // End
        Raylib.EndDrawing();
    }

    public override void LoadScene()
    {
        Console.WriteLine("Scene Demarrage load");

        sceneManager.AddScene(new OptionsScene(), false);

        // Load texture / sprites
        background = new Sprite("background.png", Settings.initwindowWidth / 2, Settings.initwindowHeight / 2, Origin.CENTER);

        // Load Object in scene
        buttonStart = new Button("startBtn.png", 1000, 250, Origin.CENTER, "buttonStartSound");
        buttonQuit = new Button("quitBtn.png", 1000, 330, Origin.CENTER, "buttonStartSound");
        buttonOptions = new Button("optBtn.png", 1000, 410, Origin.CENTER, "buttonStartSound");

        // Text
        titre = new Text("Quête du graal", 880, 100, 30, Color.White);

        // Music
        music = Raylib.LoadMusicStream("Resources/Shadowed_Catacombs.mp3");
        Raylib.SetMusicVolume(music, (float)0.5);
        Raylib.PlayMusicStream(music);
    }

    public override void Update()
    {
        // Play music
        Raylib.UpdateMusicStream(music);
        
        // Buttons
        if (buttonStart.IsButtonPressed()) {
            sceneManager.AddScene(new DungeonScene(), true);            
        }

        if (buttonQuit.IsButtonPressed()) {
            Raylib.WaitTime(0.5);
            Game.quit = true;
        }

        if (buttonOptions.IsButtonPressed())
        {
            optionsWindows = true;
        }


        if(optionsWindows)
        {
            sceneManager.GetSceneByName(ListeScene.OPTIONS).Update();
        }
    }
    public override void UnloadScene() {
        Console.WriteLine("Unload scene " + ListeScene.SCENE_DEMARRAGE);
        background.unloadTexture();
        buttonStart.UnloadButton();        
    }
}