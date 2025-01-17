using UnityEngine;

public class SudokuController : MonoBehaviour
{
    public static SudokuController Instance { get; private set; }
    
    [SerializeField] private IntScriptable selectedController;
    [SerializeField] private IntScriptable selectedNumber;
    [SerializeField] private GameObject popupSelector;
    [SerializeField] private Vector2Scriptable selectedGrid;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        selectedNumber.OnValueChanged.AddListener(OnSelectNumber);
        selectedController.OnValueChanged.AddListener(UpdateController);
        selectedGrid.SetValue(-1, -1);
    }

    private void UpdateController()
    {
        selectedGrid.SetValue(-1, -1);
    }

    public void OnSelectTile(int gridX, int gridY)
    {
        switch (selectedController.value)
        {
            case 0:
                selectedGrid.x = gridX; 
                selectedGrid.y = gridY; 
                break;
            case 1:
                SudokuHandler.Instance.SetGrid(gridX, gridY);
                break;
            case 2: 
                popupSelector.SetActive(true);
                break;

        }
    }
    
    private void OnSelectNumber()
    {
        switch (selectedController.value)
        {
            case 0:
                if (selectedGrid.x == -1)
                    break;
                SudokuHandler.Instance.SetGrid(selectedGrid.value);
                break;
            case 2:
                popupSelector.SetActive(false);
                break;
        }
    }

}
