using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GridTile : MonoBehaviour
{
    private int id;

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


    private void Awake()
    {
        button.onClick.AddListener(OnTileSelected);
    }

    public void AddClickLister(UnityAction<int> onTileSelected)
    {
        this.onTileSelected = onTileSelected;
    }

    private void OnTileSelected()
    {
        onTileSelected?.Invoke(id);
    }
}
