using UnityEngine;

namespace XRC.Assignments.Project.G07
{
    public class UndoStackItem
    {
        public GameObject gameObject { get; set; }
        public string additionalInfo { get; set; }

        public UndoStackItem(GameObject gameObject, string additionalInfo)
        {
            this.gameObject = gameObject;
            this.additionalInfo = additionalInfo;
        }
    }
}