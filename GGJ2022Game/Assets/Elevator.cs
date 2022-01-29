using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir
{
    public class Elevator : MonoBehaviour
    {
        [SerializeField]
        private float m_startDelay = 1;
        [SerializeField]
        private float m_duration = 1;
        [SerializeField]
        private float m_height = 8;


        private float m_startY;

        private void Awake()
        {
            this.m_startY = this.transform.position.y;
        }

        private void OnEnable()
        {
            this.StartCoroutine(this.Move());
        }

        private IEnumerator Move()
        {
            yield return new WaitForSeconds(this.m_startDelay);
            while (this.enabled)
            {
                yield return new WaitForSeconds(2 * this.m_duration);
                yield return MoveUp();
                yield return new WaitForSeconds(2 * this.m_duration);
                yield return MoveDown();
            }
        }

        private IEnumerator MoveUp()
        {
            while (this.transform.position.y < this.m_startY + this.m_height)
            {
                yield return new WaitForFixedUpdate();
                var y = Mathf.Clamp(this.transform.position.y + (2 * Time.fixedDeltaTime * this.m_duration), this.m_startY, this.m_startY + this.m_height);
                this.transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);
            }
            yield break;
        }

        private IEnumerator MoveDown()
        {
            while (this.transform.position.y > this.m_startY)
            {
                yield return new WaitForFixedUpdate();
                var y = Mathf.Clamp(this.transform.position.y - (2 * Time.fixedDeltaTime * this.m_duration), this.m_startY, this.m_startY + this.m_height);
                this.transform.position = new Vector3(this.transform.position.x, y, this.transform.position.z);
            }
            yield break;
        }
    }
}
