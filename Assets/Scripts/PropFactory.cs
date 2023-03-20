using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PropFactory : ScriptableObject
{
    // 存放生產的道具的預製體
    public Prop[] props;

    // 輸入序號來獲得實例化的道具
    public Prop Get(int id) {
        Prop prop = Instantiate(props[id]);
        return prop;
    }
}