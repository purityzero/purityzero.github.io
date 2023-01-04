using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class TimeUtil 
    {
        static public int GetDay(int _time)
        {
            return _time / 3600 / 24;
        }

        static public int GetHour(int _time)
        {
            return _time / 3600 % 24;
        }

        static public int GetMinute(int _time)
        {
            return (_time % 3600) / 60;
        }

        static public int GetSecond(int _time)
        {
            return (_time % 3600) % 60;
        }

        static public int GetMinute(long _time)
        {
            return (int)((_time % 3600) / 60);
        }

        static public int GetSecond(long _time)
        {
            return (int)((_time % 3600) % 60);
        }

    }
}
