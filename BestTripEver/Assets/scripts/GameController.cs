using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ----------------------------------------------------------------
public class GameController : MonoBehaviour
{
    public GameObject PuzzleGO;
    public GameObject PlayerGO;

    private Puzzle PuzzleController;
    private Player PlayerController;

    // TODO Matus : place somewhere else
    List<List<int>> puzzleMap = new List<List<int>> {
        new List<int>() {1, 0, 0, 0, 0, 1},
        new List<int>() {1, 0, 2, 2, 1, 1},
        new List<int>() {2, 0, 2, 2, 2, 1},
        new List<int>() {1, 0, 0, 1, 2, 1},
        new List<int>() {1, 1, 2, 1, 1, 1},
        new List<int>() {1, 1, 2, 0, 0, 0},
    };

    // ----------------------------------------------------------------
    private void Start()
    {
        if (PuzzleGO == null || PlayerGO == null)
            return;

        Puzzle pz = PuzzleGO.GetComponent<Puzzle>();
        Player pl = PlayerGO.GetComponent<Player>();

        if (pz == null || pl == null)
            return;

        PuzzleController = pz;
        PlayerController = pl;
    }

    // ----------------------------------------------------------------
    private bool Ok()
    {
        return PuzzleController != null && PlayerController != null;
    }

    // ----------------------------------------------------------------
    private void InitPuzzle()
    {
        PuzzleController.Generate(mi);
    }
}
