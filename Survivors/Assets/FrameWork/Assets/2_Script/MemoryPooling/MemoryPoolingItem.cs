using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * MemoryPoolingItem
 * 2022-06-02
 * kij
 */

public class MemoryPoolingItem<T> where T : MonoBase
{
    public int resKey;
    public T item;
    
    public MemoryPoolingItem(int _resKey, T _item)
    {
        resKey = _resKey;
        item = _item;
    }
}
