using System;
using Alteruna;
using UnityEngine;

namespace Scripts{

    // [RequireComponent(typeof(SingleClientEvent))]
    public class CreateBoard : MonoBehaviour
    {
        private Spawner _spawner;
        [SerializeField] private int indexToSpawn = 0;

        [NonSerialized]
        private SingleClientEvent _singleClientEvent;

        public GameLogic gameBoard;
        
        private void Awake()
        {
            // if (_singleClientEvent.IsControlled) return;

            _singleClientEvent = GetComponent<SingleClientEvent>();
            _singleClientEvent.OnClientChanged.AddListener(OnClientChanged);

            _spawner = GameObject.FindGameObjectWithTag("NetworkManager").GetComponent<Spawner>();

            _spawner.Spawn(indexToSpawn);

            gameBoard = _spawner.SpawnedObjects[0].Item1.GetComponent<GameLogic>();

        }

        private void OnClientChanged(bool isControlled)
        {
            enabled = isControlled;
        }
        
        // public void Update()
        // {
        //     // BroadcastRemoteMethod(gameBoard.Update());
        //     gameBoard.Update();
        // }
    }
}