using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class BundleLoader : MonoBehaviour {

    private string remoteUrl = "http://bcgames.xyz/games/bundles/thief/";

    private UGLFBundle bundle = null;

    private AssetBundle asbundle = null;
    private AudioClip clip = null;
    private AudioSource source = null;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        bundle = new UGLFBundle(remoteUrl);
        source = GetComponent<AudioSource>();
    }

    IEnumerator LoadBundle()
    {
        string bundleUrl = "http://bcgames.xyz/games/bundles/thief/Windows/audio.ab";
        Hash128 hash = Hash128.Parse("7640743,d72cba6a62ffa93c263dd6f24bf12133");

        UnityWebRequest request = UnityWebRequest.GetAssetBundle(bundleUrl, hash, 0);
        yield return request.SendWebRequest();

        asbundle = DownloadHandlerAssetBundle.GetContent(request);
        foreach (string asName in asbundle.GetAllAssetNames())
        {
            Debug.Log(asName);
        }
    }

    IEnumerator DoTest()
    {
        yield return null;

        clip = asbundle.LoadAsset<AudioClip>("assets/prefabs/audio/celestial visitor.wav");
        source.clip = clip;
        source.Play();

    }

    void OnGUI()
    {
        //if(GUILayout.Button("Load Bundle"))
        //{
        //    StartCoroutine(LoadBundle());
        //}

        //if (GUILayout.Button("DoTest"))
        //{
        //    StartCoroutine(DoTest());
        //}

        //if (GUILayout.Button("Unload"))
        //{
        //    //source.clip = null;
        //    Resources.UnloadAsset(clip);
        //}
        //if (GUILayout.Button("Unload2"))
        //{
        //    asbundle.Unload(false);
        //}
        if (GUILayout.Button("Load"))
        {
            bundle.StartUpdate();
        }


        GUILayout.Label("totalBytes: " + bundle.totalNeedDownloadBytes);
        GUILayout.Label("downloaded: " + bundle.downloadedBytes);
        GUILayout.Label("progress: " + bundle.Progress);
        GUILayout.Label("state: " + bundle.bundleState);

        if(GUILayout.Button("Load Audio Test"))
        {
            string assetPath = "assets/prefabs/audio/celestial visitor.wav";
            AudioClip clip = bundle.GetAsset<AudioClip>(assetPath);
            source.clip = clip;
            source.Play();
        }

        if(GUILayout.Button("Unload Audio Test"))
        {
            string assetPath = "assets/prefabs/audio/celestial visitor.wav";
            Debug.Log(bundle.UnloadAsset(assetPath));
        }
    }

	
	// Update is called once per frame
	void Update () {

		
	}


      
}
