using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerNetworkBehaviour : NetworkBehaviour
{
    [SerializeField] private NetworkObject m_playerPrefab;

    public override void OnNetworkSpawn()
    {
        if (IsHost)
        {
            //�N���C�A���g�ڑ���
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

            //���łɑ��݂���N���C�A���g�p�Ɋ֐��Ăяo��
            foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
            {
                OnClientConnected(client.ClientId);
            }
        }
        
    }
    public void OnClientConnected(ulong clientId)
    {
        //�v���C���[�I�u�W�F�N�g����
        var generatePos = new Vector3(0, 1, -8);
        generatePos.x = -5 + 5 * (NetworkManager.Singleton.ConnectedClients.Count % 3);
        NetworkObject playerObject = Instantiate(m_playerPrefab, generatePos, Quaternion.identity);
        //�ڑ��N���C�A���g��Owner�ɂ���PlayerObject�Ƃ��ăX�|�[��
        playerObject.SpawnAsPlayerObject(clientId);
    }
}
