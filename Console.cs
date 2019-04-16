using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine;
using Lean.Touch;
using SO;

public class Console : MonoBehaviour {

    public Seed currentSeed;
    public InputField ConsoleInputField;
    public StringVariable Platform;
    public BoolVariable VisualizeMapGenerate;
    public FloatVariable VisualizeDelay;
    public GameEvent clearAndGenerateMap;
    public GameEvent updateInventory;
    public GameEvent dailyRewards;
    public GameEvent helpTip;
    public SceneChange changeScene;
    public IntArrayList inentoryItemsId;
    public GameEvent newLevel;
    public BoolVariable consolOpen;
    public ItemsLibrary itemsLibrary;
    public PlayerCurrentParametersLibrary parametersLibrary;
    public Text ConsoleOutput;
    public List<int> seedList = new List<int>();

    private List<string> consoleCommands = new List<string> { "!help", "!clear", "!save_seed", "!show_seeds", "!clear_seeds", "!load_saved_seed", "!load_seed",
        "!visualize_generate", "!visualize_delay","!health", "!mana", "!food", "!gold", "!crystals", "!moves" , "!level" , "!experience", "!items_index",
        "!add_item", "!delete_all_items", "!change_scene", "!rewards", "!help_tip"};

    private List<string> commandsDescription = new List<string> { "Показать существующие команды", "Очистить консоль", "Сохранить текущий сид", "Показать сохраненные сиды",
        "Очистить сохраненные сиды", "Загрузить сид из сохраненных, \n(индекс сохраненного сида)", "Загрузить свой сид", "Визуализировать генерацию карты, \n(Переключатель Вкл/Выкл)",
        "Задержка для визуализации \n(0.5 - пол секунды)", "Изменить количество здоровья +-int", "Изменить количество маны +-int", "Изменить количество пищи +-int",
        "Изменить количество золота +-int", "Изменить количество кристаллов +-int", "Изменить количество ходов +-int", "Изменить уровень +-int",
        "Изменить текущее количества опыта  +-int", "Показать индексы всех игровых вещей", "Добавить вещь в инвентарь int (индекс)", "Удалить все вещи из инвентаря",
        "Сменить сцену string(Town, Map, Battle)", "Вызвать панель ежедневных наград", "Вызвать панель подсказок"};

    private List<string> lastCommands = new List<string>();
    private string outputText;
    private string commandParameter;
    private int lastCommandsIndex;
    private bool firstKeyCheck;
    public LayerMask LayerMask;
    private bool consoleTouch;
    private string ConsoleInput;
    //public enum consoleCommands { clear }


    protected virtual void OnEnable()
    {
        LeanTouch.OnFingerSwipe += OnFingerSwipe;
        LeanTouch.OnFingerDown += OnFingerDown;
    }

    protected virtual void OnDisable()
    {
        LeanTouch.OnFingerSwipe -= OnFingerSwipe;
        LeanTouch.OnFingerDown -= OnFingerDown;
    }

    public void OnFingerDown(LeanFinger finger)
    {
        var ray = finger.GetWorldPosition(1.0f);
        var hit = Physics2D.OverlapPoint(ray, LayerMask);
        consoleTouch = false;

        if (hit != null)
        {
            if (hit.gameObject.tag == "Console")
            {
                consoleTouch = true;
            }
        }
        else
        {
        }
    }

    public void OnFingerSwipe(LeanFinger finger)
    {
        if (consoleTouch && lastCommands.Count != 0)
        {
            var swipe = finger.SwipeScreenDelta;

            if (swipe.y > Mathf.Abs(swipe.x))
            {
                LastCommands("up");
            }
            else if (swipe.y < -Mathf.Abs(swipe.x))
            {
                LastCommands("down");
            }
        }
    }

    void Start()
    {
        seedList.AddRange(PlayerPrefsX.GetIntArray("SavedSeeds"));
    }

