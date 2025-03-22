// See https://aka.ms/new-console-template for more information

using System;
using System.Threading;
using System.Threading.Tasks;

public class FooBar {
    object _lock = new object();
    private int n;
    bool isFirst = true;

    public FooBar(int n) {
        this.n = n;
    }

    public void Foo(Action printFoo) 
    {
        lock(_lock)
        {
            for (int i = 0; i < n; i++) 
            {
                // printFoo() outputs "foo". Do not change or remove this line.
                while(isFirst!= true)
                {
                    Monitor.Wait(_lock);
                }
                printFoo();
                isFirst = false;
                Monitor.PulseAll(_lock);
            }
        
        }
    }

    public void Bar(Action printBar) {
        lock(_lock){
            for (int i = 0; i < n; i++) {
                while(isFirst!= false) {
                    Monitor.Wait(_lock);
                }
                // printBar() outputs "bar". Do not change or remove this line.
                printBar();
                isFirst = true;
                Monitor.PulseAll(_lock);
            }
        }
    }
}