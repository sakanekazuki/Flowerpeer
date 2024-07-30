using System;
using UnityEngine.InputSystem;
using UnityEngine.Events;

// ì¸óÕä«óùÉNÉâÉX
public class GameInputManager : ManagerBase<GameInputManager>
{
	GameInput input;

	public UnityEvent<InputAction.CallbackContext> onLineCreate;
	public UnityEvent onLineDelete;
	public UnityEvent onScroll;

	private void Awake()
	{
		input = new GameInput();
		input.Player.Enable();
		input.Player.LineCreate.performed += OnLineCreate;
		input.Player.LineDelete.performed += OnLineDelete;
		input.Player.ColorChange.performed += OnScroll;
	}

	void OnLineCreate(InputAction.CallbackContext context)
	{
		onLineCreate?.Invoke(context);
	}

	void OnLineDelete(InputAction.CallbackContext context)
	{
		onLineDelete?.Invoke();
	}

	void OnScroll(InputAction.CallbackContext context)
	{
		onScroll?.Invoke();
	}
}