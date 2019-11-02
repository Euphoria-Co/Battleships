using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragndrop : MonoBehaviour {

    private bool dragging = false;
    private float distance;
    private Vector2 orginalposition;
    Vector2 temp;
    private float gridsize;

    private float maxX;
    private float minX;
    private float maxY;
    private float minY;
    private Vector2 minsize;
    private Vector2 maxsize;

    public bool shipscollide;
    private bool placed;

    void Start () {
        orginalposition = transform.position;
        BoardSize ();
    }

    void BoardSize () {
        minX = Pole.startX - 0.25f;
        minY = Pole.startY + 0.25f;
        maxX = Pole.startX + (Pole.rows * Pole.tilesize - Pole.tilesize) + 0.25f;
        maxY = Pole.startY - (Pole.cols * Pole.tilesize - Pole.tilesize) - 0.25f;
      
    }

    void OnMouseDown () {
        distance = Vector2.Distance (transform.position, Camera.main.transform.position);
        dragging = true;
    }

    void OnMouseUp () {
        dragging = false;
    }
 void OnTriggerEnter2D(Collider2D col){
     if(col.tag == "ShipCollider"){
         shipscollide = true;
     }
 }

  void OnTriggerExit2D(Collider2D col){
     if(col.tag == "ShipCollider"){
         shipscollide = false;
     }
 }

    void Update () {
        if (dragging) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            Vector2 rayPoint = ray.GetPoint (distance);
            transform.position = rayPoint;
            placed = false;
        } else {
            SnapToGrid ();
        }

    }

    void SnapToGrid () {
    if(transform.position.y >= maxY && transform.position.y <= minY && transform.position.x <= maxX && transform.position.x >= minX){
        temp = new Vector2 ((Mathf.Round (transform.position.x * 2)) / 2, (Mathf.Round (transform.position.y * 2) / 2));
        transform.position = temp;
        if(placed == false && shipscollide == false){
            placed = true;
        } else if (placed == false && shipscollide == true) {
            transform.position = orginalposition;
        }
    } else {
        transform.position = orginalposition;
    }
    }

}