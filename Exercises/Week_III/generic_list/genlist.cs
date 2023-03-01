public class genlist<T>
{
    public T[] Data;
    public int DoublingSize = 0, capacity = 8;
    
    
    //constructors
    public genlist(T[] data)
    {
        this.Data = data;
    }
    public genlist()
    {
        Data = System.Array.Empty<T>();
    }
    
    
    public int Size => Data.Length;
    //Needed to change the int here to index in order to call ^1 as in lists
    //public T this[System.Index i] => Data[int.Parse(i)];
    public T this[int i]
    {
        get { return Data[i];}
        set { Data[i] = value; }
    }
    
    public void add(T item)
    {
        T[] newdata = new T[Size + 1];
        System.Array.Copy(Data,newdata,Size);
        newdata[Size] = item;
        Data = newdata;

    }
    
    public void addquick(T item)
    {
        if (DoublingSize == capacity)
        {
            T[] newdata = new T[capacity*=2];
            System.Array.Copy(Data,newdata,DoublingSize);
            Data = newdata;
        }
        //System.Console.WriteLine($"{DoublingSize} {capacity}");
        Data[DoublingSize] = item;
        DoublingSize += 1;
    }
    
    public void remove(int index)
    {
        T[] newdata = new T[Size - 1];
        
        if (index > 0)
        {
            System.Array.Copy(Data,newdata,index);
        }
        if (index < Size - 1)
        {
            System.Array.Copy(Data, index+1,newdata,index,Size - index -1);
        }

        Data = newdata;
    }

    public void Removebyconverting(int index)
    {   //Actually pretty slow converting an array to a list and back 
        var newdata = new System.Collections.Generic.List<T>(Data);
        newdata.RemoveAt(index);
        Data = newdata.ToArray();
    }

    public void Print()
    {
        for (int i = 0; i < Size; i++)
        {
            System.Console.WriteLine(Data[i]);
        }
    }
}