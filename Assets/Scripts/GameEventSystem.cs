using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventSystem : MonoBehaviour
{
    public static GameEventSystem current;
    private void Awake()
    {
        current = this;
    }

    public Func<GameObject, int> onTileSelection;

    public int TileSelection(GameObject Tile)
    {
        if(onTileSelection != null)
        {
            return onTileSelection(Tile);
        }
        return 0;
    }

}
