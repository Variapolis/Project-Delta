using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UniRx;

[UsedImplicitly]
public class Registrar : IReadOnlyReactiveProperty<bool>
{
    private readonly HashSet<object> _set = new();

    private readonly ReactiveProperty<bool> _value = new();

    [NotNull]
    public static Registrar operator +([NotNull] Registrar registrar, object registree)
    {
        registrar._set.Add(registree);
        registrar._value.Value = registrar._set.Count > 0;
        return registrar;
    }

    [NotNull]
    public static Registrar operator -([NotNull] Registrar registrar, object registree)
    {
        registrar._set.Remove(registree);
        registrar._value.Value = registrar._set.Count > 0;
        return registrar;
    }

    public IDisposable Subscribe(IObserver<bool> observer) => _value.Subscribe(observer);

    public bool Value => _value.Value;

    public bool HasValue => _value.HasValue;
}