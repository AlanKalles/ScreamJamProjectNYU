using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] GridLayoutGroup gridLayoutGroup;

    public static InventoryManager instance;
    private List<Transform> inventory,inventoryObjects;
    private RectTransform gridLayoutRectTransform;
    private Transform selectedInventory,selectedInventoryObject;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        inventory = new List<Transform>(5);
        inventoryObjects = new List<Transform>(5);
        gridLayoutRectTransform=gridLayoutGroup.GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
            CheckSelect();
        if(Input.GetKeyDown(KeyCode.Delete))
            RemoveSelectedObject();
    }
    Vector3 mouseWorldPos;
    private void FixedUpdate()
    {
        if(selectedInventoryObject != null)
        {
            mouseWorldPos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;
            selectedInventoryObject.position= mouseWorldPos;
        }
    }
    public void AddInventory(Transform ui, Transform obj)
    {
        if (inventory.Count < 5)
        {
            inventory.Add(ui);
            inventoryObjects.Add(obj);
            Transform parent = gridLayoutGroup.transform.GetChild(inventory.Count - 1);
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
        if (inventory.Count <= index) return;
        selectedInventory = inventory[index];
        selectedInventory.gameObject.SetActive(false);
        selectedInventoryObject = inventoryObjects[index];
        selectedInventoryObject.gameObject.SetActive(true);
    }
    public void DeselectInventory()
    {
        selectedInventory.position = selectedInventory.parent.position;
        selectedInventory.gameObject.SetActive(true);
        selectedInventory = null;
        selectedInventoryObject.gameObject.SetActive(false);
        selectedInventoryObject = null;
    }
    public void RemoveSelectedObject()
    {
        inventory.Remove(selectedInventory);
        inventoryObjects.Remove(selectedInventoryObject);
        Transform parent;
        for(int i=0;i < inventory.Count; i++)
        {
            parent = gridLayoutGroup.transform.GetChild(i);
            inventory[i].SetParent(parent);
            inventory[i].position = parent.position;
        }
        selectedInventory.gameObject.SetActive(false);
        selectedInventory = null;
        selectedInventoryObject = null;
    }
}
