using UnityEngine;
using System.Collections;
using System.IO;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Dreamteck
{
    public static class ResourceUtility
    {
        private static string dreamteckFolder;
        private static string dreamteckLocalFolder;
        private static bool directoryIsValid = false;

        static ResourceUtility()
        {
            string defaultPath = Application.dataPath + "/Dreamteck";

#if UNITY_EDITOR
            dreamteckFolder = EditorPrefs.GetString("Dreamteck.ResourceUtility.dreamteckProjectFolder", defaultPath);
#endif

            if (!dreamteckFolder.StartsWith(Application.dataPath))
            {
                dreamteckFolder = defaultPath;
            }

            if (!Directory.Exists(dreamteckFolder))
            {
                dreamteckFolder = FindFolder(Application.dataPath, "Dreamteck");
                directoryIsValid = Directory.Exists(dreamteckFolder);
            }
            else
            {
                directoryIsValid = true;
            }

            if (directoryIsValid)
            {
                dreamteckLocalFolder = dreamteckFolder.Substring(Application.dataPath.Length + 1);
#if UNITY_EDITOR
                EditorPrefs.SetString("Dreamteck.ResourceUtility.dreamteckProjectFolder", dreamteckFolder);
#endif
            }
        }

        //Attempts to find the input directory pattern inside a given directory and if it fails, proceeds with looking up all subfolders
        public static string FindFolder(string dir, string folderPattern)
        {
            if (folderPattern.StartsWith("/")) folderPattern = folderPattern.Substring(1);
            if (!dir.EndsWith("/")) dir += "/";
            if (folderPattern == "") return "";
            string[] folders = folderPattern.Split('/');
            if (folders.Length == 0) return "";
            string foundDir = "";
            try
            {
                foreach (string d in Directory.GetDirectories(dir))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(d);
                    if (dirInfo.Name == folders[0])
                    {
                        foundDir = d;
                        string searchDir = FindFolder(d, string.Join("/", folders, 1, folders.Length - 1));
                        if (searchDir != "")
                        {
                            foundDir = searchDir;
                            break;
                        }
                    }
                }

                if (foundDir == "")
                {
                    foreach (string d in Directory.GetDirectories(dir))
                    {
                        foundDir = FindFolder(d, string.Join("/", folders));
                        if (foundDir != "") break;
                    }
                }
            }
            catch (System.Exception excpt)
            {
                Debug.LogError(excpt.Message);
                return "";
            }

            return foundDir;
        }

        public static Texture2D LoadTexture(string dreamteckPath, string textureFileName)
        {
            string path = Application.dataPath + "/Dreamteck/" + dreamteckPath;
            if (!Directory.Exists(path))
            {
                path = FindFolder(Application.dataPath, "Dreamteck/" + dreamteckPath);
                if (!Directory.Exists(path)) return null;
            }

            if (!File.Exists(path + "/" + textureFileName)) return null;
            byte[] bytes = File.ReadAllBytes(path + "/" + textureFileName);
            Texture2D result = new Texture2D(1, 1);
            result.name = textureFileName;
            result.LoadImage(bytes);
            return result;
        }

        public static Texture2D LoadTexture(string path)
        {
            if (!File.Exists(path)) return null;
            byte[] bytes = File.ReadAllBytes(path);
            Texture2D result = new Texture2D(1, 1);
            FileInfo finfo = new FileInfo(path);
            result.name = finfo.Name;
            result.LoadImage(bytes);
            return result;
        }

        public static Texture2D[] EditorLoadTextures(string dreamteckLocalPath)
        {
            Texture2D[] textures = null;
#if UNITY_EDITOR
            string path = "Assets/" + dreamteckLocalFolder + "/" + dreamteckLocalPath;


            string[] textureGUIDs = AssetDatabase.FindAssets("t:texture2d", new string[] {path});
            textures = new Texture2D[textureGUIDs.Length];
            for (int i = 0; i < textureGUIDs.Length; i++)
            {
                textures[i] = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(textureGUIDs[i]));
            }


#endif
            return textures;
        }

        public static Texture2D EditorLoadTexture(string dreamteckLocalPath, string textureName)
        {
            Texture2D texture = null;
#if UNITY_EDITOR
            string path = "Assets/" + dreamteckLocalFolder + "/" + dreamteckLocalPath;
            string[] textureGUIDs = AssetDatabase.FindAssets(textureName + " t:texture2D", new string[] {path});
            
            if (textureGUIDs.Length > 0)
            {
                texture = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(textureGUIDs[0]));
            }
#endif
            return texture;


        }
    }
}