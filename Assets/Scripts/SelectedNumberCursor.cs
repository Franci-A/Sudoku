using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedNumberCursor : MonoBehaviour
{
    [SerializeField] private List<RectTransform> buttons;

    private void Start()
    {
        SelectNumber(buttons.Count - 1) ;
    }

    public void SelectNumber(int i)
    {
        transform.position = buttons[i].position + new Vector3(0, -10,0);
    }
}
