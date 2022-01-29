using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nidavellir
{
    public class MovingFloor : MonoBehaviour
    {
        [SerializeField]
        private float m_startDelay = 1;
        [SerializeField]
        private float m_duration = 1;
        [SerializeField]
        private float m_width = 8;


        private float m_startX;

        private void Awake()
        {
            this.m_startX = this.transform.position.x;
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
                yield return MoveRight();
                yield return new WaitForSeconds(2 * this.m_duration);
                yield return MoveLeft();
            }
        }

        private IEnumerator MoveRight()
        {
            while (this.transform.position.x < this.m_startX + this.m_width)
            {
                yield return new WaitForFixedUpdate();
                var x = Mathf.Clamp(this.transform.position.x + (4 * Time.fixedDeltaTime * this.m_duration), this.m_startX, this.m_startX + this.m_width);
                this.transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);
            }
            yield break;
        }

        private IEnumerator MoveLeft()
        {
            while (this.transform.position.x > this.m_startX)
            {
                yield return new WaitForFixedUpdate();
                var x = Mathf.Clamp(this.transform.position.x - (4 * Time.fixedDeltaTime * this.m_duration), this.m_startX, this.m_startX + this.m_width);
                this.transform.position = new Vector3(x, this.transform.position.y, this.transform.position.z);
            }
            yield break;
        }
    }
}
