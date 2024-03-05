using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UnityEngine;

public class BtnShffle : MonoBehaviour
{
    GameScript _gs;
    private TileScript[] tiles;

    // Start is called before the first frame update
    void Start()
    {
        _gs = FindObjectOfType<GameScript>();  
        this.tiles = _gs.tiles;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClick()
    {
        _gs.Shuffle();
        
    }
}
