using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FishFactory : ScriptableObject
{
    // 存放生產的魚類的預製體
    public Fish[] fishes;

    // 輸入序號來獲得實例化的魚類
    public Fish Get(int id) {
        Fish fish = Instantiate(fishes[id]);
        return fish;
    }
}
