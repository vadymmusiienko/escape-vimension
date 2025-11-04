using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notebook : MonoBehaviour
{

    private struct Command
    {
        public string description;
        public bool found;
    }

    [SerializeField] private UnityEngine.UI.Button notebookButton;
    [SerializeField] private TextMeshProUGUI commandText;

    private static Dictionary<string, Command> commands = new Dictionary<string, Command>()
    {
        {"h", new Command { description = "Left", found = false}},
        {"j", new Command { description = "Down", found = false}},
        {"k", new Command { description = "Up", found = false}},
        {"l", new Command { description = "Right", found = false}},
        {"x", new Command { description = "Cut", found = false}},
        {"1-5", new Command { description = "Multiply (Dash)", found = false}},
        {"d", new Command { description = "Delete", found = false}},
        // {"y", new Command { description = "Paste", found = false}},
        // {"/", new Command { description = "Search", found = false}},
        // {"gg", new Command { description = "Go to beginning", found = false}},
        {":q", new Command { description = "Quit", found = false}},

    };

    private CanvasGroup canvasGroup;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        notebookButton.onClick.AddListener(notebookClicked);
    }

    void Update()
    {
        // toggle notebook if escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            notebookClicked();
        }
    }

    // add a command in the notebook
    public static void findCommand(string commandKey)
    {
        if (commands.ContainsKey(commandKey))
        {
            Command cmd = commands[commandKey];
            if (!cmd.found)
            {
                cmd.found = true;
                commands[commandKey] = cmd;   
            }

        }
    }

    void notebookClicked()
    {
        // hide notebook if already open
        if (canvasGroup.alpha == 1)
        {
            canvasGroup.alpha = 0;
            return;
        }

        // show notebook
        canvasGroup.alpha = 1;

        // update text
        commandText.text = "";
        foreach (String key in commands.Keys)
        {
            Command cmd = commands[key];
            if (cmd.found)
            {
                commandText.text += $"{key} - {cmd.description}\n";
            }
        }
    }
}

