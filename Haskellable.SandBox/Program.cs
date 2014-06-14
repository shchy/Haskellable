using Haskellable.Code.Monads.HeadTailList;
using Haskellable.Code.Monads.Either;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haskellable.Code.Monads.Maybe;
using Haskellable.Code.Monoid;
using Haskellable.Code.Functor;
using Haskellable.Code.Monads.State;

namespace Haskellable.SandBox
{
    class Program
    {
        static IState<int,short> GetRandom()
        {
            var s = StateApplicative.Get<int>();
            return s.SelectMany(x =>
            {
                var rand = NextShort(x);
                var setState = StateApplicative.Put(rand.Item2);
                return setState.SelectMany(_ =>
                    new State<int, short>(
                        a => Tuple.Create(rand.Item2, rand.Item1))
                );
            });
        }

        private static Tuple<short,int> NextShort(int seed)
        {
            return Tuple.Create((short)++seed, seed);
        }

        static IState<IEnumerable<int>, int> Pop()
        {
            return new State<IEnumerable<int>, int>(
                    s => Tuple.Create(s.Skip(1),s.First()));
        }

        static IState<IEnumerable<int>, Unit> Push(int v)
        {
            return new State<IEnumerable<int>, Unit>(
                s => Tuple.Create(new[]{v}.Concat(s), Unit.Value));
        }

        //static IState<TError,TReturn> ERun<TReturn,TError>(Func<TReturn> func, TError err)
        //{
        //    var f = Fn.New((TError s) =>
        //        {
        //            var query =
        //                from r in func.ToExceptional()
        //                select Tuple.Create(s, r);
        //            return query
        //                .Return(() => Tuple.Create(err, default(TReturn)));
        //        });
        //    return new State<TError, TReturn>(f);
        //}

        static IEither<TError, TValue> ERun<TError, TValue>(Func<TValue> func, TError err)
        {
            var ei = func.ToExceptional();
            return
                ei.IsLeft
                ? err.ToLeft().ToEither<TValue>()
                : ei.Return(() => default(TValue)).ToRight().ToEither<TError>();
        }

        static string ToS(int v)
        {
            return v.ToString();
        }

        static int ToI(string v)
        {
            return int.Parse(v);
        }

        static void Main(string[] args)
        {
            var sss =
                from s1 in Fn.New(() => ToS(12)).ToExceptional("Error01")
                from s2 in Fn.New(() => { throw new Exception(); return 1; }).ToExceptional("Error02")
                from s3 in Fn.New(() => ToI(s1)).ToExceptional("Error03")
                select s3;

            sss.OnLeft(ex => Console.WriteLine(ex.Message));
            sss.OnRight(Console.WriteLine);


            sss =
                from s1 in Fn.New(() => ToS(12)).ToExceptional("Error01")
                from s3 in Fn.New(() => ToI(s1)).ToExceptional("Error03")
                select s3;

            sss.OnLeft(Console.WriteLine);
            sss.OnRight(Console.WriteLine);


            var state = GetRandom()
                        .SelectMany(
                        x => GetRandom()
                        .SelectMany(
                        y => GetRandom()
                        .SelectMany(
                        z => (x + y + z).ToState<int, int>())));

            var res = state.Run(0);

            var stateQuery =
                from x in GetRandom()
                from y in GetRandom()
                from z in GetRandom()
                select x + y + z;

            var aaaa = stateQuery.Run(99);
           
            var stack =
                from s1 in Push(1)
                from s2 in Push(5)
                from s3 in Pop()
                from s4 in StateApplicative.Get<IEnumerable<int>>() 
                select s3;

            var stackRes = stack.Run(Enumerable.Empty<int>());









            var eitherTest =
                from a in 1.ToRight<string, int>()
                from b in "un".ToRight<string, string>()
                select DateTime.Now;

            var bai = Fn.New((int x) => x * 2);
            var bai3 = bai
                        .Join(bai)
                        .Join(bai);
            Console.WriteLine(bai3(2));

            var bools = new[]{false, false, false,false,false};
            var anys = bools.Select(x => new Any(x));

            var any = new Any(false);
            var isAny = any.Concat(anys);


            var may = new First<bool>();
            var may2 = new First<bool>(true.ToMaybe());
            var may3 = may.Append(may2);

            var lope = new Lope(Tuple.Create(0,0).ToMaybe());
            var lope2 = lope.Append(new Lope(Tuple.Create(10, 0).ToMaybe()));    

            var ano = new
            {
                suji = 9,
                moji = 'q'
            };

            ano.ToMaybe().Select(x => x.suji.ToString());

            var a1 =
                from a in ano.ToMaybe()
                where a.suji < 10
                select a;

            
            var a4 =
                ano
                .ToLeft()
                .ToEither<int>()
                .OnLeft(a => Console.WriteLine(a))
                .OnRight(right => Console.WriteLine(right));

            var a5 =
                ano.suji
                .ToLeft().ToEither<double>()
                .ToCaseOf()
                .Match((IRight<double> right) => right.ToString())
                .Match((ILeft<int> left) => left.ToString())
                .Return(_ => _.ToString());

            var a6 =
                Fn.New(() => { throw new Exception(); return 1; })
                .ToExceptional()
                .OnRight(Console.WriteLine)
                .OnLeft(ex =>
                    ex.ToCaseOf()
                    .Match((ArgumentException e) => "arg")
                    .Match((SystemException e) => "sys")
                    .Return("err"));

            var a7 =
                Fn.New(() => { throw new ArgumentException(); return 1; })
                .ToExceptional()
                .ToCaseOf((ArgumentException e) => "arg")
                .Match((SystemException e) => "sys")
                .Match((int right) => "right")
                .Return("err");

            var a8 =
                from ei in ano.suji.ToLeft().ToEither<string>()
                select ei + "unko";

            var a9 =
                from ei in ano.moji.ToRight().ToEither<object>()
                select ei + "unko";


            var a10 =
                from m in 1.ToMaybe()
                where false
                select m;

            var a11 =
                from m in 2.ToMaybe()
                where false
                select m;

            var a12 =
                a10.Concat(a11).FirstOrDefault();


            
           
            var a20 =
                from a in ano.ToMaybe()
                from b in a.moji.ToMaybe()
                from c in 3.ToMaybe()
                select a.moji + b + c;

            var a21 =
                from a in ano.ToMaybe()
                from b in 1.ToMaybe()
                select a.suji + b;

            var a22 = 1.ToMaybe().Select(x => x.ToMaybe());
            var a23 = a22.SelectMany();
            var a24 = a22.SelectMany(x => 1);
            var a25 = 1.ToMaybe().SelectMany(x => x.ToMaybe());

            var obj = new MyClass{ Int = 9, Moji = "unko" };

            var intMaybe =
                from m in 9.ToMaybe()
                from m2 in 8.ToMaybe()
                select m + m2;

            var query =
                from a in obj.ToMaybe()
                where a.Int > 0
                select a;

            var query2 =
                query.Select(x => x.Int);

            var query3 =
                from i in intMaybe
                let a = i * 2
                from q1 in query
                from q2 in query2
                select i + q1.Int + q2;

            query
                .On(Console.WriteLine)
                .Or(() => Console.WriteLine("none"));
            Console.WriteLine(query3);

            var array = new[] { 9, 8, 7, 6, 5, 4, 3, 2, 1 };
            var aaa = array.ToHeadTailList()
                        .Recursion(
                            0
                            , x => x
                            , (a, b) => a + b);


            var scanResult =
                array.Scan(0, (acc, value) => acc + value, acc => acc);

            var query6 =
                Fn.New((int a) => a * 2)
                .Join(a => a.ToString())
                .Join(int.Parse)(5);

            var either = GetLeft();

            either
                .OnLeft(Console.WriteLine)
                .OnRight(Console.WriteLine);

            either = GetRight();

            either
                .OnLeft(Console.WriteLine)
                .OnRight(Console.WriteLine);

            var query7 =
                either
                .ToCaseOf()
                .Match((ILeft<int> l) => Act.New(() => Console.WriteLine(l)))
                .Match((IRight<string> r) => Act.New(() => Console.WriteLine(r)))
                .Return(() => Console.WriteLine("error"));
            query7();

            var doubleMe = Fn.New((double x) => x.ToGuards()
                                                .Where(a => new { dbl = a * 2, a })
                                                .When(a => a.dbl > 100, _ => 100.0)
                                                .Return(a => a.dbl));

            var sa = "sasa";
            var queryASsdas =
                from r1 in Fn.New(() => Test01(sa)).ToExceptional()
                from r2 in Fn.New(() => Test02(r1)).ToExceptional()
                select r1 + r2;

            Console.ReadLine();
        }


