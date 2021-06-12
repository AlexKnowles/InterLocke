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

    private int banksToGenerate = 0;
    private int offsetAhead = 6;

    private BankMetaData lastGeneratedBankMetadata;

    private System.Random random = new System.Random();
    
    void Start()
    {
        GameManager.Instance.RegisterGameStartMethod(StartNewGame);

        Camera = CameraObject.GetComponent<Camera>();

        _bankMetaDatas = BankPrefabs.Select(x => x.GetComponent<BankMetaData>()).ToArray();        
    }

    void Update()
    {
        if(!GameManager.Instance.IsGameRunning)
        {
            return;
        }

        GenerateNewBanks();
        DestroyOutOfFrameBehindBanks();
    }

    private void StartNewGame()
    {
        ClearExistingBanks();

        //Generating some banks to start
        for (int i = -6; i < offsetAhead; i++)
        {
            GenerateNewBank(i, 0);
        }
    }

    private void ClearExistingBanks()
    {
        _banksList = new List<GameObject>();
        banksToGenerate = 0;
        offsetAhead = 6

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void GenerateNewBanks()
    {
        while (banksToGenerate > 0){
            banksToGenerate--;
            GenerateNewBank(offsetAhead, GetSuitableBankIndex());
            offsetAhead++;
        }
    }

    private int GetSuitableBankIndex()
    {
        //This is garbage, don't @ me 
        List<int> suitableIndexes = new List<int>();
        int index = 0;
        foreach(BankMetaData metadata in _bankMetaDatas)
        {
            if(
                (metadata.EntryBottom >= lastGeneratedBankMetadata.EntryBottom && metadata.EntryTop <= lastGeneratedBankMetadata.ExitTop) || //enter narrow/matched from wide
                metadata.EntryTop >= lastGeneratedBankMetadata.ExitTop && metadata.EntryBottom <= lastGeneratedBankMetadata.ExitBottom //enter wider/same from narrow
                ){
                    suitableIndexes.Add(index);
            }
            index++;
        }
        return suitableIndexes[random.Next(0, suitableIndexes.Count())];
    }

    private void GenerateNewBank(int offsetAhead, int bankIndex)
    {
        var newBank = Instantiate(BankPrefabs[bankIndex], transform);
        lastGeneratedBankMetadata = _bankMetaDatas[bankIndex];
        //Be careful on width - this all assumes all banks have the same width at the moment...
        newBank.transform.position = new Vector2(offsetAhead * BankMetaData.BankWidth, 0);
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
