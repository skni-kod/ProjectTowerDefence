using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SourceSystem : MonoBehaviour
{
    [System.Serializable]
    public struct source
    {
        //name for source
        [SerializeField]
        private string name;
        //amount for source and maxAmount for source like supply
        [SerializeField]
        private int amount,maxAmount;
         //text to show amount and maxAmount
        [SerializeField]
        private Text UI;
        public source(string _name, int _amount, Text _UI,int _maxAmount=0)
        {
            maxAmount = _maxAmount;
            name = _name;
            amount = _amount;
            UI = _UI;
            UI.text = name + ": " + amount.ToString();
        }
        //update amount,can increase or decrease 
        public void updateAmount(int _amount)
        {
            amount += _amount;
            if (amount < 0) { amount = 0; }
            if(amount>maxAmount && maxAmount != 0) { amount = maxAmount; }
            UI.text = name + ": " + amount.ToString();
            if (maxAmount != 0) { UI.text += " / " + maxAmount.ToString(); }
        }
        //update maxAmont for source like supply
        public void updateMaxAmount(int _maxAmount)
        {
            maxAmount += _maxAmount;
            if (maxAmount < 1) { maxAmount = 1; }
            updateAmount(0);
        }
        public int getAmount()
        {
            return amount;
        }
        public int getMaxAmount()
        {
            return maxAmount;
        }
    }
    [Header("Resources")]
    public source Gold;
    public source Wood;
    public source Mana;
    public source Supply; 
    [Header("supply=(V button is -1 ,B button is +1 to amount and X button is -1 ,C button is +1 to maxAmount),")]
    [Header("For test ,gold=(g button is -1 ,h button is +1),")]
   
    public bool test;
    private void Start()
    {
        Gold.updateAmount(0);
        Wood.updateAmount(0);
        Mana.updateAmount(0);
        Supply.updateAmount(0);
    }
    public void Update()
    {
        if (test)
        {
            if (Input.GetKeyDown(KeyCode.G)) { Gold.updateAmount(-1); }
            if (Input.GetKeyDown(KeyCode.H)) { Gold.updateAmount(1); }
            if (Input.GetKeyDown(KeyCode.V)) { Supply.updateAmount(-1); }
            if (Input.GetKeyDown(KeyCode.B)) { Supply.updateAmount(1); }
            if (Input.GetKeyDown(KeyCode.X)) { Supply.updateMaxAmount(-1); }
            if (Input.GetKeyDown(KeyCode.C)) { Supply.updateMaxAmount(1); }

        }
    }
}
