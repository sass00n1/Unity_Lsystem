using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace StartFramework.GamePlay.LSystem
{
    public class Turtle : MonoBehaviour
    {
        private string todo;
        private Vector3 position;
        private Quaternion quaternion;
        private Vector3 posDelta;
        private Vector3 angleDelta;
        private Stack<TurtleTransform> stack = new Stack<TurtleTransform>();

        private struct TurtleTransform
        {
            public Vector3 Position;
            public Quaternion Quaternion;
            public TurtleTransform(Vector3 position, Quaternion quaternion)
            {
                Position = position;
                Quaternion = quaternion;
            }
        }

        public void TurtleInit(string todo, Vector3 position, Vector3 posDelta, Vector3 angleDelta)
        {
            this.todo = todo;
            this.position = position;
            this.posDelta = posDelta;
            this.angleDelta = angleDelta;
        }

        public void Draw()
        {
            char[] chars = todo.ToCharArray();

            IEnumerator enumerator()
            {
                for (int i = 0; i < chars.Length; i++)
                {
                    char c = chars[i];
                    if (c == 'F')
                    {
                        Vector3 delta = quaternion * posDelta;
                        Debug.DrawLine(position, position + delta, Color.green, 100);
                        position += delta;
                        yield return null;
                    }
                    else if (c == '+')
                    {
                        quaternion = Quaternion.Euler(quaternion.eulerAngles + angleDelta);
                    }
                    else if (c == '-')
                    {
                        quaternion = Quaternion.Euler(quaternion.eulerAngles - angleDelta);
                    }
                    else if (c == '[')
                    {
                        stack.Push(new TurtleTransform(position, quaternion));
                    }
                    else if (c == ']')
                    {
                        TurtleTransform turtleTransform = stack.Pop();
                        position = turtleTransform.Position;
                        quaternion = turtleTransform.Quaternion;
                    }
                    else if (c == 'X')
                    {
                        //不做任何事情
                    }
                }
            }

            StartCoroutine(enumerator());
        }

    }
}