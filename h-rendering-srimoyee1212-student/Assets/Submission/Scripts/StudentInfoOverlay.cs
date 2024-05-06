using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Do not modify
/// For information overlay on desktop assignment
/// </summary>
[RequireComponent(typeof(Text))]
public class StudentInfoOverlay : MonoBehaviour
{
    [SerializeField]
    private StudentInfoScript m_StudentInfo;

    void Start()
    {
        var text = GetComponent<Text>();
        text.text = $"{m_StudentInfo.lastName}, {m_StudentInfo.firstName} ({m_StudentInfo.ID})";
    }
}
