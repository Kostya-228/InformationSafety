﻿using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;


public class MessageBox : MonoBehaviour
{
    [SerializeField] private GameObject _question;
    [SerializeField] private GameObject _message;
    [SerializeField] private GameObject _saveBar;

    private string answer = "";

    /// <summary>
    /// Спросить что то
    /// </summary>
    /// <param name="qestion">Вопрос</param>
    /// <param name="yes"></param>
    /// <param name="no"></param>
    /// <returns></returns>
    public async Task<bool> Question(string qestion, string yes = "Да", string no = "Нет")
    {
        _question.SetActive(true);
        _question.transform.Find("Text").GetComponent<Text>().text = qestion;
        _question.transform.Find("ButtonYes").Find("Text").GetComponent<Text>().text = yes;
        _question.transform.Find("ButtonNo").Find("Text").GetComponent<Text>().text = no;
        while (answer == "") { await Task.Yield(); }
        _question.SetActive(false);
        switch (answer)
        {
            case "yes": releseAnswer();  return true;
            case "no": releseAnswer();  return false;
            default: releseAnswer();  throw new System.Exception("некорректный ответ");
        }
    }
    /// <summary>
    /// Вывести сообщение
    /// </summary>
    /// <param name="message"></param>
    /// <param name="ok"></param>
    /// <returns></returns>
    public async Task Message(string message, string ok = "Ок")
    {
        _message.SetActive(true);
        _message.transform.Find("Text").GetComponent<Text>().text = message;
        _message.transform.Find("ButtonOk").Find("Text").GetComponent<Text>().text = ok;
        while (answer == "") { await Task.Yield(); }
        releseAnswer();
        _message.SetActive(false);
    }
    /// <summary>
    /// Запустить анимацию сохранения
    /// </summary>
    public async Task SaveAnim()
    {
        _saveBar.SetActive(true);
        Animation saveBarAnim = _saveBar.GetComponent<Animation>();
        saveBarAnim.Play("UpAndDown");
        _saveBar.transform.Find("Bar").GetComponent<Animation>().Play("Rotate");
        while (saveBarAnim.isPlaying) { await Task.Yield(); }
        _saveBar.SetActive(false);
    }
    /// <summary>
    /// Сбросить ответ
    /// </summary>
    private void releseAnswer()
    {
        answer = "";
    }
    /// <summary>
    /// Метод приема ответа от кнопок
    /// </summary>
    /// <param name="answer"></param>
    public void Answer(string answer)
    {
        this.answer = answer;
    }

    
}
