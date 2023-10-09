using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _TextComponent;

    string _url = "https://docs.google.com/spreadsheets/d/1rVrNGJ8ZATl9vFnmHZEY6t7cl-hkmu8z3HHhf9bD00U/export?format=csv";


    public void DownloadCSV()
    {
        StartCoroutine(DownloadAndRead());
    }

    public IEnumerator DownloadAndRead()
    {
        var uwr = new UnityWebRequest(_url);
        uwr.method = UnityWebRequest.kHttpVerbGET;
        var resultFile = Path.Combine(Application.persistentDataPath, "result.txt");
        var dh = new DownloadHandlerFile(resultFile);
        dh.removeFileOnAbort = true;
        uwr.downloadHandler = dh;
        yield return uwr.SendWebRequest();
        if (uwr.result != UnityWebRequest.Result.Success)
            Debug.Log(uwr.error);
        else
        {
            Debug.Log("Download saved to: " + resultFile);
        }
        ProcessData(resultFile);
    }

    private void ProcessData(string data)
    {
        List<Register> registers = new List<Register>();
        StreamReader reader = new StreamReader(data);
        string line = reader.ReadLine();
        line = reader.ReadLine();
        while (line != null)
        {
            registers.Add(new Register(line));

            line = reader.ReadLine();
        }

        foreach (Register reg in registers)
        {
            _TextComponent.text += reg.GetContent();
        }
    }

    public enum RegisterType { car, character, building }
    public class Register
    {
        RegisterType type;
        string comment;
        int value;
        private string line;

        public Register(string line)
        {
            this.line = line;
            (value, comment, type) = ConvertLine(line);
        }

        private (int value, string comment, RegisterType type) ConvertLine(string line)
        {
            string[] elements = line.Split(",");

            if (elements.Length > 3)
            {
                string united = elements[1];
                for (int i = 2; i < elements.Length - 2; i++)
                {
                    united += ", " + elements[i];
                }
                elements = new string[3] { elements[0], united, elements[elements.Length - 1] };
            }
            switch (elements[0])
            {
                case "car":
                    type = RegisterType.car; break;
                case "character":
                    type = RegisterType.character; break;
                case "buiding":
                    type = RegisterType.building; break;
            }
            value = int.Parse(elements[2]);
            comment = elements[1].Replace("\"", "");

            return (value, comment, type);
        }

        public string GetContent()
        {
            return type.ToString() + $" {comment} {value}\n";
        }
    }
}
