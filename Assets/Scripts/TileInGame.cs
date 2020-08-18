using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInGame : MonoBehaviour
{
    private int collisionCount = 0;
    private bool selected = false;

    void Start()
    {
        if (GetComponent<SpriteRenderer>().sortingLayerName != "0")
            gameObject.layer = 2;
    }

    private int UponOtherTileSelected(GameObject id, int pivot)
    {
        if(id == gameObject && selected)
        {
            transform.position = new Vector3(BoardManager.instance.xBar + pivot, BoardManager.instance.yBar, 0);
        }
        return 0;
    }

    private int UponMatchDestruction(GameObject identity)
    {
        if (identity == gameObject && selected)
        {
            Destroy(gameObject);
        }
        return 0;
    }

    private int UponMatchRearrange(GameObject identity, int pos)
    {
        if(selected && identity == gameObject)
        {
            transform.position = new Vector3(BoardManager.instance.xBar + pos, BoardManager.instance.yBar, 0);
        }
        return 0;
    }

    private void OnMouseDown() //TILE IS SELECTED
    {
        if (selected)
            return;
        selected = true;

        GameEventSystem.current.onSelectedTileMove += UponOtherTileSelected;
        GameEventSystem.current.onMatch_Destroy += UponMatchDestruction;
        GameEventSystem.current.onDestroy_RearrangeBar += UponMatchRearrange;

        BoardManager.instance.bar.StackUp(gameObject);
        BoardManager.instance.bar.CheckForMatch();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int _this = int.Parse(GetComponent<SpriteRenderer>().sortingLayerName);
        int _other = int.Parse(collision.GetComponent<SpriteRenderer>().sortingLayerName);
        if(_this == _other + 1) //this object is one layer beneath the other
        {
            collisionCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int _this = int.Parse(GetComponent<SpriteRenderer>().sortingLayerName);
        int _other = int.Parse(collision.GetComponent<SpriteRenderer>().sortingLayerName);
        if (_this == _other + 1) //this object is one layer beneath the other
        {
            collisionCount--;
        }
        if(collisionCount == 0) 
            gameObject.layer = 0;
    }

    private void OnDestroy()
    {
        if (selected)
        {
            GameEventSystem.current.onSelectedTileMove -= UponOtherTileSelected;
            GameEventSystem.current.onMatch_Destroy -= UponMatchDestruction;
            GameEventSystem.current.onDestroy_RearrangeBar -= UponMatchRearrange;
        }
    }
}
