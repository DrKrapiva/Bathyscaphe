using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class MinesForDefuse : MonoBehaviour
{
    [SerializeField] GameObject red;
    [SerializeField] GameObject green;
    [SerializeField] GameObject explode;
    [SerializeField] GameObject mineBody;
    [SerializeField] private Slider sliderHP;
    private float defuseTime = 5f; // ¬рем€ до обезвреживани€ в секундах
    private float elapsedTime = 0f;
    private bool isDefused = false;
    private Coroutine defuseCoroutine;

    private void Start()
    {
        sliderHP.maxValue = defuseTime;
        sliderHP.value = elapsedTime;

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null && !isDefused)
        {
            defuseCoroutine = StartCoroutine(DefuseTimer(other.gameObject));
            Debug.Log("Start defusing");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<CharacterLocomotion>() != null && !isDefused)
        {
            if (defuseCoroutine != null)
            {
                StopCoroutine(defuseCoroutine);
                defuseCoroutine = null;

                // —бросить прогресс на одну треть
                elapsedTime = Mathf.Max(0f, elapsedTime / 3f);
                sliderHP.value = elapsedTime;

                Debug.Log("Character left the trigger, defusing stopped.");
            }
        }
    }

    private IEnumerator DefuseTimer(GameObject character)
    {
        

        while (elapsedTime < defuseTime)
        {
            if (!character.GetComponent<Collider>().bounds.Intersects(GetComponent<Collider>().bounds))
            {
                yield break;
            }

            elapsedTime += Time.deltaTime;
            sliderHP.value = elapsedTime;
            yield return null;
        }

        // ћина обезврежена
        Debug.Log("Mine defused successfully!");
        // «десь вы можете добавить логику дл€ удалени€ мины или других действий
        //Destroy(gameObject);
        AfterDefusing();
    }
    private void AfterDefusing()
    {
        red.SetActive(false);
        green.SetActive(true);

        isDefused = true;

        ArrowPointer.Instance.StopArrowCoroutine(gameObject);
        MissionDefuseMines.Instance.DefuseMine(gameObject);
    }
    public void Explode()
    {
        mineBody.SetActive(false);
        explode.SetActive(true);
        ArrowPointer.Instance.StopArrowCoroutine(gameObject);
    }
}
