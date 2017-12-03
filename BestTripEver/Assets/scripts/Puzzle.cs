﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Puzzle : MonoBehaviour {

	List<Color> colors = new List<Color>() {
		new Color(0.33f, 0, 0),
		new Color(0, 0.33f, 0),
		new Color(0, 0, 0.33f),
		new Color(0.33f, 0.33f, 0),
		new Color(0.33f, 0, 0.33f),
		new Color(0, 0.33f, 0.33f),
		new Color(0.33f, 0.33f, 0.33f)
	};

	List<List<int>> puzzleMap = new List<List<int>> {
		new List<int>() {1, 0, 0, 0, 0, 1},
		new List<int>() {1, 0, 2, 2, 1, 1},
		new List<int>() {2, 0, 2, 2, 2, 1},
		new List<int>() {1, 0, 0, 1, 2, 1},
		new List<int>() {1, 1, 2, 1, 1, 1},
		new List<int>() {1, 1, 2, 0, 0, 0},
	};
	public GameObject tilePrefab;
	public float offsetX = 0.0f;
  public float offsetY = 0.0f;

	void generatePuzzle() {
		for (int i = 0; i < puzzleMap.Count; i++)
    {
      for (int j = 0; j < puzzleMap[i].Count; j++)
      {
        GameObject tile = Instantiate(tilePrefab, new Vector3(offsetX + i * 4.21f, offsetY + j * -4.21f, 0), Quaternion.identity);
        tile.GetComponent<SpriteRenderer>().color = colors[puzzleMap[j][i]];
        tile.transform.SetParent(transform);
      }
    }
	}

	public Vector2 getDestination(ref int x, ref int y, ExtensionMethods.Direction d) {
		var destX = x;
		var destY = y;
		switch (d) {
			case ExtensionMethods.Direction.Up:
				for (int i = y - 1; i >= 0; i--) {
					if (puzzleMap[y - 1][x] == puzzleMap[i][x]) {
						destY = i;
					} else {
						break;
					}
				}
				break;
			case ExtensionMethods.Direction.Right:
				for (int i = x + 1; i < puzzleMap[y].Count; i++)
        {
          if (puzzleMap[y][x + 1] == puzzleMap[y][i])
          {
            destX = i;
          }
          else
          {
            break;
          }
        }
        break;
			case ExtensionMethods.Direction.Down:
				for (int i = y + 1; i < puzzleMap.Count; i++)
        {
          if (puzzleMap[y + 1][x] == puzzleMap[i][x])
          {
            destY = i;
          }
          else
          {
            break;
          }
        }
        break;
			case ExtensionMethods.Direction.Left:
				for (int i = x - 1; i >= 0; i--)
        {
          if (puzzleMap[y][x - 1] == puzzleMap[y][i])
          {
            destX = i;
          }
          else
          {
            break;
          }
        }
        break;
		}
		x = destX;
		y = destY;
    return new Vector2(offsetX + x * 4.21f, offsetY + y * -4.21f);
	}

	// Use this for initialization
	void Start () {
		generatePuzzle();
	}
}
