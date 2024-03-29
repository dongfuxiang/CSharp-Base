
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace performance
{
    //EventArgs基类不传递数据，只有使用它的派生类才可以实现数据传递
    public class TestEventArgs : EventArgs
    {
        public int MyProperty { get; set; }
    }

    public class MyBaseClass : IDisposable
    {

        #region 属性
        ///\\\ <summary>
        ///\\\ 后备字段为数据存储分配内存，属性不分配内存
        ///\\\ </summary>
        private int myProperty;
        /// <summary>
        /// 属性关联后备字段，value为set的隐式值参
        /// </summary>
        public int MyProperty
        {
            get { return myProperty; }
            set
            {
                myProperty = value > 100 ? 100 : value;
            }
        }
        /// <summary>
        /// 不关联后备字段的属性
        /// </summary>
        public int MyProperty02
        {
            get { return 2; }
        }
        /// <summary>
        ///1. 自动实现属性，自动创建切隐藏后备字段，省略set与get的大括号，用分号分隔开，类似公开字段
        ///2. 可在外部读取该属性，但只能在内部设置，非常重要的封装工具
        /// </summary>
        public int MyProperty03 { get; private set; }
        /// <summary>
        /// virtual 修饰的属性
        /// </summary>
        virtual public int VirProperty { get; private set; }
        virtual public int _VirProperty { private get; set; }
        #endregion

        #region 方法
        readonly int mm;
        readonly static int nn;

        public MyBaseClass(int a, int b)
        {

        }

        /// <summary>
        /// 引用参数
        /// </summary>
        /// <param name="m"></param>
        public void MyFunc01(ref int m)
        {

            m += 1;
        }
        /// <summary>
        /// 输出参数
        /// </summary>
        /// <param name="m"></param>
        public void MyFunc02(out int m)
        {
            m = 10;
        }
        /// <summary>
        /// 方法重载
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int Add(int a, int b)
        {
            return a + b;
        }
        public int Add(int a, int b, int c)
        {
            return a + b + c;
        }
        /// <summary>
        /// 可选参数
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public int MyFunc03(int a, int b = 0, RMSData rMSData = null)
        {
            return a * b;
        }
        /// <summary>
        /// 虚方法，由基类定义，派生类重写，可以在基类访问的方法
        /// </summary>
        virtual public void VirFunc()
        {
            Console.WriteLine("这是基类");
        }

        public void Dispose()
        {

        }
        #endregion

        #region 结构
        public struct MyStruct
        {
            int x;
            int y;

            public MyStruct(int a, int b)
            {
                x = a;
                y = b;
            }


        }

        #endregion

        #region 事件
        //事件与属性、方法一样是类的成员，不是类型
        //格式：public 关键字（event） 委托名(一般使用EventHandler) 事件名;
        //1.事件的发布，此类为发布者；标准事件的用法，自定义类型的泛型委托；
        public event EventHandler<TestEventArgs> MyTestEvent;
        TestEventArgs eventArgs = new TestEventArgs();
        public void DoEvent()
        {
            Random random = new Random();
            for (int i = 0; i < 10; i++)
            {
                int a = random.Next(100);
                eventArgs.MyProperty = a;
                //2.事件的触发
                //判断是否为空，为空则代表没有任何订阅函数，没必要触发
                MyTestEvent?.Invoke(this, eventArgs);

            }
        }
        #endregion
    }
    #region 枚举
    [Flags]
    //位标志
    public enum CardDeckSettings : uint
    {
        SingleDeck = 0x01,//位0
        LargePictures = 0x02,//位1
        FancyNumbers = 0x04,//位2
        Animation = 0x08//位3

    }

    public enum MyEnum : long//可设置任何整型的底层值
    {
        First = 1,
        Second = 2,
        Third = 4//不能有重复的名称，但能有重复的值
    }
    public enum _MyEnum
    {
        Aa = 11,//11 显示设置
        Bb,   //12 比之前大1
        Cc,   //13 比之前大1
        Dd = 4, //4 显示设置
        Ee,   //5 比之前大1
        Ff = Dd //4 以上定义了Dd
    }
    #endregion

    public class MyDerivedClass : MyBaseClass
    {
        /// <summary>
        /// 同时使用同一个类的另一个构造函数
        /// </summary>
        /// <param name="x"></param>

        public int AAAA { get; set; }
        public int BBBB { get; set; }
        public MyDerivedClass(int a, int b) : base(a, b)
        {
            AAAA = a * 10;
            BBBB = b * 10;
        }

        /// <summary>
        /// 隐藏基类成员
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        new public int Add(int a, int b)
        {
            //base 访问基类成员
            base.Add(1, 2, 3);
            return 0;
        }
        /// <summary>
        /// 在派生类中重写虚方法、属性
        /// </summary>
        public override void VirFunc()
        {
            Console.WriteLine("这是派生类");
        }
        public override int VirProperty => 5;
        public override int _VirProperty { set => base._VirProperty = value; }

    }

    #region 抽象类
    //含抽象成员的类一定是抽象类
    //抽象类不一定含抽象成员
    public abstract class AbClass
    {
        int a = 1;
        int b = 2;
        public abstract int MyInt { get; set; }
        abstract public void Print();
    }
    //继承抽象类要重写基类的所有抽象成员
    public class DerivedClass : AbClass
    {
        public override int MyInt { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void Print()
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    #region 拓展方法
    class SeClass
    {
        public SeClass()
        {
        }

        public void SeFunc()
        {
            Console.WriteLine("这是密封类");
        }
    }
    static class StaClass
    {
        public static void StFunc(this SeClass seClass)
        {
            Console.WriteLine("这是静态类中静态方法（SeClass的扩展方法）");
        }
    }

    #endregion

    #region 委托
    //声明委托类型，注意是类型，不是对象；
    //与类一样是用户自定义类型，声明与方法类似，只是没有实现块；
    //委托是类型声明，所以不需要在类内部声明；
    #region 不带返回值的委托
    delegate void MyDel(int value);
    public class MyDelClass
    {
        public void PrintLow(int value)
        {
            Console.WriteLine("{0}-Low Value", value);
        }

        public void PrintHigh(int value)
        {
            Console.WriteLine("{0}-High Value", value);
        }

        public void Add(int value)
        {

        }
    }
    #endregion

    #region 带返回值的委托
    delegate int MyDel_02();
    class MyDelClass_02
    {
        private int IntValue { get; set; } = 5;
        public int Add1()
        {
            return IntValue += 2;

        }
        public int Add2()
        {
            return IntValue += 5;
        }
    }

    #endregion
    #endregion

    #region 接口
    //个人理解
    //1.抽象类是针对拥有相同属性较多而拥有不同行为的类的的抽象，一个抽象类可派生许多类，这些类具有相同的属性字段，拥有不同的行为（方法）
    //2.接口是针对拥有相同属性较少而拥有相同行为类的抽象，一个接口可由多个类实现，这些类向同性应该很少，拥有相同行为（方法）

    //使用IComparable接口示例
    class MyClass : IComparable
    {
        public int TheValue;
        public int CompareTo(object obj) //引用方法的实现
        {
            //按照规定
            MyClass mc = obj as MyClass;
            if (this.TheValue < mc.TheValue) return -1;//当前对象小于参数对象，返回-1
            if (this.TheValue > mc.TheValue) return 1;//当前对象大于参数对象，返回1
            return 0;//当前对象等于参数对象，返回0
        }

    }

    #region 自定义接口
    public interface IMyInterface
    {
        //接口中没有字段，抽象类中可以有字段
        int A { get; set; }
        int B { get; set; }
        int Add();//分号代替主体
    }

    public class MyIfcClass1 : IMyInterface
    {
        public MyIfcClass1(int a, int b)
        {
            A = a;
            B = b;
        }

        public int A { get; set; }
        public int B { get; set; }

        public int Add()
        {
            return A + B;
        }
    }

    public class MyIfcClassDo
    {
        //调用接口，可传入实现接口的类的引用
        public void PrintOut(IMyInterface myInterface)
        {
            Console.WriteLine("IMyInterface.Add({0},{1})：{2}", myInterface.A, myInterface.B, myInterface.Add());
        }
    }


    #endregion

    #endregion

    #region 用户自定义运算符
    //\\1.implicit隐式，explicit显示
    //\\2.public static 是必须的
    //\\3.目标和原类型必须是不同的类型，不能不能通过继承关联
    //\\4.格式：public static implicit(或explicit) operator 目标类型 (原类型)
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }
        //将Person转换为int
        public static implicit operator int(Person p)
        {
            return p.Age;
        }
        //将Int转换为Person
        public static implicit operator Person(int i)
        {
            return new Person("Nemo", i);
        }
    }
    #endregion

    #region 泛型
    //类是对象的模板，泛型是类的模板
    //\\T是占位符，也叫类型参数
    //\\实例化过程：泛型类型->被构造类型->实例
    //\\约束
    class MyTClass<T1, T2> where T2 : struct
    {
        T1 t1;
        T2 t2;
        public MyTClass(T1 t1, T2 t2)
        {
            this.t1 = t1;
            this.t2 = t2;
        }

        public string MyTypes()
        {
            return $"T1:{t1.GetType()} T2:{t2.GetType()}";
        }
        //泛型方法
        public void PrintArray<A>(A[] arry)
        {
            Array.Reverse(arry);
            foreach (A item in arry)
            {
                Console.Write(item.ToString() + "  ");
            }
            Console.WriteLine();
        }
        /// <summary>
        /// 符合泛型委托的方法
        /// </summary>
        public void PrintString(string s)
        {
            Console.WriteLine(s);
        }
        public void PrintUpperString(string s)
        {
            Console.WriteLine(s.ToUpper());
        }

        public string PrintSome(int p1, int p2)
        {
            return (p1 + p2).ToString();
        }
    }

    //泛型拓展方法
    static class ExtendMyTClass
    {
        public static void ExtendFunc<T1, T2>(this MyTClass<T1, T2> myT, T2 t2) where T2 : struct
        {
            Console.WriteLine($"Extend:{myT.MyTypes()} T2:{t2}");
        }

    }

    //泛型委托
    //返回类型为R，参数为T
    public delegate void MyTDel<T>(T t);
    //带返回值的泛型委托
    public delegate R MyFunc<T1, T2, R>(T1 t1, T2 t2);
    #endregion

    #region 枚举器（nnumerator）与迭代器（iterator）
    //枚举器与foreach一起使用
    //\\枚举器：IEnumerator 可枚举类型：IEnumerable
    #region 使用IEnumerator与IEnumerable示例
    class ColorEnumerator : IEnumerator
    {
        string[] _colors;
        int _position = -1;

        //构造函数
        public ColorEnumerator(string[] colors)
        {
            _colors = (string[])colors.Clone();
        }

        //实现Current
        public object Current
        {
            get
            {
                //此时枚举器的位置在集合的第一个元素之前
                if (_position == -1)
                    throw new InvalidOperationException();
                //此时枚举器的位置在集合的最后一个元素之后
                if (_position > _colors.Length)
                    throw new InvalidOperationException();
                return _colors[_position];
            }
        }
        //实现MoveNext
        public bool MoveNext()
        {
            if (_position < _colors.Length - 1)
            {
                _position++;
                return true;
            }
            else
                return false;
        }
        //实现Reset
        public void Reset()
        {
            _position = -1;
        }
    }
    class Spectrum : IEnumerable
    {

        string[] Colors = new string[] { "violet", "blue", "cyan", "green", "yellow", "orange", "red" };
        //实现GetEnumerator方法，返回一个枚举器,实现GetEnumerator()方法的类叫做“可枚举类型”
        public IEnumerator GetEnumerator()
        {
            return new ColorEnumerator(Colors);
        }
    }


    #endregion

    #region 迭代器
    //迭代器可自动为我们生成可枚举类型与枚举器，迭代器块中的代码描述了如何枚举元素
    //\\1.枚举器块可以是：方法主体、属性主体、运算符主体
    //\\2.迭代器块描述了希望编译器为我们创建的枚举器类的行为，不是需要在同一时间执行的一串命令式命令
    //\\3.因为类实现了GetEnumerator()方法，类可被枚举
    #region 使用迭代器创建枚举器
    class ColorIterator
    {
        string[] _colors;

        public ColorIterator(string[] colors)
        {
            _colors = (string[])colors.Clone();
        }

        //2.返回枚举器的GetEnumerator()方法
        public IEnumerator<string> GetEnumerator()
        {
            return BlackAndWhite();
        }

        //1.产生枚举器方法的迭代器
        public IEnumerator<string> BlackAndWhite()
        {
            //方法主体作为枚举器块
            foreach (string s in _colors)
            {
                yield return s;
            }
        }
    }

    #endregion

    #region 使用迭代器创建可枚举类型
    class ColorIterable
    {
        string[] _colors;

        public ColorIterable(string[] colors)
        {
            _colors = (string[])colors.Clone();
        }

        //1.返回枚举器的GetEnumerator()方法
        public IEnumerator<string> GetEnumerator()
        {
            IEnumerable<string> iem = BlackAndWhite();//获取可枚举类型
            return iem.GetEnumerator();//获取枚举器
        }

        //2.返回可枚举类型
        public IEnumerable<string> BlackAndWhite()
        {
            //方法主体作为迭代器块
            foreach (string s in _colors)
            {
                yield return s;
            }
        }
    }

    #endregion

    #region 迭代器作为属性
    public class ColorProp
    {
        bool _listFromUVtoIR;
        string[] _colors;

        public ColorProp(bool listFromUVtoIR, string[] colors)
        {
            _listFromUVtoIR = listFromUVtoIR;
            _colors = (string[])colors.Clone();
        }
        public IEnumerator<string> GetEnumerator()
        {
            return _listFromUVtoIR ? UVtoIR : IRtoUV;
        }

        public IEnumerator<string> UVtoIR
        {
            get
            {
                foreach (var item in _colors)
                    yield return item;
            }
        }
        public IEnumerator<string> IRtoUV
        {
            get
            {
                foreach (var item in _colors.Reverse())
                    yield return item;
            }
        }
    }

    #endregion
    #endregion
    #endregion

    #region LINQ
    //使用LINQ可以很轻松的查询对象集合
    //\\1.LINQ是.NET框架的扩展，它允许我们用SQL查询数据库的方式来查询数据集合；
    //\\2.使用LINQ可以从数据库、程序对象的集合以及XML文档中查询数据
    #region 匿名类型
    class Other
    {
        public static string Name = "Tom";
    }

    class ClassTset1
    {
        //有如下创建匿名类的方法
        public void Fc()
        {
            //1.简单赋值形式
            var student = new { Age = 19, Name = "Tom", Major = "History" };
            //可以像访问普通类一样访问它
            Console.WriteLine("student：{0},{1},{2}", student.Age, student.Name, student.Major);
            //2.投影初始化语句
            string Major = "History";
            //赋值形式，成员访问表达式，标识符形式
            var student_ = new { Age = 19, Other.Name, Major };
            //也可以直接输出匿名类所有成员
            Console.WriteLine("student_：{0}", student_);
        }
    }

    #endregion

    #region 方法语法和查询语法
    //\\1.方法语法使用标准的方法调用；查询语法看上去与SQL语句类似，使用查询表达式形式书写
    //\\2.编译器会将查询语法的查询翻译为方法调用的形式，两者没有性能上的差异 


    #endregion
    #endregion

    #region 反射与特性
    public class BaseClass
    {
        public int BaseField;
    }
    public class DerievdClass : BaseClass
    {
        public int DerievdField;
    }

    #region 应用特性
    //\\1.Obsolete特性，可将程序结构标记为过期的
    //重载的第二个bool类型参数可将其标记为错误而不仅仅是警告，true表示标记为错误
    [Obsolete("使用了过期的类", false)]
    public class ObClass
    {
        [Obsolete("使用了过期的函数")]
        public void ObFuc() { }
    }
    //\\2.Conditional特性，方法应用的特性，特性传入一个编译符号，若没有定义这个编译符号编译器会忽略这个方法的所有调用
    public class CoClass
    {
        //编译器编译这段代码时会检查是否有一个编译符号被定义为DoTrace
        [Conditional("DoTrace")]
        public void CoFunc()
        {
            Console.WriteLine("");
            Console.WriteLine("Conditional特性");
        }
    }
    //\\3.调用者信息特性,可以访问文件路径、代码行数、调用成员的名称
    //这三个特性的名称为：CallerFilePath、CallerLineNumber、CallerMemberName
    //这些特性只能用于方法中你的可选参数！！！
    public class CallClass
    {
        //如果显示指定了这三个可选参数，则会使用真正的参数值，若没有显示提供，系统将会提供源代码的默认值；
        public void MyTrace(string message,
            [CallerFilePath] string fileName = "",
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string callingMember = "")
        {
            Console.WriteLine();
            Console.WriteLine("File:    {0}", fileName);
            Console.WriteLine("Line:    {0}", lineNumber);
            Console.WriteLine("Called From:{0}", callingMember);
            Console.WriteLine("Message:  {0}", message);
        }
    }


    #endregion
    #endregion
}

//程序及>命名空间>类
namespace DFX
{
    public class DFXClass1 { };
    //命名空间嵌套，原文嵌套
    namespace LCJ { };
}
//岁然命名空间嵌套看起来是一个命名空间包含另一个命名空间，但命名空间里的内容是独立的
//命名空间嵌套，分离嵌套,要使用完全限定名
namespace DFX.LCJ
{
    public class LCJClass { };
}
