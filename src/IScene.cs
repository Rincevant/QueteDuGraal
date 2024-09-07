using System.Numerics;
using Raylib_cs;

public abstract class IScene
{
    public string sceneName;
    public abstract void LoadScene();
    public abstract void Update();
    public abstract void Draw();
    public abstract void UnloadScene();
    public abstract void SignalToScene(string actionName);
    public IScene(string name) {
        sceneName = name;
    }
}