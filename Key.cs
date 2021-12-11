using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace pdz助手
{
	/// <summary>An individual keyboard key.</summary>
	[Serializable]
	public class Key : UITypeEditor
	{
		/// <summary>The shift key's virtual key code.</summary>
		public Messaging.VKeys ShiftKey { get; set; }

		/// <summary>The shift type (alt, ctrl, shift).</summary>
		public Messaging.ShiftType ShiftType { get; set; }

		/// <summary>The virtual key associated with it.</summary>
		public Messaging.VKeys Vk { get; set; }

		/// <summary>
		///     An internal counter used to count the number of attempts a button has tried to be pressed to exit after 4
		///     attempts.
		/// </summary>
		private int _buttonCounter;

		/// <summary>Default constructor</summary>
		public Key(Messaging.VKeys vk = Messaging.VKeys.NULL, Messaging.VKeys shiftKey = Messaging.VKeys.NULL, Messaging.ShiftType shiftType = Messaging.ShiftType.NONE)
		{
			_buttonCounter = 0;
			Vk = vk;
			ShiftKey = shiftKey;
			ShiftType = shiftType;
		}

		public Key(char c)
		{
			_buttonCounter = 0;
			Vk = (Messaging.VKeys)Messaging.GetVirtualKeyCode(c);
			ShiftKey = Messaging.VKeys.NULL;
			ShiftType = Messaging.ShiftType.NONE;
		}

		/// <summary>Constructor if you already have a whole key.  Good for making a dereferenced copy.</summary>
		/// <param name="key">The already built key.</param>
		public Key(Key key)
		{
			_buttonCounter = 0;
			Vk = key.Vk;
			ShiftKey = key.ShiftKey;
			ShiftType = key.ShiftType;
		}

		/// <summary>Emulates a keyboard key press.</summary>
		/// <param name="hWnd">The handle to the window that will receive the key press.</param>
		/// <param name="foreground">Whether it should be a foreground key press or a background key press.</param>
		/// <returns>If the press succeeded or failed.</returns>
		public bool Press(IntPtr hWnd, bool foreground)
		{
			if (foreground)
				return PressForeground(hWnd);

			return PressBackground(hWnd);
		}

		public bool Down(IntPtr hWnd, bool foreground)
		{
			switch (ShiftType)
			{
				case Messaging.ShiftType.NONE:
					if (foreground)
					{
						if (!Messaging.ForegroundKeyDown(hWnd, this))
						{
							_buttonCounter++;
							if (_buttonCounter == 2)
							{
								_buttonCounter = 0;
								return false;
							}
							Down(hWnd, true);
						}
					}
					else
					{
						if (!Messaging.SendMessageDown(hWnd, this, true))
						{
							_buttonCounter++;
							if (_buttonCounter == 2)
							{
								_buttonCounter = 0;
								return false;
							}
							Down(hWnd, false);
						}
					}
					return true;
			}
			return true;
		}

		public bool Up(IntPtr hWnd, bool foreground)
		{
			switch (ShiftType)
			{
				case Messaging.ShiftType.NONE:
					if (foreground)
					{
						if (!Messaging.ForegroundKeyUp(hWnd, this))
						{
							_buttonCounter++;
							if (_buttonCounter == 2)
							{
								_buttonCounter = 0;
								return false;
							}
							Up(hWnd, foreground);
						}
					}
					else
					{
						if (!Messaging.SendMessageUp(hWnd, this, true))
						{
							_buttonCounter++;
							if (_buttonCounter == 2)
							{
								_buttonCounter = 0;
								return false;
							}
							Up(hWnd, foreground);
						}
					}
					return true;
			}
			return true;
		}

		public bool PressForeground()
		{
			switch (ShiftType)
			{
				case Messaging.ShiftType.NONE:
					if (!Messaging.ForegroundKeyPress(this))
					{
						_buttonCounter++;
						if (_buttonCounter == 2)
						{
							_buttonCounter = 0;
							return false;
						}
						PressForeground();
					}
					return true;
			}
			return true;
		}

		/// <summary>Emulates a background keyboard key press.</summary>
		/// <param name="hWnd">The handle to the window that will receive the key press.</param>
		/// <returns>If the key press succeeded or failed.</returns>
		public bool PressBackground(IntPtr hWnd)
		{
			bool alt = false, ctrl = false, shift = false;
			switch (ShiftType)
			{
				case Messaging.ShiftType.ALT:
					alt = true;
					break;
				case Messaging.ShiftType.CTRL:
					ctrl = true;
					break;
				case Messaging.ShiftType.NONE:
					if (!Messaging.SendMessage(hWnd, this, true))
					{
						_buttonCounter++;
						if (_buttonCounter == 2)
						{
							_buttonCounter = 0;
							return false;
						}
						PressBackground(hWnd);
					}
					return true;
				case Messaging.ShiftType.SHIFT:
					shift = true;
					break;
			}
			if (!Messaging.SendMessageAll(hWnd, this, alt, ctrl, shift))
			{
				_buttonCounter++;
				if (_buttonCounter == 2)
				{
					_buttonCounter = 0;
					return false;
				}
				PressBackground(hWnd);
			}
			return true;
		}

		/// <summary>Emulates a foreground key press.</summary>
		/// <param name="hWnd">The handle to the window that will receive the key press.</param>
		/// <returns>Returns whether the key succeeded to be pressed or not.</returns>
		public bool PressForeground(IntPtr hWnd)
		{
			bool alt = false, ctrl = false, shift = false;
			switch (ShiftType)
			{
				case Messaging.ShiftType.ALT:
					alt = true;
					break;
				case Messaging.ShiftType.CTRL:
					ctrl = true;
					break;
				case Messaging.ShiftType.NONE:
					if (!Messaging.ForegroundKeyPress(hWnd, this))
					{
						_buttonCounter++;
						if (_buttonCounter == 2)
						{
							_buttonCounter = 0;
							return false;
						}
						PressForeground(hWnd);
					}
					return true;
				case Messaging.ShiftType.SHIFT:
					shift = true;
					break;
			}
			if (!Messaging.ForegroundKeyPressAll(hWnd, this, alt, ctrl, shift))
			{
				_buttonCounter++;
				if (_buttonCounter == 2)
				{
					_buttonCounter = 0;
					return false;
				}
				PressForeground(hWnd);
			}
			return true;
		}

		/// <summary>Allows the property grid edit form.</summary>
		/// <param name="context">The style the editor takes.</param>
		/// <returns>The drop down style.</returns>
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		/// <summary>Override to return the key's string</summary>
		/// <returns>Returns the proper string.</returns>
		public override string ToString()
		{
			return string.Format("{0} {1}", ShiftType, Vk);
		}
	}
}