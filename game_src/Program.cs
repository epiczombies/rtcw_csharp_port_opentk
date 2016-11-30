using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace game_src
{
    public class MainForm : GameWindow
    {
        static WeakReference _Instance;
        static DisplayDevice _DisplayDevice;
        Stopwatch _Swatch;
        int _MouseBtnState;

        [STAThread]
        static int Main(string[] args)
        {
            return 0; // all Ok
        }
        static MainForm CreateInstance(Size size, GraphicsMode mode, bool fullScreen)
        {
            if (_Instance != null)
                throw new Exception("MainForm instance is already created!");
            return new MainForm(size, mode, fullScreen);
        }





        /*
        ================
        Main Code to run the Game.
        ================
        */
        private MainForm(Size size, GraphicsMode mode, bool fullScreen) : base(size.Width, size.Height, mode, "game_src", fullScreen ? GameWindowFlags.Fullscreen : GameWindowFlags.Default)
        {
            _Instance = new WeakReference(this);
            _Swatch = new Stopwatch();
            this.VSync = VSyncMode.On;
            this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            if (this.Keyboard != null)
            {
                this.Keyboard.KeyRepeat = true;
                this.Keyboard.KeyDown += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs>(Keyboard_KeyDown);
                this.Keyboard.KeyUp += new EventHandler<OpenTK.Input.KeyboardKeyEventArgs>(Keyboard_KeyUp);
            }
            if (this.Mouse != null)
            {
                this.Mouse.Move += new EventHandler<OpenTK.Input.MouseMoveEventArgs>(Mouse_Move);
                this.Mouse.ButtonDown += new EventHandler<OpenTK.Input.MouseButtonEventArgs>(Mouse_ButtonEvent);
                this.Mouse.ButtonUp += new EventHandler<OpenTK.Input.MouseButtonEventArgs>(Mouse_ButtonEvent);
                this.Mouse.WheelChanged += new EventHandler<OpenTK.Input.MouseWheelEventArgs>(Mouse_WheelChanged);
            }
        }
        void Mouse_WheelChanged(object sender, MouseWheelEventArgs e)
        {
        }
        void Mouse_ButtonEvent(object sender, MouseButtonEventArgs e)
        {
            _MouseBtnState = 0;

            if (e.Button == MouseButton.Left && e.IsPressed)
                _MouseBtnState |= 1;

            if (e.Button == MouseButton.Right && e.IsPressed)
                _MouseBtnState |= 2;

            if (e.Button == MouseButton.Middle && e.IsPressed)
                _MouseBtnState |= 4;

            //Input.MouseEvent(_MouseBtnState);
        }
        void Mouse_Move(object sender, MouseMoveEventArgs e)
        {
        }
        void Keyboard_KeyUp(object sender, KeyboardKeyEventArgs e)
        {
        }
        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
        }
        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            base.OnClosing(e);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            try
            {
                //if (this.WindowState == WindowState.Minimized)
                //    Scr.SkipUpdate = true;	// no point in bothering to draw

                _Swatch.Stop();
                double ts = _Swatch.Elapsed.TotalSeconds;
                _Swatch.Reset();
                _Swatch.Start();
                //Host.Frame(ts);
            }
            catch (Exception ex)
            {
                // nothing to do
            }
        }
    }
}
