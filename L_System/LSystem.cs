using UnityEngine;
using System.Collections.Generic;
using System.Text;

namespace StartFramework.GamePlay.LSystem
{
    [RequireComponent(typeof(Turtle))]
    public class LSystem : MonoBehaviour
    {
        [SerializeField] private string axiom;
        [SerializeField] private int generation;
        [SerializeField] private List<Dic> ruleset = new List<Dic>();
        [SerializeField] private Vector2 posDelta;
        [SerializeField] private Vector3 angleDelta;

        private string sentence;
        private Dictionary<string, string> rulesetData = new Dictionary<string, string>();

        [System.Serializable]
        public struct Dic
        {
            public string predecessor;  //前身
            public string successor;    //接替者
        }

        private void Start()
        {
            //准备数据
            sentence = axiom;
            for (int i = 0; i < ruleset.Count; i++)
            {
                rulesetData.Add(ruleset[i].predecessor, ruleset[i].successor);
            }
            for (int i = 0; i < generation; i++)
            {
                GenerateSentence();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                //乌龟
                Turtle turtle = GetComponent<Turtle>();
                turtle.TurtleInit(sentence, transform.position, posDelta, angleDelta);
                turtle.Draw();
            }
        }

        private void GenerateSentence()
        {
            sentence = Generate(sentence);
            //Debug.Log(sentence);
        }

        private string Generate(string oldSentence)
        {
            StringBuilder newSentence = new StringBuilder();

            char[] chars = oldSentence.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (rulesetData.TryGetValue(chars[i].ToString(), out string replacement))
                {
                    newSentence.Append(replacement);
                }
                else
                {
                    newSentence.Append(chars[i]);
                }

            }

            return newSentence.ToString();
        }
    }

}