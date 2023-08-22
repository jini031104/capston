using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragToCreatCoin : MonoBehaviour
{
    public ShapeType coinType;

    // Start is called before the first frame update
    private string objName;
    void Awake(){
        objName = this.gameObject.name;
    }
    void Start(){
    }

    void OnMouseDrag(){
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    public enum ShapeType{
        clonCoin1, clonCoin2, clonCoin3, clonCoin4, clonCoin5, clonCoin6, clonCoin_
    }
}
