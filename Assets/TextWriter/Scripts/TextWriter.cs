using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextWriter : MonoBehaviour {

    private static TextWriter instance;

    private List<TextWriterSingle> textWriterSingleList;

    private void Awake() {
        instance = this;
        textWriterSingleList = new List<TextWriterSingle>();
    }

    public static TextWriterSingle AddWriter_Static(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, bool removeWriterBeforeAdd, Action onComplete) {
        if (removeWriterBeforeAdd) {
            instance.RemoveWriter(uiText);
        }
        return instance.AddWriter(uiText, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
    }

    private TextWriterSingle AddWriter(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete) {
        TextWriterSingle textWriterSingle = new TextWriterSingle(uiText, textToWrite, timePerCharacter, invisibleCharacters, onComplete);
        textWriterSingleList.Add(textWriterSingle);
        return textWriterSingle;
    }

    public static void RemoveWriter_Static(TMP_Text uiText) {
        instance.RemoveWriter(uiText);
    }

    private void RemoveWriter(TMP_Text uiText) {
        for (int i = 0; i < textWriterSingleList.Count; i++) {
            if (textWriterSingleList[i].GetUIText() == uiText) {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    private void Update() {
        for (int i = 0; i < textWriterSingleList.Count; i++) {
            bool destroyInstance = textWriterSingleList[i].Update();
            if (destroyInstance) {
                textWriterSingleList.RemoveAt(i);
                i--;
            }
        }
    }

    /*
     * Represents a single TextWriter instance
     * */
    public class TextWriterSingle {

        private TMP_Text uiText;
        private string textToWrite;
        private int characterIndex;
        private float timePerCharacter;
        private float timer;
        private bool invisibleCharacters;
        private Action onComplete;

        public TextWriterSingle(TMP_Text uiText, string textToWrite, float timePerCharacter, bool invisibleCharacters, Action onComplete) {
            this.uiText = uiText;
            this.textToWrite = textToWrite;
            this.timePerCharacter = timePerCharacter;
            this.invisibleCharacters = invisibleCharacters;
            this.onComplete = onComplete;
            characterIndex = 0;
        }

        // Returns true on complete
        public bool Update()
        {
            timer -= Time.deltaTime;
            while (timer <= 0f)
            {
                // Display next character or color tag
                timer += timePerCharacter;

                // Se il prossimo carattere è un tag di colore
                if (textToWrite[characterIndex] == '<')
                {
                    int endIndex = textToWrite.IndexOf('>', characterIndex);
                    int length = endIndex - characterIndex + 1;
                    string tag = textToWrite.Substring(characterIndex, length);
                    uiText.text += tag;
                    characterIndex += length;
                }
                else
                {
                    // Altrimenti, visualizza il prossimo carattere
                    uiText.text += textToWrite[characterIndex];
                    characterIndex++;
                }

                if (characterIndex >= textToWrite.Length)
                {
                    // Entire string displayed
                    if (onComplete != null) onComplete();
                    return true;
                }
            }

            return false;
        }


        public TMP_Text GetUIText() {
            return uiText;
        }

        public bool IsActive() {
            return characterIndex < textToWrite.Length;
        }

        public void WriteAllAndDestroy() {
            uiText.text = textToWrite;
            characterIndex = textToWrite.Length;
            if (onComplete != null) onComplete();
            TextWriter.RemoveWriter_Static(uiText);
        }


    }


}
