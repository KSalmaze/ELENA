using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

namespace NPC
{
    public class LLM : MonoBehaviour
    {
        [SerializeField] private CManager cManager;
        [SerializeField] private TMP_Text text;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private GameObject sendButton;
        private string _apiKey;
        [TextArea]public string testString;
        
        void Start()
        {
            _apiKey = cManager.xjk;
        }

        public void Request()
        {
            if (!string.IsNullOrWhiteSpace(inputField.text))
            {
                StartCoroutine(MakeRequest(inputField.text, text));
            }
        }
        
        public IEnumerator MakeRequest(string prompt, TMP_Text textField)
        {
            sendButton.SetActive(false);
            
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
                    textField.text = Filter(www.downloadHandler.text);
                }
            }
            
            sendButton.SetActive(true);
        }
        
        private string Filter(string response)
        {
            // Encontra o início do conteúdo da mensagem
            Debug.Log(response.IndexOf("\"content\":", StringComparison.Ordinal));
            int startIndex = response.IndexOf("\"content\":", StringComparison.Ordinal) + 11;
            if (startIndex == -1) return response;

            // Encontra o fim do conteúdo
            int endIndex = response.IndexOf("\"delta\":", startIndex, StringComparison.Ordinal);
            if (endIndex == -1) return response;

            // Extrai apenas o conteúdo da mensagem
            string filteredText = response.Substring(startIndex, endIndex - startIndex);
    
            // Remove caracteres de escape
            filteredText = filteredText.Replace("\\n", "\n");
            filteredText = filteredText.Replace("\\\"", "\"");
            filteredText = filteredText.Replace("\\\\", "\\");
    
            return filteredText.Substring(0, filteredText.Length - 4);
        }
    }
}
