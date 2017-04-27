``` ini

BenchmarkDotNet=v0.10.5, OS=Windows 10.0.14393
Processor=Intel Core i7-5820K CPU 3.30GHz (Broadwell), ProcessorCount=12
Frequency=3220787 Hz, Resolution=310.4831 ns, Timer=TSC
dotnet cli version=1.0.3
  [Host]     : .NET Core 4.6.25009.03, 64bit RyuJIT
  DefaultJob : .NET Core 4.6.25009.03, 64bit RyuJIT


```
 |          Method |     Mean |     Error |    StdDev |      Min |      Max |
 |---------------- |---------:|----------:|----------:|---------:|---------:|
 | TriangleInvert1 | 10.29 ms | 0.2058 ms | 0.2370 ms | 9.947 ms | 10.76 ms |
