using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankGenerator : MonoBehaviour
{
    public GameObject BankPrefab;
    public GameObject CameraObject;
    
    private Camera Camera;

    private List<GameObject> _banksList = new List<GameObject>();
    
    void Start()
    {
        Camera = CameraObject.GetComponent<Camera>();
        //This is dumb. Lets do better.
        for (int i = -10;i < 10;i++){
            var newBank = Instantiate(BankPrefab, transform);
            newBank.transform.position = new Vector2(i * 20, 0);
            _banksList.Add(newBank);
        }
    }


    void Update()
    {
        DestroyOutOfFrameBehindBanks();
    }

    private void DestroyOutOfFrameBehindBanks() {
        var banksToRemove = _banksList.Where(x => ObjectAheadOrInFrame(x)).ToList();
        banksToRemove.ForEach(x => Destroy(x));
        _banksList.RemoveAll(x => banksToRemove.Contains(x));
    }

    private bool ObjectAheadOrInFrame(GameObject gameObject){
        var objectCameraX = Camera.WorldToViewportPoint(gameObject.transform.position).x;
        return gameObject.transform.position.x < CameraObject.transform.position.x &&  (objectCameraX > 1 || objectCameraX < 0); 
    }
}
