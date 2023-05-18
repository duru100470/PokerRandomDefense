using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PokerRandomDefense.Infrastructure
{
    public class Data<T>
    {
        private T data = default;
        public delegate void DataEventHandler(T oldData, T newData);
        public event DataEventHandler OnValueChanged;

        public Data() { }
        public Data(T data) => this.data = data;

        public T Value
        {
            get => data;
            set
            {
                var temp = data;
                data = value;
                OnValueChanged?.Invoke(temp, data);
            }
        }

        public void Notify()
        {
            OnValueChanged?.Invoke(default, data);
        }
    }
}