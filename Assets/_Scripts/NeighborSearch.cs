using com.cyborgAssets.inspectorButtonPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeighborSearch : MonoBehaviour
{
    private int[][][] oddrDirectionDifferences =
    {
        // even rows
        new int[][] {
            new int[] {+1, 0}, new int[] {0, -1}, new int[] {-1, -1},
            new int[] {-1, 0}, new int[] {-1, +1}, new int[] {0, +1}
        },
        // odd rows
        new int[][] {
            new int[] {+1, 0}, new int[] {+1, -1}, new int[] {0, -1},
            new int[] {-1, 0}, new int[] {0, +1}, new int[] {+1, +1}
        }
    };

    [ProButton]
    public Vector2Int OddrOffsetNeighbor(Vector2Int hex, int direction)
    {
        int parity = hex.y & 1;
        int[] diff = oddrDirectionDifferences[parity][direction];
        return new Vector2Int(hex.x + diff[0], hex.y + diff[1]);
    }
}
