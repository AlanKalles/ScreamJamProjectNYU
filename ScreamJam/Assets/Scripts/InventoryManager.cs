using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    [SerializeField] Transform viewPanel;
    [SerializeField] GameObject backgroundImgPrefab, imagePrefab;

    public static InventoryManager instance;
    private List<InventoryPair> inventories;
    private RectTransform gridLayoutRectTransform, viewPortTransform;
    private InventoryPair selectedInventory;

    private bool isInventoryVisible = false;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inventories=new List<InventoryPair>();
        gridLayoutRectTransform=gridLayoutGroup.GetComponent<RectTransform>();
        viewPortTransform = gridLayoutGroup.transform.parent.GetComponent<RectTransform>();
        viewPanel.gameObject.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            if (!CheckSelect() && selectedInventory != null && !selectedInventory.isViewable)
                CheckReceiver();
    }
    Vector3 mouseWorldPos;
    private void FixedUpdate()
    {
        if (selectedInventory != null)
        {
            if (!selectedInventory.isViewable)
            {
                mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mouseWorldPos.z = 0;
                selectedInventory.obj.position = mouseWorldPos;
            }
            else
            {
                float axis = Input.GetAxisRaw("Mouse ScrollWheel");
                if (axis != 0)
                {
                    axis = Mathf.Sign(axis) * 0.8f;
                    selectedInventory.obj.localScale += new Vector3(axis, axis, 0);
                }
            }
        }
    }

    //以下两个代码控制inventory的显示和隐藏
    public void ToggleInventoryDisplay()
    {
        isInventoryVisible = !isInventoryVisible; 
        UpdateInventoryDisplay(); 
    }

    private void UpdateInventoryDisplay()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(isInventoryVisible);
        }
    }

    //public void AddInventory(Transform ui, Transform obj, bool isViewable)
    //{
    //    if (inventories.Count < 5)
    //    {
    //        inventories.Add(new InventoryPair(ui, obj, isViewable));
    //        Transform parent = gridLayoutGroup.transform.GetChild(inventories.Count - 1);
    //        ui.SetParent(parent);
    //        ui.position = parent.position;
    //        obj.gameObject.SetActive(false);
    //    }
    //}
    public void AddInventory(Sprite sprite, Transform obj, bool isViewable)
    {
        if(inventories.Count > 4)
        {
            GameObject bg = Instantiate(backgroundImgPrefab, gridLayoutGroup.transform);
            gridLayoutRectTransform.sizeDelta += new Vector2(gridLayoutGroup.spacing.x + gridLayoutGroup.cellSize.x, 0);
        }
        
        GameObject go = Instantiate(imagePrefab, gridLayoutGroup.transform.GetChild(inventories.Count));
        go.GetComponent<Image>().sprite = sprite;
        go.GetComponent<RectTransform>().sizeDelta = gridLayoutGroup.cellSize * 0.8f;
        inventories.Add(new InventoryPair(go.transform, obj, isViewable));
        go.transform.position = go.transform.parent.position;
        obj.gameObject.SetActive(false);
        obj.SetParent(null);
        DontDestroyOnLoad(obj);
    }
    //check if mouse selects one of the inventory. If selects, call SelectInventory()
    private bool CheckSelect()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 topLeft = viewPortTransform.position;
        topLeft -= viewPortTransform.sizeDelta / 2;
        Vector2 gridIndex;
        if (mousePos.x < topLeft.x || mousePos.y > topLeft.y)
            return false;
        topLeft = (Vector2)gridLayoutGroup.transform.position;
        gridIndex=mousePos - topLeft;
        if (gridIndex.y > gridLayoutRectTransform.sizeDelta.y) return false;
        gridIndex.x -= gridLayoutGroup.padding.left;
        int index = (int)(gridIndex.x / (gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x));
        //Debug.Log($"position: {gridLayoutGroup.transform.position}, size delta/2: {gridLayoutRectTransform.sizeDelta / 2}, topleft: {topLeft}");
        if (selectedInventory == null)
        {
            SelectInventory(index);
        }
        else
        {
            DeselectInventory();
        }
        return true;
    }
    private void CheckReceiver()
    {
        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (PieceReceiver.receivers.Count > 0)
        {
            for (int i = 0; i < PieceReceiver.receivers.Count; i++)
            {
                if (PieceReceiver.receivers[i].CheckBounds(mouseWorldPos))
                {
                    if (PieceReceiver.receivers[i].CheckGameObject(selectedInventory.obj.gameObject))
                    {
                        PieceReceiver.receivers[i].Action(selectedInventory.obj.gameObject);
                        RemoveSelectedObject();
                    }
                }
            }
        }
    }
    public void SelectInventory(int index)
    {
        if (inventories.Count <= index) return;
        selectedInventory = inventories[index];
        selectedInventory.ui.gameObject.SetActive(false);
        selectedInventory.obj.gameObject.SetActive(true);
        if (selectedInventory.isViewable)
        {
            viewPanel.gameObject.SetActive(true);
            selectedInventory.obj.position = viewPanel.position;
        }
    }
    public void DeselectInventory()
    {
        selectedInventory.ui.position = selectedInventory.ui.parent.position;
        selectedInventory.ui.gameObject.SetActive(true);
        selectedInventory.obj.gameObject.SetActive(false);
        if (selectedInventory.isViewable)
            viewPanel.gameObject.SetActive(false);
        selectedInventory = null;
    }
    public void RemoveSelectedObject()
    {
        inventories.Remove(selectedInventory);
        Transform parent;
        for(int i=0;i < inventories.Count; i++)
        {
            parent = gridLayoutGroup.transform.GetChild(i);
            inventories[i].ui.SetParent(parent);
            inventories[i].ui.position = parent.position;
        }
        selectedInventory.ui.gameObject.SetActive(false);
        selectedInventory = null;
    }
    class InventoryPair
    {
        public Transform ui, obj;
        public bool isViewable;
        public InventoryPair(Transform _ui, Transform _obj, bool isViewable)
        {
            ui = _ui;
            obj = _obj;
            this.isViewable = isViewable;
        }

    }
}
