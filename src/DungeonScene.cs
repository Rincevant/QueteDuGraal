using System.Numerics;
using Raylib_cs;

public class DungeonScene : IScene
{
    private static SceneManager sceneManager;

    Sprite background;
    Sprite cadreDonjon;

    // Butons
    Button buttonWalk;
    Button buy;
    Button buttonOptions;

    Text titreText;
    Text vieText;
    Text goldText;
    Text _heroName;

    Music music;

    Rectangle textBox;

    Hero _hero;

    // 14 Max dans l'affichage actuel
    List<string> logs = new List<string>();

    // Scene enfant
    OptionsScene optionsScene;
    bool optionsWindows = false;

    int count = 0;
    int gold = 0;
    int life = 10;

    // Constructeur qui porte le nom de la scene
    public DungeonScene(Hero hero) : base(ListeScene.SCENE_DUNGEON){
        sceneManager = SceneManager.GetInstance();
        _hero = hero;
    }

    public override void Draw()
    {
        // Start
        Raylib.BeginDrawing();

        Raylib.ClearBackground(Color.Black);    
        background.DrawSprite(0, Color.White, 1);

        titreText.DrawTexte();
        vieText.DrawTexteWithData(life.ToString());
        goldText.DrawTexteWithData(gold.ToString());
         
        Raylib.DrawRectanglePro(new Rectangle(textBox.X * Settings.GetScale(), textBox.Y * Settings.GetScale(), textBox.Width * Settings.GetScale(), textBox.Height * Settings.GetScale()), new Vector2(410 * Settings.GetScale(),210 * Settings.GetScale()), 0, Color.DarkGray);

        cadreDonjon.DrawSprite(0, Color.White, 1);
        _hero._portrait.DrawSprite(0, Color.White, 0.1f);
        _heroName.DrawTexte();

        int heightLog = 200;
        int start = 0;
        if (logs.Count - 13 > 0) {
            start = logs.Count - 13;
        }
        for (int i = start; i < logs.Count; i++)
        {
            Raylib.DrawText(logs[i], (int)(250 * Settings.GetScale()), (int)((360 + 10 - heightLog) * Settings.GetScale()), (int)(20 * Settings.GetScale()), Color.White);
            heightLog -= 30;
        }

        buttonWalk.DisplayButton();
        buy.DisplayButton();
        buttonOptions.DisplayButton();

        if (optionsWindows)
        {
            optionsScene.Draw();
        }

        // End
        Raylib.EndDrawing();
    }   
    
    public override void LoadScene()
    {        
        Console.WriteLine("Scene dungeon load");

        // Load texture / sprites
        background = new Sprite("background.png", Settings.initwindowWidth / 2,Settings.initwindowHeight / 2, Origin.CENTER);
        cadreDonjon = new Sprite("cadreDonjon.png", Settings.initwindowWidth / 2, Settings.initwindowHeight/2, Origin.CENTER);

        // Button
        buttonWalk = new Button("avancerBtn.png", 490,  680 , Origin.CENTER, "buttonStartSound");
        buy = new Button("buyBtn.png", 790, 680, Origin.CENTER, "buttonStartSound");
        buttonOptions = new Button("optIconBtn.png", 50, 100, Origin.CENTER, "buttonStartSound");

        // Gray textbox
        textBox = new Rectangle(640, 360, 820, 420);

        // Text
        titreText = new Text("Le cimetière", 540, 50, 30, Color.White);
        vieText = new Text("Points de vie : {}/10", 220, 100, 20, Color.White);
        goldText = new Text("Or : {}", 950, 100, 20, Color.White);       

        // Hero
        _hero._portrait._position = new Vector2(1150, 100);
        _heroName = new Text(_hero._name, 1150 - (Raylib.MeasureText(_hero._name, 20) / 2), 150, 20, Color.White);

        logs.Add(count + ": Vous découvrez le cimetière. Une brume epaisse vous entoure.");        

        // Music
        music = Raylib.LoadMusicStream("Resources/Shadowed_Catacombs.mp3");
        Raylib.SetMusicVolume(music, (float)0.5);
        Raylib.PlayMusicStream(music);

        // Scenes enfant
        optionsScene = new OptionsScene();
        optionsScene.LoadScene();
    }

    public override void UnloadScene()
    {        
        Console.WriteLine("unload content dungeon scene");
    }

    public override void  Update()
    {
         // Play music
        Raylib.UpdateMusicStream(music);

        if (buttonOptions.IsButtonPressed())
        {
            optionsWindows = true;
        }

        if (optionsWindows)
        {
            optionsScene.Update();
        }

        if (buttonWalk.IsButtonPressed()) {
            int eventValue  = getRandomNumber(1, 10);

            if(eventValue >= 1 && eventValue <= 6) {

                int eventCoffre  = getRandomNumber(1, 100);
                if(eventCoffre >=1 && eventCoffre <= 5) {
                    // Graal
                    count++;
                    logs.Add(count + ": Vous avez trouvé le Saint Graal.");
                    //resetDungeon();
                    buttonWalk.disable = true;
                    buy.disable = true;
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

    public void resetDungeon()
    {
        logs.Clear();
        gold = 0;
    }

    public override void SignalToScene(string actionName)
    {
        if (actionName.Equals("closeOptions"))
        {
            optionsWindows = false;
        }
    }
}