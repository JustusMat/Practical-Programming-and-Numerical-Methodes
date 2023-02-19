public class genlistlinkednotes<T>
{
    public node<T> first = null;
    public node<T> current = null;

    
    public void add(T item)
    {
        if (first == null)
        {
            first = new node<T>(item);
            current = first;
        }
        else
        {
            current.next = new node<T>(item);
            current = current.next;
        }
        
    }
    public void start()
    {
        current = first;
    }

    public void next()
    {
        current = current.next;
    }
}

public class node<T>
{
    public T item;
    public node<T> next;

    public node(T item)
    {
        this.item = item;
    }
    
}