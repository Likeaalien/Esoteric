using UnityEngine;
using TMPro;

public class GameUIManager : MonoBehaviour
{
    public GameObject panel_start_menu;
    public GameObject panel_hud;
    public TMP_InputField player_name_field;
    public Player player;

    void Start()
    {
        panel_start_menu.SetActive(true);
        panel_hud.SetActive(false);
    }

    public void StartGame()
    {
        player.player_nickname = player_name_field.text;
        Debug.Log(player.player_nickname);
        panel_start_menu.SetActive(false);
        panel_hud.SetActive(true);
    }
}