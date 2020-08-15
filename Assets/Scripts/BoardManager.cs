using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public static BoardManager instance;
    //
    public List<Sprite> characters = new List<Sprite>();
    public GameObject tile;
    //
    private GameLevel currentLevel;
    //
    private GameObject[] gameTiles;
    //
    public StackInBoard bar;
    //
    public int xBar, yBar;

    void Start()
    {
        instance = GetComponent<BoardManager>();
        currentLevel = Utils.ReadDefaultGameLevelFromAsset(1);

        xBar = -3; yBar = -3;
        bar = new StackInBoard();
 
        CreateBoard(); 
    }

    private void CreateBoard()
    {
        int total = currentLevel.tiles.Count;
        gameTiles = new GameObject[total];

        for(int i = 0; i < total; i++)
        {
            GameObject newTile = Instantiate(tile, new Vector3(currentLevel.tiles[i].x, currentLevel.tiles[i].y, 0), tile.transform.rotation);
            gameTiles[i] = newTile;
            
            newTile.transform.parent = transform;
            Sprite newSprite = characters[Random.Range(0, characters.Count)];
            newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
            newTile.GetComponent<SpriteRenderer>().sortingLayerName = currentLevel.tiles[i].z.ToString();
        }
    }
}
