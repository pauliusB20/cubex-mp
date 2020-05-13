using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine.Networking;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class NetworkUserRegistrationManager : MonoBehaviour
{
    //NOTE:
    /*
    Pridėti vartotojo įvestų duomenų patikrinimo sistema
    Timeout sistema, jeigu nepavyksta prisijungti per tam tikra laiką prie serverio 
    Mesti klaida
    NOTE: Pagalvoti dėl NEM
    */
    [Header("Network configuration")]
    [SerializeField] string userRegUrl;
    [SerializeField] CMRequestUrl cmurl;
    [Header("Main configuration paramters")]
    [SerializeField] InputField userNameTextBox;
    [SerializeField] InputField passwordTextBox;
    [SerializeField] InputField emailTextBox;
    [SerializeField] string status;
    [SerializeField] bool isActive;  
    [Header("GUI configuration")]
    [SerializeField] GameObject statusDialogWindow;
    [SerializeField] GameObject regForm;
    [SerializeField] GameObject failedWindow;
    [Header("Validation requirements")]
    [SerializeField] int minPassLength = 6;
    //Cached references
    private string publicDeviceIP;
    private string regDate;
    private bool isUserRegistered;
    private bool isDataSent;
    private bool isStartingResourceAmountUploaded;
    private bool isUserRegistration;

    private void Start() {
        userRegUrl = cmurl.CreateUserURL;
        isUserRegistered = false;
        isDataSent = false;
        isStartingResourceAmountUploaded = false;
        isUserRegistration = false;
    }
    private void Update() {
        //Checking if user is registrated. If registered output a success board
        ManageUserAccount();
    }
    public void ManageUserAccount()
    {
        if ((isDataSent && isUserRegistered) && (isStartingResourceAmountUploaded && isUserRegistration))
        {
            var active = isDataSent && isUserRegistered;            

            //Sets the values to false;
            isDataSent = !isDataSent;
            isUserRegistered = !isUserRegistered;
            isStartingResourceAmountUploaded = !isStartingResourceAmountUploaded;
        }
    }
    public void createUser()
    {
        isUserRegistration = !isUserRegistration;
        //Generating registration date and gathering informatino about the public device IP address
        regDate = generateDate();
        // Debug.Log("Current user's IP: " + LocalIPAddress());

        if (regDate == null){
            regForm.SetActive(false);
            failedWindow.SetActive(true);
        }
        else{
            if (accountDetailsValid(userNameTextBox.text, passwordTextBox.text, emailTextBox.text))
            {
                addUserToWebserver(
                                    userNameTextBox.text, 
                                    passwordTextBox.text,
                                    emailTextBox.text, 
                                    regDate
                                   );
            }
            else
            {
                regForm.SetActive(false);
                failedWindow.SetActive(true);
            }
        }
    }
    //Date generation method
    public string generateDate()
    {
      return DateTime.Now.ToString("g");
    }
    public void addUserToWebserver(string username, string pass, string email, string regDate)
    {  
        StartCoroutine(RegisterUserInWebserver(username, pass, email, regDate));
        // uploadResToWebserver();
        isDataSent = !isDataSent;
    }
   
    private bool accountDetailsValid(string nickname, string password, string email)
    {
        // NOTE: Later think about the nickname
        if (email.Contains('@') && email.Contains('.'))
        {
            if ((password.Length >= minPassLength && password.Any(char.IsDigit)) &&
                (password.Any(char.IsUpper) && password.Any(char.IsLower)))
            {
                return true;
            }
            else{
                return false;
            }
        }
        else
        {
            return false;
        }
    }
    IEnumerator RegisterUserInWebserver(
        string username, string pass, string email,  string regDate)
    {
        isActive = !isActive;      
        WWWForm form = new WWWForm();
		form.AddField("nickname", username);
		form.AddField("password", pass);
		form.AddField("email", email);
        form.AddField("reg_date", regDate);
       
        
        using (UnityWebRequest www = UnityWebRequest.Post(userRegUrl, form))
        {
            
            //www.SetRequestHeader ("cookie", "csrftoken=" + csrfCookie);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
                regForm.SetActive(false);
                failedWindow.SetActive(true);
            }
            else
            {                                  
                isUserRegistered = !isUserRegistered;        
                regForm.SetActive(!isUserRegistered);
                statusDialogWindow.SetActive(isUserRegistered);  //Activating register success window          
               
            }
             Debug.Log(
                 isUserRegistered ? 
                    "User registered successfully" : 
                    "ERROR! User not registered or something wrong with the server."
            
            );  
            // var getPageData = UnityWebRequest.Get(currentWebURL); 
            // Debug.Log(getPageData.downloadHandler.text);
        }
    }
    
    // public string LocalIPAddress()
    //      {
    //          IPHostEntry host;
    //          string localIP = "0.0.0.0";
    //          host = Dns.GetHostEntry(Dns.GetHostName());
    //          foreach (IPAddress ip in host.AddressList)
    //          {
    //              if (ip.AddressFamily == AddressFamily.InterNetwork)
    //              {
    //                  localIP = ip.ToString();
    //                  break;
    //              }
    //          }
    //          return localIP;
    //      }
}
