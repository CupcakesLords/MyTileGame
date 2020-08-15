using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackBar : MonoBehaviour
{
    LinkedList<GameObject> chosenTile;
    void Start()
    {
        //GameEventSystem.current.onTileSelection += Stackup;
        chosenTile = new LinkedList<GameObject>();
    }

    private int CalculatePosititonOfNewTile(GameObject Tile)
    {
        if (chosenTile.Count == 0) //no tile chosen, return 0 (position)
        {
            chosenTile.AddFirst(Tile);
            return 0;
        }
        else //tile(s) exist(s) in the list
        {
            int iterator = 0; //keeping track of the progress
            int match = 0;

            Sprite a = Tile.GetComponent<SpriteRenderer>().sprite;
            foreach (GameObject i in chosenTile)
            {
                Sprite b = i.GetComponent<SpriteRenderer>().sprite;
                if (a == b) //these two tiles have the same type(icon)
                {
                    match = iterator; Debug.Log("CUPCAKE MATCH");
                }
                iterator++;
            }

            bool match_Position_Zero = false;
            if (a == chosenTile.First.Value.GetComponent<SpriteRenderer>().sprite)
                match_Position_Zero = true;

            if (match == 0 && !match_Position_Zero) //tile type doesnt exist in the list 
            {
                int position = chosenTile.Count;
                chosenTile.AddLast(Tile);
                return position; //return position
            }
            else //tile type existed and match is the last position of the matching tile
            {
                int iterate = 0;
                GameObject j = null;
                foreach (GameObject i in chosenTile)
                {
                    if (iterate == match)
                    {
                        j = i;
                        //chosenTile.AddAfter(chosenTile.Find(i), Tile);
                        break;
                    }
                    iterate++;
                }
                chosenTile.AddAfter(chosenTile.Find(j), Tile);
                foreach (GameObject i in chosenTile)
                {
                    Sprite b = i.GetComponent<SpriteRenderer>().sprite;
                    Debug.Log("Sprite: " + b);
                }
                return match + 1;
            }
        }
    }

    private int Stackup(GameObject Tile) //happens when user clicks on a tile
    {
        int position = CalculatePosititonOfNewTile(Tile);
        int iteration = 0;
        foreach (GameObject i in chosenTile)
        {
            if(iteration >= position)
            {
                /*
                int destination = GameEventSystem.current.TileSelection(gameObject);
                Debug.Log("Position: " + destination);
                */
            }
            iteration++;
        }
        return position;
    }

    private void OnDestroy()
    {
        //GameEventSystem.current.onTileSelection -= Stackup;
    }
}

