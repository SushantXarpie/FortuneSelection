using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Random = System.Random;

public class GridManager : MonoBehaviour
{
    [SerializeField] private GameObject tilePrefab;
    [SerializeField] private int rowCount = 10;
    [SerializeField] private int columnCount = 15;

    List<GameObject> tiles = new List<GameObject>();

    private GridLayoutGroup gridLayoutGroup;

    [SerializeField] private TextMeshProUGUI selectedText;

    private Random random = new Random();

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
                TextMeshProUGUI textNumber = tile.GetComponentInChildren<TextMeshProUGUI>();
                int randomNumber = random.Next(1, 100);
                textNumber.text = randomNumber.ToString();
                textNumber.GetComponent<RectTransform>().sizeDelta = new Vector2(textNumber.preferredWidth, textNumber.preferredHeight);
                var gridTile = tile.GetComponent<GridTile>();
                gridTile.SetCardinal(cardinal);
                gridTile.SetId(randomNumber);
                gridTile.AddClickLister(OnTileSelected);
                tile.GetComponent<Image>().color = new Color32((byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), (byte)UnityEngine.Random.Range(0, 256), 255);
                tiles.Add(tile);
            }
        }
    }

    private void OnTileSelected(int x)
    {
        Debug.Log("Clicked " + tiles[x].GetComponent<GridTile>().GetId());
        string Name = "";
        foreach (Emp e in DatabaseManager.Instance.empList)
        {
            if (e.empId == GameManager.Instance.EmpId)
            {
                Name = e.empName;
                break;
            }
        }
        selectedText.text = $"{Name} Selected Box {tiles[x].GetComponent<GridTile>().GetId()}";
        selectedText.gameObject.SetActive(true);
        selectedText.GetComponent<Animator>().Play("Move_Animation");
        StartCoroutine(HideSelectedText());
    }

    private IEnumerator HideSelectedText()
    {
        yield return new WaitForSeconds(1.5f);
        selectedText.gameObject.SetActive(false);
    }
}
