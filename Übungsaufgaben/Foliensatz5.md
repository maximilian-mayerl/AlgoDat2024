# Foliensatz 5: Clean Code

## Übung 1: Code Cleanup 1

```csharp
class Player {
    private const int DEFENSE_NOMINATOR = 50;
    private const float FALL_DAMAGE_RATIO = 0.05f;
    private const float RADIATION_DAMAGE_RATIO = 0.01f;

    public float CurrentHP { get; private set; }
    public float Defense { get; private set; }

    private void ApplyDamage(float damage) {
        CurrentHP -= damage;
        CurrentHP = (CurrentHP < 0) ? 0 : CurrentHP;
    }

    public void TakeBattleDamage(float attack, float critRate, float critDamage) {
        float damage = attack * (DEFENSE_NOMINATOR / Defense);
        double critChance = Random.Shared.NextDouble();
        bool isCrit = critChance <= critRate;

        if (isCrit) {
            damage *= (1 + critDamage);
        }

        ApplyDamage(damage);
    }
    public void TakeFallDamage(float speed) {
        float fallDamagePercent = (speed * FALL_DAMAGE_RATIO > 1.0) ? 1.0f : speed * FALL_DAMAGE_RATIO;
        float damage = CurrentHP * fallDamagePercent;

        ApplyDamage(damage);
    }
    public void TakeRadiationDamage() {
        float damage = CurrentHP * RADIATION_DAMAGE_RATIO;
        ApplyDamage(damage);
    }
}
```
