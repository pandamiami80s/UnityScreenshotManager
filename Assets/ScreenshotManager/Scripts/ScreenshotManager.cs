using System;
using System.IO;
using UnityEngine;

/// <summary>
/// 2024 01 31
/// 
/// Screenshot manager (DontDestroyOnLoad)
///     * Save screenshot with KEYCODE.P
///     * Get file from UNITY_PROJECT_NAME/FOLDER_NAME
/// </summary>

public class ScreenshotManager : MonoBehaviour
{
    static ScreenshotManager screenshotManager;

    [Header("Folder")]
    [SerializeField] string folderName = "Screenshots";
    string currentFolder;

    [Header("File")]
    [SerializeField] string fileName = "yyyyMMdd_HHmmss";
    [SerializeField] string fileFormat = "png";



    void Awake()
    {
        // When current scene is reloaded multiple instances appear
        if (screenshotManager == null)
        {
            DontDestroyOnLoad(gameObject);
            screenshotManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Directory
        DirectoryInfo directoryInfo = new DirectoryInfo(Application.dataPath);
        currentFolder = string.Format("{0}/{1}", directoryInfo.Parent.ToString(), folderName);
        if (Directory.Exists(currentFolder) == false)
        {
            Directory.CreateDirectory(currentFolder);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // File name
            string path = string.Format("{0}/{1}.{2}", currentFolder, DateTime.Now.ToString(fileName), fileFormat);

            // Save screenshot
            ScreenCapture.CaptureScreenshot(path);
        }
    }
}