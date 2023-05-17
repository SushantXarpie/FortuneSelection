using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

public class GridTile : MonoBehaviour
{
    private int id;
    private int cardinal;

    private UnityAction<int> onTileSelected;
    [SerializeField] private Button button;
    public void SetId(int id)
    {
        this.id = id;
    }

    public int GetId()
    {
        return this.id;
    }


    public void SetCardinal(int cardinal)
    {
        this.cardinal = cardinal;
    }

    public int GetCardinal()
    {
        return this.cardinal;
    }

    private void OnEnable()
    {
        button.onClick.AddListener(OnTileSelected);
    }

    private void Start()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().gameObject.transform.position = gameObject.transform.position;
    }

    public void AddClickLister(UnityAction<int> onTileSelected)
    {
        this.onTileSelected = onTileSelected;
    }

    private void OnTileSelected()
    {
        onTileSelected?.Invoke(cardinal);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnTileSelected);
    }
}
