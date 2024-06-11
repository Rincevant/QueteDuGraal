using System.Numerics;
using Raylib_cs;

public class DungeonScene : IScene
{
    private static SceneManager sceneManager;

    Sprite background;
    Sprite cadreDonjon;

    Button buttonWalk;

    // 14 Max dans l'affichage actuel
    List<string> logs = new List<string>();

    int logPagin = 0;
    int count = 0;

    // Constructeur qui porte le nom de la scene
    public DungeonScene() : base(ListeScene.SCENE_DUNGEON){
        sceneManager = SceneManager.GetInstance();
    }

    public override void Draw()
    {
        // Start
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);        

        background.DrawSprite(0, Color.White);

        Raylib.DrawText("Le cimetière", Settings.windowWidth/2 - 100, 100, 30, Color.White);

        Raylib.DrawText("Points de vie : 5/5", 100, 230, 20, Color.White);
        Raylib.DrawText("Or : 126 pièces", 780, 230, 20, Color.White);

        Rectangle textBox = new Rectangle(Settings.windowWidth / 2, Settings.windowWidth / 2, 800, 400);        
        Raylib.DrawRectanglePro(textBox, new Vector2(400,200), 0, Color.DarkGray);

        cadreDonjon.DrawSprite(0, Color.White);

        int heightLog = 200;
        for (int i = logPagin; i < logs.Count; i++)
        {
            Raylib.DrawText(logs[i], (int)textBox.X - 390, (int)textBox.Y + 10 - heightLog, 20, Color.White);
            heightLog -= 30;
        }

        buttonWalk.displayButton();
        
        // End
        Raylib.EndDrawing();
    }   
    
    public override void LoadScene()
    {        
        Console.WriteLine("Scene dungeon load");

        // Load texture / sprites
        background = new Sprite("background.png", new Vector2(Settings.windowWidth/2,Settings.windowHeight/2), Origin.CENTER);
        cadreDonjon = new Sprite("cadreDonjon.png", new Vector2(Settings.windowWidth/2,Settings.windowHeight/2), Origin.CENTER);

        buttonWalk = new Button("btnAvancer", new Vector2(Settings.windowWidth/2, 800), Origin.CENTER, "buttonStartSound");

        logs.Add("Vous découvrez le cimetière. Une brume epaisse vous entoure.");

    }

    public override void UnloadScene()
    {        
        Console.WriteLine("unload content dungeon scene");
    }

    public override void  Update()
    {
        if(buttonWalk.IsButtonPressed()) {
            Random generator = new Random();
            int eventValue  = generator.Next(1, 10);

            if(eventValue >= 1 && eventValue <= 4) {
                // Coffre
                logs.Add("Vous découvez un coffre.");
            } 

            if(eventValue >= 5 && eventValue <= 10) {
                // Ennemi
                logs.Add("En garde un ennemi vous attaque !");
            }

            count++;            
            if(logs.Count >= 14) {
                logPagin = logPagin + 1;
            }
        }
    }
}