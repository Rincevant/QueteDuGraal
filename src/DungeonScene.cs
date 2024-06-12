using System.Numerics;
using Raylib_cs;

public class DungeonScene : IScene
{
    private static SceneManager sceneManager;

    Sprite background;
    Sprite cadreDonjon;

    Button buttonWalk;

    Music music;

    // 14 Max dans l'affichage actuel
    List<string> logs = new List<string>();

    int logPagin = 0;
    int count = 0;

    int gold = 0;

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
        Raylib.DrawText("Or : "+gold+" pièces", 780, 230, 20, Color.White);

        Rectangle textBox = new Rectangle(Settings.windowWidth / 2, Settings.windowWidth / 2, 800, 400);        
        Raylib.DrawRectanglePro(textBox, new Vector2(400,200), 0, Color.DarkGray);

        cadreDonjon.DrawSprite(0, Color.White);

        int heightLog = 200;
        int start = 0;
        if (logs.Count - 13 > 0) {
            start = logs.Count - 13;
        }
        for (int i = start; i < logs.Count; i++)
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

        logs.Add(count + ": Vous découvrez le cimetière. Une brume epaisse vous entoure.");

        // Music
        music = Raylib.LoadMusicStream("Resources/Shadowed_Catacombs.mp3");
        Raylib.SetMusicVolume(music, (float)0.5);
        Raylib.PlayMusicStream(music);

    }

    public override void UnloadScene()
    {        
        Console.WriteLine("unload content dungeon scene");
    }

    public override void  Update()
    {
         // Play music
        Raylib.UpdateMusicStream(music);

        if(buttonWalk.IsButtonPressed()) {
            Random generator = new Random();
            int eventValue  = generator.Next(1, 10);

            if(eventValue >= 1 && eventValue <= 4) {
                // Coffre
                count++;
                logs.Add(count + ": Vous découvez un coffre.");                
                eventValue  = generator.Next(1, 100);
                
                count++;
                logs.Add(count + ": A l'intérieur ce trouve "+eventValue+" pièces d'or.");
                gold += eventValue;
            } 

            if(eventValue >= 5 && eventValue <= 10) {
                // Ennemi
                count++;
                logs.Add(count + ": En garde un ennemi vous attaque !");
            }
        }
    }
}