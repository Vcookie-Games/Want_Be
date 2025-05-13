using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Text.RegularExpressions;
using ChickMan;

public abstract class Item : MonoBehaviour
{
    [Header("Item Settings")]
    public string itemName;
    [SerializeField] protected float _usageTime;
    [SerializeField] protected float _despawnTime;
    [SerializeField] protected bool isActive;
    [SerializeField] protected Color color;
    [SerializeField] private GameObject effectTimerPanelPrefab;
    private Transform effectTimerPanelParent;

    protected float maxDespawnTime;
    protected float maxUsageTime;
    private float time;


    GameObject panelInstance;

    public float usageTime
    {
        get { return _usageTime; }
        set
        {

            if (value < 0)
            {
                Debug.LogWarning("Thời gian không thể là số âm, đã cài mặc định 15s");
                _usageTime = maxUsageTime;
            }
            else if (value > maxUsageTime)
            {
                Debug.LogWarning("Thời gian dùng tối đa là 15s , đã cài mặc định 15s");
                _usageTime = maxUsageTime;
            }
            else
            {
                _usageTime = value;
            }
        }
    }

    public float despawnTime
    {
        get { return _despawnTime; }
        set
        {

            if (value < 0)
            {
                Debug.LogWarning("Thời gian despawn không thể là số âm, đã cài mặc định 3s");
                _despawnTime = 3f;
            }
            else if (value > maxDespawnTime)
            {
                Debug.LogWarning("Thời gian despawn tối đa là 3s, đã cài mặc định 3s");
                _despawnTime = maxDespawnTime;
            }
            else
            {
                _despawnTime = value;
            }
        }
    }


    void OnValidate()
    {
        // Clean item name
        string cleaned = Regex.Replace(itemName, "[^a-zA-Z ]", ""); // Cho phép chữ cái và khoảng trắng

        if (cleaned != itemName)
        {
            Debug.LogWarning($"Đổi tên item từ \"{itemName}\" thành \"{cleaned}\" (loại bỏ ký tự không hợp lệ)");
            itemName = cleaned;
        }

        if (_usageTime < 0)
        {
            Debug.LogWarning("Thời gian không thể là số âm, đã cài mặc định 15s");
            _usageTime = maxUsageTime;
        }
        if (_usageTime > maxUsageTime)
        {
            Debug.LogWarning("Thời gian dùng tối đa là 15s , đã cài mặc định 15s");
            _usageTime = maxUsageTime;
        }
        if (_despawnTime < 0)
        {
            Debug.LogWarning("Thời gian despawn không thể là số âm, đã cài mặc định 3s");
            _despawnTime = maxDespawnTime;
        }
        if (_despawnTime > maxDespawnTime)
        {
            Debug.LogWarning("Thời gian despawn tối đa là 3s, đã cài mặc định 3s");
            _despawnTime = maxDespawnTime;
        }

    }
    public Item()
    {
        color = Color.white;
        isActive = false;
        itemName = "Null";
        _usageTime = 15f;
        _despawnTime = 3f;
        maxUsageTime = _usageTime;
        maxDespawnTime = _despawnTime;
    }
    void Start()
    {
        effectTimerPanelParent = GameObject.Find("Popup Item").GetComponent<Transform>();
        StartCoroutine(CheckInactiveThenDespawn());
    }
    public virtual void ActivateItem()
    {
        if (effectTimerPanelParent == null)
        {
            Debug.LogWarning("Thiếu Popup Item");
            return;
        }
        isActive = true;
        ShowEffectTimer();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(UsageTimerCoroutine());
    }

    public virtual void ActivateDespawn()
    {
        Destroy(panelInstance);
        Destroy(this.gameObject);
    }

    private IEnumerator UsageTimerCoroutine()
    {
        time = _usageTime;
        while (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= despawnTime)
            {
                Debug.Log($"{itemName} sẽ biến mất sau {(int)time} giây.");
            }
            yield return null;
        }
        ActivateDespawn();

    }
    private IEnumerator CheckInactiveThenDespawn()
    {
        float timer = Random.Range(3f, 5f);
        while (timer > 0)
        {

            if (isActive)
            {
                yield break;
            }
            timer -= Time.deltaTime;
            yield return null;
        }

        if (!isActive)
        {
            Debug.Log($"{itemName} đã bị xóa.");
            Destroy(this.gameObject);
        }
    }
    public void ShowEffectTimer()
    {

        panelInstance = Instantiate(effectTimerPanelPrefab, effectTimerPanelParent);

        EffectTime newEffectTime = panelInstance.GetComponent<EffectTime>();
        StartCoroutine(newEffectTime.UpdateEffectFill(maxUsageTime, color));
    }
    public void CheckSame()
    {
        GameObject[] allItemObjects = GameObject.FindGameObjectsWithTag("Item");
        foreach (GameObject obj in allItemObjects)
        {
            Item item = obj.GetComponent<Item>();
            if (this.itemName == item.itemName && obj != this.gameObject)
            {
                item.ActivateDespawn();
            }
        }
        ActivateItem();
    }


}
