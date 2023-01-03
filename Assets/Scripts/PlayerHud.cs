using FishNet.Managing.Object;
using FishNet.Object;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHud : NetworkBehaviour
{
    public int points;
    public Image charpic;
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (base.IsOwner)
        {
            charpic.sprite = Resources.Load<Sprite>("Characters/player1pic.png");
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (base.IsHost)
        {
            charpic.sprite = Resources.Load<Sprite>("Characters/player1pic");
        }
        else
        {
            charpic.sprite = Resources.Load<Sprite>("Characters/player2pic");
        }
    }
}
