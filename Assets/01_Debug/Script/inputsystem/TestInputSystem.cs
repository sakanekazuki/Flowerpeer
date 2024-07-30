using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameInputManager.Instance.onLineDelete.AddListener(() => { Debug.Log("delete"); });
        GameInputManager.Instance.onLineCreate.AddListener((InputAction.CallbackContext context) => { Debug.Log(context.ReadValue<Vector2>()); });
        GameInputManager.Instance.onScroll.AddListener(() => { Debug.Log("scroll"); });
    }

    public void Create(InputAction.CallbackContext context)
    {
    Debug.Log("test");
    }
}