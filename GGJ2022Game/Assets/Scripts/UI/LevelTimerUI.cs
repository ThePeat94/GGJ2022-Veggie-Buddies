using System;
using TMPro;
using UnityEngine;

namespace Nidavellir.UI
{
    public class LevelTimerUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_timerText;

        private void Update()
        {
            this.m_timerText.text = string.Format("{0:mm\\:ss\\.fff}", LevelTimer.Instance.PastTimeSinceStart);
        }
    }
}