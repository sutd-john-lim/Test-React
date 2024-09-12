using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class NetworkController : MonoBehaviour
{
    [SerializeField] private string path = "http://127.0.0.1/hohoho";
    [SerializeField] private string token = "token";
    [SerializeField] private Text tokenText;
    [SerializeField] private Text pathText;
    [SerializeField] private Text responseText;
    public string message;

    private string defaultMessage = "{\"name\":\"wefaa5\",\"timespent\":900}";

    string urlHardCoded = "https://us-central1-savemytaxesindia.cloudfunctions.net/addDataToFirestore";


    void Start()
    {
        SetMessage(defaultMessage);
        SetPath(path);
    }

    public void SetToken(string token)
    {
        Debug.Log("Token Received " + token);
        this.token = token;
        tokenText.text = "Token: " + this.token;
    }

    public void SetMessage(string message)
    {
        this.message = message;
        Debug.Log("Message Received "+ this.message);
        tokenText.text = "Message: " + this.message;
    }

    public void SendApi()
    {
        Debug.Log("SentAPI " + path + " token " + token);
        this.path = urlHardCoded;
        pathText.text = "URL: " + path;
        StartCoroutine(submitRoutine());
    }

    public void SetPath(string val)
    {
        path = val;
        pathText.text = "URL: " + path;
    }


    private IEnumerator submitRoutine()
    {
        var url = path;
        var www = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Authorization", "Bearer " + token);
        UploadHandler uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(message));
        uploadHandler.contentType = "application/json";
        www.uploadHandler = uploadHandler;
        var response = www.SendWebRequest();
        while (!response.isDone)
        {
            yield return null;
        }
        responseText.text = "Response: " + www.responseCode;
        if (www.responseCode == 200)
        {
            Debug.Log("Success Post");
        }
        else
        {
            Debug.LogError("Error API " + www.responseCode);
        }
    }
}
