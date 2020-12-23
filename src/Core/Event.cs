using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroAutomation.Core
{

    public enum EventType
    {
       MOUSE_LBUTTONDOWN,
       MOUSE_LBUTTONUP,
       MOUSE_RBUTTONDOWN,
       MOUSE_RBUTTONUP,
       MOUSE_MOVE,
       KEYEVENT_UP,
       KEYEVENT_DOWN,
    }

    public class Event
    {
        public EventType Name { get; set; }

        //MouseEvent variables
        public int? MouseX { get; set; }
        public int? MouseY { get; set; }

        //KeyInput event variables 
        public string Keyname { get; set;  }
        public string UnicodeCharacter { get; set; }
        public long TimeInMillis { set; get; }
    }
}
