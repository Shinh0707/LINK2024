using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.Netcode;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Net;
using System;
using Unity.Netcode.Transports.UTP;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using Debug = UnityEngine.Debug;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] Object3DInputHandler startObject;
    [SerializeField] NetworkManager m_networkManager;
    [SerializeField] Animator m_EyeEffectAnimator;
    [SerializeField] SceneObject StartingScene;
    private bool start_clicked = false;
    private void Awake()
    {
#if UNITY_SERVER
    // �T�[�o�[�r���h�͎�����StartServer
    if (!NetworkManager.Singleton.IsServer)
    {
        NetworkManager.Singleton.StartServer();
    }
#else
        startObject.onClick.AddListener(StartClient);
#endif
    }

    private void Start()
    {
        m_EyeEffectAnimator.Play("EyeIdling");
    }
    public void StartHost()
    {
        //�ڑ����F�R�[���o�b�N
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        //�z�X�g�J�n
        NetworkManager.Singleton.StartHost();
        //�V�[����؂�ւ�
        m_EyeEffectAnimator.SetBool("EyeCloseTrigger", false);
        SceneManager.LoadScene(StartingScene, LoadSceneMode.Single);
    }

    public void StartClient()
    {
        if (!start_clicked) {
            start_clicked = true;
            Debug.Log("Start Check Client");
            m_EyeEffectAnimator.SetBool("EyeCloseTrigger", true);
            //�z�X�g�ɐڑ�
            StartCoroutine(CheckConnectHost());
        }
    }

    public IEnumerator CheckConnectHost(float timeOut=10)
    {
        bool result = NetworkManager.Singleton.StartClient();
        if (result)
        {
            float time = 0f;
            while (time < timeOut) 
            {
                if (NetworkManager.Singleton.ConnectedHostname != "") {
                    m_EyeEffectAnimator.SetBool("EyeCloseTrigger", false);
                    Debug.Log("Start as Client");
                    yield break;
                }
                yield return new WaitForSeconds(1);
                time += 1;
            }
            m_networkManager.Shutdown();
            while (m_networkManager.ShutdownInProgress)
            {
                yield return new WaitForEndOfFrame();
            }
        }
        Debug.Log("Start as Host");
        StartHost();
    }

    /// <summary>
    /// �ڑ����F�֐�
    /// </summary>
    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        // �ǉ��̏��F�菇���K�v�ȏꍇ�́A�ǉ��̎菇����������܂ł���� true �ɐݒ肵�܂�
        // true ���� false �ɑJ�ڂ���ƁA�ڑ����F��������������܂��B
        response.Pending = true;

        //�ő�l�����`�F�b�N(���̏ꍇ��4�l�܂�)
        if (NetworkManager.Singleton.ConnectedClients.Count >= 2)
        {
            response.Approved = false;//�ڑ��������Ȃ�
            response.Pending = false;
            return;
        }
        //��������͐ڑ������N���C�A���g�Ɍ���������
        response.Approved = true;//�ڑ�������

        //PlayerObject�𐶐����邩�ǂ���
        response.CreatePlayerObject = false;

        //��������Prefab�n�b�V���l�Bnull�̏ꍇNetworkManager�ɓo�^�����v���n�u���g�p�����
        response.PlayerPrefabHash = null;

        //PlayerObject���X�|�[������ʒu(null�̏ꍇVector3.zero)
        response.Position = Vector3.up*2;

        //PlayerObject���X�|�[�����̉�] (null�̏ꍇQuaternion.identity)
        response.Rotation = Quaternion.identity;

        response.Pending = false;
    }
}