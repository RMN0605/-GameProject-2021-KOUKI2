using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AiManager : MonoBehaviour
{
    [SerializeField]
    private int timer;    //生成する時間
    [SerializeField]
    private GameObject top; //生成場所参照
    [SerializeField]
    private GameObject center;//生成場所参照
    [SerializeField]
    private GameObject bottom;//生成場所参照

    public GameObject unit_parent;
    private GameObject unit_child;

    [SerializeField, Header("AI（ガチモード）")]
    private bool isGati = false;
    private bool isPlayer = true;

    private Vector3 pos;
    private bool isSummon;

    private int summon_count;

    private void Awake()
    {
        unit_parent = GameObject.Find("Unit_generation_location");
    }
    private void Start()
    {
        isSummon = false;
        summon_count = 0;
    }
    void FixedUpdate()
    {
        if (!isSummon)
            StartCoroutine(Summon(timer));
    }

    public IEnumerator Summon(float cool_time)
    {
        isSummon = true;

        #region 時間が立てば立つほど早く生成するよ
        if (summon_count > 40)
        {
            yield return new WaitForSeconds(Random.Range(cool_time - 4, cool_time)); //クールタイム計測開始
        }
        else if (summon_count > 10)
        {
            yield return new WaitForSeconds(Random.Range(cool_time - 2, cool_time)); //クールタイム計測開始
        }
        else
        {
            yield return new WaitForSeconds(Random.Range(cool_time, cool_time)); //クールタイム計測開始
        }
        #endregion
        var rmd = Random.Range(0, 100);

        #region 生成数の決定
        if (!isGati)
        {
            if (rmd > 53)
            {
                for (int i = 0; i < 1; i++)
                {
                    unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[Random.Range(0, Unit_manager.unit_list.Count)], Summon_pos(), 2);  //生成するユニット、生成する場所、生成するプレイヤー
                }
            }
            else if (rmd >= 43)
            {
                for (int i = 0; i < 2; i++)
                {
                    unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[Random.Range(0, Unit_manager.unit_list.Count)], Summon_pos(), 2);  //生成するユニット、生成する場所、生成するプレイヤー
                    unit_child.transform.parent = unit_parent.transform;
                }
            }
            else if (rmd >= 35)
            {
                for (int i = 0; i < 3; i++)
                {
                    unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[Random.Range(0, Unit_manager.unit_list.Count)], Summon_pos(), 2);  //生成するユニット、生成する場所、生成するプレイヤー
                    unit_child.transform.parent = unit_parent.transform;
                }
            }
            else if (rmd >= 4)
            {
                for (int i = 0; i < 1; i++)
                {
                    unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[Random.Range(0, Unit_manager.unit_list.Count)], Summon_pos(), 2);  //生成するユニット、生成する場所、生成するプレイヤー
                    unit_child.transform.parent = unit_parent.transform;
                }
            }
            else if (rmd >= 3)
            {
                for (int i = 0; i < 10; i++)
                {
                    unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[Random.Range(0, Unit_manager.unit_list.Count)], Summon_pos(), 2);  //生成するユニット、生成する場所、生成するプレイヤー
                    unit_child.transform.parent = unit_parent.transform;
                }
            }
        }
        else
        {
            for (int i = 0; i < 10; i++)
            {
                unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[Random.Range(0, Unit_manager.unit_list.Count)], Summon_pos(), 2);  //生成するユニット、生成する場所、生成するプレイヤー
                unit_child.transform.parent = unit_parent.transform;
            }
        }
        #endregion

        unit_child = Unit_manager.Instantiate_unit(Unit_manager.unit_list[Random.Range(0, Unit_manager.unit_list.Count)], Summon_pos(), 2);  //生成するユニット、生成する場所、生成するプレイヤー
        unit_child.transform.parent = unit_parent.transform;
        summon_count++;
        isSummon = false;
    }

    public Vector3 Summon_pos()
    {
        float place = Random.Range(0,3);
        switch (place)
        {
            case 0:
                pos = top.gameObject.transform.position;
                break;
            case 1:
                pos = center.gameObject.transform.position;
                break;
            case 2:
                pos = bottom.gameObject.transform.position;
                break;
            default:
                Debug.Log("生成時にエラーが起こってるよ");
                break;
        }
        return pos;
    }
}
