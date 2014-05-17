using Haskellable.Code.Monads.HeadTailList;
using Haskellable.Code.Monads.Either;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Haskellable.Code.Monads.Maybe;
using Haskellable.Code.Monoid;


namespace Haskellable.SandBox
{

    public class Any : Monoid<bool>
    {
        public Any(bool v) : base(v)
        {

        }

        public override IMonoid<Monoid<bool>, bool> Empty()
        {
            return new Any(false);
        }

        public override IMonoid<Monoid<bool>, bool> Append(IMonoid<Monoid<bool>, bool> item)
        {
            return new Any( this.Value || item.Value );
        }
    }

    class First<T> : Monoid<IMaybe<T>>
    {
        public First() : base()
        {
        }

        public First(IMaybe<T> v) : base(v)
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

    class Lope : Monoid<IMaybe<Tuple<int,int>>>
    {
        public Lope() : base()
        {
        }

        public Lope(IMaybe<Tuple<int,int>> v) : base(v)
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





    class Program
    {
        static void Main(string[] args)
        {
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

            var a2 =
                ano.suji
                .ToCaseOf()
                .Match((int x) => x)
                .Match((string x) => 0)
                .Return(_ => int.MaxValue);

            var a3 =
                ano
                .ToGuards()
                .Where(a => new { a.suji, a.moji, other = a.suji + a.moji })
                .When(
                    a => char.IsWhiteSpace(a.moji)
                    , a => '*')
                .When(
                    a => a.suji == 0
                    , a => '8')
                .Return(a => (char)a.other);

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


            var a13Exists =
                Fn.New<IMaybe<int>, Func<int, bool>, IMaybe<bool>>((m, predicate) => MaybeFunctor.Select(m, x => predicate(x)));

            var a14 =
                Fn.New((int x) => x.ToString()).ApplyMaybe(5.ToMaybe());

            var mf = Fn.New((int x, int y) => (x + y).ToString())
                        .ApplyMaybe(1.ToMaybe())
                        .ApplyMaybe(4.ToMaybe());

            var a16 = Fn.New<int, int, int>((x, y) => x + y);
            var a17 = a16.ApplyMaybe( 5.ToMaybe());
            var a18 = a17.ApplyMaybe( 6.ToMaybe());
            var a19 = a18
                        .Apply((int x, string y) => x + y)
                        .ApplyMaybe("unko".ToMaybe());

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


            var query4 =
                10.ToGuards()
                .Where(a=> new{ moji = a.ToString(), suji = a })
                .When(a =>  a.suji < 5, _ => "unko")
                .When(a => a.suji < 6, a => (a.suji * 2).ToString())
                .Return(_ => "otherwise");

            var query5 =
                "".ToCaseOf()
                .Match((MyClass a) => a.Int)
                .Match((int a) => a)
                .Return(_ => 99);

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
                .ToJoinable()
                .Next(a => a.ToString())
                .Next(int.Parse)
                .Return(5);

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
                .Match((ILeft<int> l) => Fn.Act(() => Console.WriteLine(l)))
                .Match((IRight<string> r) => Fn.Act(() => Console.WriteLine(r)))
                .Return(() => Console.WriteLine("error"));
            query7();

            Console.WriteLine(query4);

            Console.ReadLine();
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
}
