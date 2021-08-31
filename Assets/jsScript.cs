using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class jsScript : MonoBehaviour
{
    public InputField createUsernameIF;
    public InputField createPasswordIF;

    public Button createButton;
    public Button siButton;

    public InputField siUsernameIF;
    public InputField siPasswordIF;
    public InputField siScoreIF;

    public List<UserData> hsList;
    private void Awake()
    {
       
    }
    public void HSButton() 
    {
        StartCoroutine(SendGetRequestHS("https://jsdbvahiha.herokuapp.com/GetAll"));
    }
    IEnumerator SendGetRequestHS(string url )
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url );
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            print(webRequest.error);
        }
        else
        {
            print("web req sent");
          //  print(webRequest.downloadHandler.text);
            UsersData usersData = JsonUtility.FromJson<UsersData>(webRequest.downloadHandler.text);

            if (usersData.usersData.Length == 0)
            {
                print("null found");
            }
            else
            {
                hsList.Clear();
                for (int i = 0; i < usersData.usersData.Length; i++)
                {
                    hsList.Add(usersData.usersData[i]);
                        
                    
                 
                   
                   // print(usersData.usersData[i].name);
                }
                hsList.Sort((x, y) => x.score.CompareTo(y.score));
                hsList.Reverse();
                //print(usersData.usersData[0].password);
                //print(usersData.usersData[0].score);
            }

        }
    }
    public void SIButton()
    {
        UserData ud = new UserData()
        {
            name = siUsernameIF.text,
            password = siPasswordIF.text,
            score = int.Parse(siScoreIF.text)

        };
        StartCoroutine(SICheckIfUserExists("https://jsdbvahiha.herokuapp.com/CheckIfUserExists", ud.GetNameInURLEncoding(), ud));
    }
    IEnumerator SICheckIfUserExists(string url, string urlEncodedData, UserData udForPost)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url + urlEncodedData);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            print(webRequest.error);
        }
        else
        {
            print("web req sent");

            UsersData usersData = JsonUtility.FromJson<UsersData>(webRequest.downloadHandler.text);

            if (usersData.usersData.Length == 0)
            {
                print("user with name doesnt exist, or password is incorrect");
              
            }
            else
            {
                StartCoroutine(SendGetRequest("https://jsdbvahiha.herokuapp.com/Update", udForPost.GetNameInURLEncoding()));

                print("user updated");
            }

        }
    }
    public void CreateUserButton()
    {
        if (createUsernameIF.text.Length > 3 && createPasswordIF.text.Length > 3)
        {

            UserData ud = new UserData()
            {
                name = createUsernameIF.text,
                password = createPasswordIF.text,


                score = 0
            };

            StartCoroutine(CheckIfNameExists("https://jsdbvahiha.herokuapp.com/CheckIfNameExists", ud.GetNameInURLEncoding(), ud));
        }
        else
        {
            print("too small name or password, min 3 char");
        }

    }
    IEnumerator CheckIfNameExists(string url, string urlEncodedData, UserData udForPost)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url + urlEncodedData);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            print(webRequest.error);
        }
        else
        {
            print("web req sent");

            UsersData usersData = JsonUtility.FromJson<UsersData>(webRequest.downloadHandler.text);

            if (usersData.usersData.Length == 0)
            {

                StartCoroutine(SendDataPost("https://jsdbvahiha.herokuapp.com/createUser", udForPost));
                print("user created");
            }
            else
            {
                print("name already exists, change name please");
            }

        }
    }
    [System.Serializable]
    public struct UserData
    {
        public string name;
        public string password;


        public int score;

        //public string GetDataInURLEncoding() 
        //{
        //    return $"?name={name}&age={age}&score{score}";
        //}
        public string GetNameInURLEncoding()
        {
            return $"?name={name}&password={password}&score={score}";
        }
    }
    [System.Serializable]
    public struct UsersData
    {
        public UserData[] usersData;
    }
    UserData userData = new UserData()
    {
        name = "aaaa",
        password = "were",


        score = 222
    };
    UserData userNameData = new UserData()
    {
        name = "papa",
        password = "papa",
        score = 55
    };
    void Start()
    {
        // StartCoroutine(SendDataPost("https://jsdbvahiha.herokuapp.com/createUser", userData));
        //StartCoroutine(SendGetRequest("https://jsdbvahiha.herokuapp.com/Update", userNameData.GetNameInURLEncoding()));
        hsList = new List<UserData>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SendDataPost(string url, UserData userData)
    {
        string jsonData = JsonUtility.ToJson(userData);
        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(jsonData));

            yield return www.SendWebRequest();
        }
    }
    IEnumerator SendGetRequest(string url, string urlEncodedData)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(url + urlEncodedData);
        yield return webRequest.SendWebRequest();

        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            print(webRequest.error);
        }
        else
        {
            print("web req sent");
            //print(webRequest.downloadHandler.text);
            UsersData usersData = JsonUtility.FromJson<UsersData>(webRequest.downloadHandler.text);

            //if (usersData.usersData.Length == 0)
            //{
            //    print("null found");
            //}
            //else
            //{
            //    print(usersData.usersData[0].password);
            //    print(usersData.usersData[0].score);
            //}

        }
    }

}
