using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionManager : MonoBehaviour
{
    public static UnitSelectionManager Instance { get; set; }

    public List<GameObject> selectedUnits = new List<GameObject>();
    public List<GameObject> allUnitsList = new List<GameObject>();

    private Camera _cam;

    public LayerMask clickable;
    public LayerMask ground;

    public GameObject groundMarker;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _cam = Camera.main;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            //select clicked unit
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, clickable))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    MultiSelect(hit.collider.gameObject);
                }
                else
                {
                    SelectByClick(hit.collider.gameObject);
                }
            }
            //deselect all units
            else
            {
                if (!Input.GetKey(KeyCode.LeftShift)) DeselectAll();
            }
        }
        if (Input.GetMouseButtonDown(1) && selectedUnits.Count > 0)
        {
            RaycastHit hit;
            Ray ray = _cam.ScreenPointToRay(Input.mousePosition);

            //select clicked unit
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, ground))
            {
                groundMarker.transform.position = hit.point;

                groundMarker.SetActive(true);
            }
        }
    }

    private void MultiSelect(GameObject unit)
    {
        if (selectedUnits.Contains(unit) == false)
        {
            selectedUnits.Add(unit);
            SelectUnit(unit, true);
        }
        else
        {
            selectedUnits.Remove(unit);
            SelectUnit(unit, false);
        }
    }

    public void DeselectAll()
    {
        foreach (GameObject unit in selectedUnits)
        {
            SelectUnit(unit, false);
        }

        groundMarker.SetActive(false);
        selectedUnits.Clear();
    }

    private void SelectByClick(GameObject unit)
    {
        DeselectAll();
        selectedUnits.Add(unit);
        SelectUnit(unit, true);

    }

    private void EnableUnitMovement(GameObject unit, bool toggle)
    {
        unit.GetComponent<UnitMovement>().enabled = toggle;
    }

    private void TriggerSelectionIndicator(GameObject unit, bool isVisible)
    {
        unit.transform.GetChild(0).gameObject.SetActive(isVisible);
    }

    internal void DragSelect(GameObject unit)
    {
        if(selectedUnits.Contains(unit) == false)
        {
            selectedUnits.Add(unit);
            SelectUnit(unit, true);
        }
    }

    private void SelectUnit(GameObject unit, bool isSelected)
    {
        TriggerSelectionIndicator(unit, isSelected);
        EnableUnitMovement(unit, isSelected);
    }
}
