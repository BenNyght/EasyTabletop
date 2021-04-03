using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using FrostweepGames.Plugins.WebGLFileBrowser;

public class FileBrowser : MonoBehaviour
{
    private const string USER_FILES = "User Files";

    public RawImage contentRawImage;

    public Text fileNameText,
                fileInfoText;

    private string[] _availableTypes = new string[]
    {
            "txt", "rtf", "doc", "docx", "html", "pdf", "ttf", "rar", "zip", "xls", "xlsx", "mp3", "avi", "mp4", "mkv", "wma", "wav", "cda",
            "mpg", "mpg", "flv", "wmv", "bmp", "gif", "jpg", "jpeg", "tiff", "png"
    };

    private string _fileTypesEntered;

    private byte[] _currentLoadedData;
    private string _currentLoadedDataResolution;


    private void SaveOpenedFileButtonOnClickHandler()
    {
        if (_currentLoadedData == null)
            return;

#if UNITY_EDITOR
        string path = UnityEditor.EditorUtility.SaveFilePanel("File Browser", System.IO.Directory.GetLogicalDrives()[0], "lastLoadedData", _currentLoadedDataResolution.Replace(".", ""));

        if (path.Length != 0)
        {
            File.WriteAllBytes(path, _currentLoadedData);
        }
#elif UNITY_WEBGL

            // NOT SUPPORTED YET

            // FileBrowserDialogLib.SaveBytesToFile("lastLoadedData" + _currentLoadedDataResolution, _currentLoadedData);
#endif
    }

    public void OpenFileDialogButtonOnClickHandler()
    {
        string[] types = GetFilteredFileTypes();

#if UNITY_EDITOR
        string path = UnityEditor.EditorUtility.OpenFilePanelWithFilters("File Browser", System.IO.Directory.GetLogicalDrives()[0], new string[] { USER_FILES, string.Join(",", types) });

        if (path.Length != 0)
        {
            byte[] fileContent = File.ReadAllBytes(path);
            FileWasOpenedEventHandler(fileContent, Path.GetFileNameWithoutExtension(path), Path.GetExtension(path));
        }
#elif UNITY_WEBGL
            if (!string.IsNullOrEmpty(types[0]))
            {
                types[0] = types[0].Insert(0, ".");
            }
            FileBrowserDialogLib.FileWasOpenedEvent += FileWasOpenedEventHandler;
            FileBrowserDialogLib.FilePopupWasClosedEvent += FilePopupWasClosedEventHandler;
            FileBrowserDialogLib.OpenFileDialog(string.Join(",.", types));
#endif
    }

    private void FileWasOpenedEventHandler(byte[] data, string name, string resolution)
    {
        _currentLoadedData = data;
        _currentLoadedDataResolution = resolution;

        if (resolution.Contains(".png") || resolution.Contains(".jpeg") || resolution.Contains(".jpg"))
            contentRawImage.texture = FileBrowserDialogLib.GetTexture2D(data, name);

        fileNameText.text = name;
        fileInfoText.text = "File Name: " + name + "\nFile Resolution: " + resolution;

#if UNITY_WEBGL && !UNITY_EDITOR
            FileBrowserDialogLib.FileWasOpenedEvent -= FileWasOpenedEventHandler;
            FileBrowserDialogLib.FilePopupWasClosedEvent -= FilePopupWasClosedEventHandler;
#endif
    }

    private void FilePopupWasClosedEventHandler()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            FileBrowserDialogLib.FileWasOpenedEvent -= FileWasOpenedEventHandler;
            FileBrowserDialogLib.FilePopupWasClosedEvent -= FilePopupWasClosedEventHandler;
#endif
    }

    private void FilterOfTypesFieldOnValueChangedHandler(string value)
    {
        _fileTypesEntered = value;
    }

    private string[] GetFilteredFileTypes()
    {
        string[] types = new string[] { "" };
        if (!string.IsNullOrEmpty(_fileTypesEntered))
        {
            _fileTypesEntered = _fileTypesEntered.Replace(" ", string.Empty);
            if (_fileTypesEntered.Split(new[] { ',' }).Length > 0)
            {
                types = _fileTypesEntered.Split(new[] { ',' }, System.StringSplitOptions.RemoveEmptyEntries)
                  .Where(type => _availableTypes.Contains(type))
                  .ToArray();
            }
            else if (_availableTypes.Contains(_fileTypesEntered))
            {
                types = new string[] { _fileTypesEntered };
            }
        }
        return types;
    }
}
