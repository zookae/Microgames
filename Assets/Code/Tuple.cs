using System;

public class Tuple<T,U,V> {

	public Tuple(){
    }

    public Tuple(T first, U second, V third) {
        this.First = first;
        this.Second = second;
        this.Third = third;
    }

    public T First { get; set; }
    public U Second { get; set; }
    public V Third { get; set; }

}
//http://stackoverflow.com/questions/166089/what-is-c-sharp-analog-of-c-stdpair
