using Raylib_cs;

public static class Settings {

    public static int initwindowWidth = 1280;
    public static int initwindowHeight = 720;


    public static int windowWidth = 1280;
    public static int windowHeight = 720;
    public static string gameName = "Quête du graal";
    public static bool isFullScreen = false;

    public static float GetScale() {
        if (isFullScreen)
        {
            double scale = (double)windowWidth / (double)initwindowWidth;
            return (float) scale;
        } else {
            if (windowWidth == 1920 && windowHeight == 1080)
            {
                return 1.5f;
            }
            else if (windowWidth == 1280 && windowHeight == 720)
            {
                return 1f;
            }
        }
        return 1f;
    }
}