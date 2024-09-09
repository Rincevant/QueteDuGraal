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

    OptionsScene optionsScene;
    bool optionsWindows = false;

    public override void Draw()
    {
        // Start
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);
        
        background.DrawSprite(0, Color.White, 1);

        titre.DrawTexte();
        
        buttonStart.DisplayButton();
        buttonQuit.DisplayButton();
        buttonOptions.DisplayButton();

        if (optionsWindows) {            
            optionsScene.Draw();
        }

        // End
        Raylib.EndDrawing();
    }

    public override void LoadScene()
    {
        Console.WriteLine("Scene Demarrage load");        

        // Load texture / sprites
        background = new Sprite("background.png", 640, 360, Origin.CENTER);

        // Load Object in scene
        buttonStart = new Button("startBtn.png", 1000, 250, Origin.CENTER, "buttonStartSound");
        buttonQuit = new Button("quitBtn.png", 1000, 330, Origin.CENTER, "buttonStartSound");
        buttonOptions = new Button("optBtn.png", 1000, 410, Origin.CENTER, "buttonStartSound");

        // Text
        titre = new Text("Quête du graal", 880, 100, 30, Color.White);

        // Scenes enfant
        optionsScene = new OptionsScene();
        optionsScene.LoadScene();

        // Music
        music = Raylib.LoadMusicStream("Resources/Shadowed_Catacombs.mp3");
        Raylib.SetMusicVolume(music, (float)0.5);
        Raylib.PlayMusicStream(music);
    }

    public override void Update()
    {
        // Play music
        Raylib.UpdateMusicStream(music);

        if (optionsWindows)
        {
            optionsScene.Update();
            return;
        }

        if (buttonOptions.IsButtonPressed())
        {
            optionsWindows = true;
        }

        // Buttons
        if (buttonStart.IsButtonPressed()) {           
            sceneManager.AddScene(new CreateCharacterScene(), true);
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

    public override void SignalToScene(string actionName)
    {
        if(actionName.Equals("closeOptions"))
        {
            optionsWindows = false;
        }
    }
}