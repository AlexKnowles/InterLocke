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

    private const float BANK_PREFAB_WIDTH = 20f;

    private int banksToGenerate = 0;
    private int offsetAhead = 10;
    
    void Start()
    {
        Camera = CameraObject.GetComponent<Camera>();
        //Generating some banks to start
        for (int i = -10;i < offsetAhead;i++){
            GenerateNewBank(i);
        }
    }


    void Update()
    {
        GenerateNewBanks();
        DestroyOutOfFrameBehindBanks();
    }

    private void GenerateNewBanks()
    {
        while (banksToGenerate > 0){
            banksToGenerate--;
            GenerateNewBank(offsetAhead);
            offsetAhead++;
        }
    }

    private void GenerateNewBank(int offsetAhead)
    {
            var newBank = Instantiate(BankPrefab, transform);
            newBank.transform.position = new Vector2(offsetAhead * BANK_PREFAB_WIDTH, 0);
            _banksList.Add(newBank);
    }

    private void DestroyOutOfFrameBehindBanks() {
        var banksToRemove = _banksList.Where(x => ObjectAheadOrInFrame(x)).ToList();
        banksToRemove.ForEach(x => Destroy(x));
        banksToGenerate += banksToRemove.Count();
        _banksList.RemoveAll(x => banksToRemove.Contains(x));
    }

    private bool ObjectAheadOrInFrame(GameObject gameObject){
        var objectCameraX = Camera.WorldToViewportPoint(gameObject.transform.position).x;
        return gameObject.transform.position.x < CameraObject.transform.position.x &&  (objectCameraX > 1 || objectCameraX < -1); 
    }
}
