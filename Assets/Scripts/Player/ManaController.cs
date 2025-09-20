using UnityEngine;

public class ManaController : PlayerController
{
    // Example mana costs
    [SerializeField] private int shootManaCost = 10;
    [SerializeField] private int skill1ManaCost = 25;
    [SerializeField] private int skill2ManaCost = 40;

    // Returns true if enough mana and spends it, false otherwise
    public bool TrySpendManaForShoot()
    {
        return GameManager.Instance.TrySpendMana(shootManaCost);
    }

    public bool TrySpendManaForSkill1()
    {
        return GameManager.Instance.TrySpendMana(skill1ManaCost);
    }

    public bool TrySpendManaForSkill2()
    {
        return GameManager.Instance.TrySpendMana(skill2ManaCost);
    }
}
