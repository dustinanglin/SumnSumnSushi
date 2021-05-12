using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEDNum : MonoBehaviour
{
    private List<MeshRenderer> LEDs;

    [SerializeField]
    private Material offMat, onMat;

    [SerializeField]
    private int testnum = 0;

    [SerializeField]
    private bool doRandom = false;

    [SerializeField]
    private float randomDelay = 0.1f;

    private bool randStarted = false;

    private enum Digit
    {
        Zero = 119, //1110111
        One = 65, //1000001
        Two = 110, //1101110
        Three = 107, //1101011
        Four = 89, //1011001
        Five = 59, //0111011
        Six = 63, //0111111
        Seven = 97, //1100001
        Eight = 127, //1111111
        Nine = 121 //1111001
    }

    int[] digits = new int[] { (int)Digit.Zero, (int)Digit.One, (int)Digit.Two, (int)Digit.Three, (int)Digit.Four, (int)Digit.Five, (int)Digit.Six, (int)Digit.Seven, (int)Digit.Eight, (int)Digit.Nine };

    // Start is called before the first frame update
    void Start()
    {
        LEDs = new List<MeshRenderer>();

        foreach (MeshRenderer renderer in GetComponentsInChildren<MeshRenderer>())
        {
            renderer.material = offMat;
            LEDs.Add(renderer);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doRandom)
        {
            if (!randStarted)
            {
                StartCoroutine(DoRandomDisplay());
                randStarted = true;
            }
        }
        else
        {
            if (randStarted)
            {
                StopAllCoroutines();
                randStarted = false;
            }
            SetDigit(testnum);
        }

    }

    public void SetDigit(int digit)
    {
        digit = Mathf.Clamp(digit, 0, 9);
        SetNumValue(digits[digit]);
    }

    private void SetNumValue(int num)
    {
        BitArray b = new BitArray(new int[] { num });

        for(int i = 0; i < LEDs.Count; i++)
        {
            LEDs[i].material = b[i] ? onMat : offMat;
        }
    }


    IEnumerator DoRandomDisplay()
    {
        for(; ;)
        {
            int randomNum = Random.Range(0, 128);
            SetNumValue(randomNum);
            Debug.Log("Displaying random: " + randomNum);
            yield return new WaitForSeconds(randomDelay);
        }
    }
    
}
