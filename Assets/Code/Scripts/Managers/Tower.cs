using UnityEngine;
using System;

//Class for the shop

[Serializable]
public class Tower
{
    public string name;
    public int cost;
    public GameObject prefab;

    //just a "struct" for the towers
    public Tower (string _name, int _cost, GameObject _prefab)
    {
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }
}
