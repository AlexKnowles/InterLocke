using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankGenerator : MonoBehaviour
{
    public GameObject[] BankPrefabs; 
    private BankMetaData[] _bankMetaDatas;
    public GameObject CameraObject;

    private Camera Camera;

    private List<GameObject> _banksList = new List<GameObject>();

    private const float BANK_PREFAB_WIDTH = 20f;

    private int banksToGenerate = 0;
    private int offsetAhead = 6;

    private BankMetaData lastGeneratedBankMetadata;
    
    void Start()
    {
        Camera = CameraObject.GetComponent<Camera>();
        _bankMetaDatas = BankPrefabs.Select(x => x.GetComponent<BankMetaData>()).ToArray();
        //Generating some banks to start
        for (int i = -6;i < offsetAhead;i++){
            GenerateNewBank(i, 0);
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
            int bankIndex = GetSuitableBankIndex();
            GenerateNewBank(offsetAhead, bankIndex);
            offsetAhead++;
        }
    }

    private int GetSuitableBankIndex()
    {
        //This is where the logic for selecting from prefabs will live, but for now, lets go with just 0...
        return 0;
    }

    private void GenerateNewBank(int offsetAhead, int bankIndex)
    {
            var newBank = Instantiate(BankPrefabs[bankIndex], transform);
            lastGeneratedBankMetadata = _bankMetaDatas[bankIndex];
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
