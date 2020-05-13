using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "ReqUrl", menuName = "ReqUrl/requesturls", order = 0)]
public class CMRequestUrl : ScriptableObject {
    [Header ("UserAccount managing urls")]
    [SerializeField] string createUserURL;
    [SerializeField] string userDBDataURL;
    [SerializeField] string statusUpdateURL;
    [SerializeField] string historyUpdateURL;
    [SerializeField] string marketPageURL;
    [SerializeField] string tokenDataURL;
    [Header ("Inventory managing urls")]
    [SerializeField] string sendingItemURL;
    [SerializeField] string receivingGameItemsURL;
    [SerializeField] string receivingGameCharURL;
    [SerializeField] string deletingItemURL;
    [SerializeField] string resourcesDataURL;
    [SerializeField] string resourcesSendingURL;

    public string CreateUserURL { get { return createUserURL; } }
    public string UserDBDataURL { get { return userDBDataURL; } }
    public string StatusUpdateURL { get { return statusUpdateURL; } }
    public string HistoryUpdateURL { get { return historyUpdateURL; } }
    public string MarketPageURL { get { return marketPageURL; } }
    public string ResourcesDataURL { get { return resourcesDataURL; } }
    public string TokenDataURL { get { return tokenDataURL; } }
    public string SendingItemURL { get { return sendingItemURL; } }
    public string ReceivingGameItemsURL { get { return receivingGameItemsURL; } }
    public string ReceivingGameCharURL { get { return receivingGameCharURL; }}
    public string DeletingItemURL { get { return deletingItemURL; }}
    public string ResourcesSendingURL { get { return resourcesSendingURL; }}
}