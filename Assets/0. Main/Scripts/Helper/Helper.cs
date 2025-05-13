using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ReuseSystem.Helper
{
    public static class Helper
    {
        private static Camera camera;

        public static Camera Camera
        {
            get
            {
                if (camera == null) camera = Camera.main;
                return camera;
            }
        }

        private static PointerEventData eventDataCurrentPosition;
        private static List<RaycastResult> results;

        private static readonly Dictionary<float, WaitForSeconds> _waitForSecondsMap =
            new Dictionary<float, WaitForSeconds>();

        public static WaitForSeconds WaitForSeconds(float seconds)
        {
            if (!_waitForSecondsMap.ContainsKey(seconds))
                _waitForSecondsMap.Add(seconds, new WaitForSeconds(seconds));

            return _waitForSecondsMap[seconds];
        }

        public static bool IsOverUIAllDevice()
        {
            if (Input.touchCount > 0)
            {
                return EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
            }
            // For PC (mouse input)
            return EventSystem.current.IsPointerOverGameObject();
        }

        public static Vector3 GetMiddlePositionOfTheScreen()
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2));
        }

        public static void DebugDrawRectangle(Vector3 center, Vector3 size,Color color,float seconds = 60f)
        {
            //Draw the bottom
            Debug.DrawLine(center+new Vector3(-size.x/2, -size.y/2, -size.z/2), center + new Vector3(-size.x/2,-size.y/2,size.z/2),color,seconds);
            Debug.DrawLine(center + new Vector3(-size.x/2, -size.y/2, -size.z/2), center + new Vector3(size.x/2, -size.y/2, -size.z/2), color,seconds);
            Debug.DrawLine(center + new Vector3(size.x/2, -size.y/2, size.z/2), center + new Vector3(size.x/2, -size.y/2, -size.z/2), color,seconds);
            Debug.DrawLine(center + new Vector3(size.x/2, -size.y/2, size.z/2), center + new Vector3(-size.x/2, -size.y/2, size.z/2), color,seconds);

            //Draw the top
            Debug.DrawLine(center+new Vector3(-size.x/2, size.y/2, -size.z/2), center + new Vector3(-size.x/2,size.y/2,size.z/2),color,seconds);
            Debug.DrawLine(center + new Vector3(-size.x/2, size.y/2, -size.z/2), center + new Vector3(size.x/2, size.y/2, -size.z/2), color,seconds);
            Debug.DrawLine(center + new Vector3(size.x/2, size.y/2, size.z/2), center + new Vector3(size.x/2, size.y/2, -size.z/2), color,seconds);
            Debug.DrawLine(center + new Vector3(size.x/2, size.y/2, size.z/2), center + new Vector3(-size.x/2, size.y/2, size.z/2), color,seconds);
            
            //Draw the straight line
            Debug.DrawLine(center + new Vector3(-size.x/2, -size.y/2, -size.z/2), center + new Vector3(-size.x/2, size.y/2, -size.z/2), color,seconds);
            Debug.DrawLine(center + new Vector3(size.x/2, -size.y/2, -size.z/2), center + new Vector3(size.x/2, size.y/2, -size.z/2), color,seconds);
            Debug.DrawLine(center + new Vector3(-size.x/2, -size.y/2, size.z/2), center + new Vector3(-size.x/2, size.y/2, size.z/2), color,seconds);
            Debug.DrawLine(center + new Vector3(size.x/2, -size.y/2, size.z/2), center + new Vector3(size.x/2, size.y/2, size.z/2), color,seconds);


        }


        // Create Text in the World
        public static TextMeshPro CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAnchor textAnchor = TextAnchor.UpperLeft, TextAlignmentOptions textAlignment = TextAlignmentOptions.Left, int sortingOrder = 5000) {
            if (color == null) color = Color.white;
            return CreateWorldText(parent, text, localPosition, fontSize, (Color)color, textAnchor, textAlignment, sortingOrder);
        }
        
        // Create Text in the World
        public static TextMeshPro CreateWorldText(Transform parent, string text, Vector3 localPosition, int fontSize, Color color, TextAnchor textAnchor, TextAlignmentOptions textAlignment, int sortingOrder) {
            GameObject gameObject = new GameObject("World_Text", typeof(TextMeshPro));
            Transform transform = gameObject.transform;
            transform.SetParent(parent, false);
            transform.localPosition = localPosition;
            TextMeshPro textMesh = gameObject.GetComponent<TextMeshPro>();
            textMesh.alignment = textAlignment;
            textMesh.text = text;
            textMesh.fontSize = fontSize;
            textMesh.color = color;
            textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
            return textMesh;
        }
        public static Vector2 GetMousePositionInWorld()
        {
            return Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        public static bool IsOverUI()
        {
            eventDataCurrentPosition = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
            results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            return results.Count > 0;
        }

        public static Vector2 GetWorldPositionOfCanvasElement(RectTransform rect)
        {
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, rect.position, Camera, out Vector3 result);
            return result;
        }

        public static void DeleteChildren(this Transform t)
        {
            foreach (Transform child in t)
            {
                Object.Destroy(child.gameObject);
            }
        }

        /// <summary>
        /// percent is in 0 -> 1, 0 means always false, vise versa
        /// </summary>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static bool IsPercentTrigger(float percent)
        {
            return Random.Range(0f, 1f) <= percent;
        }

        public static string ToRGBHex(Color c)
        {
            return $"#{ToByte(c.r):X2}{ToByte(c.g):X2}{ToByte(c.b):X2}";
        }
        public static Color HexToColor(string hex)
        {
            hex = hex.Replace ("0x", "");//in case the string is formatted 0xFFFFFF
            hex = hex.Replace ("#", "");//in case the string is formatted #FFFFFF
            byte a = 255;//assume fully visible unless specified in hex
            byte r = byte.Parse(hex.Substring(0,2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2,2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4,2), System.Globalization.NumberStyles.HexNumber);
            //Only use alpha if the string has enough characters
            if(hex.Length == 8){
                a = byte.Parse(hex.Substring(6,2), System.Globalization.NumberStyles.HexNumber);
            }
            return new Color32(r,g,b,a);
        }

        private static byte ToByte(float f)
        {
            f = Mathf.Clamp01(f);
            return (byte)(f * 255);
        }

        private static List<string> keywords = new List<string>
        {
            "abstract", "as", "base", "bool", "break", "byte", "case", "catch", "char", "checked", "class", "const",
            "continue", "decimal", "default", "delegate", "do", "double", "else", "enum", "event", "explicit", "extern",
            "false", "finally", "fixed", "float", "for", "foreach", "goto", "if", "implicit", "in", "int", "interface",
            "internal", "is", "lock", "long", "namespace", "new", "null", "object", "operator", "out", "override",
            "params", "private", "protected", "public", "readonly", "ref", "return", "sbyte", "sealed", "short",
            "sizeof", "stackalloc", "static", "string", "struct", "switch", "this", "throw", "true", "try", "typeof",
            "uint", "ulong", "unchecked", "unsafe", "ushort", "using", "virtual", "void", "volatile", "while"
        };

        public static bool IsValidVariableName(string name)
        {
            // Check if the name is null or empty
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }

            // Check if the name starts with a letter or underscore
            if (!char.IsLetter(name[0]) && name[0] != '_')
            {
                return false;
            }

            // Check if each subsequent character is a letter, digit, or underscore
            for (int i = 1; i < name.Length; i++)
            {
                if (!char.IsLetterOrDigit(name[i]) && name[i] != '_')
                {
                    return false;
                }
            }

            // Check if the name is a C# keyword

            if (keywords.Contains(name))
            {
                return false;
            }

            // If all checks pass, the name is valid
            return true;
        }
    }
}
