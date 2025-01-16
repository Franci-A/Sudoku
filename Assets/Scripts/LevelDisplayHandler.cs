using TMPro;
using UnityEngine;

public class LevelDisplayHandler : MonoBehaviour
{
    [SerializeField] private TextMeshPro displayText;
    [SerializeField] private IntScriptable selectedLevel;

    void Start()
    {
        switch (selectedLevel.value)
        {
            case 55:
                displayText.text = "Easy " + "<sprite index=0>";
                break;
            case 40:
                displayText.text = "Medium " + "<sprite index=1>";
                break;
            case 25:
                displayText.text = "Hard " + "<sprite index=2>";
                break;
        }
    }
}
