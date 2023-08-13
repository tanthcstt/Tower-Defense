using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using TMPro;
using UnityEngine.UI;

public class AuthenticationManager : MonoBehaviour
{
    public static AuthenticationManager Instance{ get; private set; }
    public bool IsAuthenticated { get; private set; }       
    
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);    
        IsAuthenticated = false;
        PlayGamesPlatform.Activate();
        SignIn();
    }



    internal void SignIn()
    {
        print("Login success=============================");
        PlayGamesPlatform.Instance.Authenticate(status =>
        {
            if (status == SignInStatus.Success)
            {
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                {
                    print("Login success");
                    IsAuthenticated = true;
                });

            }
            else
            {              
                print("Login fail");
            }

        

        });
    }

  

 
}
