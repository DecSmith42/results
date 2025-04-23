```

BenchmarkDotNet v0.14.0, Windows 11 (10.0.26100.3775)
Unknown processor
.NET SDK 9.0.203
  [Host]             : .NET 9.0.4 (9.0.425.16305), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET 9.0           : .NET 9.0.4 (9.0.425.16305), X64 RyuJIT AVX-512F+CD+BW+DQ+VL+VBMI
  .NET Framework 4.8 : .NET Framework 4.8.1 (4.8.9300.0), X64 RyuJIT VectorSize=256


```
| Method                         | Job                | Runtime            | Mean        | Error     | StdDev    | Median      | Gen0   | Gen1   | Allocated |
|------------------------------- |------------------- |------------------- |------------:|----------:|----------:|------------:|-------:|-------:|----------:|
| DecSm_Result_Ok                | .NET 9.0           | .NET 9.0           |   0.1794 ns | 0.0085 ns | 0.0075 ns |   0.1750 ns |      - |      - |         - |
| Fluent_Result_Ok               | .NET 9.0           | .NET 9.0           |   4.7750 ns | 0.0748 ns | 0.0699 ns |   4.7363 ns | 0.0011 |      - |      56 B |
| DecSm_Result_Fail_With_Error   | .NET 9.0           | .NET 9.0           |   4.9338 ns | 0.0786 ns | 0.0735 ns |   4.9133 ns | 0.0013 |      - |      64 B |
| Fluent_Result_Fail_With_Error  | .NET 9.0           | .NET 9.0           |  26.0369 ns | 0.2145 ns | 0.1901 ns |  26.0253 ns | 0.0052 |      - |     264 B |
| DecSm_Result_Fail_With_Errors  | .NET 9.0           | .NET 9.0           |  18.5956 ns | 0.1460 ns | 0.1365 ns |  18.6055 ns | 0.0046 |      - |     232 B |
| Fluent_Result_Fail_With_Errors | .NET 9.0           | .NET 9.0           |  76.3068 ns | 0.4245 ns | 0.3763 ns |  76.1888 ns | 0.0134 |      - |     672 B |
| DecSm_Result_Ok_Bind_Ok        | .NET 9.0           | .NET 9.0           |   2.9257 ns | 0.0234 ns | 0.0219 ns |   2.9227 ns |      - |      - |         - |
| Fluent_Result_Ok_Bind_Ok       | .NET 9.0           | .NET 9.0           |  25.3854 ns | 0.1074 ns | 0.0839 ns |  25.4163 ns | 0.0038 |      - |     192 B |
| DecSm_Result_Ok_Bind_Fail      | .NET 9.0           | .NET 9.0           |   6.7069 ns | 0.1201 ns | 0.1123 ns |   6.7034 ns | 0.0013 |      - |      64 B |
| Fluent_Result_Ok_Bind_Fail     | .NET 9.0           | .NET 9.0           |  51.6620 ns | 0.2343 ns | 0.2077 ns |  51.5884 ns | 0.0086 |      - |     432 B |
| DecSm_Complex_1                | .NET 9.0           | .NET 9.0           |  44.2672 ns | 0.6808 ns | 0.6368 ns |  44.2232 ns | 0.0092 |      - |     464 B |
| Fluent_Complex_1               | .NET 9.0           | .NET 9.0           | 202.1584 ns | 1.0481 ns | 0.8752 ns | 201.9758 ns | 0.0308 |      - |    1552 B |
| DecSm_Activator                | .NET 9.0           | .NET 9.0           |   7.6184 ns | 0.1269 ns | 0.1187 ns |   7.6395 ns | 0.0008 |      - |      40 B |
| DecSm_Materialize              | .NET 9.0           | .NET 9.0           |   3.3838 ns | 0.0614 ns | 0.0574 ns |   3.3687 ns | 0.0008 |      - |      40 B |
| DecSm_Result_Ok                | .NET Framework 4.8 | .NET Framework 4.8 |   0.0050 ns | 0.0052 ns | 0.0049 ns |   0.0027 ns |      - |      - |         - |
| Fluent_Result_Ok               | .NET Framework 4.8 | .NET Framework 4.8 |   6.3683 ns | 0.0296 ns | 0.0262 ns |   6.3774 ns | 0.0102 |      - |      64 B |
| DecSm_Result_Fail_With_Error   | .NET Framework 4.8 | .NET Framework 4.8 |   7.4726 ns | 0.0561 ns | 0.0525 ns |   7.4598 ns | 0.0102 |      - |      64 B |
| Fluent_Result_Fail_With_Error  | .NET Framework 4.8 | .NET Framework 4.8 |  30.8401 ns | 0.1433 ns | 0.1341 ns |  30.8356 ns | 0.0446 |      - |     281 B |
| DecSm_Result_Fail_With_Errors  | .NET Framework 4.8 | .NET Framework 4.8 |  28.0778 ns | 0.3903 ns | 0.3651 ns |  28.1426 ns | 0.0370 |      - |     233 B |
| Fluent_Result_Fail_With_Errors | .NET Framework 4.8 | .NET Framework 4.8 |  99.7874 ns | 0.4499 ns | 0.3988 ns |  99.6696 ns | 0.1122 | 0.0002 |     706 B |
| DecSm_Result_Ok_Bind_Ok        | .NET Framework 4.8 | .NET Framework 4.8 |   4.3884 ns | 0.0436 ns | 0.0407 ns |   4.4001 ns |      - |      - |         - |
| Fluent_Result_Ok_Bind_Ok       | .NET Framework 4.8 | .NET Framework 4.8 |  52.7131 ns | 0.1608 ns | 0.1504 ns |  52.7102 ns | 0.0459 |      - |     289 B |
| DecSm_Result_Ok_Bind_Fail      | .NET Framework 4.8 | .NET Framework 4.8 |  10.2249 ns | 0.0942 ns | 0.0835 ns |  10.2178 ns | 0.0102 |      - |      64 B |
| Fluent_Result_Ok_Bind_Fail     | .NET Framework 4.8 | .NET Framework 4.8 |  96.7252 ns | 0.3522 ns | 0.3122 ns |  96.7068 ns | 0.0943 |      - |     594 B |
| DecSm_Complex_1                | .NET Framework 4.8 | .NET Framework 4.8 |  69.1674 ns | 0.3391 ns | 0.3172 ns |  69.1804 ns | 0.0739 |      - |     465 B |
| Fluent_Complex_1               | .NET Framework 4.8 | .NET Framework 4.8 | 299.3972 ns | 1.5897 ns | 1.4870 ns | 299.1781 ns | 0.2699 | 0.0010 |    1701 B |
