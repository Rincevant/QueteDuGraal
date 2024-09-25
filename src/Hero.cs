public class Hero
{
    public string _name;
    public Sprite _portrait;
    public int _life;
    public int _maxLife;
    public int _gold;

    public Hero(string name) {  
        _name = name;
        _portrait = new Sprite("hero.png", 0, 0, Origin.CENTER);
        _life = 100;
        _maxLife = 100;
        _gold = 50;
    }

    public int Gold { get { return _gold; } set { _gold = value; } }
}
