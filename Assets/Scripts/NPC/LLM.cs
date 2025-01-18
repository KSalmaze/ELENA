using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace NPC
{
    public class LLM : MonoBehaviour
    {
        [SerializeField] private CManager cManager;
        private string _apiKey;
        
        void Start()
        {
            _apiKey = cManager.xjk;
            StartCoroutine(MakeRequest("Hello!"));
        }
        
        public IEnumerator MakeRequest(string prompt)
        {
            string jsonData = $@"{{
            ""model"": ""llama-3.1-sonar-small-128k-online"",
            ""messages"": [
                {{""role"": ""system"", ""content"": ""Be precise and concise.""}},
                {{""role"": ""user"", ""content"": ""{prompt}""}}
            ]
        }}";

            using (UnityWebRequest www = new UnityWebRequest("https://api.perplexity.ai/chat/completions", "POST"))
            {
                byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
                www.uploadHandler = new UploadHandlerRaw(bodyRaw);
                www.downloadHandler = new DownloadHandlerBuffer();
            
                www.SetRequestHeader("Content-Type", "application/json");
                www.SetRequestHeader("Authorization", $"Bearer {_apiKey}");

                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError($"Error: {www.error}");
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }
    }
}
