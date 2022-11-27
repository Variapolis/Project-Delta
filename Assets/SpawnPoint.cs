using Photon.Pun.UtilityScripts;
using UnityEngine;
using Zenject;

public sealed class SpawnPoint : MonoBehaviour
{
    [Inject] private SpawnPointList _spawnPointList;
    [SerializeField] private string locationName;
    [SerializeField] private MPTeam team = MPTeam.Spy;


    public string LocationName => locationName;
    public MPTeam Team => team;

    private void Awake() => _spawnPointList.Add(this);

    public enum MPTeam
    {
        Spy = 1,
        Guard = 2
    }
}