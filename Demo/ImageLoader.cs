using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using EasyUI.Progress;


public class ImageLoader : MonoBehaviour
{
    [SerializeField]
    [TextArea]
    private string imageURL;

    [SerializeField]
    private RawImage uiRawImage;
     


    public void LoadImage()
    {
        Debug.Log("Loading Started.");
        Progress.Show("Please wait...",ProgressColor.Green, true);
        Progress.SetDetailsText("Loading image...");
        StartCoroutine(Load());
    }


    private IEnumerator Load(){
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(imageURL);
        request.SendWebRequest();

        while(!request.isDone){
            float progress = request.downloadProgress*100f;
            Debug.Log($"Loading {progress} %");
            Progress.SetProgressValue(progress);
            yield return null;
        }

        if (request.result == UnityWebRequest.Result.Success) {
            Debug.Log("Loading Completed.");
            Progress.Hide();
            uiRawImage.texture = DownloadHandlerTexture.GetContent(request);
        }
    }

}
