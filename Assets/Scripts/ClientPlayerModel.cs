using JetBrains.Annotations;
using UniRx;
using UnityEngine;

[UsedImplicitly]
public sealed class ClientPlayerModel
{
    public readonly ReactiveProperty<GameObject> PlayerObject = new();
    public readonly ReactiveProperty<float> PlayerHealth = new();
    public readonly ReactiveProperty<bool> IsAlive = new();
    public readonly ReactiveProperty<bool> IsReloading = new();
    public readonly ReactiveProperty<float> ReloadStatus = new();
}