    void Update()
    {
        if (Platform.value != "Android" && Platform.value != "IOS")
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && ConsoleInputField.isFocused && lastCommands.Count != 0)
            {
                LastCommands("up");
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) && ConsoleInputField.isFocused && lastCommands.Count != 0)
            {
                LastCommands("down");
            }
        }
    }

    public void ConsoleCommandCheck()
    {

        ConsoleInput = ConsoleInputField.text;

        string[] wordsArray = ConsoleInput.Split(new char[] { ' ' }, System.StringSplitOptions.RemoveEmptyEntries);

        List<string> words = new List<string>();

        words.AddRange(wordsArray);

        int intCheck;

        float floatCheck;

        if (lastCommands.Count != 0)
        {
            if (ConsoleInput != "" && words.Count != 0 && lastCommands[lastCommands.Count - 1] != ConsoleInput)
            {
                lastCommands.Add(ConsoleInput);
            }
        }
        else
        {
            if (ConsoleInput != "" && words.Count != 0)
            {
                lastCommands.Add(ConsoleInput);
            }
        }

        if (words.Count == 2)
        {
            ConsoleInput = words[0];

            commandParameter = words[1];
        }

        ConsoleTextOutput();

        switch (ConsoleInput)
        {
            case "!help":
                outputText = "All commands \n------------------------------------------------------------------- \n(Параметры пишутся после команды с пробелом) \n(!load_seed int = !load_seed 5) \n\n(Свайпами вниз-вверх  по консоле \n(или стрелочками на компьютере), можно \nвернуться к предыдущим введенным командам) \n\n";

                int index = 0;
                foreach (var item in consoleCommands)
                {
                    if (item == "!load_saved_seed" || item == "!load_seed" || item == "!Health" || item == "!Mana" || item == "!Food" || item == "!Gold" || item == "!Crystals" || item == "!Moves" )
                    {
                        outputText += string.Format("{0} int - {1} \n\n", item, commandsDescription[index]);
                    }
                    else if (item == "!visualize_delay")
                    {
                        outputText += string.Format("{0} float - {1} \n\n", item, commandsDescription[index]);
                    }
                    else
                    {
                        outputText += string.Format("{0} - {1} \n\n", item, commandsDescription[index]);
                    }
                    index++;
                }
                break;

            case "!clear":
                lastCommands.Clear();
                ConsoleOutput.text = "";
                break;

            case "!save_seed":
                if (seedList.Contains(currentSeed.value))
                {
                    outputText = "Текущий сид уже сохранен";
                }
                else
                { 
                        seedList.Add(currentSeed.value);
                        PlayerPrefsX.SetIntArray("SavedSeeds", seedList.ToArray());
                        outputText = "Сид сохранен";
                }
                break;

            case "!show_seeds":
                seedList.Clear();
                seedList.AddRange(PlayerPrefsX.GetIntArray("SavedSeeds"));
                if (seedList.Count == 0)
                {
                    outputText = "Нет сохраненных сидов";
                }
                else
                {
                    outputText = "All saved seeds \n------------------------------------------------------------------- \n";
                    int i = 0;
                    foreach (var item in seedList)
                    {
                        i++;
                        outputText += (i + ")  " + item + "\n").ToString();
                    }
                }
                break;

            case "!clear_seeds":
                seedList.Clear();
                PlayerPrefsX.SetIntArray("SavedSeeds", seedList.ToArray());
                outputText = "Сохраненные сиды удалены";
                break;

            case "!load_saved_seed":
                if (int.TryParse(commandParameter, out intCheck))
                {
                    if (int.Parse(commandParameter)-1 >= 0 && int.Parse(commandParameter) <= seedList.Count)
                    {
                        Debug.Log(int.Parse(commandParameter) + "   " + seedList.Count);
                        currentSeed.value = seedList[int.Parse(commandParameter)-1];
                        outputText = string.Format("Сид {0} загружен", seedList[int.Parse(commandParameter)-1]);
                        clearAndGenerateMap.Raise();
                    }
                    else
                    {
                        outputText = "Нет такого элемента среди сохраненных сидов";
                    }
                }
                else
                {
                    outputText = "Введите целое число";
                }
                break;

            case "!load_seed":
                if (int.TryParse(commandParameter, out intCheck))
                {
                    currentSeed.value = int.Parse(commandParameter);
                    outputText = string.Format("Сид {0} загружен", commandParameter);
                    clearAndGenerateMap.Raise();
                }
                else
                {
                    outputText = "Введите целое число";
                }
                break;

            case "!visualize_generate":
                if (VisualizeMapGenerate.value == true)
                {
                    VisualizeMapGenerate.value = false;
                    outputText = "Визуализация генерации карты выключена";
                }
                else if (VisualizeMapGenerate.value == false)
                {
                    VisualizeMapGenerate.value = true;
                    outputText = "Визуализация генерации карты включена";
                }
                break;

            case "!visualize_delay":
                if (float.TryParse(commandParameter, out floatCheck))
                {
                    VisualizeDelay.value = float.Parse(commandParameter);
                    outputText = string.Format("Задержка в {0} секунды установлена", commandParameter);
                }
                else
                {
                    outputText = "Введите число с плавающей точкой";
                }
                break;

            case "!health":
                if (int.TryParse(commandParameter, out intCheck))
                {
                    parametersLibrary.PlayerCurrentParametersList[0].ParameterValue += int.Parse(commandParameter);
                    outputText = "Здоровье изменено на " + commandParameter;
                }
                else
                {
                    outputText = "Введите целое число";
                }
                break;

            case "!mana":
                if (int.TryParse(commandParameter, out intCheck))
                {
                    parametersLibrary.PlayerCurrentParametersList[1].ParameterValue += int.Parse(commandParameter);
                    outputText = "Мана изменена на " + commandParameter;
                }
                else
                {
                    outputText = "Введите целое число";
                }
                break;

            case "!food":
                if (int.TryParse(commandParameter, out intCheck))
                {
                    parametersLibrary.PlayerCurrentParametersList[2].ParameterValue += int.Parse(commandParameter);
                    outputText = "Еда изменена на " + commandParameter;
                }
                else
                {
                    outputText = "Введите целое число";
                }
                break;

            case "!gold":
                if (int.TryParse(commandParameter, out intCheck))
                {
                    parametersLibrary.PlayerCurrentParametersList[3].ParameterValue += int.Parse(commandParameter);
                    outputText = "Золото изменено на " + commandParameter;
                }
                else
                {
                    outputText = "Введите целое число";
                }
                break;

            case "!crystals":
                if (int.TryParse(commandParameter, out intCheck))
                {
                    parametersLibrary.PlayerCurrentParametersList[4].ParameterValue += int.Parse(commandParameter);
                    outputText = "Кристаллы изменено на " + commandParameter;
                }
                else
                {
                    outputText = "Введите целое число";
                }
                break;

            case "!moves":
                if (int.TryParse(commandParameter, out intCheck))
                {
                    parametersLibrary.PlayerCurrentParametersList[5].ParameterValue += int.Parse(commandParameter);
                    outputText = "Ходы изменено на " + commandParameter;
                }
                else
                {
                    outputText = "Введите целое число";
                }
                break;

            case "!level":
                if (int.TryParse(commandParameter, out intCheck))
                {
                    parametersLibrary.PlayerCurrentParametersList[6].ParameterValue += int.Parse(commandParameter);
                    parametersLibrary.PlayerCurrentParametersList[6].ParameterValue -= 1;
                    outputText = "Уровень изменен на " + commandParameter;
                    newLevel.Raise();
                }
                else
                {
                    outputText = "Введите целое число";
                }
                break;

            case "!experience":
                if (int.TryParse(commandParameter, out intCheck))
                {
                    parametersLibrary.PlayerCurrentParametersList[7].ParameterValue += int.Parse(commandParameter);
                    outputText = "Опыт изменен на " + commandParameter;
                }
                else
                {
                    outputText = "Введите целое число";
                }
                break;

            case "!items_index":

                foreach (var item in itemsLibrary.ItemList)
                {
                    outputText += string.Format("ID {0}  -  {1} \n\n", item.id, item.itemName);
                }

                break;

            case "!add_item":

                if (int.TryParse(commandParameter, out intCheck))
                {
                    if (int.Parse(commandParameter) <= itemsLibrary.ItemList.Count)
                    {
                        if (itemsLibrary.ItemList[int.Parse(commandParameter)] != null)
                        {
                            inentoryItemsId.arrayList.Add(int.Parse(commandParameter));
                        }
                        updateInventory.Raise();
                        outputText = "Предмет добавлен в инвентарь";
                    }
                    else
                    {
                        outputText = "Предмета с таким индексом не найдено. \nДля просмотра всех предметов воспользуйтесь \nкомандой !items_index";
                    }
                }
                else
                {
                    outputText = "Введите целое число";
                }
                break;

            case "!delete_all_items":

                inentoryItemsId.arrayList.RemoveRange(0, inentoryItemsId.arrayList.Count);

                updateInventory.Raise();
                outputText = "Предметы удалены из инвентаря";

                break;

            case "!change_scene":

                changeScene.Raise(commandParameter);

                break;

            case "!rewards":

                dailyRewards.Raise();

                break;

            case "!help_tip":

                helpTip.Raise();

                break;

            default:
                outputText = "Команда не найдена ( воспользуйтесь !help ) \n";
                break;
        }

        ConsoleInputField.text = "";

        if (outputText != "")
        {
            ConsoleTextOutput(outputText);
            outputText = "";
            commandParameter = "";
        }
    }

    public void ConsoleTextOutput()
    {
        ConsoleOutput.text += ConsoleInput + "\n";

        lastCommandsIndex = lastCommands.Count - 1;
        firstKeyCheck = true;
    }

    public void ConsoleTextOutput(string text)
    {
        ConsoleOutput.text += text + "\n\n";

    }

    public void LastCommands(string dir)
    {
            if (dir == "up")
            {
                if (firstKeyCheck)
                {
                    lastCommandsIndex = lastCommands.Count - 1;
                    firstKeyCheck = false;
                }
                else
                {
                    if (lastCommandsIndex != 0)
                    {
                        lastCommandsIndex--;
                    }
                }
                ConsoleInputField.text = lastCommands[lastCommandsIndex];
                ConsoleInputField.caretPosition = ConsoleInputField.text.Length;
            }

            if (dir == "down")
            {
                if (firstKeyCheck)
                {
                    lastCommandsIndex = lastCommands.Count - 1;
                    firstKeyCheck = false;
                }
                else
                {
                    if (lastCommandsIndex != lastCommands.Count - 1)
                    {
                        lastCommandsIndex++;
                    }
                }
                ConsoleInputField.text = lastCommands[lastCommandsIndex];
                ConsoleInputField.caretPosition = ConsoleInputField.text.Length;
            }
    }

}
