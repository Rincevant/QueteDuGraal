using System.Numerics;
using Raylib_cs;

public class DungeonScene : IScene
{
    private static SceneManager sceneManager;

    Sprite background;
    Sprite cadreDonjon;

    Button buttonWalk;

    Button buy;

    Music music;

    // 14 Max dans l'affichage actuel
    List<string> logs = new List<string>();

    int logPagin = 0;
    int count = 0;

    int gold = 0;
    int life = 10;

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

        Raylib.DrawText("Points de vie : "+life+"/10", 100, 230, 20, Color.White);
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
        buy.displayButton();
        
        // End
        Raylib.EndDrawing();
    }   
    
    public override void LoadScene()
    {        
        Console.WriteLine("Scene dungeon load");

        // Load texture / sprites
        background = new Sprite("background.png", new Vector2(Settings.windowWidth/2,Settings.windowHeight/2), Origin.CENTER);
        cadreDonjon = new Sprite("cadreDonjon.png", new Vector2(Settings.windowWidth/2,Settings.windowHeight/2), Origin.CENTER);

        buttonWalk = new Button("btnAvancer", new Vector2(Settings.windowWidth/2 - 150, 800), Origin.CENTER, "buttonStartSound");
        buy = new Button("btnBuy", new Vector2(Settings.windowWidth/2 + 150, 800), Origin.CENTER, "buttonStartSound");

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
            int eventValue  = getRandomNumber(1, 10);

            if(eventValue >= 1 && eventValue <= 6) {

                int eventCoffre  = getRandomNumber(1, 100);
                if(eventCoffre >=1 && eventCoffre <= 5) {
                    // Graal
                    count++;
                    logs.Add(count + ": Vous avez trouvé le Saint Graal.");
                    buttonWalk.disable = true;
                }else if(eventCoffre >=6 && eventCoffre <= 100) {
                    // Coffre
                    count++;
                    logs.Add(count + ": Vous découvez un coffre.");                
                    int goldGagne  = getRandomNumber(1, 70);
                    
                    count++;
                    logs.Add(count + ": A l'intérieur ce trouve "+goldGagne+" pièces d'or.");
                    gold += goldGagne;
                }                
            } else if(eventValue >= 7 && eventValue <= 10) {
                // Ennemi
                count++;
                logs.Add(count + ": En garde un ennemi vous attaque !");

                count++;
                logs.Add(count + ": Vous perdez 1 point de vie mais votre ennemi est mort !");

                life--;
            }
        }
    
    
        if(buy.IsButtonPressed()) {
            if(gold >= 150) {
                // Achat potion
                count++;
                logs.Add(count + ": Vous achetez une potion de vie (+2).");

                if(life + 2 >= 10) {
                    life = 10;
                } else {
                    life += 2;
                }

                gold -= 150;
            } else {
                // Achat potion
                count++;
                logs.Add(count + ": Vous n'avez pas assez de pièces d'or (150 requis).");
            }
        }
    }

    public int getRandomNumber(int start, int end) {
        Random generator = new Random();
        return generator.Next(start, end);
    }
}