using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace LoadImage
{
    public static class ImageLoader
    {
        public static string GitHubUser = "";
        public static string Repository = "";
        public static string Branch = "main";
        public static string FolderPath = "";

        public static IEnumerator LoadImage(string fileName, Action<Texture2D> onLoaded)
        {
            string url = BuildUrl(fileName);
            Texture2D image = null;

            using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(url))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                    image = DownloadHandlerTexture.GetContent(request);
                else
                    Debug.LogWarning($"[ImageLoader] Image download failed: {request.error} / {request.responseCode}");
            }

            onLoaded?.Invoke(image);
        }

        private static string BuildUrl(string fileName)
        {
            string path = string.Join("/", new[] { FolderPath, fileName }
                .Where(part => !string.IsNullOrWhiteSpace(part)));
            path = path.Trim('/').Replace("\\", "/");
            string encodedPath = string.Join("/", path.Split('/').Select(Uri.EscapeDataString));
            return $"https://raw.githubusercontent.com/{Uri.EscapeDataString(GitHubUser)}/{Uri.EscapeDataString(Repository)}/{Uri.EscapeDataString(Branch)}/{encodedPath}";
        }
    }
}


