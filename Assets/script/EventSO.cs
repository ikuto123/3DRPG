using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EventSO : ScriptableObject
{
    public List<Event> eventList = new List<Event>();

    [System.Serializable]
    public class Event
    {
        [SerializeField] int Number;
        [SerializeField] string personName;
        [TextArea]
        [SerializeField] string word;
        [SerializeField] int yes;
        [SerializeField] int no;

        

        public string Word { get => word; }
        public int Yes { get => yes; }
        public int No { get => no; }

    }
}
