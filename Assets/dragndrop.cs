using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragndrop : MonoBehaviour {

    private bool dragging = false;
    private float distance;
    private Vector2 orginalposition;
    private Vector2 lastgood;
    private bool islastgood = false;
    Vector2 temp;
    private float gridsize;
    private float maxX;
    private float minX;
    private float maxY;
    private float minY;
    private bool placed;
    public int shipscollided;
    public int shipsize = 1;
    public bool rotated;
    float delay = 0.25f;
    bool one_click = false;
    bool timer_running;
    float timer_for_double_click;
    void Start () {
        orginalposition = transform.position;
        lastgood = transform.position;
    }

    void BoardSize () {
        minX = Pole.startX - 0.25f;
        minY = Pole.startY + 0.25f;
        maxX = Pole.startX + (Pole.rows * Pole.tilesize - Pole.tilesize) + 0.25f;
        maxY = Pole.startY - (Pole.cols * Pole.tilesize - Pole.tilesize) - 0.25f;
        if (rotated) {
            maxY = maxY + (shipsize - 1) * Pole.tilesize;
        } else {
            maxX = maxX - (shipsize - 1) * Pole.tilesize;
        }
    }

    void OnMouseDown () {
        distance = Vector2.Distance (transform.position, Camera.main.transform.position);
        dragging = true;
        if (!one_click) {
            one_click = true;
            timer_for_double_click = Time.time;
            timer_running = true;
        } else {
            one_click = false;
            Rotate ();
        }
    }

    void OnMouseUp () {
        dragging = false;
    }
    void OnTriggerEnter2D (Collider2D col) {
        if (col.tag == "ShipCollider") {
            shipscollided++;
        }
    }

    void OnTriggerExit2D (Collider2D col) {
        if (col.tag == "ShipCollider") {
            shipscollided--;

        }
    }
    void Update () {
        BoardSize ();
        if (dragging) {
            Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
            Vector2 rayPoint = ray.GetPoint (distance);
            transform.position = rayPoint;
            placed = false;
        } else {
            SnapToGrid ();
        }
        if (transform.eulerAngles.z == 270) {
            rotated = true;
        } else {
            rotated = false;
        }
        if (one_click) {
            if ((Time.time - timer_for_double_click) > delay) {
                one_click = false;
            }
        }
        
    }
    void Rotate () {
        if (rotated == false && lastgood != orginalposition && transform.position.y >= maxY + (shipsize - 1) * Pole.tilesize) {
            transform.eulerAngles = new Vector3 (0, 0, 270);
        } else if (transform.position.x <= maxX - (shipsize - 1) * Pole.tilesize) {
            transform.rotation = Quaternion.identity;       
        }
         StartCoroutine(waiter());

    }
    IEnumerator waiter(){
     yield return new WaitForSeconds(0.05f);
        if (shipscollided > 0 && transform.eulerAngles.z == 270) {
            transform.rotation = Quaternion.identity;
        } else if (shipscollided > 0) {
            transform.eulerAngles = new Vector3 (0, 0, 270);
        }
    } 
  
    void SnapToGrid () {
        if (transform.position.y >= maxY && transform.position.y <= minY && transform.position.x <= maxX && transform.position.x >= minX) {
            temp = new Vector2 ((Mathf.Round (transform.position.x * 2)) / 2, (Mathf.Round (transform.position.y * 2) / 2));
            transform.position = temp;
            if (placed == false && shipscollided == 0) {
                placed = true;
                lastgood = transform.position;
                islastgood = true;
            } else if (placed == false && shipscollided > 0) {
                if (islastgood = true) {
                    transform.position = lastgood;
                } else {
                    transform.position = orginalposition;
                    transform.rotation = Quaternion.identity;
                }
            }
        } else {
            if (transform.position.y >= maxY - (shipsize - 1) * Pole.tilesize && transform.position.x <= maxX + (shipsize - 1) * Pole.tilesize) {
                transform.position = lastgood;
            } else {
                transform.position = orginalposition;
                transform.rotation = Quaternion.identity;
            }
        }
    }

}