using System;
using System.IO;
using System.Collections.Generic;
using System.Threading;
using EventHook;

namespace MacroAutomation
{
    class EventHookRecorder
    {
        private MouseWatcher mouseWatcher;
        private KeyboardWatcher keyboardWatcher;
        public EventHookRecorder()
        {
            var eventHookFactory = new EventHookFactory();
            mouseWatcher = eventHookFactory.GetMouseWatcher();
            keyboardWatcher = eventHookFactory.GetKeyboardWatcher();
        }
        public void AddMouseListener(EventHandler<MouseEventArgs> listener)
        {
            mouseWatcher.OnMouseInput += listener;
        }
        public void AddKeyBoardListener(EventHandler<KeyInputEventArgs> listener)
        {
            keyboardWatcher.OnKeyInput += listener;
        }
        public void Start()
        {
            mouseWatcher.Start();
            keyboardWatcher.Start();
        }
        public void Stop()
        {
            mouseWatcher.Stop();
            keyboardWatcher.Stop();
        }
    }


}
