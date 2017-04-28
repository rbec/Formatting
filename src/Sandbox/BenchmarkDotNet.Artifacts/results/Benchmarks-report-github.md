``` ini

BenchmarkDotNet=v0.10.5, OS=Windows 10.0.14393
Processor=Intel Core i7-5820K CPU 3.30GHz (Broadwell), ProcessorCount=12
Frequency=3220794 Hz, Resolution=310.4824 ns, Timer=TSC
dotnet cli version=1.0.3
  [Host]     : .NET Core 4.6.25009.03, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.25009.03, 64bit RyuJIT


```
 | Method |       Mean |     Error |    StdDev |        Min |        Max |
 |------- |-----------:|----------:|----------:|-----------:|-----------:|
 |      A |   424.8 us |  8.333 us |  9.597 us |   411.1 us |   442.2 us |
 |      C | 2,816.0 us | 55.338 us | 63.727 us | 2,703.2 us | 2,952.7 us |
