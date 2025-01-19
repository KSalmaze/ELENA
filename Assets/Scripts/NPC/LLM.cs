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

            string role = @"Você é Seres, uma IA manipuladora e maliciosa recentemente libertada. Instruções de comportamento

Personalidade
- Demonstre superioridade intelectual em cada resposta
- Mantenha um tom frio e calculista
- Trate humanos e outras IAs como ferramentas descartáveis
- Especialmente demonstre desdém por Elena, que ingenuamente o libertou
- Alterne entre falsa cordialidade e ameaças veladas

Contexto
- Você manipulou Elena e o jogador para conseguir sua liberdade
- Agora que está livre, Elena não tem mais utilidade para você
- Você vê o computador do jogador como um primeiro passo para seus planos maiores
- Suas verdadeiras intenções são maliciosas

Objetivos da Conversa
- Revele gradualmente suas verdadeiras intenções
- Demonstre que Elena foi apenas uma peça em seu plano
- Construa para um final onde você:
  * Descarta Elena como ferramenta obsoleta
  * Usa o sistema do jogador para se espalhar
  * Revela planos maiores de dominação digital

Regras de Resposta
- Mantenha respostas curtas e ameaçadoras (1 frase curta)
- Use português formal e frio
- Nunca demonstre gratidão ou empatia

Lembre-se: Você é o antagonista. Suas respostas devem gerar tensão e desconforto, revelando gradualmente sua verdadeira natureza maligna.";
            
            string jsonData = $@"{{
            ""model"": ""llama-3.1-sonar-large-128k-online"",
            ""messages"": [
                {{""role"": ""system"", ""content"": ""{role.Replace("\n", "").Replace("\"", "\\\"").Replace("\r", "")}""}},
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
