using System;
using UnityEngine;
using UnityEngine.UI;

namespace EventsActionsBestPractices
{
    public class KeysUIController : MonoBehaviour
    {
        [SerializeField]
        Button creditKeysButton;
        [SerializeField]
        Button deductKeysButton;
        [SerializeField]
        Text keysText;

        private void Start()
        {
            creditKeysButton.onClick.AddListener(CreditKeys);
            deductKeysButton.onClick.AddListener(DeductKeys);
        }
        private void OnEnable()
        {
            CurrencyHandler.RegisterOnKeysChangeEvent(UpdateKeysText, true);
        }

        private void OnDisable()
        {
            creditKeysButton.onClick.RemoveAllListeners();
            deductKeysButton.onClick.RemoveAllListeners();
            CurrencyHandler.UnRegisterOnKeysChangeEvent(UpdateKeysText);
        }

        private void CreditKeys()
        {
            CurrencyHandler.CreditKeys(1);
        }

        private void DeductKeys()
        {
            CurrencyHandler.DeductKeys(1);
        }

        private void UpdateKeysText(int keysAmount)
        {
            if(keysAmount <= 0)
            {
                keysText.text = "no more keys";
            }
            else
            {
                keysText.text = keysAmount.ToString();
            }
        }
    }
}

