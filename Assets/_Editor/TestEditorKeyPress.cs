using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestEditorKeyPress : MonoBehaviour
{
    public KeyCode key;
    public UnityEvent e;
    void Update()
    {
        if (Input.GetKeyDown(key))
            e.Invoke();
    }
}
