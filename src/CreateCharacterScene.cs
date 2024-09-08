using Raylib_cs;

public class CreateCharacterScene : IScene
{
    private static SceneManager sceneManager;

    // Sprites
    Sprite hero;
    Sprite background;

    // Texts
    Text titre;

    TextArea nameArea;

    // Button
    Button okBtn;
    Button optIconBtn;

    // Scene enfant
    OptionsScene optionsScene;
    bool optionsWindows = false;

    public CreateCharacterScene() : base(ListeScene.CREATE_CHARACTER)
    {
        sceneManager = SceneManager.GetInstance();
    }
    public override void Draw()
    {
        // Start
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);

        background.DrawSprite(0, Color.White, 1);

        titre.DrawTexte();
        hero.DrawSprite(0, Color.White, 0.2f);
        nameArea.DisplayTextArea();
        okBtn.DisplayButton();

        optIconBtn.DisplayButton();
        if (optionsWindows)
        {
            optionsScene.Draw();
        }

        // End
        Raylib.EndDrawing();
    }

    public override void LoadScene()
    {
        // Sprites
        hero = new Sprite("hero.png", 640, 260, Origin.CENTER);
        background = new Sprite("background_create_char.png", 640, 360, Origin.CENTER);
    
        // Texts
        titre = new Text("Création personnage", 640 - Raylib.MeasureText("Création personnage", 40) / 2, 100, 40, Color.White);

        // Text Area
        nameArea = new TextArea(640, 400, 250, 50, Origin.CENTER, 10);

        // Buttons
        okBtn = new Button("okBtn.png", 640, 550, Origin.CENTER, "buttonStartSound");
        optIconBtn = new Button("optIconBtn.png", 50, 100, Origin.CENTER, "buttonStartSound");

        // Scene enfant
        optionsScene = new OptionsScene();
        optionsScene.LoadScene();
    }

    public override void UnloadScene()
    {
    }

    public override void Update()
    {
        nameArea.UpdateTextArea();

        if(okBtn.IsButtonPressed()) {
            string heroName = nameArea._text;

            if (heroName.Length > 0)
            {
                Hero hero = new Hero(heroName);
                sceneManager.AddScene(new DungeonScene(hero), true);
            }           
        }

        if (optIconBtn.IsButtonPressed()) {
            optionsWindows = true;
        }

        if(optionsWindows) {
            optionsScene.Update();
        }
    }

    public override void SignalToScene(string actionName)
    {
        if (actionName.Equals("closeOptions")) {
            optionsWindows = false;
        }
    }
}