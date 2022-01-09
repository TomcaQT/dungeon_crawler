using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour, IDamagable
{

    [SerializeField] private WeaponItemData _weapon; 

    [SerializeField] private Resource _hp;
    [SerializeField] private Resource _energy; // ????

    private Stat _damage;
    private Stat _resistency;
    private Stat _attackSpeed;
    private Stat _bulletSpeed;

    private const float MAX_MONEY = 1000000;
    public Currency Currency;

    public event EventHandler OnStatChange;
    
    private void Awake()
    {
        _hp = new Resource(100f, "Hp", 0f);
        _energy = new Resource(100f, "Energy", 20f);

        _damage = new Stat(10f, "Damage");
        _resistency = new Stat(5f, "Resistency");
        _attackSpeed = new Stat(.5f, "AttackSpeed");
        _bulletSpeed = new Stat(10f, "BulletSpeed");

        Currency = new Currency(100, "µ");
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        var collectible = other.GetComponent<ICollectible>();
        if (collectible != null && (Input.GetKey(KeyCode.E) || collectible.AutoCollect))
        {
            collectible.OnPickUp(this);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("Shop") && Input.GetKey(KeyCode.E))
        {
            other.GetComponent<ShopSpot>().Buy();
        }
    }


    public void TakeDamage(float amount)
    {
        if (_hp.Take(amount))
            Debug.Log("Player Die");
    }
    
    #region Resources and Stats accessors

    public void IncreaseStatOrResource(string toBoost, float amount)
    {
        switch (toBoost)
        {
            case "Hp":
                _hp.IncreaseMax(amount);
                break;
            case "Energy":
                _energy.IncreaseMax(amount);
                break;
            case "Damage":
                _damage.Increase(amount);
                break;
            case "Resistency":
                _resistency.Increase(amount);
                break;
            case "AttackSpeed":
                _attackSpeed.Decrease(amount);
                break;
            case "BulletSpeed":
                _bulletSpeed.Increase(amount);
                break;
            case "Money":
                Currency.Add(Mathf.FloorToInt(amount));
                break;
            default:
                Debug.LogError("No known stat to boost");
                break;
        }
        OnStatChange?.Invoke(this,new EventArgs());
    }
    
    public bool TryTakeEnergy(float amount) => _energy.TryTake(amount);

    public WeaponItemData Weapon 
    {
        get => _weapon;
        set
        {
            _weapon = value;
            OnStatChange?.Invoke(this, new EventArgs());
        }
    }

    public Resource Hp => _hp;
    public Resource Energy => _energy;
    
    public float Damage => _damage.Value;
    public float Resistency => _resistency.Value;
    public float AttackSpeed => _attackSpeed.Value;
    public float BulletSpeed => _bulletSpeed.Value;

    public void Heal(float amount) => _hp.Add(amount);

    public void HealPercent(float percent)
    {
        percent = Mathf.Clamp(percent, 0f, 1f);
        _hp.Add(_hp.MaxValue * percent);
    }

    public void UpgradeWeapon()
    {
        if (_weapon.ItemQuality == ItemQuality.Legendary)
            return;
        _weapon.ItemQuality++;
        OnStatChange?.Invoke(this,new EventArgs());
    }

    #endregion
}
