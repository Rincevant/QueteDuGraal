
using Raylib_cs;

public class EvenementScene : IScene
{
    private static SceneManager sceneManager;

    // Sprite
    Sprite salleTresor;
    Sprite fireCamp;

    // Buttons
    Button searchRoom;
    Button getGold;
    Button rest;


    // Constructeur qui porte le nom de la scene
    public EvenementScene() : base(ListeScene.EVENEMENT, true)
    {
        sceneManager = SceneManager.GetInstance();
    }

    public override void Draw()
    {
        // Start
        if (!_isChild)
        {
            Raylib.BeginDrawing();
        }        

        salleTresor.DrawSprite(0, Color.White, 1);
        searchRoom.DisplayButton();
        getGold.DisplayButton();

        // End
        if (!_isChild)
        {
            Raylib.EndDrawing();
        }
    }

    public override void LoadScene()
    {
        // Load texture / sprites
        salleTresor = new Sprite("salleAuTresor.png", 640, 360, Origin.CENTER);
        fireCamp = new Sprite("fireCamp.png", 640, 360, Origin.CENTER);

        //Button
        searchRoom = new Button("searchRoomBtn.png", 640, 450, Origin.CENTER, "buttonStartSound");
        getGold = new Button("getGoldBtn.png", 640, 520, Origin.CENTER, "buttonStartSound");
    }

    public override void SignalToScene(TypeSignal signal, object datas)
    {
        
    }

    public override void UnloadScene()
    {
        
    }

    public override void Update()
    {
        if(searchRoom.IsButtonPressed())
        {
            Console.WriteLine("fouille la salle");
        }

        if(getGold.IsButtonPressed())
        {
            int goldValue = Utils.getRandomNumber(10, 150);
            sceneManager.GetCurrentScene().SignalToScene(TypeSignal.GetGold, goldValue);
        }
    }
}

