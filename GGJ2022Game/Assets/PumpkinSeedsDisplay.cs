using TMPro;
using UnityEngine;

namespace Nidavellir
{
    public class PumpkinSeedsDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_text;

        public void SetPumpkinSeedCount(int count)
        {
            this.m_text.text = count.ToString();
        }
    }
}
