using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace ProjectIMBA.Input
{
	static class InputState
	{
		private static MouseState currentMouseState, prevMouseState;
		private static KeyboardState currentKeyState, prevKeyState;

		public static void Update()
		{
			prevKeyState = currentKeyState;
			prevMouseState = currentMouseState;

			currentKeyState = Keyboard.GetState();
			currentMouseState = Mouse.GetState();
		}

		public static bool isPressed(Keys key)
		{
			return currentKeyState.IsKeyDown(key);
		}

		public static bool isPressed(Action action)
		{
			return currentKeyState.IsKeyDown((Keys)action);
		}

		public static bool isToggled(Keys key)
		{
			return (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key));
		}

		public static bool isToggled(Action action)
		{
			return (currentKeyState.IsKeyDown((Keys)action) && prevKeyState.IsKeyUp((Keys)action));
		}

		public static Vector2 getMousePosition()
		{
			return new Vector2(currentMouseState.X, currentMouseState.Y);
		}

		public static Vector2 getMouseOffset()
		{
			return getMousePosition() - new Vector2(prevMouseState.X, prevMouseState.Y);
		}

		public static bool getWheelOffset(out int value)
		{
			value = currentMouseState.ScrollWheelValue - prevMouseState.ScrollWheelValue;
			return value != 0;
		}

		public static bool isWheelClicked()
		{
			return currentMouseState.MiddleButton == ButtonState.Pressed;
		}
	}

	public enum Action
	{
		#region Character Control

		WalkLeft = Keys.Left,
		WalkRight = Keys.Right,
		Jump = Keys.Up,
		Run = Keys.LeftShift,
		Hit = Keys.Space,
		Crouch = Keys.Down,
		Block = Keys.LeftAlt

		#endregion
		
	};
}
