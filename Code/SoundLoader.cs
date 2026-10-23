using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace LoadSound
{
    public static class SoundLoader
    {
        public static string GitHubUser = "";
        public static string Repository = "";
        public static string Branch = "main";
        public static string FolderPath = "";
        
        public static IEnumerator LoadSound(string fileName, Action<AudioClip> onLoaded)
        {
            string url = BuildUrl(fileName);
            AudioClip clip = null;

            using (UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, GetAudioType(url)))
            {
                ((DownloadHandlerAudioClip)request.downloadHandler).streamAudio = false;
                yield return request.SendWebRequest();
                if (request.result == UnityWebRequest.Result.Success)
                    clip = DownloadHandlerAudioClip.GetContent(request);
                else
                    Debug.LogWarning($"[SoundLoader] Audio download failed: {request.error}");
            }

            onLoaded?.Invoke(clip);
        }

        private static AudioType GetAudioType(string url)
        {
            string lower = url.ToLowerInvariant();
            if (lower.Contains(".wav")) return AudioType.WAV;
            if (lower.Contains(".ogg")) return AudioType.OGGVORBIS;
            return AudioType.MPEG;
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


