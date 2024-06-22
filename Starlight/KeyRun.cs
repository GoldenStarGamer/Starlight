using GLFW;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Starlight
{
	internal class KeyRun : Entity
	{
		private readonly Keys key;
		private readonly Window window;
		public delegate void ActionDelegate();
		private readonly ActionDelegate onPressed;

		public KeyRun(Window setwindow, Keys setkey, ActionDelegate onPressed)
		{
			this.window = setwindow;
			this.key = setkey;
			this.onPressed = onPressed;
		}

		public override void Update()
		{
			if (Glfw.GetKey(window, key) == InputState.Press)
			{
				onPressed();
			}
		}
	}
}
