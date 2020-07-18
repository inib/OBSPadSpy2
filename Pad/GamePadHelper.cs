using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OBSPadSpy2.Functions
{
    static class GamePadHelper
    {
        public static int GetAngle(int x, int y)
        {
            int angle = 0;
            double anaX = (x - 32768);
            double anaY = (y - 32768);

            int anaAngle = 0;

            try
            {
                anaAngle = (int)((180 / Math.PI) * Math.Atan2(anaX, anaY));
            }
            catch (Exception)
            {
                anaAngle = 0;
            }

            if (anaAngle >= 0)
            {
                anaAngle = (int)Math.Ceiling(anaAngle / 22.5);
            }
            else
            {
                anaAngle = (int)Math.Floor(anaAngle / 22.5);
            }
            
            switch (anaAngle)
            {
                case 0:
                    angle = 180;
                    break;
                case 1:
                    angle = 180;
                    break;
                case 2:
                    angle = 135;
                    break;
                case 3:
                    angle = 135;
                    break;
                case 4:
                    angle = 90;
                    break;
                case 5:
                    angle = 90;
                    break;
                case 6:
                    angle = 45;
                    break;
                case 7:
                    angle = 45;
                    break;
                case 8:
                    angle = 0;
                    break;
                case -1:
                    angle = 180;
                    break;
                case -2:
                    angle = 225;
                    break;
                case -3:
                    angle = 225;
                    break;
                case -4:
                    angle = 270;
                    break;
                case -5:
                    angle = 270;
                    break;
                case -6:
                    angle = 315;
                    break;
                case -7:
                    angle = 315;
                    break;
                case -8:
                    angle = 0;
                    break;

                default:
                    angle = 0;
                    break;
            }

            //if (anaAngle >= 0)
            //{
            //    if (anaAngle >= 45)
            //    {
            //        if (anaAngle >= 135)
            //        {
            //            angle = 0;
            //        }
            //        else
            //        {
            //            angle = 90;
            //        }
            //    }
            //    else
            //    {
            //        angle = 180;
            //    }
            //}
            //else
            //{
            //    if (anaAngle <= (-45))
            //    {
            //        if (anaAngle <= (-135))
            //        {
            //            angle = 0;
            //        }
            //        else
            //        {
            //            angle = 270;
            //        }
            //    }
            //    else
            //    {
            //        angle = 180;
            //    }
            //}
            return angle;
        }
    }
}
