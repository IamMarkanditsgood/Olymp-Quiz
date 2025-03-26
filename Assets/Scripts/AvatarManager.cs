using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class AvatarManager : MonoBehaviour
{
    [SerializeField] private RawImage _avatar;
    [SerializeField] private RawImage _avatarHome;
    [SerializeField] private RawImage _editorHome;

    [SerializeField] private Texture2D _basicAvatar;

    private int maxSize = 1000;
    private string _path;
    private Texture2D _texture;

    public void SetSavedPicture()
    {
        if (PlayerPrefs.HasKey("AvatarPath"))
        {
            string path = PlayerPrefs.GetString("AvatarPath");
            _avatar.texture = NativeGallery.LoadImageAtPath(path, maxSize);
            _avatarHome.texture = NativeGallery.LoadImageAtPath(path, maxSize);
            _editorHome.texture = NativeGallery.LoadImageAtPath(path, maxSize);
        }
        else
        {
            _avatar.texture = _basicAvatar;
            _avatarHome.texture = _basicAvatar;
            _editorHome.texture = _basicAvatar;
        }
    }

    public void PickFromGallery()
    {
        if (NativeGallery.IsMediaPickerBusy())
            return;

        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                _path = path;

                _texture = NativeGallery.LoadImageAtPath(path, maxSize);
                if (_texture == null)
                {
                    Debug.Log("Couldn't load texture from " + path);
                    return;
                }

                
                _editorHome.texture = _texture;
            }
        }, "Select an image", "image/*");

        Debug.Log("Permission result: " + permission);
    }

    public void Save()
    {
        if (_path != null)
        {
            PlayerPrefs.SetString("AvatarPath", _path);
            string path = PlayerPrefs.GetString("AvatarPath");
            _avatar.texture = NativeGallery.LoadImageAtPath(path, maxSize);
            _avatarHome.texture = NativeGallery.LoadImageAtPath(path, maxSize);
        }
        
    }
}
