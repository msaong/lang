// Example 215 from page 177 of C# Precisely, 2nd ed. (MIT Press 2012)
// Authors: Peter Sestoft (sestoft@itu.dk) and Henrik I. Hansen

using System;

public class Log<T> {
  private const int SIZE = 5;
  public static int InstanceCount { get; private set; }
  public int Count { get; private set; }
  private T[] log = new T[SIZE];
  public Log() { InstanceCount++; }
  public void Add(T msg) { log[Count++ % SIZE] = msg; }
  public T Last {
    get { // Return the last log entry, or null if nothing logged yet
      return Count==0 ? default(T) : log[(Count-1)%SIZE];
    }
    set { // Update the last log entry, or create one if nothing logged yet 
      if (Count==0)
        log[Count++] = value;
      else
        log[(Count-1)%SIZE] = value;
    }
  }    
  public T[] All {
    get {
      int size = Math.Min(Count, SIZE);
      T[] res = new T[size];
      for (int i=0; i<size; i++)
        res[i] = log[(Count-size+i) % SIZE];
      return res;
    }
  }
}

class TestLog {
  class MyTest {
    public static void Main(String[] args) {
      Log<String> log1 = new Log<String>();
      log1.Add("Reboot");
      log1.Add("Coffee");
      Log<DateTime> log2 = new Log<DateTime>();
      log2.Add(DateTime.Now);
      log2.Add(DateTime.Now.AddHours(1));
      DateTime[] dts = log2.All;
      // Printing both logs:
      foreach (String s in log1.All) 
	Console.Write("{0}   ", s);
      Console.WriteLine();
      foreach (DateTime dt in dts) 
	Console.Write("{0}   ", dt);
      Console.WriteLine();
      TestPairLog();
    }
    
    public static void TestPairLog() {
      Log<Pair<DateTime,String>> log = new Log<Pair<DateTime,String>>();
      log.Add(new Pair<DateTime,String>(DateTime.Now, "Tea leaves"));
      log.Add(new Pair<DateTime,String>(DateTime.Now.AddMinutes(2), "Hot water"));
      log.Add(new Pair<DateTime,String>(DateTime.Now.AddMinutes(7), "Ready"));
      Pair<DateTime,String>[] allMsgs = log.All;
      foreach (Pair<DateTime,String> p in allMsgs) 
	Console.WriteLine("At {0}: {1}", p.Fst, p.Snd);
    }
  }
}

public struct Pair<T,U> {
  public readonly T Fst;
  public readonly U Snd;
  public Pair(T fst, U snd) {
    this.Fst = fst; 
    this.Snd = snd;
  }
}
