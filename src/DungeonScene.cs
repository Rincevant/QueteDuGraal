using System.Numerics;
using Raylib_cs;

public class DungeonScene : IScene
{
    private static SceneManager sceneManager;

    Sprite background;
    Sprite cadreDonjon;

    // Butons
    Button buttonWalk;
    Button buttonOptions;

    Text titreText;
    Text vieText;
    Text goldText;
    Text _heroName;
    Text tresorGagne;

    Music music;

    Rectangle textBox;

    Hero _hero;

    // 14 Max dans l'affichage actuel
    List<string> logs = new List<string>();
    int countLog = 0;

    // Scene enfant
    OptionsScene optionsScene;
    bool optionsWindow = false;

    EvenementScene evementScene;
    bool evenementWindow = false;

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
        vieText.DrawTexteWithData(new List<string>(2) { _hero._life.ToString(), _hero._maxLife.ToString() });
        goldText.DrawTexteWithData(new List<string>(1) { _hero.Gold.ToString() });
         
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
        buttonOptions.DisplayButton();

        if (optionsWindow)
        {
            optionsScene.Draw();
        }

        if(evenementWindow)
        {
            evementScene.Draw();
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
        buttonWalk = new Button("avancerBtn.png", 640,  680 , Origin.CENTER, "buttonStartSound");
        buttonOptions = new Button("optIconBtn.png", 50, 100, Origin.CENTER, "buttonStartSound");

        // Gray textbox
        textBox = new Rectangle(640, 360, 820, 420);

        // Text
        titreText = new Text("Le cimetière", 540, 50, 30, Color.White);
        vieText = new Text("Points de vie : {}/{}", 220, 100, 20, Color.White);
        goldText = new Text("Or : {}", 950, 100, 20, Color.White);
        tresorGagne = new Text("Vous trouvez la salle au trésor. Votre butin est de {}", 640, 500, 20, Color.White);

        // Hero
        _hero._portrait._position = new Vector2(1150, 100);
        _heroName = new Text(_hero._name, 1150 - (Raylib.MeasureText(_hero._name, 20) / 2), 150, 20, Color.White);

        logs.Add(countLog + ": Vous découvrez le cimetière. Une brume epaisse vous entoure.");        

        // Music
        music = Raylib.LoadMusicStream("Resources/Shadowed_Catacombs.mp3");
        Raylib.SetMusicVolume(music, (float)0.5);
        Raylib.PlayMusicStream(music);

        // Scenes enfant
        optionsScene = new OptionsScene();
        optionsScene.LoadScene();

        evementScene = new EvenementScene();
        evementScene.LoadScene();
    }

    public override void UnloadScene()
    {        
        Console.WriteLine("unload content dungeon scene");
    }

    public override void  Update()
    {
         // Play music
        Raylib.UpdateMusicStream(music);       

        // Mise à jour scene option
        if (optionsWindow)
        {
            optionsScene.Update();
            return;
        }

        if (evenementWindow)
        {
            evementScene.Update();
            return;
        }

        if (buttonOptions.IsButtonPressed())
        {
            optionsWindow = true;
        }

        if (buttonWalk.IsButtonPressed()) {
            if(Utils.getRandomNumber(1, 100) > 70)
            {
                evenementWindow = true;
                addLog("Vous trouvez une salle au trèsor.");
            } else
            {
                addLog("Vous avancez dans le donjon.");
            }
            
        }
    }    

    public override void SignalToScene(TypeSignal signal, object datas)
    {
        if (signal.Equals(TypeSignal.CloseOptions))
        {
            optionsWindow = false;
        }

        if(signal.Equals(TypeSignal.GetGold)) {
            _hero.Gold += (int) datas;
            evenementWindow = false;
        }
    }

    private void addLog(string message) {
        countLog++;
        logs.Add(countLog + ": " + message);
    }
}