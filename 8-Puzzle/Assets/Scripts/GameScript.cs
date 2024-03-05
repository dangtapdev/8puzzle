using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    private int emptySpaceIndex = 8;
    [SerializeField]private Transform emptySpaces = null;
    private Camera _camera;
    [SerializeField]public TileScript[] tiles;
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        //Shuffle();        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)) 
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if (hit)
            {
                Debug.Log(emptySpaces.position);
                Debug.Log(hit.transform.position);
                if(Vector2.Distance(emptySpaces.position, hit.transform.position) < 1f)
                {                    
                    Vector2 lastemptySpacePosition = emptySpaces.position;
                    TileScript thisTile = hit.transform.GetComponent<TileScript>();
                    emptySpaces.position = thisTile.targetPosition;
                    thisTile.targetPosition = lastemptySpacePosition;
                    int tileIndex = findIndex(thisTile);
                    tiles[emptySpaceIndex] = tiles[tileIndex];
                    tiles[tileIndex] = null;
                    emptySpaceIndex = tileIndex;
                }
            }
        }
    }
    public void Shuffle()
    {
        if(emptySpaceIndex != 8)
        {
            var tileOnLastPos = tiles[8].targetPosition;
            tiles[8].targetPosition = emptySpaces.position;
            emptySpaces.position = tileOnLastPos;
            tiles[emptySpaceIndex] = tiles[8];
            tiles[8] = null;
            emptySpaceIndex = 8;
        }
        int invertion;
        do
        {
            for (int i = 0; i < 9; i++)
            {
                if (tiles[i] != null)
                {
                    var lastPos = tiles[i].targetPosition;
                    int randomIndex = Random.Range(0, 8);
                    tiles[i].targetPosition = tiles[randomIndex].targetPosition;
                    tiles[randomIndex].targetPosition = lastPos;
                    var tile = tiles[i];
                    tiles[i] = tiles[randomIndex];
                    tiles[randomIndex] = tile;
                }
            }
            invertion = GetInversions();
            Debug.Log("Puzzle Shuffled");
        }
        while(invertion %2 != 0);        
    }
    public int findIndex(TileScript ts)
    {
        for(int i=0;i<tiles.Length;i++)
        {
            if (tiles[i] != null)
            {
                if (tiles[i] == ts)
                    return i;
            }
        }
        return -1;
    }
    public int GetInversions()
    {
        int inversionSum = 0;
        for (int i = 0; i < tiles.Length; i++)
        {
            int thisTileInversion = 0;
            for (int j = i; j < tiles.Length; j++)
            {
                if (tiles[j] != null)
                {
                    if (tiles[i].numbers > tiles[j].numbers)
                    {
                        thisTileInversion++;
                    }
                }
            }
            inversionSum += thisTileInversion;
        }
        return inversionSum;
    }
}
