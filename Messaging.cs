using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace pdz助手
{
	/// <summary>Class for messaging and key presses</summary>
	[Serializable]
	public class Messaging
	{
		#region Unmanaged Items

		#region Constants

		/// <summary>Maps a virtual key to a key code.</summary>
		private const uint MAPVK_VK_TO_VSC = 0x00;

		/// <summary>Maps a key code to a virtual key.</summary>
		private const uint MAPVK_VSC_TO_VK = 0x01;

		/// <summary>Maps a virtual key to a character.</summary>
		private const uint MAPVK_VK_TO_CHAR = 0x02;

		/// <summary>Maps a key code to a virtual key with specified keyboard.</summary>
		private const uint MAPVK_VSC_TO_VK_EX = 0x03;

		/// <summary>Maps a virtual key to a key code with specified keyboard.</summary>
		private const uint MAPVK_VK_TO_VSC_EX = 0x04;

		/// <summary>Code if the key is toggled.</summary>
		private const ushort KEY_TOGGLED = 0x1;

		/// <summary>Code for if the key is pressed.</summary>
		private const ushort KEY_PRESSED = 0xF000;

		/// <summary>Code for no keyboard event.</summary>
		private const uint KEYEVENTF_NONE = 0x0;

		/// <summary>Code for extended key pressed.</summary>
		private const uint KEYEVENTF_EXTENDEDKEY = 0x0001;

		/// <summary>Code for keyup event.</summary>
		private const uint KEYEVENTF_KEYUP = 0x0002;

		/// <summary>Mouse input type.</summary>
		private const int INPUT_MOUSE = 0;

		/// <summary>Keyboard input type.</summary>
		private const int INPUT_KEYBOARD = 1;

		/// <summary>Hardware input type.</summary>
		private const int INPUT_HARDWARE = 2;

		#endregion Constants

		[StructLayout(LayoutKind.Explicit)]
		struct Helper
		{
			[FieldOffset(0)]
			public short Value;
			[FieldOffset(0)]
			public byte Low;
			[FieldOffset(1)]
			public byte High;
		}

		[DllImport("user32.dll")]
		static extern short VkKeyScan(char ch);

		[DllImport("user32.dll", SetLastError = false)]
		private static extern IntPtr GetMessageExtraInfo();

		/// <summary>Gets the key state of the specified key.</summary>
		/// <param name="nVirtKey">The key to check.</param>
		/// <returns></returns>
		[DllImport("user32.dll")]
		private static extern ushort GetKeyState(int nVirtKey);

		/// <summary>Gets the state of the entire keyboard.</summary>
		/// <param name="lpKeyState">The byte array to receive all the keys states.</param>
		/// <returns>Whether it succeed or failed.</returns>
		[DllImport("user32.dll")]
		private static extern bool GetKeyboardState(byte[] lpKeyState);

		/// <summary>Allows for foreground hardware keyboard key presses</summary>
		/// <param name="nInputs">The number of inputs in pInputs</param>
		/// <param name="pInputs">A Input structure for what is to be pressed.</param>
		/// <param name="cbSize">The size of the structure.</param>
		/// <returns>A message.</returns>
		[DllImport("user32.dll", SetLastError = true)]
		static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

		/// <summary>
		///     The GetForegroundWindow function returns a handle to the foreground window.
		/// </summary>
		[DllImport("user32.dll")]
		private static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool SendMessage(IntPtr hWnd, int wMsg, uint wParam, uint lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32.dll", SetLastError = true)]
		private static extern bool PostMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

		[DllImport("user32.dll")]
		private static extern uint MapVirtualKey(uint uCode, uint uMapType);

		#endregion //Unmanaged Items

		#region Structures

		#region Public

		public enum WindowsMessages
		{
			WM_NULL = 0x00,
			WM_CREATE = 0x01,
			WM_DESTROY = 0x02,
			WM_MOVE = 0x03,
			WM_SIZE = 0x05,
			WM_ACTIVATE = 0x06,
			WM_SETFOCUS = 0x07,
			WM_KILLFOCUS = 0x08,
			WM_ENABLE = 0x0A,
			WM_SETREDRAW = 0x0B,
			WM_SETTEXT = 0x0C,
			WM_GETTEXT = 0x0D,
			WM_GETTEXTLENGTH = 0x0E,
			WM_PAINT = 0x0F,
			WM_CLOSE = 0x10,
			WM_QUERYENDSESSION = 0x11,
			WM_QUIT = 0x12,
			WM_QUERYOPEN = 0x13,
			WM_ERASEBKGND = 0x14,
			WM_SYSCOLORCHANGE = 0x15,
			WM_ENDSESSION = 0x16,
			WM_SYSTEMERROR = 0x17,
			WM_SHOWWINDOW = 0x18,
			WM_CTLCOLOR = 0x19,
			WM_WININICHANGE = 0x1A,
			WM_SETTINGCHANGE = 0x1A,
			WM_DEVMODECHANGE = 0x1B,
			WM_ACTIVATEAPP = 0x1C,
			WM_FONTCHANGE = 0x1D,
			WM_TIMECHANGE = 0x1E,
			WM_CANCELMODE = 0x1F,
			WM_SETCURSOR = 0x20,
			WM_MOUSEACTIVATE = 0x21,
			WM_CHILDACTIVATE = 0x22,
			WM_QUEUESYNC = 0x23,
			WM_GETMINMAXINFO = 0x24,
			WM_PAINTICON = 0x26,
			WM_ICONERASEBKGND = 0x27,
			WM_NEXTDLGCTL = 0x28,
			WM_SPOOLERSTATUS = 0x2A,
			WM_DRAWITEM = 0x2B,
			WM_MEASUREITEM = 0x2C,
			WM_DELETEITEM = 0x2D,
			WM_VKEYTOITEM = 0x2E,
			WM_CHARTOITEM = 0x2F,

			WM_SETFONT = 0x30,
			WM_GETFONT = 0x31,
			WM_SETHOTKEY = 0x32,
			WM_GETHOTKEY = 0x33,
			WM_QUERYDRAGICON = 0x37,
			WM_COMPAREITEM = 0x39,
			WM_COMPACTING = 0x41,
			WM_WINDOWPOSCHANGING = 0x46,
			WM_WINDOWPOSCHANGED = 0x47,
			WM_POWER = 0x48,
			WM_COPYDATA = 0x4A,
			WM_CANCELJOURNAL = 0x4B,
			WM_NOTIFY = 0x4E,
			WM_INPUTLANGCHANGEREQUEST = 0x50,
			WM_INPUTLANGCHANGE = 0x51,
			WM_TCARD = 0x52,
			WM_HELP = 0x53,
			WM_USERCHANGED = 0x54,
			WM_NOTIFYFORMAT = 0x55,
			WM_CONTEXTMENU = 0x7B,
			WM_STYLECHANGING = 0x7C,
			WM_STYLECHANGED = 0x7D,
			WM_DISPLAYCHANGE = 0x7E,
			WM_GETICON = 0x7F,
			WM_SETICON = 0x80,

			WM_NCCREATE = 0x81,
			WM_NCDESTROY = 0x82,
			WM_NCCALCSIZE = 0x83,
			WM_NCHITTEST = 0x84,
			WM_NCPAINT = 0x85,
			WM_NCACTIVATE = 0x86,
			WM_GETDLGCODE = 0x87,
			WM_NCMOUSEMOVE = 0xA0,
			WM_NCLBUTTONDOWN = 0xA1,
			WM_NCLBUTTONUP = 0xA2,
			WM_NCLBUTTONDBLCLK = 0xA3,
			WM_NCRBUTTONDOWN = 0xA4,
			WM_NCRBUTTONUP = 0xA5,
			WM_NCRBUTTONDBLCLK = 0xA6,
			WM_NCMBUTTONDOWN = 0xA7,
			WM_NCMBUTTONUP = 0xA8,
			WM_NCMBUTTONDBLCLK = 0xA9,

			WM_INPUT = 0x00FF,

			WM_KEYFIRST = 0x100,
			WM_KEYDOWN = 0x100,
			WM_KEYUP = 0x101,
			WM_CHAR = 0x102,
			WM_DEADCHAR = 0x103,
			WM_SYSKEYDOWN = 0x104,
			WM_SYSKEYUP = 0x105,
			WM_SYSCHAR = 0x106,
			WM_SYSDEADCHAR = 0x107,
			WM_KEYLAST = 0x108,

			WM_IME_STARTCOMPOSITION = 0x10D,
			WM_IME_ENDCOMPOSITION = 0x10E,
			WM_IME_COMPOSITION = 0x10F,
			WM_IME_KEYLAST = 0x10F,

			WM_INITDIALOG = 0x110,
			WM_COMMAND = 0x111,
			WM_SYSCOMMAND = 0x112,
			WM_TIMER = 0x113,
			WM_HSCROLL = 0x114,
			WM_VSCROLL = 0x115,
			WM_INITMENU = 0x116,
			WM_INITMENUPOPUP = 0x117,
			WM_MENUSELECT = 0x11F,
			WM_MENUCHAR = 0x120,
			WM_ENTERIDLE = 0x121,

			WM_CTLCOLORMSGBOX = 0x132,
			WM_CTLCOLOREDIT = 0x133,
			WM_CTLCOLORLISTBOX = 0x134,
			WM_CTLCOLORBTN = 0x135,
			WM_CTLCOLORDLG = 0x136,
			WM_CTLCOLORSCROLLBAR = 0x137,
			WM_CTLCOLORSTATIC = 0x138,

			WM_MOUSEFIRST = 0x200,
			WM_MOUSEMOVE = 0x200,
			WM_LBUTTONDOWN = 0x201,
			WM_LBUTTONUP = 0x202,
			WM_LBUTTONDBLCLK = 0x203,
			WM_RBUTTONDOWN = 0x204,
			WM_RBUTTONUP = 0x205,
			WM_RBUTTONDBLCLK = 0x206,
			WM_MBUTTONDOWN = 0x207,
			WM_MBUTTONUP = 0x208,
			WM_MBUTTONDBLCLK = 0x209,
			WM_MOUSEWHEEL = 0x20A,
			WM_MOUSEHWHEEL = 0x20E,

			WM_PARENTNOTIFY = 0x210,
			WM_ENTERMENULOOP = 0x211,
			WM_EXITMENULOOP = 0x212,
			WM_NEXTMENU = 0x213,
			WM_SIZING = 0x214,
			WM_CAPTURECHANGED = 0x215,
			WM_MOVING = 0x216,
			WM_POWERBROADCAST = 0x218,
			WM_DEVICECHANGE = 0x219,

			WM_MDICREATE = 0x220,
			WM_MDIDESTROY = 0x221,
			WM_MDIACTIVATE = 0x222,
			WM_MDIRESTORE = 0x223,
			WM_MDINEXT = 0x224,
			WM_MDIMAXIMIZE = 0x225,
			WM_MDITILE = 0x226,
			WM_MDICASCADE = 0x227,
			WM_MDIICONARRANGE = 0x228,
			WM_MDIGETACTIVE = 0x229,
			WM_MDISETMENU = 0x230,
			WM_ENTERSIZEMOVE = 0x231,
			WM_EXITSIZEMOVE = 0x232,
			WM_DROPFILES = 0x233,
			WM_MDIREFRESHMENU = 0x234,

			WM_IME_SETCONTEXT = 0x281,
			WM_IME_NOTIFY = 0x282,
			WM_IME_CONTROL = 0x283,
			WM_IME_COMPOSITIONFULL = 0x284,
			WM_IME_SELECT = 0x285,
			WM_IME_CHAR = 0x286,
			WM_IME_KEYDOWN = 0x290,
			WM_IME_KEYUP = 0x291,

			WM_MOUSEHOVER = 0x2A1,
			WM_NCMOUSELEAVE = 0x2A2,
			WM_MOUSELEAVE = 0x2A3,

			WM_CUT = 0x300,
			WM_COPY = 0x301,
			WM_PASTE = 0x302,
			WM_CLEAR = 0x303,
			WM_UNDO = 0x304,

			WM_RENDERFORMAT = 0x305,
			WM_RENDERALLFORMATS = 0x306,
			WM_DESTROYCLIPBOARD = 0x307,
			WM_DRAWCLIPBOARD = 0x308,
			WM_PAINTCLIPBOARD = 0x309,
			WM_VSCROLLCLIPBOARD = 0x30A,
			WM_SIZECLIPBOARD = 0x30B,
			WM_ASKCBFORMATNAME = 0x30C,
			WM_CHANGECBCHAIN = 0x30D,
			WM_HSCROLLCLIPBOARD = 0x30E,
			WM_QUERYNEWPALETTE = 0x30F,
			WM_PALETTEISCHANGING = 0x310,
			WM_PALETTECHANGED = 0x311,

			WM_HOTKEY = 0x312,
			WM_PRINT = 0x317,
			WM_PRINTCLIENT = 0x318,

			WM_HANDHELDFIRST = 0x358,
			WM_HANDHELDLAST = 0x35F,
			WM_PENWINFIRST = 0x380,
			WM_PENWINLAST = 0x38F,
			WM_COALESCE_FIRST = 0x390,
			WM_COALESCE_LAST = 0x39F,
			WM_DDE_FIRST = 0x3E0,
			WM_DDE_INITIATE = 0x3E0,
			WM_DDE_TERMINATE = 0x3E1,
			WM_DDE_ADVISE = 0x3E2,
			WM_DDE_UNADVISE = 0x3E3,
			WM_DDE_ACK = 0x3E4,
			WM_DDE_DATA = 0x3E5,
			WM_DDE_REQUEST = 0x3E6,
			WM_DDE_POKE = 0x3E7,
			WM_DDE_EXECUTE = 0x3E8,
			WM_DDE_LAST = 0x3E8,

			WM_USER = 0x400,
			WM_APP = 0x8000
		}

		[StructLayout(LayoutKind.Sequential)]
		struct MOUSEINPUT
		{
			public int dx;
			public int dy;
			public uint mouseData;
			public uint dwFlags;
			public uint time;
			public IntPtr dwExtraInfo;
		};

		[StructLayout(LayoutKind.Sequential)]
		struct KEYBDINPUT
		{
			/*Virtual Key code.  Must be from 1-254.  If the dwFlags member specifies KEYEVENTF_UNICODE, wVk must be 0.*/
			public ushort wVk;

			/*A hardware scan code for the key. If dwFlags specifies KEYEVENTF_UNICODE, wScan specifies a Unicode character which is to be sent to the foreground application.*/
			public ushort wScan;

			/*Specifies various aspects of a keystroke.  See the KEYEVENTF_ constants for more information.*/
			public uint dwFlags;

			/*The time stamp for the event, in milliseconds. If this parameter is zero, the system will provide its own time stamp.*/
			public uint time;

			/*An additional value associated with the keystroke. Use the GetMessageExtraInfo function to obtain this information.*/
			public IntPtr dwExtraInfo;
		};

		[StructLayout(LayoutKind.Sequential)]
		struct HARDWAREINPUT
		{
			public uint uMsg;
			public ushort wParamL;
			public ushort wParamH;
		};

		struct INPUT
		{
			public int type;
			public InputUnion u;
		};

		[StructLayout(LayoutKind.Explicit)]
		struct InputUnion
		{
			[FieldOffset(0)]
			public MOUSEINPUT mi;
			[FieldOffset(0)]
			public KEYBDINPUT ki;
			[FieldOffset(0)]
			public HARDWAREINPUT hi;
		}

		[Serializable]
		public enum ShiftType
		{
			NONE = 0x0,
			SHIFT = 0x1,
			CTRL = 0x2,
			SHIFT_CTRL = SHIFT | CTRL,
			ALT = 0x4,
			SHIFT_ALT = ALT | SHIFT,
			CTRL_ALT = CTRL | ALT,
			SHIFT_CTRL_ALT = SHIFT | CTRL | ALT
		}

		public enum Message
		{
			NCHITTEST = (0x0084),
			KEY_DOWN = (0x0100), //Key down
			KEY_UP = (0x0101), //Key Up
			VM_CHAR = (0x0102), //The character being pressed
			SYSKEYDOWN = (0x0104), //An Alt/ctrl/shift + key down message
			SYSKEYUP = (0x0105), //An Alt/Ctrl/Shift + Key up Message
			SYSCHAR = (0x0106), //An Alt/Ctrl/Shift + Key character Message
			LBUTTONDOWN = (0x201), //Left mousebutton down 
			LBUTTONUP = (0x202), //Left mousebutton up 
			LBUTTONDBLCLK = (0x203), //Left mousebutton doubleclick 
			RBUTTONDOWN = (0x204), //Right mousebutton down 
			RBUTTONUP = (0x205), //Right mousebutton up 
			RBUTTONDBLCLK = (0x206), //Right mousebutton doubleclick

			/// <summary>Middle mouse button down</summary>
			MBUTTONDOWN = (0x207),

			/// <summary>Middle mouse button up</summary>
			MBUTTONUP = (0x208)
		}

		[Serializable]
		public enum VKeys
		{
			KEY_0 = 0x30, //0 key 
			KEY_1 = 0x31, //1 key 
			KEY_2 = 0x32, //2 key 
			KEY_3 = 0x33, //3 key 
			KEY_4 = 0x34, //4 key 
			KEY_5 = 0x35, //5 key 
			KEY_6 = 0x36, //6 key 
			KEY_7 = 0x37, //7 key 
			KEY_8 = 0x38, //8 key 
			KEY_9 = 0x39, //9 key
			KEY_MINUS = 0xBD, // - key
			KEY_PLUS = 0xBB, // + key
			KEY_A = 0x41, //A key 
			KEY_B = 0x42, //B key 
			KEY_C = 0x43, //C key 
			KEY_D = 0x44, //D key 
			KEY_E = 0x45, //E key 
			KEY_F = 0x46, //F key 
			KEY_G = 0x47, //G key 
			KEY_H = 0x48, //H key 
			KEY_I = 0x49, //I key 
			KEY_J = 0x4A, //J key 
			KEY_K = 0x4B, //K key 
			KEY_L = 0x4C, //L key 
			KEY_M = 0x4D, //M key 
			KEY_N = 0x4E, //N key 
			KEY_O = 0x4F, //O key 
			KEY_P = 0x50, //P key 
			KEY_Q = 0x51, //Q key 
			KEY_R = 0x52, //R key 
			KEY_S = 0x53, //S key 
			KEY_T = 0x54, //T key 
			KEY_U = 0x55, //U key 
			KEY_V = 0x56, //V key 
			KEY_W = 0x57, //W key 
			KEY_X = 0x58, //X key 
			KEY_Y = 0x59, //Y key 
			KEY_Z = 0x5A, //Z key 
			KEY_LBUTTON = 0x01, //Left mouse button 
			KEY_RBUTTON = 0x02, //Right mouse button 
			KEY_CANCEL = 0x03, //Control-break processing 
			KEY_MBUTTON = 0x04, //Middle mouse button (three-button mouse) 
			KEY_BACK = 0x08, //BACKSPACE key 
			KEY_TAB = 0x09, //TAB key 
			KEY_CLEAR = 0x0C, //CLEAR key 
			KEY_RETURN = 0x0D, //ENTER key 
			KEY_SHIFT = 0x10, //SHIFT key 
			KEY_CONTROL = 0x11, //CTRL key 
			KEY_MENU = 0x12, //ALT key 
			KEY_PAUSE = 0x13, //PAUSE key 
			KEY_CAPITAL = 0x14, //CAPS LOCK key 
			KEY_ESCAPE = 0x1B, //ESC key 
			KEY_SPACE = 0x20, //SPACEBAR 
			KEY_PRIOR = 0x21, //PAGE UP key 
			KEY_NEXT = 0x22, //PAGE DOWN key 
			KEY_END = 0x23, //END key 
			KEY_HOME = 0x24, //HOME key 
			KEY_LEFT = 0x25, //LEFT ARROW key 
			KEY_UP = 0x26, //UP ARROW key 
			KEY_RIGHT = 0x27, //RIGHT ARROW key 
			KEY_DOWN = 0x28, //DOWN ARROW key 
			KEY_SELECT = 0x29, //SELECT key 
			KEY_PRINT = 0x2A, //PRINT key 
			KEY_EXECUTE = 0x2B, //EXECUTE key 
			KEY_SNAPSHOT = 0x2C, //PRINT SCREEN key 
			KEY_INSERT = 0x2D, //INS key 
			KEY_DELETE = 0x2E, //DEL key 
			KEY_HELP = 0x2F, //HELP key 
			KEY_NUMPAD0 = 0x60, //Numeric keypad 0 key 
			KEY_NUMPAD1 = 0x61, //Numeric keypad 1 key 
			KEY_NUMPAD2 = 0x62, //Numeric keypad 2 key 
			KEY_NUMPAD3 = 0x63, //Numeric keypad 3 key 
			KEY_NUMPAD4 = 0x64, //Numeric keypad 4 key 
			KEY_NUMPAD5 = 0x65, //Numeric keypad 5 key 
			KEY_NUMPAD6 = 0x66, //Numeric keypad 6 key 
			KEY_NUMPAD7 = 0x67, //Numeric keypad 7 key 
			KEY_NUMPAD8 = 0x68, //Numeric keypad 8 key 
			KEY_NUMPAD9 = 0x69, //Numeric keypad 9 key 
			KEY_SEPARATOR = 0x6C, //Separator key 
			KEY_SUBTRACT = 0x6D, //Subtract key 
			KEY_DECIMAL = 0x6E, //Decimal key 
			KEY_DIVIDE = 0x6F, //Divide key 
			KEY_F1 = 0x70, //F1 key 
			KEY_F2 = 0x71, //F2 key 
			KEY_F3 = 0x72, //F3 key 
			KEY_F4 = 0x73, //F4 key 
			KEY_F5 = 0x74, //F5 key 
			KEY_F6 = 0x75, //F6 key 
			KEY_F7 = 0x76, //F7 key 
			KEY_F8 = 0x77, //F8 key 
			KEY_F9 = 0x78, //F9 key 
			KEY_F10 = 0x79, //F10 key 
			KEY_F11 = 0x7A, //F11 key 
			KEY_F12 = 0x7B, //F12 key 
			KEY_SCROLL = 0x91, //SCROLL LOCK key 
			KEY_LSHIFT = 0xA0, //Left SHIFT key 
			KEY_RSHIFT = 0xA1, //Right SHIFT key 
			KEY_LCONTROL = 0xA2, //Left CONTROL key 
			KEY_RCONTROL = 0xA3, //Right CONTROL key 
			KEY_LMENU = 0xA4, //Left MENU key 
			KEY_RMENU = 0xA5, //Right MENU key 
			KEY_COMMA = 0xBC, //, key
			KEY_PERIOD = 0xBE, //. key
			KEY_PLAY = 0xFA, //Play key 
			KEY_ZOOM = 0xFB, //Zoom key 
			NULL = 0x0,
		}

		#endregion //Public

		#endregion //Structures

		#region Methods

		#region Public

		public static bool GetKeyState(Key key)
		{
			if ((GetKeyState((int)key.Vk) & 0xF0) == 1)
				return true;

			return false;
		}

		public static uint GetVirtualKeyCode(char c)
		{
			var helper = new Helper { Value = VkKeyScan(c) };

			byte virtualKeyCode = helper.Low;
			byte shiftState = helper.High;

			return virtualKeyCode;
		}

		public static void BackgroundMousePosition(IntPtr hWnd, int x, int y)
		{
			PostMessage(hWnd, (int)WindowsMessages.WM_MOUSEMOVE, 0, GetLParam(x, y));
		}

		public static void BackgroundMouseClick(IntPtr hWnd, Key key, int x, int y, int delay = 100)
		{
			switch (key.Vk)
			{
				case VKeys.KEY_MBUTTON:
					PostMessage(hWnd, (int)Message.MBUTTONDOWN, (uint)key.Vk, GetLParam(x, y));
					Thread.Sleep(delay);
					PostMessage(hWnd, (int)Message.MBUTTONUP, (uint)key.Vk, GetLParam(x, y));
					break;
				case VKeys.KEY_LBUTTON:
					PostMessage(hWnd, (int)Message.LBUTTONDOWN, (uint)key.Vk, GetLParam(x, y));
					Thread.Sleep(delay);
					PostMessage(hWnd, (int)Message.LBUTTONUP, (uint)key.Vk, GetLParam(x, y));
					break;
				case VKeys.KEY_RBUTTON:
					PostMessage(hWnd, (int)Message.RBUTTONDOWN, (uint)key.Vk, GetLParam(x, y));
					Thread.Sleep(delay);
					PostMessage(hWnd, (int)Message.RBUTTONUP, (uint)key.Vk, GetLParam(x, y));
					break;
			}
		}

		public static void SendChatTextPost(IntPtr hWnd, string msg)
		{
			PostMessage(hWnd, new Key(VKeys.KEY_RETURN));
			foreach (char c in msg)
			{
				PostMessage(hWnd, new Key(c));
			}
			PostMessage(hWnd, new Key(VKeys.KEY_RETURN));
		}

		public static void SendChatTextSend(IntPtr hWnd, string msg)
		{
			SendMessage(hWnd, new Key(VKeys.KEY_RETURN), true);
			foreach (char c in msg)
			{
				SendChar(hWnd, c, true);
			}
			SendMessage(hWnd, new Key(VKeys.KEY_RETURN), true);
		}

		public static bool ForegroundKeyPress(Key key, int delay = 100)
		{
			bool temp = true;

			temp &= ForegroundKeyDown(key);
			Thread.Sleep(delay);
			temp &= ForegroundKeyUp(key);
			Thread.Sleep(delay);
			return temp;
		}

		public static bool ForegroundKeyPress(IntPtr hWnd, Key key, int delay = 100)
		{
			bool temp = true;

			temp &= ForegroundKeyDown(hWnd, key);
			Thread.Sleep(delay);
			temp &= ForegroundKeyUp(hWnd, key);
			Thread.Sleep(delay);
			return temp;
		}

		public static bool ForegroundKeyDown(Key key)
		{
			uint intReturn;
			INPUT structInput;
			structInput = new INPUT();
			structInput.type = INPUT_KEYBOARD;

			// Key down shift, ctrl, and/or alt
			structInput.u.ki.wScan = 0;
			structInput.u.ki.time = 0;
			structInput.u.ki.dwFlags = 0;
			// Key down the actual key-code
			structInput.u.ki.wVk = (ushort)key.Vk;
			intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));

			// Key up shift, ctrl, and/or alt
			//keybd_event((int)key.VK, GetScanCode(key.VK) + 0x80, KEYEVENTF_NONE, 0);
			//keybd_event((int)key.VK, GetScanCode(key.VK) + 0x80, KEYEVENTF_KEYUP, 0);
			return true;
		}

		public static bool ForegroundKeyUp(Key key)
		{
			uint intReturn;
			INPUT structInput;
			structInput = new INPUT();
			structInput.type = INPUT_KEYBOARD;

			// Key down shift, ctrl, and/or alt
			structInput.u.ki.wScan = 0;
			structInput.u.ki.time = 0;
			structInput.u.ki.dwFlags = 0;
			// Key down the actual key-code
			structInput.u.ki.wVk = (ushort)key.Vk;

			// Key up the actual key-code
			structInput.u.ki.dwFlags = KEYEVENTF_KEYUP;
			intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(typeof(INPUT)));
			return true;
		}

		public static bool ForegroundKeyDown(IntPtr hWnd, Key key)
		{
			if (GetForegroundWindow() != hWnd)
			{
				if (!SetForegroundWindow(hWnd))
					return false;
			}
			return ForegroundKeyDown(key);
		}

		public static bool ForegroundKeyUp(IntPtr hWnd, Key key)
		{
			if (GetForegroundWindow() != hWnd)
			{
				if (!SetForegroundWindow(hWnd))
					return false;
			}
			return ForegroundKeyUp(key);
		}

		public static bool ForegroundKeyPressAll(IntPtr hWnd, Key key, bool alt, bool ctrl, bool shift, int delay = 100)
		{
			if (GetForegroundWindow() != hWnd)
			{
				if (!SetForegroundWindow(hWnd))
					return false;
			}
			uint intReturn;
			INPUT structInput;
			structInput = new INPUT();
			structInput.type = INPUT_KEYBOARD;

			// Key down shift, ctrl, and/or alt
			structInput.u.ki.wScan = 0;
			structInput.u.ki.time = 0;
			structInput.u.ki.dwFlags = 0;
			if (alt)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_MENU;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (ctrl)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_CONTROL;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (shift)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_SHIFT;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);

				if (key.ShiftKey != VKeys.NULL)
				{
					structInput.u.ki.wVk = (ushort)key.ShiftKey;
					intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
					Thread.Sleep(delay);
				}
			}

			// Key up the actual key-code			
			ForegroundKeyPress(hWnd, key);

			structInput.u.ki.dwFlags = KEYEVENTF_KEYUP;
			if (shift && key.ShiftKey == VKeys.NULL)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_SHIFT;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (ctrl)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_CONTROL;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (alt)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_MENU;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			return true;
		}

		public static bool PostMessage(IntPtr hWnd, Key key, int delay = 100)
		{
			//Send KEY_DOWN
			if (PostMessage(hWnd, (int)Message.KEY_DOWN, (uint)key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
				return false;
			Thread.Sleep(delay);
			//Send VM_CHAR
			if (PostMessage(hWnd, (int)Message.VM_CHAR, (uint)key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
				return false;
			Thread.Sleep(delay);
			if (PostMessage(hWnd, (int)Message.KEY_UP, (uint)key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
				return false;
			Thread.Sleep(delay);

			return true;
		}

		public static bool PostMessageAll(IntPtr hWnd, Key key, bool alt, bool ctrl, bool shift, int delay = 100)
		{
			CheckKeyShiftState();
			uint intReturn;
			INPUT structInput;
			structInput = new INPUT();
			structInput.type = INPUT_KEYBOARD;

			// Key down shift, ctrl, and/or alt
			structInput.u.ki.wScan = 0;
			structInput.u.ki.time = 0;
			structInput.u.ki.dwFlags = 0;
			if (alt)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_MENU;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (ctrl)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_CONTROL;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (shift)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_SHIFT;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);

				if (key.ShiftKey != VKeys.NULL)
				{
					//Send KEY_DOWN
					if (PostMessage(hWnd, (int)Message.KEY_DOWN, (uint)key.Vk, GetLParam(1, key.ShiftKey, 0, 0, 0, 0)))
						return false;
					Thread.Sleep(delay);
				}
			}

			PostMessage(hWnd, key);

			structInput.u.ki.dwFlags = KEYEVENTF_KEYUP;
			if (shift && key.ShiftKey == VKeys.NULL)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_SHIFT;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (ctrl)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_CONTROL;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (alt)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_MENU;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}

			return true;
		}

		public static bool SendMessageDown(IntPtr hWnd, Key key, bool checkKeyboardState, int delay = 100)
		{
			if (checkKeyboardState)
				CheckKeyShiftState();
			//Send KEY_DOWN
			if (SendMessage(hWnd, (int)Message.KEY_DOWN, (uint)key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
				return false;
			Thread.Sleep(delay);

			//Send VM_CHAR
			if (SendMessage(hWnd, (int)Message.VM_CHAR, (uint)key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
				return false;
			Thread.Sleep(delay);

			return true;
		}

		public static bool SendMessageUp(IntPtr hWnd, Key key, bool checkKeyboardState, int delay = 100)
		{
			if (checkKeyboardState)
				CheckKeyShiftState();

			//Send KEY_UP
			if (SendMessage(hWnd, (int)Message.KEY_UP, (uint)key.Vk, GetLParam(1, key.Vk, 0, 0, 1, 1)))
				return false;
			Thread.Sleep(delay);

			return true;
		}

		public static bool SendChar(IntPtr hWnd, char c, bool checkKeyboardState)
		{
			if (checkKeyboardState)
				CheckKeyShiftState();

			//Send VM_CHAR
			if (SendMessage(hWnd, (int)Message.VM_CHAR, c, 0))
				return false;

			return true;
		}

		public static bool SendMessage(IntPtr hWnd, Key key, bool checkKeyboardState, int delay = 100)
		{
			if (checkKeyboardState)
				CheckKeyShiftState();

			//Send KEY_DOWN
			if (SendMessage(hWnd, (int)Message.KEY_DOWN, (uint)key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
				return false;
			Thread.Sleep(delay);

			//Send VM_CHAR
			if (SendMessage(hWnd, (int)Message.VM_CHAR, (uint)key.Vk, GetLParam(1, key.Vk, 0, 0, 0, 0)))
				return false;
			Thread.Sleep(delay);

			//Send KEY_UP
			if (SendMessage(hWnd, (int)Message.KEY_UP, (uint)key.Vk, GetLParam(1, key.Vk, 0, 0, 1, 1)))
				return false;
			Thread.Sleep(delay);

			return true;
		}

		public static bool SendMessageAll(IntPtr hWnd, Key key, bool alt, bool ctrl, bool shift, int delay = 100)
		{
			CheckKeyShiftState();
			uint intReturn;
			INPUT structInput = new INPUT
			{
				type = INPUT_KEYBOARD,
				u = new InputUnion
				{
					ki = { wScan = 0, time = 0, dwFlags = 0 }
				}
			};

			// Key down shift, ctrl, and/or alt
			if (alt)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_MENU;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (ctrl)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_CONTROL;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (shift)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_SHIFT;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);

				if (key.ShiftKey != VKeys.NULL)
				{
					//Send KEY_DOWN
					if (SendMessage(hWnd, (int)Message.KEY_DOWN, (uint)key.Vk, GetLParam(1, key.ShiftKey, 0, 0, 0, 0)))
						return false;
					Thread.Sleep(delay);
				}
			}

			SendMessage(hWnd, key, false);

			structInput.u.ki.dwFlags = KEYEVENTF_KEYUP;
			if (shift && key.ShiftKey == VKeys.NULL)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_SHIFT;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (ctrl)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_CONTROL;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}
			if (alt)
			{
				structInput.u.ki.wVk = (ushort)VKeys.KEY_MENU;
				intReturn = SendInput(1, new[] { structInput }, Marshal.SizeOf(new INPUT()));
				Thread.Sleep(delay);
			}

			return true;
		}

		public static void CheckKeyShiftState()
		{
			while ((GetKeyState((int)VKeys.KEY_MENU) & KEY_PRESSED) == KEY_PRESSED ||
				   (GetKeyState((int)VKeys.KEY_CONTROL) & KEY_PRESSED) == KEY_PRESSED ||
				   (GetKeyState((int)VKeys.KEY_SHIFT) & KEY_PRESSED) == KEY_PRESSED)
			{
				Thread.Sleep(1);
			}
		}

		#endregion //Public

		#region Private

		private static uint GetScanCode(VKeys key)
		{
			return MapVirtualKey((uint)key, MAPVK_VK_TO_VSC_EX);
		}

		private static uint GetDwExtraInfo(Int16 repeatCount, VKeys key, byte extended, byte contextCode, byte previousState,
			byte transitionState)
		{
			var lParam = (uint)repeatCount;
			uint scanCode = MapVirtualKey((uint)key, MAPVK_VK_TO_VSC_EX) + 0x80;
			lParam += scanCode * 0x10000;
			lParam += (uint)((extended) * 0x1000000);
			lParam += (uint)((contextCode * 2) * 0x10000000);
			lParam += (uint)((previousState * 4) * 0x10000000);
			lParam += (uint)((transitionState * 8) * 0x10000000);
			return lParam;
		}

		private static uint GetLParam(int x, int y)
		{
			return (uint)((y << 16) | (x & 0xFFFF));
		}

		private static uint GetLParam(Int16 repeatCount, VKeys key, byte extended, byte contextCode, byte previousState,
			byte transitionState)
		{
			var lParam = (uint)repeatCount;
			//uint scanCode = MapVirtualKey((uint)key, MAPVK_VK_TO_CHAR);
			uint scanCode = GetScanCode(key);
			lParam += scanCode * 0x10000;
			lParam += (uint)((extended) * 0x1000000);
			lParam += (uint)((contextCode * 2) * 0x10000000);
			lParam += (uint)((previousState * 4) * 0x10000000);
			lParam += (uint)((transitionState * 8) * 0x10000000);
			return lParam;
		}

		private static uint RemoveLeadingDigit(uint number)
		{
			return (number - ((number % (0x10000000)) * (0x10000000)));
		}

		#endregion Private

		#endregion //Methods
	}
}
