using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI = SharpDX.DirectInput;

namespace OBSPadSpy2.Pad
{
    class Gamepad
    {
        private DI.Joystick Pad;
        private DI.JoystickState PadState;
        private DI.DirectInput dInput;
        private bool padConnected = false;
        private bool[] padTrans;

        public Gamepad()
        {
            dInput = new DI.DirectInput();
            var list = dInput.GetDevices(DI.DeviceType.Gamepad, DI.DeviceEnumerationFlags.AttachedOnly);
            if (list.Count == 0)
            {
                list = dInput.GetDevices(DI.DeviceType.Joystick, DI.DeviceEnumerationFlags.AttachedOnly);
                if (list.Count == 0)
                {
                    padConnected = false;
                }
                else
                {
                    padConnected = true;
                }
            }
            else
            {
                padConnected = true;
            }

            if (padConnected)
            {
                padTrans = new bool[8];
                Pad = new DI.Joystick(dInput, list[0].InstanceGuid);
                //Pad.Acquire();
            }
        }

        public void Start()
        {
            if (padConnected)
            {
                Pad.Acquire();
            }
        }

        public void Stop()
        {
            if (padConnected)
            {
                Pad.Unacquire();
            }
        }

        public void RefreshState()
        {
            if (padConnected)
            {
                PadState = Pad.GetCurrentState();
            }
            else
            {
                PadState = new DI.JoystickState();
            }
        }

        public DI.JoystickState GetJoyState()
        {
            return PadState;
        }

        public override string ToString()
        {
            return Pad.Information.ProductName;
        }

        public bool[] GetState()
        {
            for (int i = 0; i < 4; i++)
            {
                if (PadState.Buttons[i])
                {
                    padTrans[i] = true;
                }
                else
                {
                    padTrans[i] = false;
                }
            }

            int dig = PadState.PointOfViewControllers[0];
            int angle = Functions.GamePadHelper.GetAngle(PadState.X, PadState.Y);

            double anaX = (PadState.X - 32768);
            double anaY = (PadState.Y - 32768);

            if (Math.Abs(anaX) < 5000 && Math.Abs(anaY) < 5000)
            {
                if (dig != -1)
                {
                    angle = dig / 100;
                }
                else
                {
                    angle = -1;
                }
            }

            if (angle == 45)
            {
                padTrans[4] = true;
                padTrans[5] = true;
                padTrans[6] = false;
                padTrans[7] = false;
            }
            if (angle == 90)
            {
                padTrans[4] = true;
                padTrans[5] = false;
                padTrans[6] = false;
                padTrans[7] = false;
            }
            if (angle == 135)
            {
                padTrans[4] = false;
                padTrans[5] = true;
                padTrans[6] = true;
                padTrans[7] = false;
            }
            if (angle == 180)
            {
                padTrans[4] = false;
                padTrans[5] = true;
                padTrans[6] = false;
                padTrans[7] = false;
            }
            if (angle == 225)
            {
                padTrans[4] = false;
                padTrans[5] = true;
                padTrans[6] = true;
                padTrans[7] = false;
            }
            if (angle == 270)
            {
                padTrans[4] = false;
                padTrans[5] = false;
                padTrans[6] = true;
                padTrans[7] = false;
            }
            if (angle == 270)
            {
                padTrans[4] = false;
                padTrans[5] = false;
                padTrans[6] = true;
                padTrans[7] = true;
            }
            if (angle == 0)
            {
                padTrans[4] = false;
                padTrans[5] = false;
                padTrans[6] = false;
                padTrans[7] = true;
            }
            if (angle == -1)
            {
                padTrans[4] = false;
                padTrans[5] = false;
                padTrans[6] = false;
                padTrans[7] = false;
            }

            return padTrans;
        }

        public bool ButtonAPressed()
        {
            bool pressed = false;

            if (padConnected)
            {
                PadState = Pad.GetCurrentState();
                if (PadState.Buttons[0])
                {
                    pressed = true;
                }
            }
            return pressed;
        }

    }
}
