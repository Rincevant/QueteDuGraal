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
    }

    public override void UnloadScene()
    {
    }

    public override void Update()
    {
        nameArea.UpdateTextArea();

        if(okBtn.IsButtonPressed()) {
            string heroName = nameArea.text;

            if (heroName.Length > 0)
            {
                Hero hero = new Hero(heroName);
                sceneManager.AddScene(new DungeonScene(hero), true);
            }
            else { 
                // Pas de nom
            }            
        }
    }

    public override void SignalToScene(string actionName)
    {
        throw new NotImplementedException();
    }
}