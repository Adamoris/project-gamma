using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Mirror;
using System;

public class ChatBehavior : NetworkBehaviour
{

    [SerializeField] private GameObject chatUI = null;
    [SerializeField] private GameObject chatText = null;
    [SerializeField] private GameObject inputField = null;
    private bool canChat;
    private Coroutine textLife;

    private static event Action<string> OnMessage;

    public override void OnStartAuthority()
    {
        canChat = false;
        OnMessage += HandleNewMessage;
    }

    IEnumerator TextLife(float time)
    {
        yield return new WaitForSeconds(time);

        chatText.GetComponent<TMP_Text>().text = "";
        chatText.SetActive(false);
    }

    private void Update()
    {
        if (isLocalPlayer && !canChat && Input.GetKeyDown(KeyCode.Return))
        {
            //if (textLife != null)
            //{
            //    StopCoroutine(textLife);
            //}
            
            chatText.SetActive(true);
            inputField.SetActive(true);
            inputField.GetComponent<TMP_InputField>().ActivateInputField();
            canChat = true;
        }
        else if (isLocalPlayer && canChat)
        {
            inputField.GetComponent<TMP_InputField>().ActivateInputField();
        }
    }

    [ClientCallback]

    private void OnDestroy()
    {
        if (!hasAuthority) { return; }

        OnMessage -= HandleNewMessage;
    }

    private void HandleNewMessage(string message)
    {
        chatText.GetComponent<TMP_Text>().text += message;
    }

    [Client]
    public void Send(string message)
    {
        if (!Input.GetKeyDown(KeyCode.Return)) { return; }

        if (string.IsNullOrWhiteSpace(message)) { return; }

        CmdSendMessage(message);

        inputField.GetComponent<TMP_InputField>().text = string.Empty;
        
        inputField.SetActive(false);
        IEnumerator ExecuteAfterTime(float time)
        {
            yield return new WaitForSeconds(time);

            canChat = false;
        }
        StartCoroutine(ExecuteAfterTime(0.1f));
        //textLife = StartCoroutine(TextLife(5));
    }

    [Command]
    private void CmdSendMessage(string message)
    {
        //insert validation here:

        //Send
        RpcHandleMessage($"[{connectionToClient.connectionId}]: {message}");
    }

    [ClientRpc]
    private void RpcHandleMessage(string message)
    {
        //chatText.SetActive(true);
        OnMessage?.Invoke($"\n{message}");
    }
}
