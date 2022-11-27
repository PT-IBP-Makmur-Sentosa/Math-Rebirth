using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TraitManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI traitName;
    [SerializeField] TextMeshProUGUI traitInfo;
    [SerializeField] string[] Tname;
    [SerializeField] Animator anim;
    string[] Tinfo = new string[4];
    
    
    int idx = 0;
    // Start is called before the first frame update
    void Start()
    {
        Tinfo[0] = @"Having a hobby of going to the gym during his leisure time. He has a very healthy body and trained muscle. Someone saw him lifting a car by himself, but that itself is just a rumour.

Trait focuses on Strength, +5 Str per Level & +2 for other stats.";
        Tinfo[1] = @"He was once the ace of his university track and field team. He has a very good agility and quick. It was rumoured that he's as fast as Usain Bolt.

Trait focuses on Agility, +5 Agi per Level & +2 for other stats.";
        Tinfo[2] = @"Reading is the same as breathing for him. He has been reading lots of books since young age, that being said, he knows a lot of knowledge thus having a sharper mind than the average human. Some suspect he might be the reincarnation of Albert Einstein.

Trait focuses on Intelligent, +5 Int per Level & +2 for other stats.";
        Tinfo[3] = @"Not much can be said about this guy. He is just your ordinary Joe you can find anywhere. Not sure how did he get into this mess.

Trait is equally distributed, +3 for all stats per level.";

        traitName.text = Tname[0];
        traitInfo.text = Tinfo[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void previousTrait()
    {

        if (idx - 1 < 0) idx = Tname.Length - 1;
        else idx -= 1;
        traitName.text = Tname[idx];
        traitInfo.text = Tinfo[idx];
        anim.SetInteger("index", idx);
    }
    public void nextTrait()
    {
        if (idx + 1 > Tname.Length - 1) idx = 0;
        else idx += 1;
        traitName.text = Tname[idx];
        traitInfo.text = Tinfo[idx];
        anim.SetInteger("index", idx);
    }
}