        static int Test01(string suji)
        {
            return int.Parse(suji);
        }

        static string Test02(int suti)
        {
            return suti.ToString();
        }

        private static IEither<int, string> GetRight()
        {
            return "right".ToRight<int,string>();
        }

        private static IEither<int, string> GetLeft()
        {
            var a = 9.ToLeft();
            return a.ToEither<string>();
        }
    }

    class MyClass
    {
        public int Int { get; set; }
        public string Moji { get; set; }
    }


    public class Any : Monoid<bool>
    {
        public Any(bool v)
            : base(v)
        {

        }

        public override IMonoid<Monoid<bool>, bool> Empty()
        {
            return new Any(false);
        }

        public override IMonoid<Monoid<bool>, bool> Append(IMonoid<Monoid<bool>, bool> item)
        {
            return new Any(this.Value || item.Value);
        }
    }

    class First<T> : Monoid<IMaybe<T>>
    {
        public First()
            : base()
        {
        }

        public First(IMaybe<T> v)
            : base(v)
        {
        }

        public override IMonoid<Monoid<IMaybe<T>>, IMaybe<T>> Empty()
        {
            return new First<T>(new Nothing<T>());
        }

        public override IMonoid<Monoid<IMaybe<T>>, IMaybe<T>> Append(IMonoid<Monoid<IMaybe<T>>, IMaybe<T>> item)
        {
            return
                new First<T>(this.Value.Concat(item.Value).FirstOrMaybe());
        }
    }

    class Lope : Monoid<IMaybe<Tuple<int, int>>>
    {
        public Lope()
            : base()
        {
        }

        public Lope(IMaybe<Tuple<int, int>> v)
            : base(v)
        {
        }

        public override IMonoid<Monoid<IMaybe<Tuple<int, int>>>, IMaybe<Tuple<int, int>>> Empty()
        {
            return new Lope(new Nothing<Tuple<int, int>>());
        }

        public override IMonoid<Monoid<IMaybe<Tuple<int, int>>>, IMaybe<Tuple<int, int>>> Append(IMonoid<Monoid<IMaybe<Tuple<int, int>>>, IMaybe<Tuple<int, int>>> item)
        {
            var query =
                from x in this.Value
                from y in item.Value
                let l = Tuple.Create(x.Item1 + y.Item1, x.Item2 + y.Item2)
                where Math.Abs(l.Item1 - l.Item2) < 5
                select l;
            return new Lope(query);
        }
    }


}
