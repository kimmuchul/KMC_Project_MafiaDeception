using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OpenAI;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using System.IO;
using NoF;
using System.Threading.Tasks;
using System;
//using DesignPatterns.Singleton;

namespace NoF
{
public class GPTBasicController : Singleton<GPTBasicController>
{
    private OpenAIApi openAI = new OpenAIApi("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");
    public int like_value = 0;

    public string npcname;
    public string personality;
    public string restrictions;
    public string knowledge;

    public void SetChatAISystem(string name, string personality, string restrictions, string knowledge)
    {
            this.npcname = name;
            this.personality = personality;
            this.restrictions = restrictions;
            this.knowledge = knowledge;

    }
    public async Task<string>  SendMessageToGPTAndGetAnswer(string  message)
    {
        CreateChatCompletionRequest request = new CreateChatCompletionRequest();
        List<ChatMessage> messages = new List<ChatMessage>();
        messages.Add(AddMessage("system", $"name : {npcname}"));
        messages.Add(AddMessage("system", $"personality :{personality}"));
        messages.Add(AddMessage("system", $"restrictions : {restrictions}"));
        messages.Add(AddMessage("system", $"knowledge : {knowledge}"));
        messages.Add(AddMessage("user", message));
        
        request.Messages = messages;
            request.Model = "gpt-4o";
            //request.Model = "gpt-4o-mini";
        var response = await openAI.CreateChatCompletion(request);

        if(response.Choices != null&& response.Choices.Count > 0)
        {
            var chatResponse = response.Choices[0].Message;
            Debug.Log(chatResponse.Content);
            return  chatResponse.Content;
           
        }
            return "응답을 받지 못했습니다.";
    }
    private ChatMessage AddMessage(string role, string content)
    {
        ChatMessage message = new ChatMessage();
        message.Role = role;
        message.Content = content;
        return message;
    }

    }
    

}