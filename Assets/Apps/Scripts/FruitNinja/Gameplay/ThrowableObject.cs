using UnityEngine;

namespace FruitNinja.Gameplay
{
    public abstract class ThrowableObject : MonoBehaviour
    {
        protected Rigidbody rigidbody;

        public event System.Action<SliceInfo> OnSliced;

        private void Awake() {
            rigidbody = GetComponent<Rigidbody>();
        }
        private void Update() {
            ClickHandler();
        }
        private void OnEnable() {
            OnSliced += Sliced;
        }

        private void OnDisable() {
            OnSliced -= Sliced;
        }

        private void Sliced(SliceInfo sliceInfo) {
            Hide();
        }

        private void ClickHandler() {
            if (Input.GetMouseButton(0)) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100)) {
                    if (hit.transform == transform) {
                        OnSliced?.Invoke(new SliceInfo(this));
                    }
                }
            }
        }

        internal void Throw(float power) {
            rigidbody.velocity = GetDirectionVariant(0.2f) * power;
            rigidbody.angularVelocity = GetRandomAngularVelocity();
        }

        private Vector3 GetRandomAngularVelocity() {
            return new Vector3(RandomFloat(1f), RandomFloat(1f), RandomFloat(1f));
        }

        private float RandomFloat(float v) {
            return Random.Range(-v, v);
        }

        internal void Init(Transform transform) {
            this.transform.position = transform.position;
            this.transform.rotation = transform.rotation;
        }

        private Vector3 GetDirectionVariant(float xRange) {
            return new Vector3(Random.Range(-xRange, xRange), 1, 0);
        }

        public void Show() {
            gameObject.SetActive(true);
        }
        public void Hide() {
            gameObject.SetActive(false);
        }
    }
}
