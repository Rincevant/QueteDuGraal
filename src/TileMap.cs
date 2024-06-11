using System.Numerics;
using Raylib_cs;

class TileMap
{
    public Texture2D tileSet;
    //public Dictionary<Vector2, Tile> tileMap;
    public int tileSizeWidth;
    public int tileSizeHeight;
    public int nbRow;
    public int nbCol;
    //public Vector2 position { get; set; }
    //public Vector2 atlasCoord { get; set; }

    /*public Rectangle Rect
    {
        // atlasX * tileSizeWidth, altasY * tileSizeHeight, si tileSizeWidth = 0 alors texture Width, si tileSizeHeight = 0 alors texture Height
        get { return new Rectangle(
            this.atlasCoord.X != 0 ? (int)atlasCoord.X * tileSizeWidth : 0 , 
            this.atlasCoord.Y != 0 ? (int)atlasCoord.Y * tileSizeHeight : 0, 
            this.tileSizeWidth != 0 ? this.tileSizeWidth : this.tileSet.Width , 
            this.tileSizeHeight != 0 ? this.tileSizeHeight : this.tileSet.Height); 
        }
    }

    public Rectangle Dest
    {
        get { return new Rectangle((int)position.X, (int)position.Y, tileSizeWidth, tileSizeHeight); }
    }*/
    private string textureBasePath = "resources/";

    public TileMap(string textureName, int tileSizeWidth, int tileSizeHeight, int nbRow, int nbCol)
    {
        this.tileSet = Raylib.LoadTexture(textureBasePath + textureName);
        this.tileSizeWidth = tileSizeWidth;
        this.tileSizeHeight = tileSizeHeight;
        this.nbRow = nbRow;
        this.nbCol = nbCol;

        DecoupeTileSet();
    }

    private void DecoupeTileSet() {
        for (int i = 0; i < nbRow; i++)
        {
            for (int j = 0; j < nbCol; j++)
            {
                // Create Tile
            }
        }
    }

    public void unloadTexture() {
        Raylib.UnloadTexture(tileSet);
    }
}