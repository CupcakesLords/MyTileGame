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
        GameEventSystem.current.onSelectedTileMove += UponOtherTileSelected;
        GameEventSystem.current.onMatch_Destroy += UponMatchDestruction;
        GameEventSystem.current.onDestroy_RearrangeBar += UponMatchRearrange;
    }

    private int UponOtherTileSelected(int pivot)
    {
        if (!selected)
            return 0;
        if(transform.position.x >= BoardManager.instance.xBar + pivot)
        {
            transform.position += new Vector3(1, 0, 0);
        }
        return 0;
    }

    private int UponMatchDestruction(GameObject identity)
    {
        if (identity == gameObject)
        {
            Debug.Log("DELET THIS");
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
        
        int destination = BoardManager.instance.bar.StackUp(gameObject);
        Launch(destination);

        BoardManager.instance.bar.CheckForMatch();

        selected = true;
    }

    private void Launch(int position)
    {
        transform.position = new Vector3(BoardManager.instance.xBar + position, BoardManager.instance.yBar, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log("HIT: " + GetComponent<SpriteRenderer>().sprite);
        int _this = int.Parse(GetComponent<SpriteRenderer>().sortingLayerName);
        int _other = int.Parse(collision.GetComponent<SpriteRenderer>().sortingLayerName);
        if(_this == _other + 1) //this object is one layer beneath the other
        {
            collisionCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Debug.Log("OMEGALUL: " + GetComponent<SpriteRenderer>().sprite);
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
        GameEventSystem.current.onSelectedTileMove -= UponOtherTileSelected;
        GameEventSystem.current.onMatch_Destroy -= UponMatchDestruction;
        GameEventSystem.current.onDestroy_RearrangeBar -= UponMatchRearrange;
    }
}
