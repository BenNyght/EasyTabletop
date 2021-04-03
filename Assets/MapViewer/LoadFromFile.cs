using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using FrostweepGames.Plugins.WebGLFileBrowser;
using UnityEngine.UI;
using System.Linq;

public class LoadFromFile : MonoBehaviour
{
    private const string USER_FILES = "Images";

    private Sprite spriteSave;
    private RawImage contentRawImage;

    private string[] _availableTypes = new string[]
    {
        "jpg",
        "jpeg",
        "png"
    };

    private string _fileTypesEntered;

    private byte[] _currentLoadedData;

    public void OpenFolder()
    {
        OpenFileDialogButtonOnClickHandler();
    }

    private void LoadMap(Sprite sprite)
    {
        GameObject imageSelection = GameObject.Find("SelectionData");
        imageSelection.GetComponent<SelectionData>().map = sprite;

        LevelLoader loader = GameObject.Find("LevelLoader").GetComponent<LevelLoader>();
        loader.LoadLevel("Mapviewer");
    }

    private void OpenFileDialogButtonOnClickHandler()
    {
        string[] types = { 
            "jpg",
            "jpeg",
            "png"
        };

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

    private void FilePopupWasClosedEventHandler()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            FileBrowserDialogLib.FileWasOpenedEvent -= FileWasOpenedEventHandler;
            FileBrowserDialogLib.FilePopupWasClosedEvent -= FilePopupWasClosedEventHandler;
#endif
    }

    private void FileWasOpenedEventHandler(byte[] data, string name, string resolution)
    {
        _currentLoadedData = data;

#if UNITY_WEBGL && !UNITY_EDITOR
                    FileBrowserDialogLib.FileWasOpenedEvent -= FileWasOpenedEventHandler;
                    FileBrowserDialogLib.FilePopupWasClosedEvent -= FilePopupWasClosedEventHandler;
#endif

        spriteSave = GetSprite(_currentLoadedData);
        LoadMap(spriteSave);
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

    public static Sprite GetSprite(byte[] data, string name = "custom")
    {
        if (data == null)
            return null;

        var texture = GetTexture2D(data, name);

        var sprite = Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)), Vector2.one / 2f, 100, 1, SpriteMeshType.FullRect, Vector4.zero);

        return sprite;
    }

    public static Texture2D GetTexture2D(byte[] data, string name = "custom")
    {
        if (data == null)
            return null;

        var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false, true);

        texture.name = name;
        texture.LoadImage(data);
        texture.Apply();

        return texture;
    }
}
