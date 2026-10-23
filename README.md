# IMightBeInDebt's Resources
General modding resources for my released and upcoming mods

If you would like a guide on how to make simple web requests
to your own repository, go to the [usages]([https://github.com/IMightBeInDebt/IMightBeInDebtsResources/blob/main/README.md#Usages](https://github.com/IMightBeInDebt/IMightBeInDebtsResources/tree/main#usages)) section

---

# Usages

- Create a folder in your hierarchy named `WebRequestLoaders`
- Create a file inside of `WebRequestLoaders` named `ImageLoader.cs`
- Copy the code from this repository and paste it into the file
- Create another file inside of `WebRequestLoaders` and name it `AudioLoader.cs`
- Edit both of the file's GitHubUser, Repository, Branch, and FolderPath to your liking 

From there, call the methods by using a coroutine or something else! Here's an example for a image:

```
using LoadImage;
using UnityEngine;
using UnityEngine.UI;

public class ImageExample : MonoBehaviour
{
[SerializeField] private RawImage myRawImage;

    private void Start()
    {
        StartCoroutine(ImageLoader.LoadImage("CoolImageIFound.png", texture =>
        {
            if (texture != null)
                myRawImage.texture = texture;
        }));
    }
}
```

And if you want to call a sound:

```
using UnityEngine;

public class SoundExample : MonoBehaviour
{
[SerializeField] private AudioSource audioSource;

    private void Start()
    {
        StartCoroutine(SoundLoader.LoadSound("CoolSoundIAlsoFound.mp3", clip =>
        {
            if (clip == null) return;

            audioSource.clip = clip;
            audioSource.Play();
        }));
    }
}
```

---
> This product is not affiliated with Gorilla Tag or Another Axiom LLC and is not endorsed or otherwise sponsored by Another Axiom LLC. Portions of the materials contained herein are property of Another Axiom LLC. © 2024 Another Axiom LLC.

> more coming soon... 
