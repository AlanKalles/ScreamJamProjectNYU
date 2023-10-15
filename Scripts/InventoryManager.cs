using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public Transform img1, img2, obj1, obj2;
    [SerializeField] GridLayoutGroup gridLayoutGroup;
    [SerializeField] Transform viewPanel;

    public static InventoryManager instance;
    private List<InventoryPair> inventories;
    private RectTransform gridLayoutRectTransform;
    private InventoryPair selectedInventory;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inventories=new List<InventoryPair>();
        gridLayoutRectTransform=gridLayoutGroup.GetComponent<RectTransform>();
        viewPanel.gameObject.SetActive(false);

        AddInventory(img1, obj1,true);
        AddInventory(img2, obj2,false);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CheckSelect();
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
    public void AddInventory(Transform ui, Transform obj, bool isViewable)
    {
        if (inventories.Count < 5)
        {
            inventories.Add(new InventoryPair(ui, obj, isViewable));
            Transform parent = gridLayoutGroup.transform.GetChild(inventories.Count - 1);
            ui.SetParent(parent);
            ui.position = parent.position;
            obj.gameObject.SetActive(false);
        }
    }
    //check if mouse selects one of the inventory. If selects, call SelectInventory()
    private void CheckSelect()
    {
        Vector2 mousePos = Input.mousePosition;
        Vector2 topLeft = gridLayoutGroup.transform.position;
        topLeft -= gridLayoutRectTransform.sizeDelta / 2;
        Vector2 gridIndex = mousePos - topLeft;
        int index = (int)(gridIndex.x / (gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x));
        if (gridIndex.y > gridLayoutGroup.cellSize.y)
            return;
        if (selectedInventory == null)
        {
            SelectInventory(index);
        }
        else
        {
            DeselectInventory();
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
