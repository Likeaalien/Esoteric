using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    public GameObject panel_start_menu;
    public GameObject panel_hud;
    public GameObject panel_cheat;
    public TMP_InputField player_name_field;
    public Player player;
    public Button play_button;

    void Start()
    {
        panel_start_menu.SetActive(true);
        panel_hud.SetActive(false);

        player_name_field.onValueChanged.AddListener(ValidateNick);

        ValidateNick(player_name_field.text);
    }

    public void StartGame()
    {
        player.player_nickname = player_name_field.text;
        
        panel_start_menu.SetActive(false);
        if (player.player_nickname == "Marvin")
        {
            panel_cheat.SetActive(true);
        }
        panel_hud.SetActive(true);
    }
    private void ValidateNick(string text)
    {
        play_button.interactable = text.Length >= 3 && text.Length <= 12;
    }
}