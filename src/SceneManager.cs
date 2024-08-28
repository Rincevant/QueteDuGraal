class SceneManager
{
    private static SceneManager instance;
    private Stack<IScene> scenes;

    private IScene currentScene;

    private SceneManager() {
        scenes = new();
    }

    public static SceneManager GetInstance()
    {
        if (instance == null)
        {
            instance = new SceneManager();
        }
        return instance;
    }

    public void AddScene(IScene newScene, bool isCurrent)
    {
        newScene.LoadScene();
        scenes.Push(newScene);
        if (isCurrent) {
            currentScene = newScene;
        }        
    }

    public void LoadActualScene()
    {
        this.currentScene.LoadScene();
    }

    public void RemoveScene(string name)
    {
        IScene sceneToDelete = GetSceneByName(name);
        if(sceneToDelete == null) {
            return;
        }

        sceneToDelete.UnloadScene();

        Stack<IScene> tempStack = new Stack<IScene>();

        // Pop elements from the original stack to the temporary stack until we find the scene to remove
        while (scenes.Count > 0)
        {
            IScene scene = scenes.Pop();
            if (!scene.Equals(sceneToDelete))
            {
                tempStack.Push(scene);
            }            
        }
        scenes = tempStack;        
    }

    public IScene GetCurrentScene() {
        return currentScene;
    }

    public IScene? GetSceneByName(string name) {
        foreach (IScene scene in scenes)
        {
            if(scene.sceneName.Equals(name)){
                return scene;
            }
        }
        return null;
    }
}