using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInGame : MonoBehaviour
{
    private int collisionCount = 0;

    void Start()
    {
        if (GetComponent<SpriteRenderer>().sortingLayerName != "0")
            gameObject.layer = 2;
    }

    private void OnMouseDown()
    {
        int destination = GameEventSystem.current.TileSelection(gameObject);
        Debug.Log("Position: " + destination);
        Launch(destination);
    }

    private void Launch(int position)
    {
        //Vector2 direction = new Vector2(-3 + position, -3);
        //GetComponent<Rigidbody2D>().AddForce(direction * 300);
        transform.position = new Vector3(-3 + position, -3, 0);
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
}
