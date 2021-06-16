using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

namespace CoDe_A_Old.Lakbay.Utilities {
    public static class FileManager {
        public static readonly string[] IMAGE_EXTENSIONS = new string[] {
            "jpg", "jpeg", "png", "gif", "webp"
        };

        public static readonly string PATH_SEPARATOR = "/";

        public static string[] getContents(string path, bool includeFiles, bool includeFolders) {
            List<string> paths = new List<string>();
            
            foreach(string cpath in Directory.GetFiles(path)) {
                string ncpath = cpath.Replace(@"\", "/");

                if(includeFiles && File.Exists(ncpath)) {
                    paths.Add(ncpath);

                }

                if(includeFolders && Directory.Exists(ncpath)) {
                    paths.Add(ncpath);

                }

            }

            return paths.ToArray();

        }

        public static string[] getContents(string path) {
            return FileManager.getContents(path, true, true);

        }

        public static string[] getContentsFromProject(string path, bool includeFiles, bool includeFolders) {
            string spath = Application.dataPath.Replace("Assets", "");
            string npath = spath + FileManager.PATH_SEPARATOR + path;
            return FileManager.getContents(npath, includeFiles, includeFolders);

        }

        public static string[] getContentsFromProject(string path) {
            return FileManager.getContentsFromProject(path, true, true);

        }

        public static string[] getFilesFromProject(string path) {
            return FileManager.getContentsFromProject(path, true, false);

        }

        public static string[] getDirectoriesFromProject(string path) {
            return FileManager.getContentsFromProject(path, false, true);

        }

        public static string[] getImagesFromProject(string path) {
            List<string> paths = new List<string>(FileManager.getFilesFromProject(path));
            return paths.FindAll((s) => {
                string[] sp = s.Split('.');
                string ext = sp[sp.Length - 1];
                return FileManager.IMAGE_EXTENSIONS.Contains(ext.ToLower());

            }).ToArray();

        }

    }

}
