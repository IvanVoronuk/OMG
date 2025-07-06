using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MainWindowItem : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Button button;
        
        public Image Image => image;
        public Button Button => button;
    }
}