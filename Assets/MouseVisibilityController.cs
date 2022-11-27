using System;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;

[UsedImplicitly]
public class MouseVisibilityController
{
    private readonly CompositeDisposable _disposable = new();
    private MouseVisibilityController(Registrar mouseRegistrar) => mouseRegistrar.Subscribe(b => Cursor.visible = b).AddTo(_disposable);
    ~MouseVisibilityController() => _disposable.Dispose();
}