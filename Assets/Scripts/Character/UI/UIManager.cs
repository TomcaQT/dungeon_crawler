using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _hpText;
    [SerializeField] private TextMeshProUGUI _energyText;
    [SerializeField] private TextMeshProUGUI _roomText;
    [SerializeField] private TextMeshProUGUI _weaponText;
    [SerializeField] private TextMeshProUGUI _moneyText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private TextMeshProUGUI _attackSpeedText;

    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _energyBar;

    [SerializeField] private GameObject _menu;
    [SerializeField] private GameObject _playAgainMenu;
    
    
    private PlayerStats _playerStats;
    private GameManager _gameManager;

    private bool _isPaused = false;
    
    private void Awake()
    {
        
        
    }

    private void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        _playerStats.Hp.OnResourceChange += e_UpdateHp;
        _playerStats.Energy.OnResourceChange += e_UpdateEnergy;
        _playerStats.OnStatChange += e_UpdateStat;
        _playerStats.Currency.OnCurrencyChange += e_UpdateMoney;
        _playerStats.OnPlayerDeath += e_PlayerDeath;

        _hpText.text = $"{_playerStats.Hp.Value}/{_playerStats.Hp.MaxValue}";
        _energyText.text = $"{_playerStats.Energy.Value}";
        e_UpdateStat(this,new EventArgs());
        e_UpdateMoney(this,new EventArgs());
        
        _menu.SetActive(false);
        _playAgainMenu.SetActive(false);
    }

    private void Update()
    {
        GetInput();
        UpdateUI();
    }

    private void UpdateUI()
    {
        _roomText.text = $"Room: {_gameManager.RoomNumber}";
    }

    private void e_UpdateHp(object sender, ResourceChangeEventArgs e)
    {
        _hpText.text = $"{e.Value:F1}/{_playerStats.Hp.MaxValue:F0}";
        _hpBar.value = _playerStats.Hp.Percentage01;
    }

    private void e_UpdateEnergy(object sender, ResourceChangeEventArgs e)
    {
        _energyText.text = $"{e.Value:F0}";
        _energyBar.value = _playerStats.Energy.Percentage01;
    }

    private void e_UpdateStat(object sender, EventArgs e)
    {
        _weaponText.text = $"{_playerStats.Weapon.Name}\n {_playerStats.Weapon.ItemQuality}";
        _damageText.text = $"Damage: {_playerStats.Damage * Utils.WeaponQualityMultiplier(_playerStats.Weapon.ItemQuality)}";
        

        float attackSpeed = _playerStats.AttackSpeed >= 0f ? 1/(_playerStats.AttackSpeed + ((1f-Utils.WeaponQualityMultiplier(_playerStats.Weapon.ItemQuality))*.1f)) : 0f;
        
        _attackSpeedText.text = $"AttackSpeed: {(attackSpeed):F2}";
    }

    private void e_UpdateMoney(object sender, EventArgs e)
    {
        _moneyText.text = $"µ: {_playerStats.Currency.Value}";
    }

    private void e_PlayerDeath(object sender, EventArgs e)
    {
        _playAgainMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void PauseMenu()
    {
       
        if (_isPaused)
        {
            Time.timeScale = 1;
            _isPaused = false;
            _menu.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            _isPaused = true;
            _menu.SetActive(true);
        }
    }

    public void PlayAgain()
    {
        
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseMenu();
        }
    }
    
}
