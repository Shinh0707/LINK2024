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
    // サーバービルドは自動でStartServer
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
        //接続承認コールバック
        NetworkManager.Singleton.ConnectionApprovalCallback = ApprovalCheck;
        //ホスト開始
        NetworkManager.Singleton.StartHost();
        //シーンを切り替え
        m_EyeEffectAnimator.SetBool("EyeCloseTrigger", false);
        SceneManager.LoadScene(StartingScene, LoadSceneMode.Single);
    }

    public void StartClient()
    {
        if (!start_clicked) {
            start_clicked = true;
            Debug.Log("Start Check Client");
            m_EyeEffectAnimator.SetBool("EyeCloseTrigger", true);
            //ホストに接続
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
    /// 接続承認関数
    /// </summary>
    private void ApprovalCheck(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
    {
        // 追加の承認手順が必要な場合は、追加の手順が完了するまでこれを true に設定します
        // true から false に遷移すると、接続承認応答が処理されます。
        response.Pending = true;

        //最大人数をチェック(この場合は4人まで)
        if (NetworkManager.Singleton.ConnectedClients.Count >= 2)
        {
            response.Approved = false;//接続を許可しない
            response.Pending = false;
            return;
        }
        //ここからは接続成功クライアントに向けた処理
        response.Approved = true;//接続を許可

        //PlayerObjectを生成するかどうか
        response.CreatePlayerObject = false;

        //生成するPrefabハッシュ値。nullの場合NetworkManagerに登録したプレハブが使用される
        response.PlayerPrefabHash = null;

        //PlayerObjectをスポーンする位置(nullの場合Vector3.zero)
        response.Position = Vector3.up*2;

        //PlayerObjectをスポーン時の回転 (nullの場合Quaternion.identity)
        response.Rotation = Quaternion.identity;

        response.Pending = false;
    }
}