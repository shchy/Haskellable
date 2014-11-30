Haskellable
===========

```csharp

var query =
  from m in 777.ToMaybe()
  where m % 7 == 0
  let x = m * 2
  where x < 1000
  select m;

query.On(Console.WriteLine);
query.Or(()=>Console.WriteLine("Nothing"));
var safeValue = query.Return(0);

if (query.IsSomething)
{
  Console.WriteLine("Something");
}

```



Nuget
--------
```
PM> Install-Package Haskellable
```
https://www.nuget.org/packages/Haskellable/

