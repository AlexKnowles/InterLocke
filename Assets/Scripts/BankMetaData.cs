using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankMetaData : MonoBehaviour
{
    //This is static for now, in the interests of speed, but we might want to make it changable later...
    public static int BankWidth = 20;
    
    public int EntryTop;
    public int EntryBottom;

    public int ExitTop;
    public int ExitBottom;
}
