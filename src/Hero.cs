public class Hero
{
    public string _name;
    public Sprite _portrait;

    public Hero(string name) {  
        _name = name;
        _portrait = new Sprite("hero.png", 0, 0, Origin.CENTER);
    }
}
