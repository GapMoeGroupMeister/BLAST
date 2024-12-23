using System;
using UnityEngine;

namespace Crogen.AttributeExtension
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class Button : PropertyAttribute
    {
        public string ButtonName { get; private set; }
        public int Space { get; private set; }
        
        public Button()
        {
            this.ButtonName = string.Empty;
            this.Space = 0;
        }
        
        public Button(string buttonName = "")
        {
            this.ButtonName = buttonName;
            this.Space = 0;
        }
        
        public Button(int space = 0)
        {
            this.ButtonName = string.Empty;
            this.Space = space;
        }
        
        public Button(string buttonName = "", int space = 0)
        {
            this.ButtonName = buttonName;
            this.Space = space;
        }
    }
}
