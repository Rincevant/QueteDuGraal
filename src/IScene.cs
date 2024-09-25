using System.Numerics;
using Raylib_cs;

public abstract class IScene
{
    public string _sceneName;
    public bool _isChild;
    public abstract void LoadScene();
    public abstract void Update();
    public abstract void Draw();
    public abstract void UnloadScene();
    public abstract void SignalToScene(TypeSignal signal, object datas);
    public IScene(string name, bool isChild = false) {
        _sceneName = name;
        _isChild = isChild;
    }
}