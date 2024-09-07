using Raylib_cs;
using System.Threading;

public class OptionsScene : IScene
{
    private static SceneManager sceneManager;

    // Sprites
    Sprite optionsFrame;

    // Buttons
    Button fullScreen;
    Button btn1920;
    Button btn1280;
    Button close;

    public bool test = true;

    // Constructeur qui porte le nom de la scene
    public OptionsScene() : base(ListeScene.OPTIONS)
    {
        sceneManager = SceneManager.GetInstance();
    }

    public override void Draw() {

        // Start
        //Raylib.BeginDrawing();

        optionsFrame.DrawSprite(0, Color.White, 1);

        fullScreen.DisplayButton();
        btn1280.DisplayButton();
        btn1920.DisplayButton();
        close.DisplayButton();

        // End
        //Raylib.EndDrawing();
    }

    public override void LoadScene() {
        Console.WriteLine("Scene Options load");

        // Sprites
        optionsFrame = new Sprite("optionFrame.png", 640, 360, Origin.CENTER);

        // Buttons
        fullScreen = new Button("fullScreenBtn.png", 640, 250, Origin.CENTER, "buttonStartSound");
        btn1920 = new Button("1920resoBtn.png", 640, 350, Origin.CENTER, "buttonStartSound");
        btn1280 = new Button("1280resoBtn.png", 640, 450, Origin.CENTER, "buttonStartSound");
        close = new Button("closeBtn.png", 640, 550, Origin.CENTER, "buttonStartSound");
    }

    public override void Update()
    {
        // Buttons
        if (close.IsButtonPressed())
        {
            sceneManager.GetCurrentScene().SignalToScene("closeOptions");
        }

        if(fullScreen.IsButtonPressed())
        {
            ToggleFullScreen();
        }

        if (btn1920.IsButtonPressed()) {
            ToggleScreenResolution(1920, 1080);
        }

        if (btn1280.IsButtonPressed()) {
            ToggleScreenResolution(1280, 720);
        }

    }

    public override void UnloadScene() {
        Console.WriteLine("Unload scene " + ListeScene.OPTIONS);
        optionsFrame.unloadTexture();
    }

    public override void SignalToScene(string actionName)
    {
        throw new NotImplementedException();
    }

    private void ToggleFullScreen()
    {
        int monitor = Raylib.GetCurrentMonitor();
        if (!Raylib.IsWindowFullscreen())
        {            
            Raylib.SetWindowSize(Raylib.GetMonitorWidth(monitor), Raylib.GetMonitorHeight(monitor));
            Raylib.ToggleFullscreen();
        }

        Settings.windowWidth = Raylib.GetMonitorWidth(monitor);
        Settings.windowHeight = Raylib.GetMonitorHeight(monitor);
        Settings.isFullScreen = true;
        sceneManager.LoadActualScene();
    }

    private void ToggleScreenResolution(int width, int height)
    {
        if (Raylib.IsWindowFullscreen()) {
            Raylib.ToggleFullscreen();
        } 
         Raylib.SetWindowSize(width, height);

        Settings.windowWidth = width;
        Settings.windowHeight = height;
        Settings.isFullScreen = false;
        sceneManager.LoadActualScene();
    }

}
