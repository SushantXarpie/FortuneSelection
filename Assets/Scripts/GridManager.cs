using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int rowCount = 10;
    [SerializeField] private int columnCount = 15;

    List<GameObject> tiles = new List<GameObject>();

    private GridLayoutGroup gridLayoutGroup;

    [SerializeField] private TextMeshProUGUI selectedText;

    private void Awake()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = columnCount;
    }

    void Start()
    {
        // Create a grid of 10x15
        for (int x = 0; x < rowCount; x++)
        {
            // Create a row
            for (int y = 0; y < columnCount; y++)
            {
                // Create a tile
                int cardinal = x * columnCount + y;
                var tile = Instantiate(tilePrefab, parent: gameObject.transform);
                tile.name = "Tile " + cardinal;
                var gridTile = tile.GetComponent<GridTile>();
                gridTile.SetId(cardinal);
                gridTile.AddClickLister(OnTileSelected);
                tile.GetComponent<Image>().color = new Color32((byte)Random.Range(0, 256), (byte)Random.Range(0, 256), (byte)Random.Range(0, 256), 255);
                tiles.Add(tile);
            }
        }
    }

    private void OnTileSelected(int x)
    {
        Debug.Log("Clicked " + tiles[x].name);
        selectedText.text = $"Selected {tiles[x].name}";
    }
}
