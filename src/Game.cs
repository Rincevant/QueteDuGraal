
using Raylib_cs;
public class Game {

    private static SceneManager sceneManager;
    public static bool quit = false;

    public Game() {
        // Initialisation de la fenetre du jeu
        Raylib.InitWindow(1280, 720, Settings.gameName);
        Raylib.InitAudioDevice();
        Raylib.SetTargetFPS(60);
        Initialisation();
    }

    public void Initialisation() {
        // Demarrage du jeu
        sceneManager = SceneManager.GetInstance();
        sceneManager.AddScene(new DungeonScene(new Hero("test")), true);
        //sceneManager.AddScene(new EvenementScene(), true);
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