using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConsoleController : MonoBehaviour
{
    bool showConsole;
    bool showHelp;
    string input;
    Vector2 scroll;

    public List<object> commandList;
    public static ConsoleCommand<int> ADD_GOLD;
    public static ConsoleCommand HELP;

    private void Awake()
    {
        
        HELP = new ConsoleCommand("help", "Show all commands", "help", () => 
        {
            showHelp = true;
        });

        ADD_GOLD = new ConsoleCommand<int>("add_gold", "Add gold to your pocket", "add_gold <gold_amount>", (x) =>
        {
            Debug.Log(SourceSystem.Instance);
            Debug.Log(SourceSystem.Instance.Gold);
            Debug.Log(x);
            SourceSystem.Instance.Gold.updateAmount(x);
        });

        commandList = new List<object>
        {
            ADD_GOLD,
            HELP
        };
    }

    public void OnToggleDebugConsole(InputValue value)
    {
        showConsole = !showConsole;
    }

    public void OnReturn(InputValue value)
    {
        if (showConsole)
        {
            HandleInput();
            input = "";
        }
    }
    private void OnGUI()
    {

        if (!showConsole)
        {
            return;
        }
        float y = 0f;

        if (showHelp)
        {
            GUI.Box(new Rect(0, y, Screen.width, 100), "");
            Rect virwport = new Rect(0, 0, Screen.width - 30, 20 * commandList.Count);

            scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 90), scroll, virwport);
            for (int i = 0; i < commandList.Count; i++)
            {
                ConsoleCommandBase commmandBase = commandList[i] as ConsoleCommandBase;

                string label = $"{commmandBase.CommandFormat} - {commmandBase.CommandDescription}";

                Rect labelRect = new Rect(5, 20 * i, virwport.width - 100, 20);

                GUI.Label(labelRect, label);
            }
            GUI.EndScrollView();

            y += 100;
        }
        GUI.Box(new Rect(0, y, Screen.width, 30), "");
        GUI.backgroundColor = new Color(0, 0, 0, 0);
        input = GUI.TextField(new Rect(10f, y + 5f, Screen.width - 20f, 20f), input);
    }

    private void HandleInput()
    {
        string[] prop = input.Split(' ');
        for (int i = 0; i < commandList.Count; i++)
        {
            ConsoleCommandBase commandBase = commandList[i] as ConsoleCommandBase;
            if (input.Contains(commandBase.CommandId))
            {
                if (commandList[i] as ConsoleCommand != null)
                {
                    (commandList[i] as ConsoleCommand).Invoke();
                }
                else if (commandList[i] as ConsoleCommand<int> != null)
                {
                    (commandList[i] as ConsoleCommand<int>).Invoke(int.Parse(prop[1]));
                }
            }

        }
    }

}
