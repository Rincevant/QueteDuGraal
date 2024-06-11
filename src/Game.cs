using System.Data;
using Raylib_cs;

public class Game {

    private static SceneManager sceneManager;
    public static bool quit = false;

    public Game() {
        // Initialisation de la fenetre du jeu
        Raylib.InitWindow(Settings.windowWidth, Settings.windowHeight, Settings.gameName);
        Raylib.InitAudioDevice();
        Initialisation();
    }

    public void Initialisation() {
        // Demarrage du jeu
        sceneManager = SceneManager.GetInstance();
        sceneManager.AddScene(new DungeonScene());
    }
    
    public void Run() {
        while (!Raylib.WindowShouldClose() && !quit)
        {
            sceneManager.GetCurrentScene().Update();
            sceneManager.GetCurrentScene().Draw();
        }

        Raylib.CloseAudioDevice();
        Raylib.CloseWindow();        
    }
}