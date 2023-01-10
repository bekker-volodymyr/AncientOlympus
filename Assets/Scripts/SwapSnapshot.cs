using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapSnapshot
{
    public int[] OldIndexes { get; set; } // Indexes of part that moved by player
    public int[] NewIndexes { get; set; } // Indexes of part that moved automatically
    public SwapSnapshot(int fromX, int fromY, int toX, int toY)
    {
        OldIndexes = new int[2] { fromX, fromY };
        NewIndexes = new int[2] { toX, toY };
    }
}
