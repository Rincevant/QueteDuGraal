using System.Data;
using System.Threading;
using Raylib_cs;

public class Game {

    private static SceneManager sceneManager;
    public static bool quit = false;

    public Game() {
        // Initialisation de la fenetre du jeu
        Raylib.InitWindow(Settings.initwindowWidth, Settings.initwindowHeight, Settings.gameName);
        Raylib.InitAudioDevice();
        Raylib.SetTargetFPS(60);
        Initialisation();
    }

    public void Initialisation() {
        // Demarrage du jeu
        sceneManager = SceneManager.GetInstance();
        sceneManager.AddScene(new MenuDemarrageScene(), true);
    }

    public void toogleFullScreen(int width, int height) {
        if(!Raylib.IsWindowFullscreen()) {
            Raylib.SetWindowSize(width, height);
            Raylib.ToggleFullscreen();
        } else {
            Raylib.ToggleFullscreen();
            Raylib.SetWindowSize(width, height);            
        }

        Settings.windowWidth = width;
        Settings.windowHeight = height;
        sceneManager.LoadActualScene();
    }
    
    public void Run() {
        while (!Raylib.WindowShouldClose() && !quit)
        {
            if(Raylib.IsKeyPressed(KeyboardKey.Space)) {
                if (!Raylib.IsWindowFullscreen()) {
                    int monitor = Raylib.GetCurrentMonitor();
                    toogleFullScreen(Raylib.GetMonitorWidth(monitor), Raylib.GetMonitorHeight(monitor));
                } else {
                    toogleFullScreen(Settings.initwindowWidth, Settings.initwindowHeight);
                }                    
            }

            sceneManager.GetCurrentScene().Update();
            sceneManager.GetCurrentScene().Draw();
        }

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();        
    }

    
}