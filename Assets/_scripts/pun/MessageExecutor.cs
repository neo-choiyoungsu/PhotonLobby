using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageExecutor
{
    public MessageExecutor()
    {
        MessageSender.OnMessageSent = OnMessageSent;
    }
    
    public void OnMessageReceived(Message message)
    {
        switch (message.Type)
        {
        }
    }
    
    public void OnMessageSent(Message message)
    {
        switch (message.Type)
        {
        }
    }
}
