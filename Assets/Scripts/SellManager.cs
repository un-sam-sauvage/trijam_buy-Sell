using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellManager : MonoBehaviour
{

    private SellManager _sellManager;

    public SellManager Instance => _sellManager;
  
    void Awake()
    {
        _sellManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
