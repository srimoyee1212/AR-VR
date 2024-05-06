using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "Create Student Submission")]
public class StudentInfoScript : ScriptableObject
{
    /// <summary>
    /// DO NOT MODIFY THIS SCRIPT
    /// </summary>
    [Header("Student Information")]
    [Header("Student Last Name")]
    public string lastName;
    [Header("Student First Name")]
    public string firstName;
    [Header("Student Cornell NetID")]
    public string ID;
    [Header("GitHub Username")]
    public string Username;
    [Header("Please provide full links")] 
    [Header("GitHub Repository URL")]
    public string repoURL;
    [Header("Video Recording URL")]
    public string videoURL;
    [Header("Work Summary")]
    [Header("Write a short summary of\nyour approach to this assignment\nand list the main challenges you faced")]
    [TextArea(minLines:20,100)] public string workSummary;

}