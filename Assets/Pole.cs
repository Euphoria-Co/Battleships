using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{



    public static int rows = 10;
    public static int cols = 10;
    static public float tilesize = 0.5f;
    public static int startX = -6;
    public static int startY = 2;

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        GameObject reference = (GameObject)Instantiate(Resources.Load("Pole"));

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject tile = (GameObject)Instantiate(reference, transform);

                float posX = col * tilesize;
                float posY = row * -tilesize;

                tile.transform.position = new Vector3(posX+startX, posY+startY,1);
            
            }
        }
        Destroy(reference);
    }

    void Update()
    {
        
    }
}
