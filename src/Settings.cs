using Raylib_cs;

public static class Settings {

    public static int initwindowWidth = 1280;
    public static int initwindowHeight = 720;


    public static int windowWidth = 1280;
    public static int windowHeight = 720;
    public static string gameName = "Quête du graal";

    public static float getScale() {
        if(Raylib.IsWindowFullscreen()) {
            return 3f;
        } else {
            return 1f;
        }
    }
}