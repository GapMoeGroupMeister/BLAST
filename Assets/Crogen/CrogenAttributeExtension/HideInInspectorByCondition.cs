using System;
using UnityEngine;

namespace Crogen.AttributeExtension
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class HideInInspectorByCondition : PropertyAttribute
    {
        public bool Reversed { get; private set; }
        public string BooleanPropertyName { get; private set; }

        public HideInInspectorByCondition(string booleanPropertyName, bool reversed = false)
        {
            this.BooleanPropertyName = booleanPropertyName;
            this.Reversed = reversed;
        }
    }
}
