using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace UBlockly.UGUI
{
    public class NetworkController : MonoBehaviour
    {
        [SerializeField] private string host = "http://127.0.0.1";
        [SerializeField] private string path_login = "/users/login";
        [SerializeField] private string path_submit = "/api/submit";
        [SerializeField] private string path_get = "/api/data";
        [SerializeField] private string path_logout = "/api/logout";
        [SerializeField] private string port = "3000";
        [SerializeField] private string token;


        public void GetToken(string token)
        {
            Debug.Log("Token Received "+ token);
            this.token = token;
        }

        public void SendApi()
        {
            Debug.Log("SentAPI");
            StartCoroutine(submitRoutine());
        }


        private IEnumerator submitRoutine()
        {
            var url = host + ":" + port + path_submit;
            string message = "{\"timeTaken\": \"" + 1234 + "\",\"code\":\" CODE \",\"totalTimeSpent\":\"" + 123456 + ",\"stars\": \"3\"}";
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
            if (www.responseCode == 200)
            {
                Debug.Log("Success Post");
            }
            else
            {
                Debug.LogError("Error API "+ www.responseCode);
            }
        }
    }
}